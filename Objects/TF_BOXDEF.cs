using System;

using System.Drawing;
using CWData;

namespace SSAPP.Objects
{
    public class TF_BOXDEF : Entity
    {
        public TF_BOXDEF()
        {
            _TableName = "ZZ_TF_BOXDEF";
            _ColumnId = "NID";
        }

        #region  Ù–‘
        public int NID
        {
            get
            {
                return base.GetProperty("NID", 0);
            }
            set
            {
                base.SetValue("NID", value);
            }
        }
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
        public string PRD_NO
        {
            get
            {
                return base.GetProperty("PRD_NO", "");
            }
            set
            {
                base.SetValue("PRD_NO", value);
            }
        }
        public string PRD_NAME
        {
            get
            {
                return base.GetProperty("PRD_NAME", "");
            }
            set
            {
                base.SetValue("PRD_NAME", value);
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
        public string Color_ID
        {
            get
            {
                return base.GetProperty("Color_ID", "");
            }
            set
            {
                base.SetValue("Color_ID", value);
            }
        }
        public string Color_Name
        {
            get
            {
                return base.GetProperty("Color_Name", "");
            }
            set
            {
                base.SetValue("Color_Name", value);
            }
        }
        public string Size_Id
        {
            get
            {
                return base.GetProperty("Size_Id", "");
            }
            set
            {
                base.SetValue("Size_Id", value);
            }
        }
        public string Size_Name
        {
            get
            {
                return base.GetProperty("Size_Name", "");
            }
            set
            {
                base.SetValue("Size_Name", value);
            }
        }
        public string ORDERID
        {
            get
            {
                return base.GetProperty("ORDERID", "");
            }
            set
            {
                base.SetValue("ORDERID", value);
            }
        }
        public int EST_ITM
        {
            get
            {
                return base.GetProperty("EST_ITM", 0);
            }
            set
            {
                base.SetValue("EST_ITM", value);
            }
        }
        public Decimal QTY
        {
            get
            {
                return base.GetProperty("QTY", (Decimal)0);
            }
            set
            {
                base.SetValue("QTY", value);
            }
        }
        public string BARCODE
        {
            get
            {
                return base.GetProperty("BARCODE", "");
            }
            set
            {
                base.SetValue("BARCODE", value);
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
        #endregion
    }
}
