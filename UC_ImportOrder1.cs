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
    public partial class UC_ImportOrder1 : UserControl
    {
        IDBServer erpserver;
        IDBServer sserver;
        string OS_ID = "SO";
        string P_SIZE = "CM";
        string P_COLOR = "YS";
        public UC_ImportOrder1()
        {
            InitializeComponent();
            erpserver = ServerFactory.GetServer();
            sserver = ServerFactory.GetServer(ServerType.ThirdDB);
            comb_Audit.SelectedIndex = 0;

         
        }
        const string const_sql1 = "select HtOrder_ID from HtContractOrder01t where 1=1 ";
        private DataTable  GetimportData()
        {
            DataTable dt=null;

            StringBuilder sbsql = new StringBuilder(const_sql1);
            if (this.textBox1.Text.Length > 0)
            {
                sbsql.Append(string.Format(" and HtOrder_ID like '%{0}%'", textBox1.Text));
            }
            if (!wDateTimePicker1.IsEmpty)
            {
                sbsql.Append(string.Format(" and ContractDate>='{0}'", wDateTimePicker1.Text));
            }
            if (!wDateTimePicker2.IsEmpty)
            {
                sbsql.Append(string.Format(" and ContractDate<='{0}'", wDateTimePicker2.Text));
            }
            if (txt_Ks.Text.Length > 0)
            {
                sbsql.Append(string.Format(" and Contstyle_ID like '%{0}%'", txt_Ks.Text));
            }
            if (this.txt_cus.Text.Length > 0)
            {
                sbsql.Append(string.Format(" and Client_Nm like '%{0}%'", txt_cus.Text));
            }
            if (this.txt_hy.Text.Length > 0)
            {
                sbsql.Append(string.Format(" and Cont_ID like '%{0}%'", txt_hy.Text));
            }
            if (this.comb_Audit.SelectedIndex == 1)//已审核
            {
                sbsql.Append(string.Format(" and IsAudit=2 ", txt_hy.Text));
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
            string sql1 = "select  * from HtContractOrder01t  where HtOrder_ID='{0}'";
            string sql2 = "select * from HtContractOrder02t where HtOrder_ID='{0}' order by HtOrder_Seq";
            string sql3 = "select Dcop_NM from XTDcop Where Dcop_ID='{0}'";
            string sql4 = @" if not exists(select *   from PMARK_DSC where FLDNAME=@FLDNAME and VALUE=@VALUE)
 insert into PMARK_DSC(FLDNAME,VALUE,DSC)values(@FLDNAME,@VALUE,@DSC)";
            //string sql5 = "select top 1 MATE_ID,MATE_NM,Color_ID,Color_NM,PdtSpec_Id,PdtSpec_NM  from  Gymateat where MATE_ID='{0}'";
            string sql6 = @" if not exists(select * from CUR_ID where CUR_ID=@CUR_ID)
 insert into CUR_ID(CUR_ID,IJ_DD,NAME,EXC_RTO,EXC_RTO_I,EXC_RTO_O) values (@CUR_ID,@IJ_DD,@NAME,@EXC_RTO,@EXC_RTO_I,@EXC_RTO_O)";

            DataTable sstfdt;
            DataTable ssmfdt;
            MY_WH defaultWh = MY_WH.GetTopWh();
            //bool isExistsMFCustom = BillGlobal.TableExists("MF_POS_Z");
            //bool isExistsTFCustom = BillGlobal.TableExists("TF_POS_Z");

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
                string  OrderID, Client_Id, Cont_Id, ContStyle_Id;
                string sizeid, sizename, colorid, colorname;
                string mCryKind_ID, mCryKind_Nm;
                float mCryCoef;

                DateTime OS_DD;
                LogTxt("开始导入数据……");
                DbParams dbp=new  DbParams ();

                foreach (DataRow dr in dt.Rows)
                {
                    OrderID = dr["HtOrder_ID"].ToString();
                    sql = string.Format(sql1, OrderID);
                    ssmfdt = sserver.GetDataTable(sql);
                    if(ssmfdt.Rows.Count<=0)continue ;
                    DataRow ssmfdr = ssmfdt.Rows[0];
                    Client_Id = ssmfdr["Client_Id"].ToString();
                    Cont_Id = ssmfdr["Cont_Id"].ToString();
                    ContStyle_Id = ssmfdr["ContStyle_Id"].ToString();
                    OS_DD=DateTime.Parse( BillGlobal.GetDateString((DateTime)ssmfdr["BillDate"]));
                    mCryKind_ID = DataTableHelper.GetRowString(ssmfdr, "mCryKind_ID");
                    mCryKind_Nm = DataTableHelper.GetRowString(ssmfdr, "mCryKind_Nm"); 
                    mCryCoef = DataTableHelper.GetRowFloat(ssmfdr, "mCryCoef");

                    MATE_ID = ssmfdr["PdtMate_Id"].ToString();
                    if (MATE_ID.Length <= 0)
                    {
                        MessageService.ShowWarning(string.Format("编号为【{0}】的订单号，产品编号为空！",OrderID));
                        continue;
                    }
                    if (mCryKind_ID.Length >4)
                    {
                        MessageService.ShowWarning(string.Format("编号为【{0}】的订单号，汇率编号长度不能大于4！", OrderID));
                        continue;
                    }
                    dbp.Clear();
                    dbp["CUR_ID"] = mCryKind_ID;
                    dbp["NAME"] = mCryKind_Nm;
                    dbp["IJ_DD"] = BillGlobal.GetDateString(DateTime.Now);
                    dbp["EXC_RTO"] = dbp["EXC_RTO_O"] = dbp["EXC_RTO_I"] = mCryCoef;

                    erpserver.ExecuteSQL(sql6, dbp);

                    prdt.PRD_NO = MATE_ID;
                    prdt.NAME = DataTableHelper.GetRowString(ssmfdr, "PdtMate_Nm");
                    //prdt.SPC = DataTableHelper.GetRowString(tfdr, "PdtSpec_NM");
                    prdt.Insert();

                    MF_POS mfpos = MF_POS.GetByCUS_OS_NO(Cont_Id);
                    if (mfpos.OS_NO.Length <= 0)//此合约没有导入
                    {
                        LogTxt(string.Format("全新导入合约号为{0}，跟单号为{1}的订单--", Cont_Id, OrderID));
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
                        mfpos.QT_NO = OrderID;
                        mfpos.SEND_MTH = "1";
                        mfpos.CUR_ID = "";
                        mfpos.CLS_ID = "F";
                        mfpos.EXC_RTO = 1;
                        mfpos.AMT_INT = 0;
                        mfpos.DIS_CNT = 0;
                        mfpos.HIS_PRICE = "T";
                        mfpos.BAT_NO = ContStyle_Id;
                        mfpos.TAX_ID = "2";
                        mfpos.CUR_ID = mCryKind_ID;
                        mfpos.EXC_RTO =(decimal) mCryCoef;
                        mfpos.TAX_ID = "1";//扣税类别 
                        //mfpos.CHK_MAN=
                        mfpos.USR = AttnObjects.PSWD.LoginUser.USR;
                        mfpos.PRT_SW = "N";
                        ///自定义表的内容
                        mfpos["CJTK"] = ssmfdr["TradeClause"].ToString();
                        mfpos["FKFS"] = ssmfdr["PayKind"].ToString();

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

                    sql = string.Format(sql2, OrderID);
                    sstfdt = sserver.GetDataTable(sql);
                    //DbParams dbp = new DbParams();
                     dbp.Clear();

                    decimal qty;
                    foreach (DataRow tfdr in sstfdt.Rows)
                    {
                        colorid = sizeid = string.Empty;
                        //导入产品信息
                        //MATE_ID = tfdr["PdtMate_Id"].ToString();
                        //sql = string.Format(sql5, MATE_ID);
                        //DataTable dt_prd = sserver.GetDataTable(sql);
                        //if (dt_prd.Rows.Count == 1)
                        //{
                        //    DataRow prdr = dt_prd.Rows[0];

                            //prdt.PRD_NO = MATE_ID;
                            //prdt.NAME = DataTableHelper.GetRowString(prdr, "MATE_NM");
                            ////prdt.SPC = DataTableHelper.GetRowString(tfdr, "PdtSpec_NM");
                            //prdt.Insert();

                        colorid = DataTableHelper.GetRowString(tfdr, "Color_ID");
                        colorname = DataTableHelper.GetRowString(tfdr, "Color_Nm");
                        sizeid = DataTableHelper.GetRowString(tfdr, "PdtSpec_ID");
                        sizename = DataTableHelper.GetRowString(tfdr, "PdtSpec_NM");

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
                        //}
                        dbp.Clear();
                        dbp.Populate(tfdr);
                        qty=(decimal)DataTableHelper.GetRowFloat(tfdr, "PdtPlanQty");

                        tfpos.ITM = ++itm;
                        tfpos.WH = defaultWh.WH;
                        tfpos.QT_NO = OrderID;
                        tfpos.PRD_NO = prdt.PRD_NO;
                        tfpos.PRD_NAME = prdt.NAME;
                        tfpos.OS_DD = OS_DD;
                        tfpos.UNIT = "1";
                        tfpos.UP = (decimal)DataTableHelper.GetRowFloat(ssmfdr, "mCryPdtPrice");
                        tfpos.AMT = tfpos.QTY * tfpos.UP;
                        tfpos.AMTN = tfpos.AMT;
                        tfpos.TAX = 0;
                        tfpos.QTY = qty;
                        tfpos.EST_DD = dbp.GetProperty("DeliverDate", OS_DD);
                        tfpos.EST_ITM = dbp.GetProperty("HtOrder_Seq", 1);
                        tfpos.PRE_EST_DD = OS_DD;
                        tfpos.PRE_ITM = tfpos.EST_ITM;
                        tfpos.TAX_RTO = 5;
                        tfpos.PRD_MARK=colorid.PadLeft(12,' ')+sizeid.PadLeft(12,' ');
                        tfpos.BAT_NO = mfpos.BAT_NO;
                        //自定义表的内容
                        tfpos["MLCF"] = dbp.GetProperty("ClothElement", "");
                        tfpos.Insert();

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
