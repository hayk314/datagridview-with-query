namespace DataGridView_withQuery
{
    partial class FrmAdvanced_Search
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
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnDown = new System.Windows.Forms.Button();
            this.BtnUp = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuNewSearchRow = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuCopySearchRow = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuPasteSearchRow = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDeleteSearchRow = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuClearAll = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuWizard = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuLoadFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSaveSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFindNext = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.BtnWizard = new System.Windows.Forms.Button();
            this.BtnAddNewSearchRow = new System.Windows.Forms.Button();
            this.BtnFindNext = new System.Windows.Forms.Button();
            this.BtnFilter = new System.Windows.Forms.Button();
            this.BtnCommands = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.RichTextBox();
            this.lblSearchConditions = new System.Windows.Forms.Label();
            this.dgvSearch = new System.Windows.Forms.DataGridView();
            this.BtnStopFilter = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.BtnExit = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnDown
            // 
            this.BtnDown.Location = new System.Drawing.Point(785, 75);
            this.BtnDown.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDown.Name = "BtnDown";
            this.BtnDown.Size = new System.Drawing.Size(42, 43);
            this.BtnDown.TabIndex = 127;
            this.toolTip1.SetToolTip(this.BtnDown, "Move the row down");
            this.BtnDown.UseVisualStyleBackColor = true;
            this.BtnDown.Click += new System.EventHandler(this.BtnDown_Click);
            // 
            // BtnUp
            // 
            this.BtnUp.Location = new System.Drawing.Point(783, 23);
            this.BtnUp.Margin = new System.Windows.Forms.Padding(4);
            this.BtnUp.Name = "BtnUp";
            this.BtnUp.Size = new System.Drawing.Size(44, 44);
            this.BtnUp.TabIndex = 126;
            this.toolTip1.SetToolTip(this.BtnUp, "Move the row up");
            this.BtnUp.UseVisualStyleBackColor = true;
            this.BtnUp.Click += new System.EventHandler(this.BtnUp_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuNewSearchRow,
            this.MenuCopySearchRow,
            this.MenuPasteSearchRow,
            this.MenuDeleteSearchRow,
            this.MenuClearAll,
            this.MenuMoveUp,
            this.MenuMoveDown,
            this.toolStripMenuItem1,
            this.MenuWizard,
            this.MenuLoadFromFile,
            this.MenuSaveSearch,
            this.toolStripMenuItem2,
            this.MenuFilter,
            this.MenuFindNext,
            this.toolStripMenuItem3,
            this.MenuHelp});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(292, 334);
            // 
            // MenuNewSearchRow
            // 
            this.MenuNewSearchRow.BackColor = System.Drawing.SystemColors.Control;
            this.MenuNewSearchRow.Name = "MenuNewSearchRow";
            this.MenuNewSearchRow.Size = new System.Drawing.Size(291, 24);
            this.MenuNewSearchRow.Text = "New search row";
            this.MenuNewSearchRow.Click += new System.EventHandler(this.MenuNewSearchRow_Click);
            // 
            // MenuCopySearchRow
            // 
            this.MenuCopySearchRow.Name = "MenuCopySearchRow";
            this.MenuCopySearchRow.Size = new System.Drawing.Size(291, 24);
            this.MenuCopySearchRow.Text = "Copy the selected row";
            this.MenuCopySearchRow.Click += new System.EventHandler(this.MenuCopySearchRow_Click);
            // 
            // MenuPasteSearchRow
            // 
            this.MenuPasteSearchRow.Name = "MenuPasteSearchRow";
            this.MenuPasteSearchRow.Size = new System.Drawing.Size(291, 24);
            this.MenuPasteSearchRow.Text = "Paste row from clipboard";
            this.MenuPasteSearchRow.Click += new System.EventHandler(this.MenuPasteSearchRow_Click);
            // 
            // MenuDeleteSearchRow
            // 
            this.MenuDeleteSearchRow.Name = "MenuDeleteSearchRow";
            this.MenuDeleteSearchRow.Size = new System.Drawing.Size(291, 24);
            this.MenuDeleteSearchRow.Text = "Delete the selected row";
            this.MenuDeleteSearchRow.Click += new System.EventHandler(this.MenuDeleteSearchRow_Click);
            // 
            // MenuClearAll
            // 
            this.MenuClearAll.Name = "MenuClearAll";
            this.MenuClearAll.Size = new System.Drawing.Size(291, 24);
            this.MenuClearAll.Text = "Clear All";
            this.MenuClearAll.Click += new System.EventHandler(this.MenuClearAll_Click);
            // 
            // MenuMoveUp
            // 
            this.MenuMoveUp.Name = "MenuMoveUp";
            this.MenuMoveUp.Size = new System.Drawing.Size(291, 24);
            this.MenuMoveUp.Text = "Move the row Up";
            this.MenuMoveUp.Click += new System.EventHandler(this.MenuMoveUp_Click);
            // 
            // MenuMoveDown
            // 
            this.MenuMoveDown.Name = "MenuMoveDown";
            this.MenuMoveDown.Size = new System.Drawing.Size(291, 24);
            this.MenuMoveDown.Text = "Move the row Down";
            this.MenuMoveDown.Click += new System.EventHandler(this.MenuMoveDown_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(288, 6);
            // 
            // MenuWizard
            // 
            this.MenuWizard.Name = "MenuWizard";
            this.MenuWizard.Size = new System.Drawing.Size(291, 24);
            this.MenuWizard.Text = "Search wizard";
            this.MenuWizard.Click += new System.EventHandler(this.MenuWizard_Click);
            // 
            // MenuLoadFromFile
            // 
            this.MenuLoadFromFile.Name = "MenuLoadFromFile";
            this.MenuLoadFromFile.Size = new System.Drawing.Size(291, 24);
            this.MenuLoadFromFile.Text = "Load search conditions from file";
            this.MenuLoadFromFile.Click += new System.EventHandler(this.MenuLoadFromFile_Click);
            // 
            // MenuSaveSearch
            // 
            this.MenuSaveSearch.Name = "MenuSaveSearch";
            this.MenuSaveSearch.Size = new System.Drawing.Size(291, 24);
            this.MenuSaveSearch.Text = "Save search conditions to file";
            this.MenuSaveSearch.Click += new System.EventHandler(this.MenuSaveSearch_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(288, 6);
            // 
            // MenuFilter
            // 
            this.MenuFilter.Name = "MenuFilter";
            this.MenuFilter.Size = new System.Drawing.Size(291, 24);
            this.MenuFilter.Text = "Apply the filter";
            this.MenuFilter.Click += new System.EventHandler(this.MenuFilter_Click);
            // 
            // MenuFindNext
            // 
            this.MenuFindNext.Name = "MenuFindNext";
            this.MenuFindNext.Size = new System.Drawing.Size(291, 24);
            this.MenuFindNext.Text = "Find next";
            this.MenuFindNext.Click += new System.EventHandler(this.MenuFindNext_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(288, 6);
            // 
            // MenuHelp
            // 
            this.MenuHelp.Name = "MenuHelp";
            this.MenuHelp.Size = new System.Drawing.Size(291, 24);
            this.MenuHelp.Text = "Help";
            this.MenuHelp.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(151, 252);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(620, 23);
            this.progressBar1.TabIndex = 136;
            this.progressBar1.Visible = false;
            // 
            // BtnWizard
            // 
            this.BtnWizard.Location = new System.Drawing.Point(619, 375);
            this.BtnWizard.Margin = new System.Windows.Forms.Padding(4);
            this.BtnWizard.Name = "BtnWizard";
            this.BtnWizard.Size = new System.Drawing.Size(84, 31);
            this.BtnWizard.TabIndex = 135;
            this.BtnWizard.Text = "Wizard";
            this.BtnWizard.UseVisualStyleBackColor = true;
            this.BtnWizard.Click += new System.EventHandler(this.BtnWizard_Click);
            // 
            // BtnAddNewSearchRow
            // 
            this.BtnAddNewSearchRow.Location = new System.Drawing.Point(526, 375);
            this.BtnAddNewSearchRow.Margin = new System.Windows.Forms.Padding(4);
            this.BtnAddNewSearchRow.Name = "BtnAddNewSearchRow";
            this.BtnAddNewSearchRow.Size = new System.Drawing.Size(84, 31);
            this.BtnAddNewSearchRow.TabIndex = 134;
            this.BtnAddNewSearchRow.Text = "New Row";
            this.BtnAddNewSearchRow.UseVisualStyleBackColor = true;
            this.BtnAddNewSearchRow.Click += new System.EventHandler(this.BtnAddNewSearchRow_Click);
            // 
            // BtnFindNext
            // 
            this.BtnFindNext.Location = new System.Drawing.Point(229, 375);
            this.BtnFindNext.Margin = new System.Windows.Forms.Padding(4);
            this.BtnFindNext.Name = "BtnFindNext";
            this.BtnFindNext.Size = new System.Drawing.Size(91, 31);
            this.BtnFindNext.TabIndex = 133;
            this.BtnFindNext.Text = "Find Next";
            this.BtnFindNext.UseVisualStyleBackColor = true;
            this.BtnFindNext.Click += new System.EventHandler(this.BtnFindNext_Click);
            // 
            // BtnFilter
            // 
            this.BtnFilter.Location = new System.Drawing.Point(130, 375);
            this.BtnFilter.Margin = new System.Windows.Forms.Padding(4);
            this.BtnFilter.Name = "BtnFilter";
            this.BtnFilter.Size = new System.Drawing.Size(91, 31);
            this.BtnFilter.TabIndex = 132;
            this.BtnFilter.Text = "Filter";
            this.BtnFilter.UseVisualStyleBackColor = true;
            this.BtnFilter.Click += new System.EventHandler(this.BtnFilter_Click);
            // 
            // BtnCommands
            // 
            this.BtnCommands.Location = new System.Drawing.Point(24, 375);
            this.BtnCommands.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCommands.Name = "BtnCommands";
            this.BtnCommands.Size = new System.Drawing.Size(91, 31);
            this.BtnCommands.TabIndex = 131;
            this.BtnCommands.Text = "Commands";
            this.BtnCommands.UseVisualStyleBackColor = true;
            this.BtnCommands.Click += new System.EventHandler(this.BtnCommands_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(24, 285);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.ReadOnly = true;
            this.txtSearch.Size = new System.Drawing.Size(745, 78);
            this.txtSearch.TabIndex = 130;
            this.txtSearch.Text = "";
            // 
            // lblSearchConditions
            // 
            this.lblSearchConditions.AutoSize = true;
            this.lblSearchConditions.Location = new System.Drawing.Point(23, 252);
            this.lblSearchConditions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchConditions.Name = "lblSearchConditions";
            this.lblSearchConditions.Size = new System.Drawing.Size(121, 17);
            this.lblSearchConditions.TabIndex = 129;
            this.lblSearchConditions.Text = "Search conditions";
            // 
            // dgvSearch
            // 
            this.dgvSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearch.Location = new System.Drawing.Point(24, 13);
            this.dgvSearch.Margin = new System.Windows.Forms.Padding(4);
            this.dgvSearch.Name = "dgvSearch";
            this.dgvSearch.Size = new System.Drawing.Size(747, 223);
            this.dgvSearch.TabIndex = 128;
            this.dgvSearch.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvSearch_CellBeginEdit);
            this.dgvSearch.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearch_CellContentClick);
            this.dgvSearch.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearch_CellEndEdit);
            this.dgvSearch.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvSearch_EditingControlShowing);
            // 
            // BtnStopFilter
            // 
            this.BtnStopFilter.Enabled = false;
            this.BtnStopFilter.Location = new System.Drawing.Point(327, 375);
            this.BtnStopFilter.Margin = new System.Windows.Forms.Padding(4);
            this.BtnStopFilter.Name = "BtnStopFilter";
            this.BtnStopFilter.Size = new System.Drawing.Size(84, 31);
            this.BtnStopFilter.TabIndex = 125;
            this.BtnStopFilter.Text = "Stop";
            this.BtnStopFilter.UseVisualStyleBackColor = true;
            this.BtnStopFilter.Click += new System.EventHandler(this.BtnStopFilter_Click);
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(771, 21);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(7, 60);
            this.Label1.TabIndex = 124;
            // 
            // BtnExit
            // 
            this.BtnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.BtnExit.Location = new System.Drawing.Point(711, 375);
            this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(61, 31);
            this.BtnExit.TabIndex = 123;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // FrmAdvanced_Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 503);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.BtnWizard);
            this.Controls.Add(this.BtnAddNewSearchRow);
            this.Controls.Add(this.BtnFindNext);
            this.Controls.Add(this.BtnFilter);
            this.Controls.Add(this.BtnCommands);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearchConditions);
            this.Controls.Add(this.dgvSearch);
            this.Controls.Add(this.BtnDown);
            this.Controls.Add(this.BtnUp);
            this.Controls.Add(this.BtnStopFilter);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.BtnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(927, 488);
            this.Name = "FrmAdvanced_Search";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Advanced Search";
            this.Load += new System.EventHandler(this.FrmAdvanced_Search_Load);
            this.Resize += new System.EventHandler(this.FrmAdvanced_Search_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button BtnDown;
        private System.Windows.Forms.Button BtnUp;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuNewSearchRow;
        private System.Windows.Forms.ToolStripMenuItem MenuCopySearchRow;
        private System.Windows.Forms.ToolStripMenuItem MenuPasteSearchRow;
        private System.Windows.Forms.ToolStripMenuItem MenuDeleteSearchRow;
        private System.Windows.Forms.ToolStripMenuItem MenuClearAll;
        private System.Windows.Forms.ToolStripMenuItem MenuMoveUp;
        private System.Windows.Forms.ToolStripMenuItem MenuMoveDown;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem MenuWizard;
        private System.Windows.Forms.ToolStripMenuItem MenuLoadFromFile;
        private System.Windows.Forms.ToolStripMenuItem MenuSaveSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem MenuFilter;
        private System.Windows.Forms.ToolStripMenuItem MenuFindNext;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem MenuHelp;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button BtnWizard;
        private System.Windows.Forms.Button BtnAddNewSearchRow;
        private System.Windows.Forms.Button BtnFindNext;
        private System.Windows.Forms.Button BtnFilter;
        private System.Windows.Forms.Button BtnCommands;
        private System.Windows.Forms.RichTextBox txtSearch;
        private System.Windows.Forms.Label lblSearchConditions;
        private System.Windows.Forms.DataGridView dgvSearch;
        private System.Windows.Forms.Button BtnStopFilter;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button BtnExit;
    }
}