using System;

using System.Drawing;
using CWData;

namespace SSAPP.Objects
{
    public class MF_BOXDEF : Entity
    {
        public MF_BOXDEF()
        {
            _TableName = "ZZ_MF_BOXDEF";
            _ColumnId = "BX_NO";
        }

        #region  Ù–‘
        public string BX_NO
        {
            get
            {
                return base.GetProperty("BX_NO", "");
            }
            set
            {
                base.SetValue("BX_NO", value);
            }
        }
        public DateTime BX_DD
        {
            get
            {
                return base.GetProperty("BX_DD", System.DateTime.MinValue);
            }
            set
            {
                base.SetValue("BX_DD", value);
            }
        }
        public string BXNAME
        {
            get
            {
                return base.GetProperty("BXNAME", "");
            }
            set
            {
                base.SetValue("BXNAME", value);
            }
        }
        public string Cont_Id
        {
            get
            {
                return base.GetProperty("Cont_Id", "");
            }
            set
            {
                base.SetValue("Cont_Id", value);
            }
        }
        public string Style_Id
        {
            get
            {
                return base.GetProperty("Style_Id", "");
            }
            set
            {
                base.SetValue("Style_Id", value);
            }
        }

        public string CUS_OS_NO
        {
            get
            {
                return base.GetProperty("CUS_OS_NO", "");
            }
            set
            {
                base.SetValue("CUS_OS_NO", value);
            }
        }
        public string BAT_NO
        {
            get
            {
                return base.GetProperty("BAT_NO", "");
            }
            set
            {
                base.SetValue("BAT_NO", value);
            }
        }
        public Decimal BX_LENGTH
        {
            get
            {
                return base.GetProperty("BX_LENGTH", (Decimal)0);
            }
            set
            {
                base.SetValue("BX_LENGTH", value);
            }
        }
        public Decimal BX_WIDTH
        {
            get
            {
                return base.GetProperty("BX_WIDTH", (Decimal)0);
            }
            set
            {
                base.SetValue("BX_WIDTH", value);
            }
        }
        public Decimal BX_HEIGH
        {
            get
            {
                return base.GetProperty("BX_HEIGH", (Decimal)0);
            }
            set
            {
                base.SetValue("BX_HEIGH", value);
            }
        }
        public Decimal VOLUME
        {
            get
            {
                return base.GetProperty("VOLUME", (Decimal)0);
            }
            set
            {
                base.SetValue("VOLUME", value);
            }
        }
        public Decimal BX_NET
        {
            get
            {
                return base.GetProperty("BX_NET", (Decimal)0);
            }
            set
            {
                base.SetValue("BX_NET", value);
            }
        }
        public string REM
        {
            get
            {
                return base.GetProperty("REM", "");
            }
            set
            {
                base.SetValue("REM", value);
            }
        }
        public int BX_Type
        {
            get
            {
                return base.GetProperty("BX_Type", 0);
            }
            set
            {
                base.SetValue("BX_Type", value);
            }
        }
        public string USR
        {
            get
            {
                return base.GetProperty("USR", "");
            }
            set
            {
                base.SetValue("USR", value);
            }
        }
        #endregion

    }
}
