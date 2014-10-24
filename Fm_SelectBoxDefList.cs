using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AttnObjects;
using CWData;

namespace SSAPP
{
    public partial class Fm_SelectBoxDefList : Form
    {
        UC_BoxDefList boxdeflist;
        IDBServer myServer;
        bool ExpendAll = true;
        public event EventHandler OnSelect;

        public Fm_SelectBoxDefList()
        {
            InitializeComponent();
            myServer = ServerFactory.GetServer();

            boxdeflist = new UC_BoxDefList();
            boxdeflist.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(boxdeflist);

            //boxdeflist.DoubleClick += boxdeflist_DoubleClick;
        }

        void boxdeflist_DoubleClick(object sender, EventArgs e)
        {
            if (OnSelect != null)
            {
                OnSelect(this, e);
            }
        }
        public string Cont_Id
        {
            set
            {
                this.textBox1.Text = value;
            }
        }
        public string Style_Id
        {
            set
            {
                this.textBox2.Text = value;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            //this.Close();
        }
        const string sql_select = "select 0 as ImageIndex, * from ZZ_MF_BOXDEF where 1=1 ";
        public void LoadData()
        {
            StringBuilder sqlcondtion = new StringBuilder();
            if (this.textBox1.Text.Length > 0)
            {
                sqlcondtion.Append(" and ");
                sqlcondtion.Append(string.Format(" Cont_Id like '%{0}%' ", this.textBox1.Text));

            }
            if (this.textBox2.Text.Length > 0)
            {
                sqlcondtion.Append(" and ");
                sqlcondtion.Append(string.Format(" Style_Id like '%{0}%' ", this.textBox2.Text));

            }
            string sql=string.Empty;
            if(sqlcondtion.Length>0)
            {
             sql= sql_select + sqlcondtion.ToString();
            }
            else
            {
                sql = "select top 300 0 as ImageIndex,* from ZZ_MF_BOXDEF order by BX_NO DESC ";
            }
            DataSet ds = myServer.GetDataSet(sql);
            DataTable dt1 = ds.Tables[0];
            dt1.TableName = "dt_mf";
            string sql_tf = "select 1 as ImageIndex,* from  ZZ_TF_BOXDEF where BX_NO='{0}'";
            DataTable dt2 = BillGlobal.GetEmptyTable(sql_tf);
            dt2.TableName="dt_tf";
            foreach (DataRow dr in dt1.Rows)
            {
                sql = string.Format(sql_tf, dr["BX_NO"].ToString());
                DataTable dttf = myServer.GetDataTable(sql);
                foreach(DataRow dr1 in dttf.Rows)
                {
                    DataRow newdr=dt2.NewRow();
                    newdr.ItemArray = dr1.ItemArray;
                    dt2.Rows.Add(newdr);
                }
            }
            ds.Tables.Add(dt2);

            DataRelation Rel = new DataRelation("MasterRelation", dt1.Columns["BX_NO"], dt2.Columns["BX_NO"], false);
            dt2.ParentRelations.Add(Rel);

            boxdeflist.DataSource = ds;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ExpendAll
            this.button2.Enabled = false;

            try
            {
                boxdeflist.Expend(ExpendAll);
                ExpendAll = !ExpendAll;
                if (ExpendAll)
                {
                    this.button2.Text = "全部展开";
                }
                else
                {
                    this.button2.Text = "全部收起";
                }
            }
            finally
            {
                this.button2.Enabled = true;
            }
        }
        public DataRow[] GetSelectRows()
        {
            return boxdeflist.GetSelectRows();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (OnSelect != null)
            {
                OnSelect(this, e);
            }
            this.Hide();
            //Close();
        }
    }
}
