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
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using fyiReporting.RDL;

namespace fyiReporting.RDL
{
	///<summary>
	/// Main Report definition; this is the top of the tree that contains the complete
	/// definition of a instance of a report.
	///</summary>
	public class Report
	{
		ReportDefn _Report;
		DataSources _DataSources;
		DataSets _DataSets;
		int _RuntimeName=0;		// used for the generation of unique runtime names
		IDictionary _LURuntimeName;		// Runtime names
		ICollection _UserParameters;	// User parameters
		internal ReportLog rl;	// report log
		RCache _Cache;

		// Some report runtime variables
		private string _Folder;			// folder name
		private string _ReportName;		// report name
		private string _CSS;			// after rendering ASPHTML; this is separate
		private string _JavaScript;		// after rendering ASPHTML; this is separate
		private object _CodeInstance;	// Instance of the class generated for the Code element
		private Page _CurrentPage;		// needed for page header/footer references
		private string _UserID;			// UserID of client executing the report
		private string _ClientLanguage;	// Language code of the client executing the report.
		private DataSourcesDefn _ParentConnections;	// When running subreport with merge transactions this is parent report connections

		internal int PageNumber=1;			// current page number
		internal int TotalPages=1;			// total number of pages in report
		internal DateTime ExecutionTime;	// start time of report execution

		/// <summary>
		/// Construct a runtime Report object using the compiled report definition.
		/// </summary>
		/// <param name="r"></param>
		public Report(ReportDefn r)
		{
			_Report = r;
			_Cache = new RCache();
			rl = new ReportLog(r.rl);
			_ReportName = r.Name;
			_UserParameters = null;
			_LURuntimeName = new ListDictionary();	// shouldn't be very many of these
			if (r.Code != null)
				_CodeInstance = r.Code.Load(this);
			if (r.Classes != null)
				r.Classes.Load(this);
		}

		internal Page CurrentPage
		{
			get {return _CurrentPage;}
			set {_CurrentPage = value;}
		}

		internal Rows GetPageExpressionRows(string exprname)
		{
			if (_CurrentPage == null)
				return null;

			return _CurrentPage.GetPageExpressionRows(exprname);
		}

		/// <summary>
		/// Read all the DataSets in the report
		/// </summary>
		/// <param name="parms"></param>
		public void RunGetData(IDictionary parms)
		{
			ExecutionTime = DateTime.Now;
			_Report.RunGetData(this, parms);
			return;
		}

		/// <summary>
		/// Renders the report using the requested presentation type.
		/// </summary>
		/// <param name="sg">IStreamGen for generating result stream</param>
		/// <param name="type">Presentation type: HTML, XML, PDF, or ASP compatible HTML</param>
		public void RunRender(IStreamGen sg, OutputPresentationType type)
		{
			RunRender(sg, type, "");
		}

		/// <summary>
		/// Renders the report using the requested presentation type.
		/// </summary>
		/// <param name="sg">IStreamGen for generating result stream</param>
		/// <param name="type">Presentation type: HTML, XML, PDF, MHT, or ASP compatible HTML</param>
		/// <param name="prefix">For HTML puts prefix allowing unique name generation</param>
		public void RunRender(IStreamGen sg, OutputPresentationType type, string prefix)
		{
			if (sg == null)
				throw new ArgumentException("IStreamGen argument cannot be null.", "sg");
			RenderHtml rh=null;

			PageNumber = 1;		// reset page numbers
			TotalPages = 1;
			IPresent ip;
			MemoryStreamGen msg = null;
			switch (type)
			{
				case OutputPresentationType.PDF:
					ip = new RenderPdf(this, sg);
					_Report.Run(ip);
					break;
				case OutputPresentationType.XML:
					if (_Report.DataTransform != null && _Report.DataTransform.Length > 0)
					{
						msg = new MemoryStreamGen();
						ip = new RenderXml(this, msg);
						_Report.Run(ip);
						RunRenderXmlTransform(sg, msg);
					}
					else
					{
						ip = new RenderXml(this, sg);    
						_Report.Run(ip);
					}
					break;
				case OutputPresentationType.MHTML:
					this.RunRenderMht(sg);
					break;
				case OutputPresentationType.ASPHTML:
				case OutputPresentationType.HTML:
				default:
					ip = rh = new RenderHtml(this, sg);
					rh.Asp = (type == OutputPresentationType.ASPHTML);
					rh.Prefix = prefix;
					_Report.Run(ip);
					// Retain the CSS and JavaScript
					if (rh != null)
					{
						_CSS = rh.CSS;
						_JavaScript = rh.JavaScript;
					}
					break;
			}

			sg.CloseMainStream();
            _Cache = new RCache();
            return;
		}

		private void RunRenderMht(IStreamGen sg)
		{
			OneFileStreamGen temp = null;
			FileStream fs=null;
			try
			{
				string tempHtmlReportFileName = Path.ChangeExtension(Path.GetTempFileName(), "htm");
				temp = new OneFileStreamGen(tempHtmlReportFileName, true);
				RunRender(temp, OutputPresentationType.HTML);
				temp.CloseMainStream();

				// Create the mht file (into a temporary file position)
				MhtBuilder mhtConverter = new MhtBuilder();
				string fileName = Path.ChangeExtension(Path.GetTempFileName(), "mht");
				mhtConverter.SavePageArchive(fileName, "file://" + tempHtmlReportFileName);

				// clean up the temporary files
				foreach (string tempFileName in temp.FileList)
				{
					try
					{
						File.Delete(tempFileName);
					}
					catch{}
				}

				// Copy the mht file to the requested stream
				Stream os = sg.GetStream();
				fs = File.OpenRead(fileName);
				byte[] ba = new byte[4096];
				int rb=0;
				while ((rb = fs.Read(ba, 0, ba.Length)) > 0)
				{
					os.Write(ba, 0, rb);
				}
				
			}
			catch (Exception ex)
			{
				rl.LogError(8, "Error converting HTML to MHTML " + ex.Message + 
									Environment.NewLine + ex.StackTrace);
			}
			finally
			{
				if (temp != null)
					temp.CloseMainStream();
				if (fs != null)
					fs.Close();
                _Cache = new RCache();
            }
		}

		/// <summary>
		/// RunRenderPdf will render a Pdf given the page structure
		/// </summary>
		/// <param name="sg"></param>
		/// <param name="pgs"></param>
		public void RunRenderPdf(IStreamGen sg, Pages pgs)
		{
			PageNumber = 1;		// reset page numbers
			TotalPages = 1;

			IPresent ip = new RenderPdf(this, sg);	
			try
			{
				ip.Start();
				ip.RunPages(pgs);
				ip.End();
			}
			finally
			{
				pgs.CleanUp();		// always want to make sure we cleanup to reduce resource usage
                _Cache = new RCache();
            }

			return;
		}

		private void RunRenderXmlTransform(IStreamGen sg, MemoryStreamGen msg)
		{
			try
			{
				string file;
				if (_Report.DataTransform[0] != Path.DirectorySeparatorChar)
					file = this.Folder + Path.DirectorySeparatorChar + _Report.DataTransform;
				else
					file = this.Folder + _Report.DataTransform;
				XmlUtil.XslTrans(file, msg.GetText(), sg.GetStream());
			}	
			catch (Exception ex)
			{
				rl.LogError(8, "Error processing DataTransform " + ex.Message + "\r\n" + ex.StackTrace);
			}
			finally 
			{
				msg.Dispose();
			}
			return;
		}


		/// <summary>
		/// Build the Pages for this report.
		/// </summary>
		/// <returns></returns>
		public Pages BuildPages()
		{
			PageNumber = 1;		// reset page numbers
			TotalPages = 1;

			Pages pgs = new Pages(this);
			pgs.PageHeight = _Report.PageHeight.Points;
			pgs.PageWidth = _Report.PageWidth.Points;
			try
			{
				Page p = new Page(1);				// kick it off with a new page
				pgs.AddPage(p);

				// Create all the pages
				_Report.Body.RunPage(pgs);

				if (pgs.LastPage.IsEmpty() && pgs.PageCount > 1) // get rid of extraneous pages which
					pgs.RemoveLastPage();			//   can be caused by region page break at end

				// Now create the headers and footers for all the pages (as needed)
				if (_Report.PageHeader != null)
					_Report.PageHeader.RunPage(pgs);
				if (_Report.PageFooter != null)
					_Report.PageFooter.RunPage(pgs);
				// clear out any runtime clutter
				foreach (Page pg in pgs)
					pg.ResetPageExpressions();

                pgs.SortPageItems();             // Handle ZIndex ordering of pages
			}
			catch (Exception e)
			{
				rl.LogError(8, "Exception running report\r\n" + e.Message + "\r\n" + e.StackTrace);
			}
			finally
			{
				pgs.CleanUp();		// always want to make sure we clean this up since 
                _Cache = new RCache();
			}

			return pgs;
		}

		public NeedPassword GetDataSourceReferencePassword
		{
			get {return _Report.GetDataSourceReferencePassword;}
			set {_Report.GetDataSourceReferencePassword = value;}
		}

		public ReportDefn ReportDefinition
		{
			get {return this._Report;}
		}

		internal void SetReportDefinition(ReportDefn r)
		{
			_Report = r;
		}

		public string Description
		{
			get { return _Report.Description; }
		}

		public string Author
		{
			get { return _Report.Author; }
		}

		public string CSS
		{
			get { return _CSS; }
		}

		public string JavaScript
		{
			get { return _JavaScript; }
		}
 
		internal object CodeInstance
		{
			get {return this._CodeInstance;}
		}

		internal string CreateRuntimeName(object ro)
		{
			_RuntimeName++;					// increment the name generator
			string name = "o" + _RuntimeName.ToString();
			_LURuntimeName.Add(name, ro);
			return name;			
		}

		public DataSources DataSources
		{
			get 
			{
				if (_Report.DataSourcesDefn == null)
					return null;
				if (_DataSources == null)
					_DataSources = new DataSources(this, _Report.DataSourcesDefn);
				return _DataSources;
			}
		}

		public DataSets DataSets
		{
			get 
			{ 
				if (_Report.DataSetsDefn == null)
					return null;
				if (_DataSets == null)
					_DataSets = new DataSets(this, _Report.DataSetsDefn); 

				return _DataSets;
			}
		}

		/// <summary>
		/// User provided parameters to the report.  IEnumerable is a list of UserReportParameter.
		/// </summary>
		public ICollection UserReportParameters
		{
			get 
			{
				if (_UserParameters != null)	// only create this once
					return _UserParameters;		//  since it can be expensive to build

				if (ReportDefinition.ReportParameters == null || ReportDefinition.ReportParameters.Count <= 0)
				{
                    List<UserReportParameter> parms = new List<UserReportParameter>(1);
                    _UserParameters = parms;
				}
				else
				{
                    List<UserReportParameter> parms = new List<UserReportParameter>(ReportDefinition.ReportParameters.Count);
					foreach (ReportParameter p in ReportDefinition.ReportParameters)
					{
						UserReportParameter urp = new UserReportParameter(this, p);
						parms.Add(urp);
					}
					parms.TrimExcess();
					_UserParameters = parms;
				}
				return _UserParameters;
			}
		}
		/// <summary>
		/// Get/Set the folder containing the report.
		/// </summary>
		public string Folder
		{
			get { return _Folder==null? _Report.ParseFolder: _Folder; }
			set { _Folder = value; }
		}

		/// <summary>
		/// Get/Set the report name.  Usually this is the file name of the report sans extension.
		/// </summary>
		public string Name
		{
			get { return _ReportName; }
			set { _ReportName = value; }
		}

		/// <summary>
		/// Returns the height of the page in points.
		/// </summary>
		public float PageHeightPoints
		{
			get { return _Report.PageHeight.Points;	}
		}
		/// <summary>
		/// Returns the width of the page in points.
		/// </summary>
		public float PageWidthPoints
		{
			get { return _Report.PageWidthPoints; }
		}
		
		/// <summary>
		/// Returns the maximum severity of any error.  4 or less indicating report continues running.
		/// </summary>
		public int ErrorMaxSeverity
		{
			get 
			{
				if (this.rl == null)
					return 0;
				else
					return rl.MaxSeverity;
			}
		}

		/// <summary>
		/// List of errors encountered so far.
		/// </summary>
		public IList ErrorItems
		{
			get
			{
				if (this.rl == null)
					return null;
				else
					return rl.ErrorItems;
			}
		}

		/// <summary>
		/// Clear all errors generated up to now.
		/// </summary>
		public void ErrorReset()
		{
			if (this.rl == null)
				return;
			rl.Reset();
			return;
		}

		/// <summary>
		/// Get/Set the UserID, that is the running user.
		/// </summary>
		public string UserID
		{
			get { return _UserID == null? Environment.UserName: _UserID; }
			set { _UserID = value; }
		}
		/// <summary>
		/// Get/Set the three letter ISO language of the client of the report.
		/// </summary>
		public string ClientLanguage
		{
			get { return _ClientLanguage == null? CultureInfo.CurrentCulture.ThreeLetterISOLanguageName: _ClientLanguage; }
			set { _ClientLanguage = value; }
		}

		internal DataSourcesDefn ParentConnections
		{
			get {return _ParentConnections; }
			set {_ParentConnections = value; }
		}

		internal RCache Cache
		{
			get {return _Cache;}
		}
	}

	internal class RCache
	{
		Hashtable _RunCache;

		internal RCache()
		{
			_RunCache = new Hashtable();
		}

		internal void Add(ReportLink rl, string name, object o)
		{
			_RunCache.Add(GetKey(rl,name), o);
		}

		internal void AddReplace(ReportLink rl, string name, object o)
		{
			string key = GetKey(rl,name);
			_RunCache.Remove(key);
			_RunCache.Add(key, o);
		}

		internal object Get(ReportLink rl, string name)
		{
			return _RunCache[GetKey(rl,name)];
		}

		internal void Remove(ReportLink rl, string name)
		{
			_RunCache.Remove(GetKey(rl,name));
		}

		internal void Add(ReportDefn rd, string name, object o)
		{
			_RunCache.Add(GetKey(rd,name), o);
		}

		internal void AddReplace(ReportDefn rd, string name, object o)
		{
			string key = GetKey(rd,name);
			_RunCache.Remove(key);
			_RunCache.Add(key, o);
		}

		internal object Get(ReportDefn rd, string name)
		{
			return _RunCache[GetKey(rd,name)];
		}

		internal void Remove(ReportDefn rd, string name)
		{
			_RunCache.Remove(GetKey(rd,name));
		}

		internal void Add(string key, object o)
		{
			_RunCache.Add(key, o);
		}

		internal void AddReplace(string key, object o)
		{
			_RunCache.Remove(key);
			_RunCache.Add(key, o);
		}

		internal object Get(string key)
		{
			return _RunCache[key];
		}

		internal void Remove(string key)
		{
			_RunCache.Remove(key);
		}

		internal object Get(int i, string name)
		{
			return _RunCache[GetKey(i,name)];
		}

		internal void Remove(int i, string name)
		{
			_RunCache.Remove(GetKey(i,name));
		}

		string GetKey(ReportLink rl, string name)
		{
			return GetKey(rl.ObjectNumber, name);
		}

		string GetKey(ReportDefn rd, string name)
		{
			if (rd.Subreport == null)	// top level report use 0 
				return GetKey(0, name);
			else						// Use the subreports object number
				return GetKey(rd.Subreport.ObjectNumber, name);
		}

		string GetKey(int onum, string name)
		{
			return name + onum.ToString();
		}
	}

	// holder objects for value types
	internal class ODateTime
	{
		internal DateTime dt;

		internal ODateTime(DateTime adt)
		{
			dt = adt;
		}
	}

	internal class ODecimal
	{
		internal decimal d;

		internal ODecimal(decimal ad)
		{
			d = ad;
		}
	}

	internal class ODouble
	{
		internal double d;

		internal ODouble(double ad)
		{
			d = ad;
		}
	}

	internal class OFloat
	{
		internal float f;

		internal OFloat(float af)
		{
			f = af;
		}
	}

	internal class OInt
	{
		internal int i;

		internal OInt(int ai)
		{
			i = ai;
		}
	}
}
