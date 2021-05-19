/* ====================================================================
    Copyright (C) 2004-2006  fyiReporting Software, LLC

    This file is part of the fyiReporting RDL project.
	
    This library is free software; you can redistribute it and/or modify
    it under the terms of the GNU Lesser General public License as published by
    the Free Software Foundation; either version 2.1 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser General public License for more details.

    You should have received a copy of the GNU Lesser General public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301  USA

    For additional information, email info@fyireporting.com or visit
    the website www.fyiReporting.com.
*/
using System;
using System.Collections;
using System.IO;
using System.Reflection;


using fyiReporting.RDL;


namespace fyiReporting.RDL
{
	/// <summary>
	/// Obtain the runtime value of a Report parameter
	/// </summary>
	[Serializable]
	internal class FunctionReportParameter : IExpr
	{
		protected ReportParameter p;
        ReportParameterMethodEnum _type;
        IExpr _arg;                         // when MultiValue parameter; methods may need arguments

		/// <summary>
		/// obtain value of ReportParameter
		/// </summary>
		public FunctionReportParameter(ReportParameter parm) 
		{
			p=parm;
            _type = ReportParameterMethodEnum.Value;
            _arg = null;
		}

        internal ReportParameterMethodEnum ParameterMethod
        {
            get { return _type; }
            set { _type = value; }
        }

        internal void SetParameterMethod(string pm, IExpr[] args)
        {
            if (!this.p.MultiValue)
                throw new ArgumentException(string.Format("{0} must be a MultiValue parameter to accept methods", this.p.Name.Nm));

            if (pm == null)
            {
                _type = ReportParameterMethodEnum.Index;
            }
            else switch (pm)
            {
                case "Contains": _type = ReportParameterMethodEnum.Contains; break;
                case "BinarySearch": _type = ReportParameterMethodEnum.BinarySearch; break;
                case "Count": 
                    _type = ReportParameterMethodEnum.Count; 
                    if (args != null)
                        throw new ArgumentException("Count does not have any arguments.");
                    break;
                case "IndexOf": _type = ReportParameterMethodEnum.IndexOf; break;
                case "LastIndexOf": _type = ReportParameterMethodEnum.LastIndexOf; break;
                case "Value": _type = ReportParameterMethodEnum.Value; break;
                default:
                    throw new ArgumentException(string.Format("{0} is an unknown array method.", pm));
            }

            if (_type != ReportParameterMethodEnum.Count)
            {
                if (args == null || args.Length != 1)
                    throw new ArgumentException(string.Format("{0} must have exactly one argument.", pm));

                _arg = args[0];
            }

            return;
        }

		public virtual TypeCode GetTypeCode()
		{
            switch (_type)
            {
                case ReportParameterMethodEnum.Contains:
                    return TypeCode.Boolean;
                case ReportParameterMethodEnum.BinarySearch:
                case ReportParameterMethodEnum.Count:
                case ReportParameterMethodEnum.IndexOf:
                case ReportParameterMethodEnum.LastIndexOf:
                    return TypeCode.Int32;
                case ReportParameterMethodEnum.Value:
                    return p.MultiValue ? TypeCode.Object : p.dt;   // MultiValue type is really ArrayList
                case ReportParameterMethodEnum.Index:
                default:
                    return p.dt;
            }
		}

		public virtual bool IsConstant()
		{
			return false;
		}

		public virtual IExpr ConstantOptimization()
		{	// not a constant expression
			return this;
		}

		// Evaluate is for interpretation  (and is relatively slow)
		public virtual object Evaluate(Report rpt, Row row)
		{
            return this.p.MultiValue? EvaluateMV(rpt, row): p.GetRuntimeValue(rpt);
		}

        private object EvaluateMV(Report rpt, Row row)
        {
            ArrayList ar = p.GetRuntimeValues(rpt);
            
            object va = this._arg == null ? null : _arg.Evaluate(rpt, row);

            switch(_type) 
            {
                case ReportParameterMethodEnum.Value:
                    return ar;
                case ReportParameterMethodEnum.Contains:
                    return ar.Contains(va);
                case ReportParameterMethodEnum.BinarySearch:
                    return ar.BinarySearch(va);
                case ReportParameterMethodEnum.Count:
                    return ar.Count;
                case ReportParameterMethodEnum.IndexOf:
                    return ar.IndexOf(va);
                case ReportParameterMethodEnum.LastIndexOf:
                    return ar.LastIndexOf(va);
                case ReportParameterMethodEnum.Index:
                    int i = Convert.ToInt32(va);
                    return ar[i];
                default:
                    throw new Exception("Internal error: unknown Report Parameter method");
            }
        }
		
		public virtual double EvaluateDouble(Report rpt, Row row)
		{
            object rtv = Evaluate(rpt, row);
			if (rtv == null)
				return Double.NaN;

			switch (this.GetTypeCode())
			{
				case TypeCode.Double:
					return ((double) rtv);
				case TypeCode.Object:
					return Double.NaN;
				case TypeCode.Int32:
					return (double) ((int) rtv);
				case TypeCode.Boolean:
					return Convert.ToDouble((bool) rtv);
				case TypeCode.String:
					return Convert.ToDouble((string) rtv);
				case TypeCode.DateTime:
					return Convert.ToDouble((DateTime) rtv);
				default:
					return Double.NaN;
			}
		}
		
		public virtual decimal EvaluateDecimal(Report rpt, Row row)
		{
            object rtv = Evaluate(rpt, row);
			if (rtv == null)
				return Decimal.MinValue;

            switch (this.GetTypeCode())
			{
				case TypeCode.Double:
					return Convert.ToDecimal((double) rtv);
				case TypeCode.Object:
					return Decimal.MinValue;
				case TypeCode.Int32:
					return Convert.ToDecimal((int) rtv);
				case TypeCode.Boolean:
					return Convert.ToDecimal((bool) rtv);
				case TypeCode.String:
					return Convert.ToDecimal((string) rtv);
				case TypeCode.DateTime:
					return Convert.ToDecimal((DateTime) rtv);
				default:
					return Decimal.MinValue;
			}
		}

		public virtual string EvaluateString(Report rpt, Row row)
		{
            object rtv = Evaluate(rpt, row);
			if (rtv == null)
				return null;

			return rtv.ToString();
		}

		public virtual DateTime EvaluateDateTime(Report rpt, Row row)
		{
            object rtv = Evaluate(rpt, row);
			if (rtv == null)
				return DateTime.MinValue;

            switch (this.GetTypeCode())
			{
				case TypeCode.Double:
					return Convert.ToDateTime((double) rtv);
				case TypeCode.Object:
					return DateTime.MinValue;
				case TypeCode.Int32:
					return Convert.ToDateTime((int) rtv);
				case TypeCode.Boolean:
					return Convert.ToDateTime((bool) rtv);
				case TypeCode.String:
					return Convert.ToDateTime((string) rtv);
				case TypeCode.DateTime:
					return (DateTime) rtv;
				default:
					return DateTime.MinValue;
			}
		}

		public virtual bool EvaluateBoolean(Report rpt, Row row)
		{
            object rtv = Evaluate(rpt, row);

			if (rtv == null)
				return false;

            switch (this.GetTypeCode())
			{
				case TypeCode.Double:
					return Convert.ToBoolean((double) rtv);
				case TypeCode.Object:
					return false;
				case TypeCode.Int32:
					return Convert.ToBoolean((int) rtv);
				case TypeCode.Boolean:
					return (bool) rtv;
				case TypeCode.String:
					return Convert.ToBoolean((string) rtv);
				case TypeCode.DateTime:
					return Convert.ToBoolean((DateTime) rtv);
				default:
					return false;
			}
		}
	}
    
    public enum ReportParameterMethodEnum
    {
        Value,
        Contains,
        BinarySearch,
        Count,
        IndexOf,
        LastIndexOf,
        Index
    }

}
