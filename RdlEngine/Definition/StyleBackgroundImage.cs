/* ====================================================================
    Copyright (C) 2004-2006  fyiReporting Software, LLC

    This file is part of the fyiReporting RDL project.
	
    This library is free software; you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation; either version 2.1 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301  USA

    For additional information, email info@fyireporting.com or visit
    the website www.fyiReporting.com.
*/
using System;
using System.Xml;
using System.Text;
using System.IO;
using System.Drawing.Imaging;
using System.Globalization;
using System.Threading;

namespace fyiReporting.RDL
{
	///<summary>
	/// Represents the background image information in a Style.
	///</summary>
	[Serializable]
	internal class StyleBackgroundImage : ReportLink
	{
		StyleBackgroundImageSourceEnum _Source;	// Identifies the source of the image:
		Expression _Value;	// (string) See Source. Expected datatype is string or
							// binary, depending on Source. If the Value is
							// null, no background image is displayed.
		Expression _MIMEType;	// (string) The MIMEType for the image.
							// Valid values are: image/bmp, image/jpeg,
							// image/gif, image/png, image/x-png
							// Required if Source = Database. Ignored otherwise.
		Expression _BackgroundRepeat;	// (Enum BackgroundRepeat) Indicates how the background image should
							// repeat to fill the available space: Default: Repeat
		bool _ConstantImage;	// true if constant image
	
		internal StyleBackgroundImage(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Source=StyleBackgroundImageSourceEnum.Unknown;
			_Value=null;
			_MIMEType=null;
			_BackgroundRepeat=null;
			_ConstantImage=false;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Source":
						_Source = StyleBackgroundImageSource.GetStyle(xNodeLoop.InnerText);
						break;
					case "Value":
						_Value = new Expression(r, this, xNodeLoop, ExpressionType.String);
						break;
					case "MIMEType":
						_MIMEType = new Expression(r, this, xNodeLoop, ExpressionType.String);
						break;
					case "BackgroundRepeat":
						_BackgroundRepeat = new Expression(r, this, xNodeLoop, ExpressionType.Enum);
						break;
					default:	
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown BackgroundImage element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			if (_Source == StyleBackgroundImageSourceEnum.Unknown)
				OwnerReport.rl.LogError(8, "BackgroundImage requires the Source element.");
			if (_Value == null)
				OwnerReport.rl.LogError(8, "BackgroundImage requires the Value element.");
			
		}

		// Handle parsing of function in final pass
		override internal void FinalPass()
		{
			if (_Value != null)
				_Value.FinalPass();
			if (_MIMEType != null)
				_MIMEType.FinalPass();
			if (_BackgroundRepeat != null)
				_BackgroundRepeat.FinalPass();

			_ConstantImage = this.IsConstant();
			return;
		}

		// Generate a CSS string from the specified styles
		internal string GetCSS(Report rpt, Row row, bool bDefaults)
		{
			StringBuilder sb = new StringBuilder();

			// TODO: need to handle other types of sources
			if (_Value != null && _Source==StyleBackgroundImageSourceEnum.External)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "background-image:url(\"{0}\");",_Value.EvaluateString(rpt, row));
			else if (bDefaults)
				return "background-image:none;";	
            			
			if (_BackgroundRepeat != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "background-repeat:{0};",_BackgroundRepeat.EvaluateString(rpt, row));
			else if (bDefaults)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "background-repeat:repeat;");

			return sb.ToString();
		}

		internal bool IsConstant()
		{
			if (_Source == StyleBackgroundImageSourceEnum.Database)
				return false;

			bool rc = true;

			if (_Value != null)
				rc = _Value.IsConstant();
			if (!rc)
				return false;

			if (_BackgroundRepeat != null)
				rc = _BackgroundRepeat.IsConstant();

			return rc;
		}
	
		internal PageImage GetPageImage(Report rpt, Row row)
		{
			string mtype=null; 
			Stream strm=null;
			System.Drawing.Image im=null;
			PageImage pi=null;

			WorkClass wc = GetWC(rpt);
			if (wc.PgImage != null)
			{	// have we already generated this one
				// reuse most of the work; only position will likely change
				pi = new PageImage(wc.PgImage.ImgFormat, wc.PgImage.ImageData, wc.PgImage.SamplesW, wc.PgImage.SamplesH);
				pi.Name = wc.PgImage.Name;				// this is name it will be shared under
				return pi;
			}

			try 
			{
				strm = GetImageStream(rpt, row, out mtype); 
				im = System.Drawing.Image.FromStream(strm);
				int height = im.Height;
				int width = im.Width;
				MemoryStream ostrm = new MemoryStream();
				ImageFormat imf;
				//				if (mtype.ToLower() == "image/jpeg")    //TODO: how do we get png to work
				//					imf = ImageFormat.Jpeg;
				//				else
				imf = ImageFormat.Jpeg;
				im.Save(ostrm, imf);
				byte[] ba = ostrm.ToArray();
				ostrm.Close();
				pi = new PageImage(imf, ba, width, height);
				pi.SI = new StyleInfo();	// this will just default everything
				if (_BackgroundRepeat != null)
				{
					string r = _BackgroundRepeat.EvaluateString(rpt, row).ToLower();
					switch (r)
					{
						case "repeat":
							pi.Repeat = ImageRepeat.Repeat;
							break;
						case "repeatx":
							pi.Repeat = ImageRepeat.RepeatX;
							break;
						case "repeaty":
							pi.Repeat = ImageRepeat.RepeatY;
							break;
						case "norepeat":
						default:
							pi.Repeat = ImageRepeat.NoRepeat;
							break;
					}
				}
				else
					pi.Repeat = ImageRepeat.Repeat;

				if (_ConstantImage)
				{
					wc.PgImage = pi;
					// create unique name; PDF generation uses this to optimize the saving of the image only once
					pi.Name = "pi" + Interlocked.Increment(ref Parser.Counter).ToString();	// create unique name
				}
			}
			finally
			{
				if (strm != null)
					strm.Close();
				if (im != null)
					im.Dispose();
			}
			return pi;
		}

		Stream GetImageStream(Report rpt, Row row, out string mtype)
		{
			mtype=null; 
			Stream strm=null;
			try 
			{
				switch (this._Source)
				{
					case StyleBackgroundImageSourceEnum.Database:
						if (_MIMEType == null)
							return null;
						mtype = _MIMEType.EvaluateString(rpt, row);
						object o = _Value.Evaluate(rpt, row);
						strm = new MemoryStream((byte[]) o);
						break;
					case StyleBackgroundImageSourceEnum.Embedded:
						string name = _Value.EvaluateString(rpt, row);
						EmbeddedImage ei = (EmbeddedImage) OwnerReport.LUEmbeddedImages[name];
						mtype = ei.MIMEType;
						byte[] ba = Convert.FromBase64String(ei.ImageData);
						strm = new MemoryStream(ba);
						break;
					case StyleBackgroundImageSourceEnum.External:
						if (_MIMEType == null)
							return null;
						mtype = _MIMEType.EvaluateString(rpt, row);
						string fname = _Value.EvaluateString(rpt, row);
						strm = new FileStream(fname, System.IO.FileMode.Open, FileAccess.Read);		
						break;
					default:
						return null;
				}
			}
			catch
			{
				if (strm != null)
				{
					strm.Close();
					strm = null;
				}
			}

			return strm;
		}

		static internal string GetCSSDefaults()
		{
			return "background-image:none;";
		}

		internal StyleBackgroundImageSourceEnum Source
		{
			get { return  _Source; }
			set {  _Source = value; }
		}

		internal Expression Value
		{
			get { return  _Value; }
			set {  _Value = value; }
		}

		internal Expression MIMEType
		{
			get { return  _MIMEType; }
			set {  _MIMEType = value; }
		}

		internal Expression BackgroundRepeat
		{
			get { return  _BackgroundRepeat; }
			set {  _BackgroundRepeat = value; }
		}

		private WorkClass GetWC(Report rpt)
		{
			WorkClass wc = rpt.Cache.Get(this, "wc") as WorkClass;
			if (wc == null)
			{
				wc = new WorkClass();
				rpt.Cache.Add(this, "wc", wc);
			}
			return wc;
		}

		private void RemoveWC(Report rpt)
		{
			rpt.Cache.Remove(this, "wc");
		}

		class WorkClass
		{
			internal PageImage PgImage;	// When ConstantImage is true this will save the PageImage for reuse
			internal WorkClass()
			{
				PgImage=null;
			}
		}
	}

	internal enum BackgroundRepeat
	{
		Repeat,			// repeat image in both x and y directions
		NoRepeat,		// don't repeat
		RepeatX,		// repeat image in x direction
		RepeatY			// repeat image in y direction
	}
}
