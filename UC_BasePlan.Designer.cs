namespace SSAPP
{
    partial class UC_BasePlan
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_Style = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_CUS_SO_NO = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.date_BoxBill = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_BoxBNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Cont_Id = new System.Windows.Forms.TextBox();
            this.txt_BAT_NO = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_BAT_NO);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txt_Cont_Id);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txt_Style);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txt_CUS_SO_NO);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.date_BoxBill);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_BoxBNo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(674, 67);
            this.panel1.TabIndex = 0;
            // 
            // txt_Style
            // 
            this.txt_Style.Location = new System.Drawing.Point(517, 32);
            this.txt_Style.Name = "txt_Style";
            this.txt_Style.Size = new System.Drawing.Size(124, 23);
            this.txt_Style.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(474, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 14);
            this.label6.TabIndex = 14;
            this.label6.Text = "款式";
            // 
            // txt_CUS_SO_NO
            // 
            this.txt_CUS_SO_NO.Location = new System.Drawing.Point(517, 5);
            this.txt_CUS_SO_NO.Name = "txt_CUS_SO_NO";
            this.txt_CUS_SO_NO.Size = new System.Drawing.Size(124, 23);
            this.txt_CUS_SO_NO.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(247, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 14);
            this.label3.TabIndex = 12;
            this.label3.Text = "合约号";
            // 
            // date_BoxBill
            // 
            this.date_BoxBill.CustomFormat = "yyyy-MM-dd";
            this.date_BoxBill.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_BoxBill.Location = new System.Drawing.Point(299, 5);
            this.date_BoxBill.Name = "date_BoxBill";
            this.date_BoxBill.Size = new System.Drawing.Size(124, 23);
            this.date_BoxBill.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "单据日期";
            // 
            // txt_BoxBNo
            // 
            this.txt_BoxBNo.Location = new System.Drawing.Point(81, 5);
            this.txt_BoxBNo.Name = "txt_BoxBNo";
            this.txt_BoxBNo.ReadOnly = true;
            this.txt_BoxBNo.Size = new System.Drawing.Size(124, 23);
            this.txt_BoxBNo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "装箱单号";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 67);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(674, 381);
            this.panel2.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(68, 25);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(674, 381);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(666, 348);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "装箱单";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(666, 348);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "装箱明细";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(434, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 14);
            this.label4.TabIndex = 16;
            this.label4.Text = "客户订单号";
            // 
            // txt_Cont_Id
            // 
            this.txt_Cont_Id.Location = new System.Drawing.Point(299, 32);
            this.txt_Cont_Id.Name = "txt_Cont_Id";
            this.txt_Cont_Id.Size = new System.Drawing.Size(124, 23);
            this.txt_Cont_Id.TabIndex = 17;
            // 
            // txt_BAT_NO
            // 
            this.txt_BAT_NO.Location = new System.Drawing.Point(81, 32);
            this.txt_BAT_NO.Name = "txt_BAT_NO";
            this.txt_BAT_NO.Size = new System.Drawing.Size(124, 23);
            this.txt_BAT_NO.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 14);
            this.label5.TabIndex = 18;
            this.label5.Text = "批号";
            // 
            // UC_BoxPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "UC_BoxPlan";
            this.Size = new System.Drawing.Size(674, 448);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txt_BoxBNo;
        private System.Windows.Forms.DateTimePicker date_BoxBill;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Style;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_CUS_SO_NO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txt_Cont_Id;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_BAT_NO;
        private System.Windows.Forms.Label label5;
    }
}
