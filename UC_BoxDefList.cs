using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using WinControls.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;

namespace SSAPP
{
    public partial class UC_BoxDefList : UserControl
    {
        protected EditGrid dataGrid = new EditGrid();
        //protected EditGrid dataGrid1 = new EditGrid();
        protected string ViewName = "ZZ_TF_BOXDEF";
        //protected string ViewName1 = "V_ZZ_TF_BOXDEF";
        GridView MainView;
        public UC_BoxDefList()
        {
            InitializeComponent();
            myInit();
        }
        public event EventHandler DoubleClick
        {
            add
            {
                MainView.DoubleClick += value;
            }
            remove
            {
                MainView.DoubleClick -= value;
            }
        }
        private void myInit()
        {
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");

            dataGrid.Dock = DockStyle.Fill;
            //dataGrid.NewItemRowPosition = NewItemRowPosition.None;
            dataGrid.Name = "dataGrid1" + this.GetType().ToString();
            dataGrid.CanEdit = false;
            dataGrid.CanDelete = false;
            dataGrid.NewItemRowPosition = NewItemRowPosition.None;
            dataGrid.IsShowFooter = false;
            dataGrid.IsShowCustomMenu = false;
            dataGrid.DataMember = "dt_mf";
            ////dataGrid.IsShowCustomMenu = false;
            //dataGrid.InitNewRow += dataGrid_InitNewRow;
            //dataGrid.OnValidatingEditor += dataGrid_OnValidatingEditor;
            

            GridView gridview = dataGrid.MainGridView;
            MainView = gridview;

            gridview.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gridview.ChildGridLevelName = "MasterRelation";
            gridview.DetailHeight = 300;
            gridview.OptionsView.ShowChildrenInGroupPanel = true;
            gridview.ViewCaption = "dt_mf";
            gridview.OptionsDetail.AllowZoomDetail = false;
            gridview.OptionsDetail.AutoZoomDetail = false;
            gridview.OptionsDetail.ShowDetailTabs = true;
            gridview.OptionsDetail.EnableMasterViewMode = true;
            gridview.OptionsSelection.EnableAppearanceFocusedCell = false;

            gridview.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            //gridview.OptionsSelection.InvertSelection = false;

            dataGrid.SetCaption("select_Box");

            GridColumn col;

            RepositoryItemImageComboBox repositoryItemImageComboBox3=new RepositoryItemImageComboBox ();
            // 
            // repositoryItemImageComboBox3
            // 
            repositoryItemImageComboBox3.AutoHeight = false;
            
            repositoryItemImageComboBox3.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            repositoryItemImageComboBox3.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem( 0, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem(1, 1)});
            repositoryItemImageComboBox3.Name = "repositoryItemImageComboBox3";
            repositoryItemImageComboBox3.SmallImages = this.imageList1;

            col = new GridColumn();
            col.Caption = " ";
            col.FieldName = "ImageIndex";
            col.VisibleIndex = 0;
            col.ColumnEdit = repositoryItemImageComboBox3;
            col.OptionsColumn.AllowEdit = false;
            col.OptionsColumn.AllowSize = false;
            col.OptionsColumn.FixedWidth = true;
            col.OptionsColumn.ShowCaption = false;
            col.OptionsColumn.AllowMove = false;
            col.ImageIndex = 0;
            col.Width = 60;
            col.OptionsColumn.AllowSort =  DevExpress.Utils.DefaultBoolean.False;
            gridview.Columns.Insert(0,col);

            
            //col = new GridColumn();
            //col.Caption = "装箱号";
            //col.FieldName = "BX_NO";
            //col.VisibleIndex = gridview.Columns.Count;
            //gridview.Columns.Add(col);

            //col = new GridColumn();
            //col.Caption = "包装名称";
            //col.FieldName = "BXNAME";
            //col.VisibleIndex = gridview.Columns.Count;
            //gridview.Columns.Add(col);

            //col = new GridColumn();
            //col.Caption = "合约号";
            //col.FieldName = "Cont_Id";
            //col.VisibleIndex = gridview.Columns.Count;
            //gridview.Columns.Add(col);


            myGridView gridview1 = new myGridView();
            gridview1.Name = "gridview1";
            gridview1.CanEdit = false;
            gridview1.CanAddRow = false;
            

            gridview1.SetCaption("select_Prd");
            gridview1.GridControl = dataGrid;
            gridview1.ViewCaption = "明细";
            //gridview1.ChildGridLevelName = "";


            //gridview1
            GridLevelNode gridLevelNode1 = new GridLevelNode();
            gridLevelNode1.LevelTemplate = gridview1;
            gridLevelNode1.RelationName = "MasterRelation";
            
            dataGrid.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});

            dataGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {gridview,
        gridview1});

            this.Controls.Add(dataGrid);

            col = new GridColumn();
            col.Caption = " ";
            col.FieldName = "ImageIndex";
            col.VisibleIndex = 0;
            col.ColumnEdit = repositoryItemImageComboBox3;
            col.OptionsColumn.AllowEdit = false;
            col.OptionsColumn.AllowSize = false;
            col.OptionsColumn.FixedWidth = true;
            col.OptionsColumn.ShowCaption = false;
            col.OptionsColumn.AllowMove = false;
            col.ImageIndex = 0;
            col.Width = 20;
            col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            gridview1.Columns.Insert(0,col);

        }
        public DataSet DataSource
        {
            get
            {
                return (DataSet)dataGrid.DataSource;
            }
            set
            {
                dataGrid.DataSource = value;
            }
        }
        /// <summary>
        /// 获取多选的行
        /// </summary>
        /// <returns></returns>
        public DataRow[] GetSelectRows()
        {
            if (this.MainView.SelectedRowsCount <= 0)
            {
                return null;
            }
            int[] rowshd = this.MainView.GetSelectedRows();

            DataRow[] rows = new System.Data.DataRow[MainView.SelectedRowsCount];
            int j = 0;
            foreach (int i in rowshd)
            {
                rows[j++] = (this.MainView.GetDataRow(i));
            }
            return rows;
        }
        public void Expend(bool isall)
        {
            if (null == MainView) return;

            if (isall)
            {
                MainView.BeginInit();
                for (int r = 0; r < MainView.DataRowCount; r++)
                {
                    MainView.ExpandMasterRow(r);
                }
                MainView.EndInit();
            }
            else 
            {
                MainView.CollapseAllDetails();
            }
        }
    }
}
