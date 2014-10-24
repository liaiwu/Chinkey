using System;
using System.Collections.Generic;
using System.Text;

using AttnObjects;

namespace SSAPP.Objects
{
    public class SS_TF_POS : TF_POS
    {
        /// <summary>
        /// 是否存在转入单
        /// </summary>
        /// <param name="os_id"></param>
        /// <param name="qt_no"></param>
        /// <returns></returns>
        public static bool ExistsImport(string Cont_Id, string ContStyle_Id)
        {
            //string sql = string.Format(" if exists(select * from TF_POS where OS_ID='{0}' and QT_NO='{1}') select 1 else select 0", os_id, qt_no);
            string sql = @"if exists( select * from mf_pos m left join tf_pos t on t.os_id=m.os_id and t.os_no=m.os_no where m.os_id='SO' and
m.CUS_OS_NO='{0}' and t.BAT_NO='{1}')
select 1
else
select 0";
            string strresult = SqlServer.GetFirstString(sql);
            return strresult == "1" ? true : false;
        }
    }
}
