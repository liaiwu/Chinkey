using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using WinControls.Controls;
using AttnObjects;
using CWData;
using DevExpress.XtraEditors.Repository;
using WinControls.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using SSAPP.Objects;
using ICSharpCode.Core;
using WinControls.PrintCenter;
using DevExpress.XtraPivotGrid;
using WinControls;

namespace SSAPP
{
    public partial class UC_BasePlan : UCEditBase
    {
        protected EditGrid dataGrid = new EditGrid();
        protected EditGrid dataGrid1 = new EditGrid();
        protected string ViewName = "ZZ_TF_BOXBILL";
        protected string ViewName1 = "V_ZZ_TF_BOXDEF";
        
        //protected string EditViewName = "TF_YGWage";
        //protected string TableName = "TF_YGWage";

        DataTable curdt;
        DataTable curdt1;
        DataTable curdt2;//P  数汇总表
        string BillID = "BZ";
        IDBServer erpserver;
        IDBServer sserver;
        MF_BOXBILL mfEntity = new MF_BOXBILL();
        public UC_BasePlan()
        {
            InitializeComponent();
            base.printBh = "08103";
            erpserver = ServerFactory.GetServer();
            sserver = ServerFactory.GetServer(ServerType.ThirdDB);

            myInit();
            New(null, null);
        }

        private void myInit()
        {
            dataGrid.Dock = DockStyle.Fill;
            //dataGrid.NewItemRowPosition = NewItemRowPosition.None;
            dataGrid.Name = "dataGrid1"+ this.GetType().ToString();
            dataGrid.CanEdit = true;
            dataGrid.CanDelete = true;
            //dataGrid.IsShowCustomMenu = false;
            dataGrid.InitNewRow += dataGrid_InitNewRow;
            dataGrid.OnValidatingEditor += dataGrid_OnValidatingEditor;
            //dataGrid.FocusedRowChanged += dataGrid_FocusedRowChanged;
            dataGrid.OnCreateColumning += dataGrid_OnCreateColumning;
            //dataGrid.CanSort = true;
            this.tabPage1.Controls.Add(dataGrid);
            dataGrid.SetCaption(ViewName);

            dataGrid1.Dock = DockStyle.Fill;
            dataGrid1.NewItemRowPosition = NewItemRowPosition.None;
            dataGrid1.Name = "dataGrid2" + this.GetType().ToString();
            dataGrid1.CanEdit = false;
            dataGrid1.CanDelete = false;

            this.tabPage2.Controls.Add(dataGrid1);
            dataGrid1.SetCaption(ViewName1);
            
            date_BoxBill.Value = DateTime.Now;

        }

        void dataGrid_OnValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (e.Value == null) return;
            string fieldname=dataGrid.FocusedColumn.FieldName;
            if (fieldname == "BEGIN_NO" || fieldname == "END_NO")
            {
                int no=0;
                int no1 = 0;
                if (!int.TryParse(e.Value.ToString(),out no))
                {
                    e.Valid = false;
                    e.ErrorText = "请输入数字";
                    return;
                }
                DataRow curdr=dataGrid.GetDataRow(dataGrid.FocusedRowHandle);
                if (fieldname == "BEGIN_NO")
                {
                    if (curdr["END_NO"] == null || curdr["END_NO"] == DBNull.Value || string.IsNullOrEmpty(curdr["END_NO"].ToString()))
                    {
                        int qty = DataTableHelper.GetRowInt(curdr, "BZ_QTY");
                        if (qty != 0)
                        {
                            dataGrid.SetRowValue("END_NO", no+qty- 1);
                        }
                    }
                    else
                    {
                        no1 = DataTableHelper.GetRowInt(curdr, "END_NO");
                        dataGrid.SetRowValue("BZ_QTY", no1 - no + 1);
                    }
                }
                else
                {
                    no1 = DataTableHelper.GetRowInt(curdr, "BEGIN_NO");
                    dataGrid.SetRowValue("BZ_QTY", no-no1 +1);
                }
            }
            else if (fieldname == "BX_NO")
            {
                MF_BOXDEF boxdef = MF_BOXDEF.GetById<MF_BOXDEF>(e.Value.ToString());
                if (boxdef.BX_NO.Length > 0)
                {
                    DataRow dr = dataGrid.GetDataRow(dataGrid.FocusedRowHandle);
                    dr["BX_NAME"] = boxdef.BXNAME;
                    dr["BZ_LENGTH"] = boxdef.BX_LENGTH;
                    dr["BZ_WIDTH"] = boxdef.BX_WIDTH;
                    dr["BZ_HEIGH"] = boxdef.BX_HEIGH;
                    dr["VOLUME"] = boxdef.VOLUME;
                    dr["PZ"] = boxdef.BX_NET;
                }
            }
            else if (fieldname == "JZ")
            {
                decimal jz,pz;
                jz = pz = 0;
                if (decimal.TryParse(e.Value.ToString(), out jz) )
                {
                    DataRow dr = dataGrid.GetDataRow(dataGrid.FocusedRowHandle);
                    pz =(decimal) DataTableHelper.GetRowFloat(dr, "PZ");
                    dr["MZ"] = jz + pz;
                }

            }
        }

        void dataGrid_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {            
            dataGrid.SetRowValue(e.RowHandle, "BZ_NO", this.txt_BoxBNo.Text);
            dataGrid.SetRowValue(e.RowHandle, "Cont_Id", this.txt_Cont_Id.Text);
            dataGrid.SetRowValue(e.RowHandle, "Style_Id", this.txt_Style.Text);
        }
        RepositoryItemGridLookUpEdit EditItem_BX_NO;
        void dataGrid_OnCreateColumning(ZZ_TAB_VIEW dct, out DevExpress.XtraEditors.Repository.RepositoryItem RepositoryItem)
        {
            RepositoryItem = null;
            if (dct.FldName == "BX_NO")
            {
                EditItem_BX_NO = DevColumnHelper.GetLookupItem(dct);
                EditItem_BX_NO.NullText = "";
                EditItem_BX_NO.ValueMember = "包装编号";
                EditItem_BX_NO.DisplayMember = "包装名称";

                //DevColumnHelper.SetLookupItemCaption(Repository_PRD, "MateSlopG01t");
                EditItem_BX_NO.PopupFormSize = new System.Drawing.Size(500, 300);

                string sql = "select BX_NO as 包装编号,BXNAME as 包装名称, CUS_OS_NO as 合约号, BAT_NO as  款式,BX_Type as 包装方式  from dbo.ZZ_MF_BOXDEF";
                EditItem_BX_NO.DataSource = myServer.GetDataTable(sql);

                //DevExpress.XtraGrid.Columns.GridColumn col;
                //col = new DevExpress.XtraGrid.Columns.GridColumn();
                //col.Caption = "ID";
                //col.FieldName = "ID";
                //col.Name = "cloID";
                //col.Visible = true;
                //col.VisibleIndex = 1;
                //col.Width = 20;
                //Repository_PRD.View.Columns.Add(

                RepositoryItem = EditItem_BX_NO;
                //Repository_PRD.DataSource
            }
        }
        public override void New(object sender, EventArgs e)
        {
            this.txt_BoxBNo.Text = BillGlobal.GetNo(BillID, true);
            curdt = BillGlobal.GetEmptyTable("select * from ZZ_TF_BOXBILL");
            this.dataGrid.DataSource = curdt;
        }
        public override bool Save(object sender, EventArgs e)
        {
            //if (!Valid()) return false;
            dataGrid.CloseEditor();
            using (WaitDialog fmwd = new WaitDialog())
            {
                if (isNew)
                {
                    mfEntity.BZ_ID = BillID;
                    mfEntity.BZ_NO = this.txt_BoxBNo.Text = BillGlobal.GetNo(BillID, false);
                    mfEntity.USR = AttnObjects.PSWD.LoginUser.USR;
                    GetDataFromFace(mfEntity);
                    mfEntity.Insert();
                }
                else
                {
                    GetDataFromFace(mfEntity);
                    mfEntity.SaveChanges();
                }
                TF_BOXBILL tfEntity = new TF_BOXBILL();

                foreach (DataRow dr in curdt.Rows)
                {
                    if (dr.RowState == DataRowState.Deleted)
                    {
                        dr.RejectChanges();
                        tfEntity.Populate(dr);
                        tfEntity.Delete();
                        dr.Delete();
                        continue;
                    }
                    tfEntity.Populate(dr);
                    tfEntity.BZ_NO = mfEntity.BZ_NO;

                    if (dr.RowState == DataRowState.Added || dr.RowState == DataRowState.Detached)
                    {
                        tfEntity.Insert();
                    }
                    else if (dr.RowState == DataRowState.Modified)
                    {
                        tfEntity.SaveChanges();
                    }

                }
                isNew = false;
                curdt.AcceptChanges();
            }
            return true;
        }
        private void GetDataFromFace(MF_BOXBILL mfEntity)
        {
            mfEntity.CUS_OS_NO = this.txt_CUS_SO_NO.Text;
            mfEntity.Cont_Id = this.txt_Cont_Id.Text;
            mfEntity.Style_Id = this.txt_Style.Text;
            mfEntity.BAT_NO = this.txt_BAT_NO.Text;

            mfEntity["BZ_DD"] = BillGlobal.GetDateString(this.date_BoxBill.Value);
        }
        Fm_ScBillOD fmSc = null;
        public override void Find(object sender, EventArgs e)
        {
            if (fmSc == null)
            { fmSc = new Fm_ScBillOD(); }
            else
            {
                fmSc.RefreshData();
            }

            fmSc.SQL = "select LAW.BZ_NO as '单号',convert(varchar(10),LAW.BZ_DD,20) as '日期' from ZZ_MF_BOXBILL LAW where LAW.BZ_ID='BZ' and (convert(varchar(10),LAW.BZ_DD,20)>='{0}' and convert(varchar(10),LAW.BZ_DD,20)<='{1}') ";

            if (fmSc.ShowDialog() == DialogResult.OK)
            {
                this.txt_BoxBNo.Text = fmSc.GetSelectRow()["单号"].ToString();
                //this.Text = fmSc.GetSelectRow()["编号"].ToString();
                LoadData(this.txt_BoxBNo.Text);
            }
        }
        const string sql1 = " select * from ZZ_MF_BOXBILL where BZ_NO='{0}' AND BZ_ID='{1}' ";
        const string sql2 = " select * from ZZ_TF_BOXBILL where BZ_NO='{0}' ";
        private void LoadData(string boxno)
        {
            string sql = string.Format(sql1, boxno, BillID);
            mfEntity = MF_BOXBILL.GetEntity<MF_BOXBILL>(sql, null);

            if (mfEntity.BZ_NO.Length > 0)
            {
                this.txt_BoxBNo.Text = mfEntity.BZ_NO;
                this.txt_Style.Text = mfEntity.Style_Id;
                this.date_BoxBill.Value = mfEntity.BZ_DD;
                this.txt_CUS_SO_NO.Text = mfEntity.CUS_OS_NO;
                this.txt_BAT_NO.Text = mfEntity.BAT_NO;
                this.txt_Cont_Id.Text = mfEntity.Cont_Id;
                
                sql = string.Format(sql2, mfEntity.BZ_NO);
                this.curdt = myServer.GetDataTable(sql);
                this.dataGrid.DataSource = this.curdt;
            }
            else
            {
                MessageService.ShowWarning("未找到此单据！");
            }
            isNew = false;
        }
        public override void Delete()
        {
            if (isNew) { New(null, null); }
            else
            {
                if (!MessageService.AskQuestion("您要删除此单据吗？"))
                {
                    return;
                }
                mfEntity.Delete();
                New(null, null);
            }
        }
        protected override object GetPrintData()
        {
            ExpandMX();
            ExpandPMX();

            string sql = string.Format(sql1, this.txt_BoxBNo.Text, BillID);
            DataTable mfdt = erpserver.GetDataTable(sql, null);

            ReportDataTable rdt_mf = ReportDataTable.ConvertDataTable(mfdt);
            rdt_mf.TableName = "表头";

            ReportDataTable rdt_tf = ReportDataTable.ConvertDataTable(this.curdt1);
            rdt_tf.TableName = "明细数据";

            ReportDataTable rdt_tf1 = ReportDataTable.ConvertDataTable(this.curdt2);
            rdt_tf1.TableName = "明细数据P";

            //ReportDataTable rdt_tf = ReportDataTable.ConvertDataTable(curdt);
            //rdt_tf.PopulateCaption(this.ViewName);
            //rdt_tf.TableName = "表身";

            ReportDataSet dataset = new ReportDataSet();
            dataset.Tables.Add(rdt_tf);
            dataset.Tables.Add(rdt_mf);
            dataset.Tables.Add(rdt_tf1);
            //dataset.Tables.Add(rdt_tf);

            return dataset;

        }

        StringBuilder sbsql = new StringBuilder();
        const string sql_tf_boxdef = @"  SELECT m.BX_NO,m.BX_Type,t.PRD_NO,t.PRD_NAME,t.Color_ID,t.Color_Name,t.Size_Id,t.Size_Name,t.ORDERID,t.EST_ITM,t.QTY as BX_QTY,(isnull(t.QTY,0)*{0}) as QTY ,t.BARCODE,t.USR,
  t.P2Name,t.P2QTY,t.P4Name,t.P4QTY
  FROM ZZ_MF_BOXDEF m left join ZZ_TF_BOXDEF t on t.BX_NO=m.BX_NO
  WHERE m.BX_NO ='{1}'";

        private void ExpandMX()
        {
            sbsql.Length = 0;
            if (curdt1 == null)
            {
                curdt1 = new DataTable();
            }
            curdt1.Clear();
            string sql;
            string bxno;
            int qty;
            foreach (DataRow dr in this.curdt.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    continue;
                }
                //if (dr["BZ_QTY"] == null || dr["BZ_QTY"] == DBNull.Value || dr["BX_NO"] == null || dr["BX_NO"] == DBNull.Value)
                //{
                //    continue;
                //}
                bxno = dr["BX_NO"].ToString();
                qty = 0;
                int.TryParse(dr["BZ_QTY"].ToString(), out qty);
                sql = string.Format(sql_tf_boxdef, qty, bxno);
                DataTable dtmx = myServer.GetDataTable(sql);
                dtmx.Merge(dr.Table.Clone(), false, MissingSchemaAction.Add);
                FillDataTableByRow(dtmx, dr);
                dtmx.AcceptChanges();
                curdt1.Merge(dtmx, false, MissingSchemaAction.Add);
            }
            this.dataGrid1.DataSource = curdt1;

        }
        const string sql_tf_boxdef_group2P = @"  SELECT count(t.bat_no) as count1,m.BX_NO,t.PRD_NAME,t.ORDER_NO,sum(isnull(t.QTY,0)) as BX_QTY,sum(isnull(t.QTY,0))*{0} as QTY ,max(t.BARCODE) as BARCODE,
  t.P2Name,max(isnull(t.P2QTY,0)) as P2QTY,t.P4Name,max(isnull(t.P4QTY,0)) as P4QTY,
  max(isnull(t.P2QTY,0))*{0} as sum_P2QTY,max(isnull(t.P4QTY,0))*{0} as sum_P4QTY
  FROM ZZ_MF_BOXDEF m left join ZZ_TF_BOXDEF t on t.BX_NO=m.BX_NO
  WHERE m.BX_NO ='{1}'
  group by m.BX_NO,t.PRD_NAME,t.ORDER_NO,t.P2Name,t.P4Name,t.PColorName,t.PSizeName";

        private void ExpandPMX()
        {
            sbsql.Length = 0;
            if (curdt2 == null)
            {
                curdt2 = new DataTable();
            }
            curdt2.Clear();
            string sql;
            string bxno;
            int qty;
            foreach (DataRow dr in this.curdt.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    continue;
                }
                //if (dr["BZ_QTY"] == null || dr["BZ_QTY"] == DBNull.Value || dr["BX_NO"] == null || dr["BX_NO"] == DBNull.Value)
                //{
                //    continue;
                //}
                bxno = dr["BX_NO"].ToString();
                qty = 0;
                int.TryParse(dr["BZ_QTY"].ToString(), out qty);
                sql = string.Format(sql_tf_boxdef_group2P, qty, bxno);
                DataTable dtmx = myServer.GetDataTable(sql);
                dtmx.Merge(dr.Table.Clone(), false, MissingSchemaAction.Add);
                FillDataTableByRow(dtmx, dr);
                dtmx.AcceptChanges();
                curdt2.Merge(dtmx, false, MissingSchemaAction.Add);
            }

        }
        private void FillDataTableByRow(DataTable filldt, DataRow dr)
        {
            foreach (DataRow filldr in filldt.Rows)
            {
                foreach (DataColumn col in dr.Table.Columns)
                {
                    filldr[col.ColumnName] = dr[col];
                }
            }
           
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 1)
            {
                ExpandMX();
            }
        }


    }
}
