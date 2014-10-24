using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using AttnObjects;
using WinControls.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using CWData;
using ICSharpCode.Core;

using SSAPP.Objects;
using WinFormControls.Editors;
using WinControls;
using WinControls.Controls.DevGridColumn;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;


namespace SSAPP
{
    public partial class UC_BoxDef : UCEditBase
    {
        IDBServer erpserver;
        IDBServer sserver;

        protected EditGrid dataGrid = new EditGrid();
        protected EditGrid dataGrid1 = new EditGrid();
        protected EditGrid dataGrid2;
        protected string ViewName = "ZZ_TF_BOXDEF";
        protected string ViewName1 = "ZZ_TF_BOXDEF1";
        //protected string EditViewName = "TF_YGWage";
        //protected string TableName = "TF_YGWage";
        MF_BOXDEF mfEntity;
        DataTable curdt;
        DataTable curdt1;
        DataSet curdataset;
        string BillID = "BX";
        public UC_BoxDef()
        {
            InitializeComponent();
            erpserver = ServerFactory.GetServer();
            sserver = ServerFactory.GetServer(ServerType.ThirdDB);

            curdt = BillGlobal.GetEmptyTable("select * from ZZ_TF_BOXDEF");
            curdt1 = BillGlobal.GetEmptyTable("select * from ZZ_TF_BOXDEF1");

            myInit();
            New(null, null);
        }
        private void myInit()
        {
            dataGrid.Dock = DockStyle.Fill;
            dataGrid.Name = "dataGrid1"+ this.GetType().ToString();
            dataGrid.CanEdit = true;
            dataGrid.CanDelete = true;
            dataGrid.CanAddRow = true;
            dataGrid.OnValidatingEditor += dataGrid_OnValidatingEditor;
            dataGrid.OnCreateColumning += dataGrid_OnCreateColumning;
            dataGrid.InitNewRow += dataGrid_InitNewRow;
            dataGrid.OnRowDeleting += dataGrid_OnRowDeleting;
            GridView gridview = dataGrid.MainGridView;
            gridview.CustomRowCellEditForEditing += gridview_CustomRowCellEditForEditing;

            gridview.CellValueChanged += gridview_CellValueChanged;
            this.panel2.Controls.Add(dataGrid);
            dataGrid.SetCaption(ViewName);
            this.comb_BoxType.SelectedIndex = 0;
            date_BoxNo.Value = DateTime.Now;
            dataGrid1.Dock = DockStyle.Fill;
            dataGrid1.Name = "dataGrid2" + this.GetType().ToString();
            dataGrid1.CanEdit = true;
            dataGrid1.CanDelete = true;
            dataGrid1.CanAddRow = true;
            dataGrid1.InitNewRow += dataGrid1_InitNewRow;
            dataGrid1.OnValidatingEditor += dataGrid1_OnValidatingEditor;
            dataGrid1.OnRowDeleting += dataGrid1_OnRowDeleting;
            dataGrid1.SetCaption(ViewName1);
            this.groupBox1.Controls.Add(dataGrid1);

            #region MasterDetail

            //gridview = dataGrid1.MainGridView;
            //gridview.OptionsDetail.AllowZoomDetail = false;
            //gridview.OptionsDetail.AutoZoomDetail = false;
            //gridview.OptionsDetail.ShowDetailTabs = true;
            //gridview.OptionsDetail.EnableMasterViewMode = true;
            //gridview.DetailHeight = 300;


            //dataGrid2 = new EditGrid();
            //dataGrid2.Name = "dataGrid3" + this.GetType().ToString();
            //dataGrid2.CanEdit = false;
            //dataGrid2.CanDelete = false;
            //dataGrid2.CanAddRow = false;
            //dataGrid2.SetCaption(ViewName);
            //GridView gridview1 = dataGrid2.MainGridView;
            //gridview1.GridControl = dataGrid1;
            //gridview1.ViewCaption = "包装明细";
            //gridview1.OptionsMenu.EnableColumnMenu = false;

            //GridLevelNode gridLevelNode1 = new GridLevelNode();
            //gridLevelNode1.LevelTemplate = gridview1;
            //gridLevelNode1.RelationName = "MasterRelation";

            //dataGrid1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            //gridLevelNode1});

            //dataGrid1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {gridview,gridview1});
            #endregion

            this.curdt.TableName = "tfdt";
            this.curdt1.TableName = "tfdt1";
            curdataset = new DataSet();
            curdataset.Tables.Add(this.curdt);
            curdataset.Tables.Add(this.curdt1);

            DataRelation Rel = new DataRelation("MasterRelation", curdt1.Columns["PGuid"], curdt.Columns["PGuid"], false);
            curdt.ParentRelations.Add(Rel);
            dataGrid1.DataMember = "tfdt1";
            dataGrid1.DataSource = curdataset;

        }

        void gridview_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string fieldname = e.Column.FieldName;
            if (fieldname == "PGuid" || fieldname == "P2QTY")
            {
                try
                {
                    DataRow dr = dataGrid.GetFocusedDataRow();
                    string pguid = dr["PGuid"].ToString();
                    DataRow[] rows = this.curdt1.Select("PGuid='" + pguid + "'");
                    if (rows.Length > 0)
                    {
                        sumtoPack(pguid, rows[0]);
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        ///  小包装的删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dataGrid1_OnRowDeleting(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            dataGrid1.DeleteRow(e.RowHandle);
        }
        void dataGrid_OnRowDeleting(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            dataGrid.DeleteRow(e.RowHandle);
        }
        void dataGrid1_OnValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (e.Value == null) return;

            if (dataGrid1.FocusedColumn.FieldName == "PQty")
            {
                DataRow dr = dataGrid1.GetDataRow(dataGrid1.FocusedRowHandle);
                string pguid = dr["PGuid"].ToString();
                decimal bs = 0;
                if (decimal.TryParse(e.Value.ToString(), out bs))
                {
                    decimal p2qty = 0;
                    decimal qty = 0;
                    decimal sumqty = 0;
                    decimal utqty = 0;
                    foreach (DataRow dr1 in this.curdt.Rows)
                    {
                        dr1["P2SumQTY"] = bs;
                        if (dr1["PGuid"].ToString() == pguid)
                        {
                            if (dr1["P2QTY"] != null && decimal.TryParse(dr1["P2QTY"].ToString(), out p2qty))
                            {
                                utqty = utqty + p2qty;
                                qty= p2qty * bs;
                                sumqty = sumqty + qty;
                                dr1["QTY"] = qty;
                            }
                        }
                    }
                    dr["PSumQty"] = sumqty;
                    dr["PUtQty"] = utqty;
                }

               
            }
        }

        const string sql1 = @"select top 1000 (cast(t.CalcMateSlopG_ID as varchar(20))+'_'+cast(t.CalcMateSlopG_Seq as varchar(10))) as ORDERID,t.CalcMateSlopG_ID,m.Cont_Id,m.ContStyle_Id,t.PdtMate_Id,t.PdtMateNm_Nm,t.PdtSpec_Id,t.PdtSpec_NM,t.Color_Id,t.Color_Nm from
 JhCalcMateSlopG01t m left join JhCalcMateSlopG02t t on t.CalcMateSlopG_ID=m.CalcMateSlopG_ID where 1=1 ";
        const string sql5=@"select top 1000 m.Cont_Id,m.ContStyle_Id,t.PdtMate_Id,t.PdtMateNm_Nm,t.PdtSpec_Id,t.PdtSpec_NM,t.Color_Id,t.Color_Nm from
 JhCalcMateSlopG01t m left join JhCalcMateSlopG02t t on t.CalcMateSlopG_ID=m.CalcMateSlopG_ID
where t.CalcMateSlopG_ID='{0}' and t.CalcMateSlopG_Seq='{1}'";

        void dataGrid_OnValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (e.Value == null) return;

            string fieldname=dataGrid.FocusedColumn.FieldName ;
            if (fieldname== "ORDERID")
            {
                if (e.Value == null) return;
                DataRow dr = dataGrid.GetDataRow(dataGrid.FocusedRowHandle);
                //string ORDERID = dr["ORDERID"].ToString();

                string[] aStr = e.Value.ToString().Split('_');
                
                if (aStr.Length == 2)
                {
                    dr["ORDER_NO"] = aStr[0];
                    dr["EST_ITM"] = aStr[1];
                }
                string sql = string.Format(sql5, aStr[0], aStr[1]);
                DataTable dt = sserver.GetDataTable(sql);
                if (dt.Rows.Count == 1)
                {
                    if (this.txt_Cont_Id.Text.Length == 0)
                    {
                        this.txt_Cont_Id.Text = dt.Rows[0]["Cont_Id"].ToString();
                    }
                    dr["Cont_Id"] = dt.Rows[0]["Cont_Id"];
                    dr["Style_Id"] = dt.Rows[0]["ContStyle_Id"];
                    dr["PRD_NO"] = dt.Rows[0]["PdtMate_Id"];

                    dr["PRD_NAME"] = dt.Rows[0]["PdtMateNm_Nm"];
                    dr["Color_ID"] = dt.Rows[0]["Color_ID"];
                    dr["Color_Name"] = dt.Rows[0]["Color_NM"];
                    dr["Size_Id"] = dt.Rows[0]["PdtSpec_Id"];
                    dr["Size_Name"] = dt.Rows[0]["PdtSpec_NM"];
                    dr["PColorName"] = dt.Rows[0]["Color_NM"];
                    dr["PSizeName"] = dt.Rows[0]["PdtSpec_NM"];
                }
            }
            else if (fieldname == "PGuid" || fieldname == "P2QTY")
            {
                DataRow dr = dataGrid.GetFocusedDataRow();
                string pguid = string.Empty;
                decimal p2qty = 0;
                if (fieldname == "PGuid")
                {
                    pguid = e.Value.ToString();
                    if (!decimal.TryParse(dr["P2QTY"].ToString(), out p2qty))
                    {
                        return;
                    }
                }
                else
                {
                    if (!decimal.TryParse(e.Value.ToString(), out p2qty))
                    {
                        return;
                    }
                    pguid = dr["PGuid"].ToString();
                }
                DataRow[] rows = this.curdt1.Select("PGuid='" + pguid + "'");
                decimal bs = 0;
                decimal utqty = 0;
                if (rows.Length > 0)
                {
                    dr["P2SumQTY"] = rows[0]["PQty"];
                    if (!decimal.TryParse(rows[0]["PQty"].ToString(), out bs))
                    {
                        return;
                    }

                    dr["QTY"] = bs * p2qty;
                }               
            }
        }
        private void sumtoPack(string pguid, DataRow packRow)
        {
            DataRow[] rows = this.curdt.Select("PGuid='" + pguid + "'");
            decimal qty = 0;
            decimal sumqty = 0;
            decimal sum_qty, sum_sumqty;
            sum_qty = sum_sumqty = 0;
            try
            {
                foreach (DataRow dr in rows)
                {
                    decimal.TryParse(dr["P2QTY"].ToString(), out qty);
                    decimal.TryParse(dr["QTY"].ToString(), out sumqty);
                    sum_qty += qty;
                    sum_sumqty += sumqty;
                }
                packRow["PUtQty"] = sum_qty;
                packRow["PSumQty"] = sum_sumqty;
            }
            catch
            { 
            }
        }


        void dataGrid_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            dataGrid.SetRowValue(e.RowHandle, "QTY", 1);
            dataGrid.SetRowValue(e.RowHandle, "BX_NO", this.txt_BoxNo.Text);
            //dataGrid.SetRowValue(e.RowHandle, "P2Name", "2P");
            //dataGrid.SetRowValue(e.RowHandle, "P4Name", "4P");
            //dataGrid.SetRowValue(e.RowHandle, "P2QTY", 2);
            //dataGrid.SetRowValue(e.RowHandle, "P4QTY", 4);
            DataRow dr= dataGrid1.GetFocusedDataRow();
            if (dr != null)
            {
                dataGrid.SetRowValue(e.RowHandle, "PGuid", dr["PGuid"]);
            }
        }
        void dataGrid1_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            dataGrid1.SetRowValue(e.RowHandle, "BX_NO", this.txt_BoxNo.Text);
            dataGrid1.SetRowValue(e.RowHandle, "PGuid", BillGlobal.GetGuidString());
            if (dataGrid1.FocusedColumn.FieldName != "PName")
            {
                dataGrid1.SetRowValue(e.RowHandle, "PName", "2P");
            }
        }
        StringBuilder sbsql = new StringBuilder();
        void gridview_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "ORDERID")
            {
                sbsql.Length = 0;
                sbsql.Append(sql1);

                if (txt_Cont_Id.Text.Length > 0)
                {
                    sbsql.Append(" and m.Cont_Id like '%");
                    sbsql.Append(txt_Cont_Id.Text);
                    sbsql.Append("%' ");
                }
                if (txt_Style.Text.Length > 0)
                {
                    sbsql.Append(" and m.ContStyle_Id like '%");
                    sbsql.Append(txt_Style.Text);
                    sbsql.Append("%' ");
                }
                string sql = sbsql.ToString();                
                //Repository_PRD.View.ActiveFilterString = sbsql.ToString();
                Repository_PRD.DataSource = sserver.GetDataTable(sql);
                e.RepositoryItem = Repository_PRD;

            }
            else if (e.Column.FieldName== "PGuid")
            {
                Repository_PGuid.DataSource = this.curdt1;
            }
        }
       
        RepositoryItemGridLookUpEdit Repository_PRD;
        RepositoryItemGridLookUpEdit Repository_PGuid;
        DevGridColButtonEdit colprdno;
        void dataGrid_OnCreateColumning(ZZ_TAB_VIEW dct, out DevExpress.XtraEditors.Repository.RepositoryItem RepositoryItem)
        {
            RepositoryItem = null;

            if (dct.FldName == "ORDERID")
            {
                Repository_PRD = DevColumnHelper.GetLookupItem(dct);
                Repository_PRD.NullText = "";
                Repository_PRD.ValueMember = "ORDERID";
                Repository_PRD.DisplayMember = "ORDERID";

                DevColumnHelper.SetLookupItemCaption(Repository_PRD, "MateSlopG01t");
                Repository_PRD.PopupFormSize = new System.Drawing.Size(500, 300);
                //DevExpress.XtraGrid.Columns.GridColumn col;
                //col = new DevExpress.XtraGrid.Columns.GridColumn();
                //col.Caption = "ID";
                //col.FieldName = "ID";
                //col.Name = "cloID";
                //col.Visible = true;
                //col.VisibleIndex = 1;
                //col.Width = 20;

                //Repository_PRD.View.Columns.Add(

                //RepositoryItem = Repository_PRD;
                //Repository_PRD.DataSource
            }
            else if (dct.FldName == "PGuid")
            {
                Repository_PGuid = DevColumnHelper.GetLookupItem(dct);
                Repository_PGuid.NullText = "";
                Repository_PGuid.ValueMember = "PGuid";
                Repository_PGuid.DisplayMember = "PName";
                DevExpress.XtraGrid.Columns.GridColumn col;
                col = new DevExpress.XtraGrid.Columns.GridColumn();
                col.Caption = "小包装名称";
                col.FieldName = "PName";
                col.Name = "PName";
                col.Visible = true;
                col.VisibleIndex = 1;
                col.Width = 100;

                Repository_PGuid.View.Columns.Add(col);

                Repository_PGuid.DataSource = this.curdt1;

                RepositoryItem = Repository_PGuid;

            }
            //else if (dct.FldName == "PRD_NO")
            //{

            //    colprdno = new DevGridColButtonEdit(dct);
            //    //colprdno.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            //    colprdno.ButtonClick += rpItemBtn_ButtonClick;
            //    //colprdno.ReadOnly = true;

            //    RepositoryItem = colprdno;
            //}
        }

        Sc_Prd prdsc;
        void rpItemBtn_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            //MessageService.ShowMessage(sender.ToString());
            sbsql.Length = 0;
            sbsql.Append(sql1);

            if (txt_Cont_Id.Text.Length > 0)
            {
                sbsql.Append(" and m.Cont_Id like '%");
                sbsql.Append(txt_Cont_Id.Text);
                sbsql.Append("%' ");
            }
            if (txt_Style.Text.Length > 0)
            {
                sbsql.Append(" and m.ContStyle_Id like '%");
                sbsql.Append(txt_Style.Text);
                sbsql.Append("%' ");
            }
            string sql = sbsql.ToString();

            DataTable prdt = sserver.GetDataTable(sql);
            ButtonEdit btn = sender as ButtonEdit;

            
            prdsc = new Sc_Prd(ref prdt, btn.Text);
            prdsc.MultiSelect = true;

            if (prdsc.ShowDialog() == DialogResult.OK)
            {
                DataRow[] rows = prdsc.GetSelectRows();
            }

        }


        public override void New(object sender, EventArgs e)
        {
            isNew = true;
            this.txt_BoxNo.Text = BillGlobal.GetNo(BillID, true);
            this.curdt.Clear();
            this.curdt1.Clear();
            dataGrid.DataSource = curdt;
            dataGrid1.DataSource = curdt1;
        }
        public override bool Save(object sender, EventArgs e)
        {
            if (!Valid()) return false;
            using (WaitDialog fmwd = new WaitDialog())
            {
                dataGrid.CloseEditor();
                if (isNew)
                {
                    mfEntity = new MF_BOXDEF();
                    GetDataFromFace(mfEntity);
                    mfEntity.BX_NO = this.txt_BoxNo.Text = BillGlobal.GetNo(BillID, false);
                    mfEntity.USR = AttnObjects.PSWD.LoginUser.USR;
                    mfEntity.Insert();
                }
                else
                {
                    GetDataFromFace(mfEntity);
                    mfEntity.SaveChanges();
                }

                TF_BOXDEF tfEntity = new TF_BOXDEF();
                foreach (DataRow dr in curdt.Rows)
                {
                    if (dr.RowState == DataRowState.Deleted)
                    {
                        dr.RejectChanges();
                        tfEntity.Populate(dr);
                        tfEntity.Delete();
                        //myServer.ExeStroeProcedure(tfsql_delete, tfEntity);
                        //myServer.ExecuteSQL(tfsql_delete, tfEntity);
                        dr.Delete();
                        continue;
                    }
                    tfEntity.Populate(dr);
                    tfEntity.BX_NO = mfEntity.BX_NO;

                    if (dr.RowState == DataRowState.Added || dr.RowState == DataRowState.Detached)
                    {
                        tfEntity.Insert();
                    }
                    else if (dr.RowState == DataRowState.Modified)
                    {
                        tfEntity.SaveChanges();
                    }

                }
                DbParams dbp = new DbParams();
                foreach (DataRow dr in curdt1.Rows)
                {
                    dbp.Clear();
                    if (dr.RowState == DataRowState.Deleted)
                    {
                        dr.RejectChanges();
                        dbp.Populate(dr);
                        myServer.ExeStroeProcedure("sp_ZZ_TF_BOXDEF1_Delete", dbp);
                        dr.Delete();
                        continue;
                    }
                    dbp.Populate(dr);

                    if (dr.RowState == DataRowState.Added || dr.RowState == DataRowState.Detached)
                    {
                        myServer.ExeStroeProcedure("sp_ZZ_TF_BOXDEF1_Insert", dbp);

                    }
                    else if (dr.RowState == DataRowState.Modified)
                    {
                        myServer.ExeStroeProcedure("sp_ZZ_TF_BOXDEF1_Update", dbp);
                    }
                }
                isNew = false;
                curdt.AcceptChanges();
                curdt1.AcceptChanges();
            }
            return true;
        }
        private void GetDataFromFace(MF_BOXDEF mfEntity)
        {
            mfEntity.Style_Id = this.txt_Style.Text;
            mfEntity.BXNAME = this.txt_BoxName.Text;
            mfEntity.BX_Type = this.comb_BoxType.SelectedIndex;
            mfEntity["BX_DD"] =BillGlobal.GetDateString(this.date_BoxNo.Value);
            mfEntity.Cont_Id = this.txt_Cont_Id.Text;

            decimal l, w, h,v,n;
            l = w = h = v = n = 0;
            if (decimal.TryParse(txt_Length.Text, out l))
            {
                mfEntity.BX_LENGTH = l;
            }
            if (decimal.TryParse(this.txt_Width.Text, out w))
            {
                mfEntity.BX_WIDTH = w;
            }
            if (decimal.TryParse(this.txt_Heigh.Text, out h))
            {
                mfEntity.BX_HEIGH = h;
            }
            if (decimal.TryParse(this.txt_Volume.Text, out v))
            {
                mfEntity.VOLUME = v;
            }
            if (decimal.TryParse(this.txt_BxNet.Text, out n))
            {
                mfEntity.BX_NET = n;
            }

        }
        Fm_ScBillOD fmSc = null;
        public override void Find(object sender, EventArgs e)
        {
            if (fmSc == null)
            { 
                fmSc = new Fm_ScBillOD();
                fmSc.SQL = "select LAW.BX_NO as '单号',convert(varchar(10),LAW.BX_DD,20) as '日期',BXNAME as 包装名称 from ZZ_MF_BOXDEF LAW where (convert(varchar(10),LAW.BX_DD,20)>='{0}' and convert(varchar(10),LAW.BX_DD,20)<='{1}') ";
            }
            else
            {
                fmSc.RefreshData();
            }

            if (fmSc.ShowDialog() == DialogResult.OK)
            {
                this.txt_BoxNo.Text = fmSc.GetSelectRow()["单号"].ToString();
                LoadData(this.txt_BoxNo.Text);
            }
        }
        const string sql2 = " select * from zz_tf_boxdef where BX_NO='{0}' ";
        const string sql3 = " select * from zz_tf_boxdef1 where BX_NO='{0}' ";
        private void LoadData(string boxno)
        {
            mfEntity = MF_BOXDEF.GetById<MF_BOXDEF>(boxno);
            if (mfEntity.BX_NO.Length > 0)
            {
               
                this.txt_BoxNo.Text = mfEntity.BX_NO;
                this.txt_Style.Text = mfEntity.Style_Id;
                this.txt_BoxName.Text = mfEntity.BXNAME;
                this.comb_BoxType.SelectedIndex = mfEntity.BX_Type;
                this.date_BoxNo.Value = mfEntity.BX_DD;
                this.txt_Cont_Id.Text = mfEntity.Cont_Id;
                txt_Length.Text = mfEntity.BX_LENGTH.ToString();
                this.txt_Width.Text = mfEntity.BX_WIDTH.ToString();
                this.txt_Heigh.Text = mfEntity.BX_HEIGH.ToString();
                this.txt_Volume.Text = mfEntity.VOLUME.ToString();
                this.txt_BxNet.Text = mfEntity.BX_NET.ToString();

                string sql = string.Format(sql2, mfEntity.BX_NO);
                DataTable dt = myServer.GetDataTable(sql);
                this.curdt.Clear();
                this.curdt.Merge(dt, false);

                sql = string.Format(sql3, mfEntity.BX_NO);
                dt = myServer.GetDataTable(sql);
                this.curdt1.Clear();
                this.curdt1.Merge(dt);

                this.dataGrid.DataSource = this.curdt;
                this.dataGrid1.DataSource = this.curdt1;
                isNew = false;

                //this.curdt1.Merge();
            }
            else
            {
                MessageService.ShowWarning("未找到此单据！");
            }
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
        private bool Valid()
        {
            //if (this.txt_CUS_SO_NO.Text.Length == 0)
            //{
            //    MessageService.ShowWarning("请输入合约号！");
            //    this.txt_CUS_SO_NO.Focus();
            //    return false;
            //}
            if (curdt.Rows.Count <= 0)
            {
                MessageService.ShowWarning("请输入明细数据！");
                return false;
            }
            return true;
        }

        private void txt_Length_Validating(object sender, CancelEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox == null || txtBox.Text.Length==0) return;
            decimal l, w, h;
            
            if (!decimal.TryParse(txtBox.Text, out l))
            {
                e.Cancel = true;
                return;
            }
            l = w = h = 0;
            bool isValid = true;
            //if (txtBox.Name != "txt_Volume")
            //{
            isValid &= decimal.TryParse(txt_Length.Text, out l);
            isValid &= decimal.TryParse(txt_Width.Text, out w);
            isValid &= decimal.TryParse(this.txt_Heigh.Text, out h);
            if (isValid)
            {
                txt_Volume.Text = (l * w * h).ToString();
            }                
            //}

        }
        string sql_1 = @"  select top 1000 p.MATE_NM as '名称',p.MATE_SPEC as '规格' from  JhCalcMateSlopG01t m left join JhCalcMateSlopG03t t on t.CalcMateSlopG_Id=m.CalcMateSlopG_Id
  left join Gymateat p on p.MATE_ID=t.MATE_ID where p.MATE_NM like '%箱%' and m.Cont_Id like '%{0}%' and m.ContStyle_Id like '%{1}%'";

        Sc_Prd fmSc_xiang;
        private void txt_BoxName_ButtonClick(object sender, EventArgs e)
        {
            WTextBoxButton txtbtn = sender as WTextBoxButton;
            string sql =string.Format(sql_1,this.txt_Cont_Id.Text,this.txt_Style.Text);
            DataTable biltypedt = sserver.GetDataTable(sql);

            if (fmSc_xiang == null)
            {
                fmSc_xiang = new Sc_Prd(ref biltypedt);
                fmSc_xiang.MultiSelect = false;
            }
            if (txtbtn.Text.Length > 0)
            {
                fmSc_xiang.FindStr(txtbtn.Text);
            }
            if (fmSc_xiang.ShowDialog() == DialogResult.OK)
            {
                txtbtn.Text = fmSc_xiang.GetSelectRow()[0].ToString();
                string spc = fmSc_xiang.GetSelectRow()[1].ToString();
                string []str1=spc.Split('*');
                int ip;
                for (int i = 0; i < str1.Length; i++)
                {
                    ip = 0;
                    switch (i)
                    {
                        case 0:
                            if (txt_Length.Text.Length == 0)
                            {
                                if (int.TryParse(str1[i], out ip))
                                {
                                    txt_Length.Text = ip.ToString();
                                }
                            }
                            break;
                        case 1:
                            if (this.txt_Width.Text.Length == 0)
                            {
                                if (int.TryParse(str1[i], out ip))
                                {
                                    txt_Width.Text = ip.ToString();
                                }
                            }
                            break;
                        case 2:
                            if (this.txt_Heigh.Text.Length == 0)
                            {
                                if (int.TryParse(str1[i], out ip))
                                {
                                    txt_Heigh.Text = ip.ToString();
                                }
                            }
                            break;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FmEdit fm = new FmEdit();
            fm.ShowDialog();

        }

    }
}
