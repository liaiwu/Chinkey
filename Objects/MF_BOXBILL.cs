using System;

using System.Drawing;
using CWData;

namespace SSAPP.Objects
{
    public class MF_BOXBILL : Entity
    {
        public MF_BOXBILL()
        {
            _TableName = "ZZ_MF_BOXBILL";
            _ColumnId = "BZ_ID,BZ_NO";
        }

        #region   Ù–‘
        public string BZ_ID
        {
            get
            {
                return base.GetProperty("BZ_ID", "");
            }
            set
            {
                base.SetValue("BZ_ID", value);
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
        public DateTime BZ_DD
        {
            get
            {
                return base.GetProperty("BZ_DD", System.DateTime.MinValue);
            }
            set
            {
                base.SetValue("BZ_DD", value);
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
        public DateTime SYS_DD
        {
            get
            {
                return base.GetProperty("SYS_DD", System.DateTime.MinValue);
            }
            set
            {
                base.SetValue("SYS_DD", value);
            }
        }
        public string CUS_NO
        {
            get
            {
                return base.GetProperty("CUS_NO", "");
            }
            set
            {
                base.SetValue("CUS_NO", value);
            }
        }
        public string CUS_NAME
        {
            get
            {
                return base.GetProperty("CUS_NAME", "");
            }
            set
            {
                base.SetValue("CUS_NAME", value);
            }
        }
        public string CUS_WHERE
        {
            get
            {
                return base.GetProperty("CUS_WHERE", "");
            }
            set
            {
                base.SetValue("CUS_WHERE", value);
            }
        }
        #endregion

    }
}
