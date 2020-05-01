/*
  Author: Hayk Aleksanyan
  email:  hayk.aleksanyan@gmail.com
  web:    https://github.com/hayk314
*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DataGridView_withQuery
{
    public partial class FrmSimple_Search : Form
    {
        public DGV_SearchMeta dgvToBeSearchedMeta = null; // contains the grid to be searched and some meta data

        private bool AnyMatch = false;       // true, if the search record has matched anything before
        private int LastSearched_Row = 0;    // the row which was visited by the search the last time
        private int LastFound_Column = 0;    // the last column (for all column search) where smth was found

        private string LastSearched_String = "";   // the last searched string
        private int LastSearched_Column = -1;      // the last searched column index

        private bool LastSearched_Exact = false;   // the value of last time the check exact match

        private ComboBox cmbSearch_PrevState = null;  // the previous state of the cmbSearch


        public FrmSimple_Search()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Construct a simple search form.
        /// </summary>
        /// <param name="dgvToSearch">The datagridview control on which the search will be performed</param>
        /// <param name="searchTitle">User defined title of the search form.</param>
        public FrmSimple_Search(DataGridView dgvToSearch, string searchTitle, ComboBox cmbSearch_PrevState = null)
        {
            InitializeComponent();
            this.dgvToBeSearchedMeta = new DGV_SearchMeta(dgvToSearch, searchTitle);

            // the search values are stored in the main class (DataGridView_withQuery) and are passed to
            // this form to recover previously searched values
            if (cmbSearch_PrevState != null)
            {
                for (int i = 0; i < cmbSearch_PrevState.Items.Count; ++i)
                {
                    this.cmbSearch.Items.Add(cmbSearch_PrevState.Items[i]);
                }
                this.cmbSearch_PrevState = cmbSearch_PrevState;
            }
        }

        /// <summary>
        /// Reads the columns of the datagridview on which search will be performed.
        /// </summary>
        private void ReadGridColumns()
        {
            // reads the column names of the datagrid_search and populates the combo box

            if ((this.dgvToBeSearchedMeta.dgv != null) && (this.dgvToBeSearchedMeta.dgv.ColumnCount > 0))
            {

                Dictionary<int, string> grid_columns = new Dictionary<int, string>();

                grid_columns.Add(-1, "- - - - - - - - - - - - - - - All columns - - - - - - - - - - - - - - -");

                bool visible_Col = false;

                for (int k = 0; k < this.dgvToBeSearchedMeta.dgv.ColumnCount; k++)
                {
                    if ((this.dgvToBeSearchedMeta.dgv.Columns[k].Visible) &&
                        !(this.dgvToBeSearchedMeta.dgv.Columns[k].CellTemplate is DataGridViewCheckBoxCell))
                    {
                        grid_columns.Add(k, (this.dgvToBeSearchedMeta.dgv.Columns[k].HeaderText == "") ? this.dgvToBeSearchedMeta.dgv.Columns[k].Name : this.dgvToBeSearchedMeta.dgv.Columns[k].HeaderText);
                        visible_Col = true;
                    }
                }


                if (visible_Col) //we need at least 1 visible column to do smth
                {
                    cmbColumns.DataSource = new BindingSource(grid_columns, null);
                    cmbColumns.DisplayMember = "Value";
                    cmbColumns.ValueMember = "Key";
                }
            }
        }
        
        private void FrmSimple_Search_Load(object sender, EventArgs e)
        {
            this.ReadGridColumns();
            this.Text = "Simple search on " + this.dgvToBeSearchedMeta.title; // this parameter is passed by the form owning the grid_ToSearch
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.cmbSearch.Text == "")
            {
                MessageBox.Show("Please enter the search text", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.cmbSearch.Focus();
                return;
            }

            if (this.cmbColumns.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a column to search", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.cmbColumns.Focus();
                return;
            }

            // store the search value in the combo
            if (this.cmbSearch.Items.Contains(this.cmbSearch.Text) == false)
            {
                this.cmbSearch.Items.Add(this.cmbSearch.Text);
            }


            int searchColIndex = ((KeyValuePair<int, string>)cmbColumns.SelectedItem).Key;

            string search_text = this.cmbSearch.Text;
            string s = "";
            bool found = false;


            // if smth has changed in the search we start from the 1st row
            if ((this.LastSearched_Column != searchColIndex) || (this.LastSearched_Exact != this.check_Exact.Checked) || (this.LastSearched_String != search_text))
            {
                this.LastSearched_Row = 0;
                this.LastFound_Column = 0;
                this.LastSearched_Column = searchColIndex;
                this.LastSearched_String = search_text;
                this.LastSearched_Exact = this.check_Exact.Checked;

                this.AnyMatch = false;
            }

            if (searchColIndex != -1)
            {   // look in a specific column

                for (int k = LastSearched_Row; k < this.dgvToBeSearchedMeta.dgv.RowCount; k++)
                {
                    if (this.dgvToBeSearchedMeta.dgv.Rows[k].Visible == false) // only check visible rows
                        continue;

                    s = (this.dgvToBeSearchedMeta.dgv.Rows[k].Cells[searchColIndex].Value == null) ? "" : this.dgvToBeSearchedMeta.dgv.Rows[k].Cells[searchColIndex].Value.ToString();
                    if (s == "")
                        continue;


                    if (StaticFunctions.IsSubstring(s, search_text, this.check_Exact.Checked))
                    {
                        found = true;
                        LastSearched_Row = k + 1;
                        if (LastSearched_Row >= this.dgvToBeSearchedMeta.dgv.RowCount)
                            LastSearched_Row = 0;

                        this.dgvToBeSearchedMeta.dgv.CurrentCell = this.dgvToBeSearchedMeta.dgv.Rows[k].Cells[searchColIndex];

                        break;
                    }
                }
            }
            else // the search col = -1, i.e. we need to check all columns
            {
                int[] searchable_Cols = null; //the columns which are visible and which are not of bool type
                int u = 0;
                for (int i = 0; i < this.dgvToBeSearchedMeta.dgv.ColumnCount; i++)
                {
                    if ((this.dgvToBeSearchedMeta.dgv.Columns[i].Visible == true) && !(this.dgvToBeSearchedMeta.dgv.Columns[i].CellTemplate is DataGridViewCheckBoxCell))
                    {
                        u++;
                        Array.Resize(ref searchable_Cols, u);
                        searchable_Cols[u - 1] = i;
                    }
                }

                if (searchable_Cols == null)
                {
                    MessageBox.Show("There are no columns in the grid.", Constants.msgWarning, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                for (int k = LastSearched_Row; k < this.dgvToBeSearchedMeta.dgv.RowCount; k++)
                {
                    if (this.dgvToBeSearchedMeta.dgv.Rows[k].Visible == false) //We only check visible rows
                        continue;

                    for (int i = this.LastFound_Column; i < searchable_Cols.Length; i++)
                    {
                        s = (this.dgvToBeSearchedMeta.dgv.Rows[k].Cells[searchable_Cols[i]].Value == null) ? "" : this.dgvToBeSearchedMeta.dgv.Rows[k].Cells[searchable_Cols[i]].Value.ToString();
                        if (s == "")
                            continue;

                        if (StaticFunctions.IsSubstring(s, search_text, this.check_Exact.Checked))
                        {
                            found = true;

                            if (i >= searchable_Cols.Length - 1)
                            {
                                this.LastFound_Column = 0;
                                LastSearched_Row = k + 1;
                                if (LastSearched_Row >= this.dgvToBeSearchedMeta.dgv.RowCount)
                                    LastSearched_Row = 0;
                            }
                            else
                            {
                                LastFound_Column = i + 1;
                                LastSearched_Row = k;
                            }

                            this.dgvToBeSearchedMeta.dgv.CurrentCell = this.dgvToBeSearchedMeta.dgv.Rows[k].Cells[searchable_Cols[i]];

                            break;
                        }
                    }
                    

                    if (found)
                    {
                        this.AnyMatch = true;
                        break;
                    }
                    else
                        this.LastFound_Column = 0;
                }
            }


            if (found == false)
            {
                LastSearched_Row = 0;
                if (this.AnyMatch == false)
                    MessageBox.Show("Nothing was found", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("The search has reached the end of the grid", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSimple_Search_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.cmbSearch_PrevState != null && this.cmbSearch.Items.Count > 0 ) {
                this.cmbSearch_PrevState.Items.Clear();
                for (int i = 0; i < this.cmbSearch.Items.Count; ++i)
                {
                    this.cmbSearch_PrevState.Items.Add(this.cmbSearch.Items[i]);
                }                
            }            
        }
    }
}
