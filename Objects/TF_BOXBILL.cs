using System;

using System.Drawing;
using CWData;

namespace SSAPP.Objects
{
    public class TF_BOXBILL : Entity
    {
        public TF_BOXBILL()
        {
            _TableName = "ZZ_TF_BOXBILL";
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
        public string BZ_NO
        {
            get
            {
                return base.GetProperty("BZ_NO", "");
            }
            set
            {
                base.SetValue("BZ_NO", value);
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
        public string BX_NAME
        {
            get
            {
                return base.GetProperty("BX_NAME", "");
            }
            set
            {
                base.SetValue("BX_NAME", value);
            }
        }
        public int BEGIN_NO
        {
            get
            {
                return base.GetProperty("BEGIN_NO", 0);
            }
            set
            {
                base.SetValue("BEGIN_NO", value);
            }
        }
        public int END_NO
        {
            get
            {
                return base.GetProperty("END_NO", 0);
            }
            set
            {
                base.SetValue("END_NO", value);
            }
        }
        public int BZ_QTY
        {
            get
            {
                return base.GetProperty("BZ_QTY", 0);
            }
            set
            {
                base.SetValue("BZ_QTY", value);
            }
        }
        public Decimal BZ_LENGTH
        {
            get
            {
                return base.GetProperty("BZ_LENGTH", (Decimal)0);
            }
            set
            {
                base.SetValue("BZ_LENGTH", value);
            }
        }
        public Decimal BZ_WIDTH
        {
            get
            {
                return base.GetProperty("BZ_WIDTH", (Decimal)0);
            }
            set
            {
                base.SetValue("BZ_WIDTH", value);
            }
        }
        public Decimal BZ_HEIGH
        {
            get
            {
                return base.GetProperty("BZ_HEIGH", (Decimal)0);
            }
            set
            {
                base.SetValue("BZ_HEIGH", value);
            }
        }
        public Decimal MZ
        {
            get
            {
                return base.GetProperty("MZ", (Decimal)0);
            }
            set
            {
                base.SetValue("MZ", value);
            }
        }
        public Decimal JZ
        {
            get
            {
                return base.GetProperty("JZ", (Decimal)0);
            }
            set
            {
                base.SetValue("JZ", value);
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
        public string SHOP
        {
            get
            {
                return base.GetProperty("SHOP", "");
            }
            set
            {
                base.SetValue("SHOP", value);
            }
        }
        #endregion

    }
}
