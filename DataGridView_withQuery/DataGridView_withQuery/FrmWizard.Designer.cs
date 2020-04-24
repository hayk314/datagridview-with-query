namespace DataGridView_withQuery
{
    partial class FrmWizard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label5 = new System.Windows.Forms.Label();
            this.radioBtnNo = new System.Windows.Forms.RadioButton();
            this.radioBtnYes = new System.Windows.Forms.RadioButton();
            this.TxtSearchVal2 = new System.Windows.Forms.TextBox();
            this.TxtSearchVal1 = new System.Windows.Forms.TextBox();
            this.Label_SearchValue2 = new System.Windows.Forms.Label();
            this.Label_SearchValue1 = new System.Windows.Forms.Label();
            this.TxtSearchQuery = new System.Windows.Forms.TextBox();
            this.Panel_Step2 = new System.Windows.Forms.Panel();
            this.Panel_Step1 = new System.Windows.Forms.Panel();
            this.ComboSearchCondition = new System.Windows.Forms.ComboBox();
            this.ComboDataType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.radioBtnNot = new System.Windows.Forms.RadioButton();
            this.radioBtnOr = new System.Windows.Forms.RadioButton();
            this.radioBtnAnd = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.Panel_Step0 = new System.Windows.Forms.Panel();
            this.ListGridColumns = new System.Windows.Forms.ListBox();
            this.BtnBack = new System.Windows.Forms.Button();
            this.BtnNext = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.Panel_Step2.SuspendLayout();
            this.Panel_Step1.SuspendLayout();
            this.Panel_Step0.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 353);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 17);
            this.label5.TabIndex = 104;
            this.label5.Text = "Query";
            // 
            // radioBtnNo
            // 
            this.radioBtnNo.AutoSize = true;
            this.radioBtnNo.Location = new System.Drawing.Point(277, 86);
            this.radioBtnNo.Name = "radioBtnNo";
            this.radioBtnNo.Size = new System.Drawing.Size(50, 21);
            this.radioBtnNo.TabIndex = 95;
            this.radioBtnNo.Text = "NO";
            this.radioBtnNo.UseVisualStyleBackColor = true;
            this.radioBtnNo.CheckedChanged += new System.EventHandler(this.radioBtnNo_CheckedChanged);
            // 
            // radioBtnYes
            // 
            this.radioBtnYes.AutoSize = true;
            this.radioBtnYes.Checked = true;
            this.radioBtnYes.Location = new System.Drawing.Point(203, 86);
            this.radioBtnYes.Name = "radioBtnYes";
            this.radioBtnYes.Size = new System.Drawing.Size(56, 21);
            this.radioBtnYes.TabIndex = 94;
            this.radioBtnYes.TabStop = true;
            this.radioBtnYes.Text = "YES";
            this.radioBtnYes.UseVisualStyleBackColor = true;
            this.radioBtnYes.CheckedChanged += new System.EventHandler(this.radioBtnYes_CheckedChanged);
            // 
            // TxtSearchVal2
            // 
            this.TxtSearchVal2.Location = new System.Drawing.Point(203, 49);
            this.TxtSearchVal2.Name = "TxtSearchVal2";
            this.TxtSearchVal2.Size = new System.Drawing.Size(227, 22);
            this.TxtSearchVal2.TabIndex = 3;
            this.TxtSearchVal2.TextChanged += new System.EventHandler(this.TxtSearchVal2_TextChanged);
            // 
            // TxtSearchVal1
            // 
            this.TxtSearchVal1.Location = new System.Drawing.Point(203, 16);
            this.TxtSearchVal1.Name = "TxtSearchVal1";
            this.TxtSearchVal1.Size = new System.Drawing.Size(227, 22);
            this.TxtSearchVal1.TabIndex = 2;
            this.TxtSearchVal1.TextChanged += new System.EventHandler(this.TxtSearchVal1_TextChanged);
            // 
            // Label_SearchValue2
            // 
            this.Label_SearchValue2.AutoSize = true;
            this.Label_SearchValue2.Location = new System.Drawing.Point(15, 49);
            this.Label_SearchValue2.Name = "Label_SearchValue2";
            this.Label_SearchValue2.Size = new System.Drawing.Size(141, 17);
            this.Label_SearchValue2.TabIndex = 1;
            this.Label_SearchValue2.Text = "Search second value";
            // 
            // Label_SearchValue1
            // 
            this.Label_SearchValue1.AutoSize = true;
            this.Label_SearchValue1.Location = new System.Drawing.Point(15, 16);
            this.Label_SearchValue1.Name = "Label_SearchValue1";
            this.Label_SearchValue1.Size = new System.Drawing.Size(91, 17);
            this.Label_SearchValue1.TabIndex = 0;
            this.Label_SearchValue1.Text = "Search value";
            // 
            // TxtSearchQuery
            // 
            this.TxtSearchQuery.Location = new System.Drawing.Point(77, 353);
            this.TxtSearchQuery.Name = "TxtSearchQuery";
            this.TxtSearchQuery.ReadOnly = true;
            this.TxtSearchQuery.Size = new System.Drawing.Size(571, 22);
            this.TxtSearchQuery.TabIndex = 103;
            // 
            // Panel_Step2
            // 
            this.Panel_Step2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_Step2.Controls.Add(this.radioBtnNo);
            this.Panel_Step2.Controls.Add(this.radioBtnYes);
            this.Panel_Step2.Controls.Add(this.TxtSearchVal2);
            this.Panel_Step2.Controls.Add(this.TxtSearchVal1);
            this.Panel_Step2.Controls.Add(this.Label_SearchValue2);
            this.Panel_Step2.Controls.Add(this.Label_SearchValue1);
            this.Panel_Step2.Location = new System.Drawing.Point(12, 562);
            this.Panel_Step2.Name = "Panel_Step2";
            this.Panel_Step2.Size = new System.Drawing.Size(636, 165);
            this.Panel_Step2.TabIndex = 102;
            this.Panel_Step2.Visible = false;
            // 
            // Panel_Step1
            // 
            this.Panel_Step1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_Step1.Controls.Add(this.ComboSearchCondition);
            this.Panel_Step1.Controls.Add(this.ComboDataType);
            this.Panel_Step1.Controls.Add(this.label3);
            this.Panel_Step1.Controls.Add(this.label2);
            this.Panel_Step1.Location = new System.Drawing.Point(12, 408);
            this.Panel_Step1.Name = "Panel_Step1";
            this.Panel_Step1.Size = new System.Drawing.Size(443, 148);
            this.Panel_Step1.TabIndex = 101;
            this.Panel_Step1.Visible = false;
            // 
            // ComboSearchCondition
            // 
            this.ComboSearchCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboSearchCondition.FormattingEnabled = true;
            this.ComboSearchCondition.Location = new System.Drawing.Point(203, 54);
            this.ComboSearchCondition.Name = "ComboSearchCondition";
            this.ComboSearchCondition.Size = new System.Drawing.Size(180, 24);
            this.ComboSearchCondition.TabIndex = 3;
            this.ComboSearchCondition.SelectedIndexChanged += new System.EventHandler(this.ComboSearchCondition_SelectedIndexChanged);
            // 
            // ComboDataType
            // 
            this.ComboDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboDataType.FormattingEnabled = true;
            this.ComboDataType.Location = new System.Drawing.Point(203, 16);
            this.ComboDataType.Name = "ComboDataType";
            this.ComboDataType.Size = new System.Drawing.Size(180, 24);
            this.ComboDataType.TabIndex = 2;
            this.ComboDataType.SelectedIndexChanged += new System.EventHandler(this.ComboDataType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Column Search Condition";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Column Data Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(339, 17);
            this.label1.TabIndex = 91;
            this.label1.Text = "Grid columns, choose one to create a search criteria";
            // 
            // radioBtnNot
            // 
            this.radioBtnNot.AutoSize = true;
            this.radioBtnNot.Location = new System.Drawing.Point(245, 16);
            this.radioBtnNot.Name = "radioBtnNot";
            this.radioBtnNot.Size = new System.Drawing.Size(59, 21);
            this.radioBtnNot.TabIndex = 95;
            this.radioBtnNot.Text = "NOT";
            this.radioBtnNot.UseVisualStyleBackColor = true;
            this.radioBtnNot.CheckedChanged += new System.EventHandler(this.radioBtnNot_CheckedChanged);
            // 
            // radioBtnOr
            // 
            this.radioBtnOr.AutoSize = true;
            this.radioBtnOr.Location = new System.Drawing.Point(177, 16);
            this.radioBtnOr.Name = "radioBtnOr";
            this.radioBtnOr.Size = new System.Drawing.Size(50, 21);
            this.radioBtnOr.TabIndex = 94;
            this.radioBtnOr.Text = "OR";
            this.radioBtnOr.UseVisualStyleBackColor = true;
            this.radioBtnOr.CheckedChanged += new System.EventHandler(this.radioBtnOr_CheckedChanged);
            // 
            // radioBtnAnd
            // 
            this.radioBtnAnd.AutoSize = true;
            this.radioBtnAnd.Checked = true;
            this.radioBtnAnd.Location = new System.Drawing.Point(101, 16);
            this.radioBtnAnd.Name = "radioBtnAnd";
            this.radioBtnAnd.Size = new System.Drawing.Size(58, 21);
            this.radioBtnAnd.TabIndex = 93;
            this.radioBtnAnd.TabStop = true;
            this.radioBtnAnd.Text = "AND";
            this.radioBtnAnd.UseVisualStyleBackColor = true;
            this.radioBtnAnd.CheckedChanged += new System.EventHandler(this.radioBtnAnd_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 17);
            this.label4.TabIndex = 92;
            this.label4.Text = "Conjuction";
            // 
            // Panel_Step0
            // 
            this.Panel_Step0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_Step0.Controls.Add(this.radioBtnNot);
            this.Panel_Step0.Controls.Add(this.radioBtnOr);
            this.Panel_Step0.Controls.Add(this.radioBtnAnd);
            this.Panel_Step0.Controls.Add(this.label4);
            this.Panel_Step0.Controls.Add(this.label1);
            this.Panel_Step0.Controls.Add(this.ListGridColumns);
            this.Panel_Step0.Location = new System.Drawing.Point(12, 12);
            this.Panel_Step0.Name = "Panel_Step0";
            this.Panel_Step0.Size = new System.Drawing.Size(636, 328);
            this.Panel_Step0.TabIndex = 100;
            // 
            // ListGridColumns
            // 
            this.ListGridColumns.FormattingEnabled = true;
            this.ListGridColumns.ItemHeight = 16;
            this.ListGridColumns.Location = new System.Drawing.Point(14, 94);
            this.ListGridColumns.Name = "ListGridColumns";
            this.ListGridColumns.Size = new System.Drawing.Size(607, 212);
            this.ListGridColumns.TabIndex = 90;
            this.ListGridColumns.SelectedIndexChanged += new System.EventHandler(this.ListGridColumns_SelectedIndexChanged);
            // 
            // BtnBack
            // 
            this.BtnBack.Enabled = false;
            this.BtnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.BtnBack.Location = new System.Drawing.Point(698, 348);
            this.BtnBack.Margin = new System.Windows.Forms.Padding(4);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(78, 31);
            this.BtnBack.TabIndex = 99;
            this.BtnBack.Text = "<< Back";
            this.BtnBack.UseVisualStyleBackColor = true;
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // BtnNext
            // 
            this.BtnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.BtnNext.Location = new System.Drawing.Point(784, 348);
            this.BtnNext.Margin = new System.Windows.Forms.Padding(4);
            this.BtnNext.Name = "BtnNext";
            this.BtnNext.Size = new System.Drawing.Size(78, 31);
            this.BtnNext.TabIndex = 98;
            this.BtnNext.Text = "Next >>";
            this.BtnNext.UseVisualStyleBackColor = true;
            this.BtnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.BtnExit.Location = new System.Drawing.Point(880, 348);
            this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(61, 31);
            this.BtnExit.TabIndex = 97;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // FrmWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 738);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TxtSearchQuery);
            this.Controls.Add(this.Panel_Step2);
            this.Controls.Add(this.Panel_Step1);
            this.Controls.Add(this.Panel_Step0);
            this.Controls.Add(this.BtnBack);
            this.Controls.Add(this.BtnNext);
            this.Controls.Add(this.BtnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmWizard";
            this.Text = "Search Wizard";
            this.Load += new System.EventHandler(this.FrmWizard_Load);
            this.Panel_Step2.ResumeLayout(false);
            this.Panel_Step2.PerformLayout();
            this.Panel_Step1.ResumeLayout(false);
            this.Panel_Step1.PerformLayout();
            this.Panel_Step0.ResumeLayout(false);
            this.Panel_Step0.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioBtnNo;
        private System.Windows.Forms.RadioButton radioBtnYes;
        private System.Windows.Forms.TextBox TxtSearchVal2;
        private System.Windows.Forms.TextBox TxtSearchVal1;
        private System.Windows.Forms.Label Label_SearchValue2;
        private System.Windows.Forms.Label Label_SearchValue1;
        private System.Windows.Forms.TextBox TxtSearchQuery;
        private System.Windows.Forms.Panel Panel_Step2;
        private System.Windows.Forms.Panel Panel_Step1;
        private System.Windows.Forms.ComboBox ComboSearchCondition;
        private System.Windows.Forms.ComboBox ComboDataType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioBtnNot;
        private System.Windows.Forms.RadioButton radioBtnOr;
        private System.Windows.Forms.RadioButton radioBtnAnd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel Panel_Step0;
        private System.Windows.Forms.ListBox ListGridColumns;
        internal System.Windows.Forms.Button BtnBack;
        internal System.Windows.Forms.Button BtnNext;
        internal System.Windows.Forms.Button BtnExit;
    }
}