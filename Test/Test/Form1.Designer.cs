namespace Test
{
    partial class Form1
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
            this.btnLoad = new System.Windows.Forms.Button();
            this.txtDataRecords = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearchSimple = new System.Windows.Forms.Button();
            this.btnSearchAdvanced = new System.Windows.Forms.Button();
            this.Datagrid_1 = new DataGridView_withQuery.DataGridView_withQuery();
            ((System.ComponentModel.ISupportInitialize)(this.Datagrid_1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(23, 14);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(138, 32);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Load Data";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtDataRecords
            // 
            this.txtDataRecords.Location = new System.Drawing.Point(344, 14);
            this.txtDataRecords.Name = "txtDataRecords";
            this.txtDataRecords.Size = new System.Drawing.Size(119, 22);
            this.txtDataRecords.TabIndex = 2;
            this.txtDataRecords.Text = "500";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(208, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Number of samples";
            // 
            // btnSearchSimple
            // 
            this.btnSearchSimple.Location = new System.Drawing.Point(488, 14);
            this.btnSearchSimple.Name = "btnSearchSimple";
            this.btnSearchSimple.Size = new System.Drawing.Size(126, 32);
            this.btnSearchSimple.TabIndex = 4;
            this.btnSearchSimple.Text = "Simple Search";
            this.btnSearchSimple.UseVisualStyleBackColor = true;
            this.btnSearchSimple.Click += new System.EventHandler(this.btnSearchSimple_Click);
            // 
            // btnSearchAdvanced
            // 
            this.btnSearchAdvanced.Location = new System.Drawing.Point(634, 14);
            this.btnSearchAdvanced.Name = "btnSearchAdvanced";
            this.btnSearchAdvanced.Size = new System.Drawing.Size(172, 32);
            this.btnSearchAdvanced.TabIndex = 5;
            this.btnSearchAdvanced.Text = "Advanced Sarch";
            this.btnSearchAdvanced.UseVisualStyleBackColor = true;
            this.btnSearchAdvanced.Click += new System.EventHandler(this.btnSearchAdvanced_Click);
            // 
            // Datagrid_1
            // 
            this.Datagrid_1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Datagrid_1.Location = new System.Drawing.Point(23, 93);
            this.Datagrid_1.Name = "Datagrid_1";
            this.Datagrid_1.RowTemplate.Height = 24;
            this.Datagrid_1.SearchFormTitle = "Test grid";
            this.Datagrid_1.Size = new System.Drawing.Size(917, 361);
            this.Datagrid_1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 670);
            this.Controls.Add(this.btnSearchAdvanced);
            this.Controls.Add(this.btnSearchSimple);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDataRecords);
            this.Controls.Add(this.Datagrid_1);
            this.Controls.Add(this.btnLoad);
            this.Name = "Form1";
            this.Text = "Testing datagridview with query";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.Datagrid_1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private DataGridView_withQuery.DataGridView_withQuery Datagrid_1;
        private System.Windows.Forms.TextBox txtDataRecords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearchSimple;
        private System.Windows.Forms.Button btnSearchAdvanced;
    }
}

