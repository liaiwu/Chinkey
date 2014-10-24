using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using CWData;
using ICSharpCode.Core;
using WinControls;
using AttnObjects;
using WinControls.Helpers;
using SSAPP.Objects;

namespace SSAPP
{
    public partial class UC_ImportOrder : UserControl
    {
        IDBServer erpserver;
        IDBServer sserver;
        string OS_ID = "SO";
        string P_SIZE = "CM";
        string P_COLOR = "YS";
        public UC_ImportOrder()
        {
            InitializeComponent();
            erpserver = ServerFactory.GetServer();
            sserver = ServerFactory.GetServer(ServerType.ThirdDB);
        }
        const string const_sql1 = "select CalcMateSlopG_ID from JhCalcMateSlopG01t where 1=1 ";
        private DataTable  GetimportData()
        {
            DataTable dt=null;
            StringBuilder sbsql = new StringBuilder(const_sql1);
            if (this.textBox1.Text.Length > 0)
            {
                sbsql.Append(string.Format(" and CalcMateSlopG_ID like '%{0}%'", textBox1.Text));
            }
            if (!wDateTimePicker1.IsEmpty)
            {
                sbsql.Append(string.Format(" and BillDate>='{0}'", wDateTimePicker1.Text));
            }
            if (!wDateTimePicker2.IsEmpty)
            {
                sbsql.Append(string.Format(" and BillDate<='{0}'", wDateTimePicker2.Text));
            }
            if (txt_Ks.Text.Length > 0)
            {
                sbsql.Append(string.Format(" and Contstyle_ID like '%{0}%'", txt_Ks.Text));
            }
            if (this.txt_cus.Text.Length > 0)
            {
                sbsql.Append(string.Format(" and Client_ID like '%{0}%'", txt_cus.Text));
            }
            if (this.txt_hy.Text.Length > 0)
            {
                sbsql.Append(string.Format(" and Cont_ID like '%{0}%'", txt_hy.Text));
            }
            string sql = sbsql.ToString();
            dt=sserver.GetDataTable(sql);
            return dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = GetimportData();
            if (dt.Rows.Count <= 0)
            {
                MessageService.ShowMessage("未找到任何数据！");
            }
            if (!MessageService.AskQuestion(string.Format("共找到[{0}]条数据，要进行导入吗？", dt.Rows.Count)))
            {
                return;
            }
            try
            {
                ImportOrder(dt);
            }
            catch (Exception er)
            {
                MessageService.ShowError(er.Message);
            }
        }
        private void ImportOrder(DataTable dt)
        {
            string sql = string.Empty;
            string sql1 = "select  * from JhCalcMateSlopG01t  where CalcMateSlopG_ID='{0}'";
            string sql2 = "select * from JhCalcMateSlopG02t where CalcMateSlopG_ID='{0}' order by  CalcMateSlopG_Idx";
            string sql3 = "select Dcop_NM from XTDcop Where Dcop_ID='{0}'";
            string sql4 = @" if not exists(select *   from PMARK_DSC where FLDNAME=@FLDNAME and VALUE=@VALUE)
 insert into PMARK_DSC(FLDNAME,VALUE,DSC)values(@FLDNAME,@VALUE,@DSC)";
            string sql5 = "select top 1 MATE_ID,MATE_NM,Color_ID,Color_NM,PdtSpec_Id,PdtSpec_NM  from  Gymateat where MATE_ID='{0}'";
            DataTable sstfdt;
            DataTable ssmfdt;
            MY_WH defaultWh = MY_WH.GetTopWh();

            using (WaitDialog fmwd = new WaitDialog())
            {
                fmwd.Show();
                CUST cust = new CUST();
                cust.USR1 = AttnObjects.PSWD.LoginUser.USR;
                cust.OBJ_ID = "1";

                PRDT prdt = new PRDT();
                prdt.UT = "件";

                //prdt.USR = LoginService.LoginUser.USR;
                prdt.USR = AttnObjects.PSWD.LoginUser.USR;
                prdt.CHK_MAN = prdt.USR;

                SS_TF_POS tfpos = new SS_TF_POS();
                string MATE_ID;//产品ID
                string  CalcMateSlopG_ID, Client_Id, Cont_Id, ContStyle_Id;
                string sizeid, sizename, colorid, colorname;
                DateTime OS_DD;
                LogTxt("开始导入数据……");
                foreach (DataRow dr in dt.Rows)
                {
                    CalcMateSlopG_ID = dr["CalcMateSlopG_ID"].ToString();
                    sql = string.Format(sql1, CalcMateSlopG_ID);
                    ssmfdt = sserver.GetDataTable(sql);
                    if(ssmfdt.Rows.Count<=0)continue ;
                    DataRow ssmfdr = ssmfdt.Rows[0];
                    Client_Id = ssmfdr["Client_Id"].ToString();
                    Cont_Id = ssmfdr["Cont_Id"].ToString();
                    ContStyle_Id = ssmfdr["ContStyle_Id"].ToString();
                    OS_DD=DateTime.Parse( BillGlobal.GetDateString((DateTime)ssmfdr["BillDate"]));

                    MF_POS mfpos = MF_POS.GetByCUS_OS_NO(Cont_Id);
                    if (mfpos.OS_NO.Length <= 0)//此合约没有导入
                    {
                        LogTxt(string.Format("全新导入合约号为{0}，跟单号为{1}的订单--", Cont_Id, CalcMateSlopG_ID));
                        ///导入客户信息
                        cust.CUS_NO = Client_Id;
                        sql = string.Format(sql3, cust.CUS_NO);
                        cust.NAME = sserver.GetFirstString(sql);
                        cust.Insert();
                        cust = CUST.GetById<CUST>(Client_Id);

                        mfpos.OS_ID = OS_ID;
                        mfpos.OS_NO = BillGlobal.GetNo(OS_ID, false);
                        mfpos.CUS_NO = Client_Id;
                        mfpos.CUS_OS_NO = Cont_Id;
                        mfpos.OS_DD = OS_DD;
                        mfpos.PAY_MTH = cust.CLS_MTH;
                        mfpos.PAY_DAYS = cust.CLS_DD;
                        mfpos.CHK_DAYS = cust.CHK_DD;
                        mfpos.QT_NO = CalcMateSlopG_ID;
                        mfpos.SEND_MTH = "1";
                        mfpos.CUR_ID = "";
                        mfpos.CLS_ID = "F";
                        mfpos.EXC_RTO = 1;
                        mfpos.AMT_INT = 0;
                        mfpos.DIS_CNT = 0;
                        mfpos.HIS_PRICE = "T";
                        mfpos.BAT_NO = ContStyle_Id;
                        mfpos.TAX_ID = "2";
                        //mfpos.CHK_MAN=
                        mfpos.USR = AttnObjects.PSWD.LoginUser.USR;
                        mfpos.PRT_SW = "N";

                        mfpos.Insert();
                    }
                    else
                    {
                        if (SS_TF_POS.ExistsImport(Cont_Id,ContStyle_Id))
                        {
                            LogTxt(string.Format("发现跟合约为{0}，款式为{1}订单的已导入，略过此订单。", Cont_Id, ContStyle_Id));
                            continue;
                        }
                        LogTxt(string.Format("发现合约号为{0}的订单已导入，进行订单合并，ERP订单号：{1}", Cont_Id, mfpos.OS_NO));
                    }


                    tfpos.OS_ID = OS_ID;
                    tfpos.OS_NO = mfpos.OS_NO;
                    int itm = TF_POS.GetMaxItem(OS_ID, mfpos.OS_NO);

                    sql = string.Format(sql2, dr["CalcMateSlopG_ID"].ToString());
                    sstfdt = sserver.GetDataTable(sql);
                    DbParams dbp = new DbParams();
                    decimal qty;
                    foreach (DataRow tfdr in sstfdt.Rows)
                    {
                        colorid = sizeid = string.Empty;
                        //导入产品信息
                        MATE_ID = tfdr["PdtMate_Id"].ToString();
                        sql = string.Format(sql5, MATE_ID);
                        DataTable dt_prd = sserver.GetDataTable(sql);
                        if (dt_prd.Rows.Count == 1)
                        {
                            DataRow prdr = dt_prd.Rows[0];

                            prdt.PRD_NO = MATE_ID;
                            prdt.NAME = DataTableHelper.GetRowString(prdr, "MATE_NM");
                            //prdt.SPC = DataTableHelper.GetRowString(tfdr, "PdtSpec_NM");
                            prdt.Insert();

                            colorid = DataTableHelper.GetRowString(prdr, "Color_ID");
                            colorname = DataTableHelper.GetRowString(prdr, "Color_NM");
                            sizeid = DataTableHelper.GetRowString(prdr, "PdtSpec_Id");
                            sizename = DataTableHelper.GetRowString(prdr, "PdtSpec_NM");
                            if (sizeid.Length > 0)//导入规格信息
                            {
                                dbp["FLDNAME"] = P_SIZE;
                                dbp["VALUE"] = sizeid;
                                dbp["DSC"] = sizename;
                                erpserver.ExecuteSQL(sql4, dbp);
                            }
                            if (colorid.Length > 0)//导入颜色信息
                            {
                                dbp["FLDNAME"] = P_COLOR;
                                dbp["VALUE"] = colorid;
                                dbp["DSC"] = colorname;
                                erpserver.ExecuteSQL(sql4, dbp);
                            }
                        }
                        dbp.Clear();
                        dbp.Populate(tfdr);
                        qty=(decimal)DataTableHelper.GetRowFloat(tfdr, "PdtPlanQty");

                        tfpos.ITM = ++itm;
                        tfpos.WH = defaultWh.WH;
                        tfpos.QT_NO = CalcMateSlopG_ID;
                        tfpos.PRD_NO = prdt.PRD_NO;
                        tfpos.PRD_NAME = prdt.NAME;
                        tfpos.OS_DD = OS_DD;
                        tfpos.UNIT = "1";                       
                        tfpos.UP = (decimal)DataTableHelper.GetRowFloat(ssmfdr, "mcryPdtPrice");
                        tfpos.AMT = tfpos.QTY * tfpos.UP;
                        tfpos.AMTN = tfpos.AMT;
                        tfpos.TAX = 0;
                        tfpos.QTY = dbp.GetProperty("PdtPlanQty1", qty);
                        tfpos.EST_DD =dbp.GetProperty("PdtRDate1",OS_DD);
                        tfpos.EST_ITM =dbp.GetProperty("CalcMateSlopG_Idx",1);
                        tfpos.PRE_EST_DD = OS_DD;
                        tfpos.PRE_ITM = tfpos.EST_ITM;
                        tfpos.TAX_RTO = 5;
                        tfpos.PRD_MARK=colorid.PadLeft(12,' ')+sizeid.PadLeft(12,' ');
                        tfpos.BAT_NO = mfpos.BAT_NO;
                        tfpos.Insert();

                        if (!dbp.IsNull("PdtRDate2"))
                        {
                            tfpos.ITM = ++itm;
                            tfpos.QTY = dbp.GetProperty("PdtPlanQty2", 0);
                            tfpos.EST_DD = dbp.GetProperty("PdtRDate2", OS_DD);
                            tfpos.Insert();
                        }
                        if (!dbp.IsNull("PdtRDate3"))
                        {
                            tfpos.ITM = ++itm;
                            tfpos.QTY = dbp.GetProperty("PdtPlanQty3", 0);
                            tfpos.EST_DD = dbp.GetProperty("PdtRDate3", OS_DD);
                            tfpos.Insert();
                        }
                        if (!dbp.IsNull("PdtRDate4"))
                        {
                            tfpos.ITM = ++itm;
                            tfpos.QTY = dbp.GetProperty("PdtPlanQty4", 0);
                            tfpos.EST_DD = dbp.GetProperty("PdtRDate4", OS_DD);
                            tfpos.Insert();
                        }
                        if (!dbp.IsNull("PdtRDate5"))
                        {
                            tfpos.ITM = ++itm;
                            tfpos.QTY = dbp.GetProperty("PdtPlanQty5", 0);
                            tfpos.EST_DD = dbp.GetProperty("PdtRDate5", OS_DD);
                            tfpos.Insert();
                        }

                    }
                }
            }
            LogTxt("导入结束！");
            MessageService.ShowMessage("导入完毕！");
        }

        public void LogTxt(string mes)
        {
            this.listBox1.Items.Insert(0, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")+":"+mes);
            this.listBox1.SelectedIndex = 0;
        }
    }
}
