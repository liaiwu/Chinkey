namespace SSAPP
{
    partial class UC_ImportOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_ImportOrder));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_hy = new System.Windows.Forms.TextBox();
            this.txt_Ks = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_cus = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.wDateTimePicker2 = new WinFormControls.Editors.WDateTimePicker();
            this.wDateTimePicker1 = new WinFormControls.Editors.WDateTimePicker();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "起止时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(203, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "-";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(543, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(171, 29);
            this.button1.TabIndex = 4;
            this.button1.Text = "执行导入";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(354, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "合约号";
            // 
            // txt_hy
            // 
            this.txt_hy.Location = new System.Drawing.Point(409, 35);
            this.txt_hy.Name = "txt_hy";
            this.txt_hy.Size = new System.Drawing.Size(120, 23);
            this.txt_hy.TabIndex = 6;
            // 
            // txt_Ks
            // 
            this.txt_Ks.Location = new System.Drawing.Point(595, 0);
            this.txt_Ks.Name = "txt_Ks";
            this.txt_Ks.Size = new System.Drawing.Size(119, 23);
            this.txt_Ks.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(540, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "款式号";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 14);
            this.label5.TabIndex = 9;
            this.label5.Text = "客户号";
            // 
            // txt_cus
            // 
            this.txt_cus.Location = new System.Drawing.Point(77, 35);
            this.txt_cus.Name = "txt_cus";
            this.txt_cus.Size = new System.Drawing.Size(266, 23);
            this.txt_cus.TabIndex = 10;
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 14;
            this.listBox1.Location = new System.Drawing.Point(3, 92);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(834, 228);
            this.listBox1.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 14);
            this.label6.TabIndex = 12;
            this.label6.Text = "状态";
            // 
            // wDateTimePicker2
            // 
            this.wDateTimePicker2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.wDateTimePicker2.DateTime = new System.DateTime(2014, 7, 17, 0, 0, 0, 0);
            this.wDateTimePicker2.DbValue = ((object)(resources.GetObject("wDateTimePicker2.DbValue")));
            this.wDateTimePicker2.FiledName = "";
            this.wDateTimePicker2.Location = new System.Drawing.Point(223, 6);
            this.wDateTimePicker2.Name = "wDateTimePicker2";
            this.wDateTimePicker2.Size = new System.Drawing.Size(120, 23);
            this.wDateTimePicker2.TabIndex = 14;
            this.wDateTimePicker2.TableName = "";
            // 
            // wDateTimePicker1
            // 
            this.wDateTimePicker1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.wDateTimePicker1.DateTime = new System.DateTime(2014, 7, 17, 0, 0, 0, 0);
            this.wDateTimePicker1.DbValue = ((object)(resources.GetObject("wDateTimePicker1.DbValue")));
            this.wDateTimePicker1.FiledName = "";
            this.wDateTimePicker1.Location = new System.Drawing.Point(77, 6);
            this.wDateTimePicker1.Name = "wDateTimePicker1";
            this.wDateTimePicker1.Size = new System.Drawing.Size(120, 23);
            this.wDateTimePicker1.TabIndex = 13;
            this.wDateTimePicker1.TableName = "";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(409, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(119, 23);
            this.textBox1.TabIndex = 16;
            this.textBox1.Text = "122710210002";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(354, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 14);
            this.label7.TabIndex = 15;
            this.label7.Text = "跟单号";
            // 
            // UC_ImportOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.wDateTimePicker2);
            this.Controls.Add(this.wDateTimePicker1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.txt_cus);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_Ks);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_hy);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "UC_ImportOrder";
            this.Size = new System.Drawing.Size(840, 323);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_hy;
        private System.Windows.Forms.TextBox txt_Ks;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_cus;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label6;
        private WinFormControls.Editors.WDateTimePicker wDateTimePicker1;
        private WinFormControls.Editors.WDateTimePicker wDateTimePicker2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
    }
}
