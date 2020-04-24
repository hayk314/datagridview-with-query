/*
  Author: Hayk Aleksanyan
  email:  hayk.aleksanyan@gmail.com
  web:    https://github.com/hayk314
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using System.Runtime.Serialization.Formatters.Binary;

namespace DataGridView_withQuery
{
    public partial class FrmAdvanced_Search : Form
    {
        public DGV_SearchMeta dgvToBeSearchedMeta = null; // contains the grid to be searched and some meta data

        public bool suspend_Edit_Checks = false;  // is used to externally control the value checks on the editing of the search grid

        private DataGridViewRow CopiedSearchRow = null;  // the user can copy-paste rows in search grid, mimics the clipboard
        private ComboBox ComboinGridCell = null;       // points to the active combobox of a cell in dgvSearch

        private string GridLastState = "";  // Serialization of the dgvSearch. Is used to control the start index of Find-Next operation:
                                            // e.g. a user starts Find-Next with a given search grid, then on the way changes conditions.
                                            // Comparing the current state of the grid with this value, we decide if the Find-Next must start
                                            // from Row 0.

        private int LastSearched_Row = -1;  // stores the index of the last row in the grid to be searched, which was searched with Find-Next
        private int LastMatched_Row = -1;   // stores the index of the last matched row in the grid to be searched with Find-Next
        private bool StopOnFirstMatch = false; // used to indicate if the running search is Filter or Find-Next

        private List<int> UnMatchedRows = null; // row indices of grid_ToSearch that did not matched the search query (the filter) 
        private int MatchCount = 0;             // how many (visible) rows match the filter


        /* the grid structure
         conj
         colName
         colValType
         operator
         searchVal
         searchVal2
         indexInGrid
         colNameInGrid
         */

        #region "Constructors"

        public FrmAdvanced_Search()
        {
            InitializeComponent();

            DoResize();
        }

        public FrmAdvanced_Search(DataGridView dgvToSearch, string searchTitle)
        {
            InitializeComponent();

            DoResize();

            this.dgvToBeSearchedMeta = new DGV_SearchMeta(dgvToSearch, searchTitle);
        }

        #endregion

        #region "Form handlers"

        private void DoResize()
        {
            // provides adaptivness (responsiveness) of the screen

            BtnDown.Left = this.Width - BtnDown.Width - 30;
            BtnUp.Left = BtnDown.Left;
            BtnUp.Top = 10;

            dgvSearch.Location = new Point(15, 10);
            dgvSearch.Width = BtnDown.Left - dgvSearch.Left - 7;

            progressBar1.Left = lblSearchConditions.Left + lblSearchConditions.Width + 10;
            progressBar1.Width = dgvSearch.Left + dgvSearch.Width - progressBar1.Left;

            BtnExit.Left = dgvSearch.Left + dgvSearch.Width - BtnExit.Width;
            BtnExit.Top = Height - BtnExit.Height - 43;

            txtSearch.Left = dgvSearch.Left;
            txtSearch.Height = 50 + Convert.ToInt32(0.12 * Height);
            txtSearch.Top = BtnExit.Top - txtSearch.Height - 5;
            txtSearch.Width = dgvSearch.Width;

            lblSearchConditions.Left = dgvSearch.Left + 1;
            lblSearchConditions.Top = txtSearch.Top - lblSearchConditions.Height - 15;
            progressBar1.Top = lblSearchConditions.Top;

            BtnCommands.Left = dgvSearch.Left;
            BtnCommands.Top = BtnExit.Top;

            BtnFilter.Top = BtnExit.Top;
            BtnFilter.Left = BtnCommands.Left + BtnCommands.Width + 20;

            BtnFindNext.Top = BtnExit.Top;
            BtnFindNext.Left = BtnFilter.Left + BtnFilter.Width + 5;

            BtnStopFilter.Top = BtnExit.Top;
            BtnStopFilter.Left = BtnFindNext.Left + BtnFindNext.Width + 5;

            BtnAddNewSearchRow.Top = BtnExit.Top;
            BtnAddNewSearchRow.Left = BtnStopFilter.Left + BtnStopFilter.Width + 5;

            BtnWizard.Top = BtnExit.Top;
            BtnWizard.Left = BtnAddNewSearchRow.Left + BtnAddNewSearchRow.Width + 5;

            dgvSearch.Height = lblSearchConditions.Top - dgvSearch.Top - 10;
        }

        private void FreezeControls(bool freeze = false)
        {
            // Enable/Disable controls when the background worker doing the search, is in action

            MenuCopySearchRow.Enabled = freeze;
            MenuDeleteSearchRow.Enabled = freeze;
            MenuFilter.Enabled = freeze;
            MenuFindNext.Enabled = freeze;
            MenuHelp.Enabled = freeze;
            MenuLoadFromFile.Enabled = freeze;
            MenuMoveDown.Enabled = freeze;
            MenuMoveUp.Enabled = freeze;
            MenuNewSearchRow.Enabled = freeze;
            MenuPasteSearchRow.Enabled = freeze;
            MenuSaveSearch.Enabled = freeze;
            MenuWizard.Enabled = freeze;

            BtnCommands.Enabled = freeze;
            BtnDown.Enabled = freeze;
            BtnUp.Enabled = freeze;
            BtnExit.Enabled = freeze;
            BtnFilter.Enabled = freeze;
            BtnFindNext.Enabled = freeze;
            BtnWizard.Enabled = freeze;
            BtnAddNewSearchRow.Enabled = freeze;

            BtnStopFilter.Enabled = !freeze;
        }


        private void FrmAdvanced_Search_Load(object sender, EventArgs e)
        {
            CreateGridofSearch();

            Text = "Advanced search on " + this.dgvToBeSearchedMeta.title; // this parameter is passed by the form owning the grid_ToSearch

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;

            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.WorkerReportsProgress = true;
        }

        private void FrmAdvanced_Search_Resize(object sender, EventArgs e)
        {
            DoResize();
        }

        #endregion

        public void ReplaceSearchTextBy(string s)
        {
            this.txtSearch.Text = s;
        }

        private void ResetFindNextParams()
        {
            LastSearched_Row = -1;
            LastMatched_Row = -1;
        }

        #region "Encoder-decoders for Search datagridview"

        private string EncodeGridToString()
        {
            // serialize the grid into a string 

            string ans = "";

            for (int i = 0; i < this.dgvSearch.RowCount; ++i)
            {
                string tmp = "";

                for (int j = 0; j < this.dgvSearch.ColumnCount; ++j)
                {
                    if (dgvSearch.Rows[i].Cells[j].Value == null)
                    {
                        tmp += string.Format("'{0}':'{1}'", dgvSearch.Columns[j].Name, "");
                    }
                    else
                    {   // escape single quotes ' in the column values by \
                        // it is assumed that the column names will not contain single quotes ''; TODO: remove this assumption
                        tmp += string.Format("'{0}':'{1}'", dgvSearch.Columns[j].Name, dgvSearch.Rows[i].Cells[j].Value.ToString().Replace("'", "\'"));
                    }
                    if (j < dgvSearch.ColumnCount - 1)
                        tmp += ", ";
                }
                ans += "[" + tmp + "]";
                if (i < this.dgvSearch.RowCount - 1)
                    ans += "\n";
            }

            return ans;
        }

        private List<Dictionary<string, string>> EncodeGridToListofDict()
        {
            // encode the search grid into a list of dictionaries where KEY is the column name, and its value is the value from dgvSearch

            List<Dictionary<string, string>> ans = new List<Dictionary<string, string>>();

            for (int i = 0; i < this.dgvSearch.RowCount; ++i)
            {
                Dictionary<string, string> tmp = new Dictionary<string, string>();

                for (int j = 0; j < this.dgvSearch.ColumnCount; ++j)
                {
                    if (dgvSearch.Rows[i].Cells[j].Value == null)
                    {
                        tmp.Add(dgvSearch.Columns[j].Name, "");
                    }
                    else
                    {
                        tmp.Add(dgvSearch.Columns[j].Name, dgvSearch.Rows[i].Cells[j].Value.ToString());
                    }
                }
                ans.Add(tmp);
            }

            return ans;
        }

        private List<Dictionary<string, string>> DecodeGridDictFromBinary(string fileName)
        {
            // read a binary file representing the search grid (generated by the Save-to-File functionality)
            // the result is expected to be in the form of EncodeGridToListofDict()

            List<Dictionary<string, string>> ans = new List<Dictionary<string, string>>();

            System.IO.Stream sw = System.IO.File.Open(fileName, System.IO.FileMode.Open);
            BinaryFormatter b = new BinaryFormatter();
            ans = (List<Dictionary<string, string>>)b.Deserialize(sw);
            sw.Close();

            return ans;
        }

        private string MakeQueryText()
        {
            // serialize the grid to plain-English looking equivalent query

            string strQuery = "";  // the result

            string strOR_Block = "";

            for (int i = 0; i < dgvSearch.RowCount; ++i)
            {
                // some safety checks and sanitation of the grid to avoid null conversion exception
                if (dgvSearch.Rows[i].Cells["conj"].Value == null)
                    continue;
                if (dgvSearch.Rows[i].Cells["colName"].Value == null)
                    continue;
                if (dgvSearch.Rows[i].Cells["operator"].Value == null)
                    continue;
                if (dgvSearch.Rows[i].Cells["searchVal"].Value == null)
                    dgvSearch.Rows[i].Cells["searchVal"].Value = "";
                if (dgvSearch.Rows[i].Cells["searchVal2"].Value == null)
                    dgvSearch.Rows[i].Cells["searchVal2"].Value = "";


                if ((dgvSearch.Rows[i].Cells["conj"].Value.ToString() == Constants.ConjOr) ||
                    ((dgvSearch.Rows[i].Cells["conj"].Value.ToString() == Constants.ConjDeafult)))
                {
                    if (strOR_Block != "")
                    {   // if we already had build an OR block before, we add it to query and start a new OR block from scratch.
                        if (strQuery != "")
                        {
                            strQuery += " OR ( " + strOR_Block + " )";
                        }
                        else
                            strQuery += "( " + strOR_Block + " )";
                    }
                    strOR_Block = "";
                }
                else if (dgvSearch.Rows[i].Cells["conj"].Value.ToString() == Constants.ConjAnd)
                    strOR_Block += " AND ";
                else if (dgvSearch.Rows[i].Cells["conj"].Value.ToString() == Constants.ConjNot)
                    strOR_Block += " AND_NOT ";


                strOR_Block += "<" + dgvSearch.Rows[i].Cells["colName"].Value.ToString() + ">";

                switch (dgvSearch.Rows[i].Cells["operator"].Value.ToString())
                {
                    case Constants.operator_Eq:
                        strOR_Block += " equals to ";
                        strOR_Block += "'" + dgvSearch.Rows[i].Cells["searchVal"].Value.ToString() + "'";
                        break;
                    case Constants.operator_GEq:
                        strOR_Block += " greater or equal ";
                        strOR_Block += dgvSearch.Rows[i].Cells["searchVal"].Value.ToString();
                        break;
                    case Constants.operator_LEq:
                        strOR_Block += " less or equal ";
                        strOR_Block += dgvSearch.Rows[i].Cells["searchVal"].Value.ToString();
                        break;
                    case Constants.operator_Like:
                        strOR_Block += " is Like ";
                        strOR_Block += "'" + dgvSearch.Rows[i].Cells["searchVal"].Value.ToString() + "'";
                        break;
                    case Constants.operator_NotEq:
                        strOR_Block += " is not equal to ";
                        strOR_Block += "'" + dgvSearch.Rows[i].Cells["searchVal"].Value.ToString() + "'";
                        break;
                    case Constants.operator_Between:
                        strOR_Block += " is between ";
                        strOR_Block += "'" + dgvSearch.Rows[i].Cells["searchVal"].Value.ToString() + "'";
                        strOR_Block += " and '" + dgvSearch.Rows[i].Cells["searchVal2"].Value.ToString() + "'";
                        break;
                    case Constants.operator_Edit_Distance:
                        strOR_Block += " is within edit distance of ";
                        strOR_Block += dgvSearch.Rows[i].Cells["searchVal2"].Value.ToString();
                        strOR_Block += " from '" + dgvSearch.Rows[i].Cells["searchVal"].Value.ToString() + "'";
                        break;
                    default:
                        break;
                }
            }

            if (strQuery != "")
            {
                strQuery += " OR ( " + strOR_Block + " )";
            }
            else
                strQuery += "( " + strOR_Block + " )";

            return "Search in " + dgvToBeSearchedMeta.title + " WHERE \n" + strQuery;

        }

        #endregion

        public void UpdateQueryText()
        {
            txtSearch.Text = MakeQueryText();
            HighLight_SearchText();  // apply the highlight on search text
        }

        private void HighLight_SearchText()
        {
            // Highlights the search conjunctions (AND, OR, NOT) in the generated search text

            int len = txtSearch.TextLength;
            int index = 0;
            int lastIndex = txtSearch.Text.LastIndexOf(" AND ");

            while (index < lastIndex)
            {
                txtSearch.Find(" AND ", index, len, RichTextBoxFinds.MatchCase);
                txtSearch.SelectionBackColor = Color.FromArgb(240, 240, 0);
                index = txtSearch.Text.IndexOf(" AND ", index) + 1;
            }


            index = 0;
            lastIndex = txtSearch.Text.LastIndexOf(" OR ");

            while (index < lastIndex)
            {
                txtSearch.Find(" OR ", index, len, RichTextBoxFinds.MatchCase);
                txtSearch.SelectionBackColor = Color.FromArgb(0, 150, 150);
                index = txtSearch.Text.IndexOf(" OR ", index) + 1;
            }


            index = 0;
            lastIndex = txtSearch.Text.LastIndexOf(" AND_NOT ");

            while (index < lastIndex)
            {
                txtSearch.Find(" AND_NOT ", index, len, RichTextBoxFinds.MatchCase);
                txtSearch.SelectionBackColor = Color.FromArgb(240, 110, 114);
                index = txtSearch.Text.IndexOf(" AND_NOT ", index) + 1;
            }
        }

        #region "Search functionality"

        private bool CellDataMatchesWithSearch(string cellValue, string colDataType, string operatorName, string searchValue1, string searchValue2 = "")
        {
            // true, if and only if the given cellValue satisfies the search conditions
            // cellValue, colDataType, searchOperator, searchValue1, searchValue2

            // the convention is that if the operator's action is not defined for the given value types,  return false
            // e.g. >= applied on strings is not defined, and will result in false value

            // first check the dataType, then the operator applied to it

            if (colDataType == Constants.ValueType_Bool) // BOOLEAN
            {
                if (operatorName != Constants.operator_Eq && operatorName != Constants.operator_NotEq)
                    return false;

                // only ==   and   != is supported for boolean
                if (operatorName == Constants.operator_Eq)
                {
                    // A genuine boolean field would become True/False when converted to string value
                    if (searchValue1 == Constants.ValueBool_True)
                        return cellValue == "True";
                    else
                        return cellValue != "True";
                }
                if (operatorName == Constants.operator_NotEq)
                {
                    if (searchValue1 == Constants.ValueBool_True)
                        return cellValue == "False";
                    else
                        return cellValue != "False";
                }
            }
            else if (colDataType == Constants.ValueType_Date)  // DateTime
            {
                // NOT applicable operators
                if (operatorName == Constants.operator_Edit_Distance || operatorName == Constants.operator_Like)
                    return false;

                DateTime cellDate, searchDate;

                // some edge cases 
                if (operatorName == Constants.operator_Eq)
                {
                    if (cellValue == "")
                        return searchValue1 == "";
                }
                if (operatorName == Constants.operator_NotEq)
                {
                    if (cellValue == "")
                        return searchValue1 != "";
                }

                try
                {
                    cellDate = Convert.ToDateTime(cellValue);
                    searchDate = Convert.ToDateTime(searchValue1);
                }
                catch
                {
                    return false;
                }


                if (operatorName == Constants.operator_Eq)
                {
                    return cellDate == searchDate;
                }
                else if (operatorName == Constants.operator_Between)
                {
                    try
                    {
                        //DateTime searchDate2 = DateTime.ParseExact(searchValue2, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime searchDate2 = Convert.ToDateTime(searchValue2);

                        return (cellDate >= searchDate && cellDate <= searchDate2);
                    }
                    catch
                    {
                        return false;
                    }
                }
                else if (operatorName == Constants.operator_GEq)
                {
                    return cellDate >= searchDate;
                }
                else if (operatorName == Constants.operator_LEq)
                {
                    return cellDate <= searchDate;
                }
                else if (operatorName == Constants.operator_NotEq)
                {
                    return cellDate != searchDate;
                }
                else
                {
                    // Nothing else is supported at this point
                    return false;
                }
            }
            else if (colDataType == Constants.ValueType_Numeric)  // NUMERIC 
            {
                if (operatorName == Constants.operator_Edit_Distance || operatorName == Constants.operator_Like)
                    return false;

                double cellVal, searchVal;
                try
                {
                    cellVal = Convert.ToDouble(cellValue);
                    searchVal = Convert.ToDouble(searchValue1);
                }
                catch
                {
                    return false;
                }

                if (operatorName == Constants.operator_Between)
                {
                    double searchVal2;
                    try
                    {
                        searchVal2 = Convert.ToDouble(searchValue2);
                    }
                    catch
                    {
                        return false;
                    }
                    return (cellVal >= searchVal && cellVal <= searchVal2);
                }
                else if (operatorName == Constants.operator_Eq)
                {
                    return cellVal == searchVal;
                }
                else if (operatorName == Constants.operator_GEq)
                {
                    return cellVal >= searchVal;
                }
                else if (operatorName == Constants.operator_LEq)
                {
                    return cellVal <= searchVal;
                }
                else if (operatorName == Constants.operator_NotEq)
                {
                    return cellVal != searchVal;
                }
                else
                {
                    // Nothing else is supported at this point
                    return false;
                }
            }
            else if (colDataType == Constants.ValueType_String)   // String
            {
                if (operatorName == Constants.operator_Between || operatorName == Constants.operator_GEq || operatorName == Constants.operator_LEq)
                    return false;

                if (operatorName == Constants.operator_Edit_Distance)
                {
                    int tolerance = 0;
                    try
                    {
                        tolerance = Convert.ToInt32(searchValue2);
                    }
                    catch
                    {
                        return false;
                    }

                    if (searchValue1.Length > tolerance + cellValue.Length || cellValue.Length > searchValue1.Length + tolerance)
                        return false;

                    return StaticFunctions.EditDistance(cellValue, searchValue1) <= tolerance;

                }
                else if (operatorName == Constants.operator_Eq)
                {
                    return cellValue == searchValue1;
                }
                else if (operatorName == Constants.operator_Like)
                {
                    return StaticFunctions.IsSubstring(cellValue, searchValue1, false);
                }
                else if (operatorName == Constants.operator_NotEq)
                {
                    return cellValue != searchValue1;
                }
                else
                {
                    // Nothing else is supported at this point
                    return false;
                }
            }
            else
            {
                // nothing else is supported at this point
                return false;
            }


            return true;
        }

        private int[][] SanitizeSearchGrid()
        {
            // split search grid into valid OR blocks and return the partition
            // each element of the result is an array of ints representing a single OR block, 
            // where each array stores the row indices of the correspondig OR block

            // the result:: search grid is split into blocks divided by OR conjunctions
            // each invalid search row is thrown away from the result

            int[][] res = null;

            int start = -1; // start of a new block
            int blockN = 0; // N of OR blocks
            int blockSize = 0; // size of the current block
            int i = 0;


            while (i < dgvSearch.RowCount)
            {
                // first check if the row is valid; since grid values might be NULL, we use null coalescing operator ??
                if ((dgvSearch.Rows[i].Cells["conj"].Value ?? "").ToString() == "" ||
                    (dgvSearch.Rows[i].Cells["indexInGrid"].Value ?? "").ToString() == "" ||
                    (dgvSearch.Rows[i].Cells["colValType"].Value ?? "").ToString() == "" ||
                    (dgvSearch.Rows[i].Cells["operator"].Value ?? "").ToString() == "")
                {
                    i++;
                    continue; // this search row is invalid
                }
                else
                {
                    if (start == -1 || dgvSearch.Rows[i].Cells["conj"].Value.ToString() == Constants.ConjOr)
                    {
                        start = i;

                        blockN++;
                        Array.Resize(ref res, blockN);
                        res[blockN - 1] = new int[1];
                        res[blockN - 1][0] = i;

                        blockSize = 1;
                    }
                    else
                    {
                        // we extend the current block, which is the last one in the array of blocks

                        blockSize++;
                        Array.Resize(ref res[blockN - 1], blockSize);
                        res[blockN - 1][blockSize - 1] = i;
                    }
                }
                i++;
            }


            return res;
        }

        private bool MatchGridRowAgainstSearchQuery(int RowIndex, int[][] SearchBlocks)
        {
            // RowIndex is an index of a row in grid_ToSearch, which is the grid that this filter applies to
            // return True, if the search query is valid for that row

            // invalid or incomplete search rows will be ignored ( or are assumed to be true )

            for (int i = 0; i < SearchBlocks.Length; i++)
            {
                bool blockMatch = true; // true if the search block's criteria is satisfied by the RowIndex
                int j = 0;

                while (j < SearchBlocks[i].Length && blockMatch == true)
                {
                    int k = Convert.ToInt32(dgvSearch.Rows[SearchBlocks[i][j]].Cells["indexInGrid"].Value.ToString());
                    string cellValue = (dgvToBeSearchedMeta.dgv.Rows[RowIndex].Cells[k].Value ?? "").ToString();

                    if (CellDataMatchesWithSearch(cellValue, dgvSearch.Rows[SearchBlocks[i][j]].Cells["colValType"].Value.ToString(),
                        dgvSearch.Rows[SearchBlocks[i][j]].Cells["operator"].Value.ToString(),
                        dgvSearch.Rows[SearchBlocks[i][j]].Cells["searchVal"].Value.ToString(),
                        dgvSearch.Rows[SearchBlocks[i][j]].Cells["searchVal2"].Value.ToString()) == false)
                    {
                        blockMatch = false;
                    }
                    j++;
                }

                if (blockMatch)
                    return true; // one of the OR blocks is valid, hence the entire query
            }


            return false;

        }

        #endregion

        private void CreateGridofSearch()
        {
            dgvSearch.AllowUserToDeleteRows = false;
            dgvSearch.AllowUserToAddRows = false;
            dgvSearch.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvSearch.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvSearch.MultiSelect = false;

            dgvSearch.ContextMenuStrip = contextMenuStrip1;

            dgvSearch.Rows.Clear();
            dgvSearch.Columns.Clear();
            dgvSearch.ColumnCount = 8;

            int k = -1; //  use k below as an index of columns to allow easy rearrangment of columns if that will be necessary

            dgvSearch.Columns[++k].HeaderText = "Conjunction"; dgvSearch.Columns[k].Name = "conj";
            dgvSearch.Columns[++k].HeaderText = "Column name"; dgvSearch.Columns[k].Name = "colName";
            dgvSearch.Columns[++k].HeaderText = "Column value type"; dgvSearch.Columns[k].Name = "colValType";
            dgvSearch.Columns[++k].HeaderText = "Operator"; dgvSearch.Columns[k].Name = "operator";
            dgvSearch.Columns[++k].HeaderText = "Search value"; dgvSearch.Columns[k].Name = "searchVal";
            dgvSearch.Columns[++k].HeaderText = "Search value (additional)"; dgvSearch.Columns[k].Name = "searchVal2";

            dgvSearch.Columns[++k].HeaderText = "Index in grid"; dgvSearch.Columns[k].Name = "indexInGrid";
            dgvSearch.Columns[++k].HeaderText = "Name of the column in grid"; dgvSearch.Columns[k].Name = "colNameInGrid";

            dgvSearch.Columns["indexInGrid"].Visible = false;
            dgvSearch.Columns["colNameInGrid"].Visible = false;

            dgvSearch.RowHeadersWidth = 26;
            dgvSearch.ColumnHeadersHeight = 29;

            dgvSearch.Columns["conj"].Width = 100;
            dgvSearch.Columns["colName"].Width = 156;


            for (k = 0; k < dgvSearch.ColumnCount; k++)
            {
                dgvSearch.Columns[k].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (k > 1)
                    dgvSearch.Columns[k].Width = 100;
            }

            dgvSearch.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            dgvSearch.AllowUserToResizeColumns = true;
            dgvSearch.AllowUserToResizeRows = false;
            dgvSearch.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            AddNewSearchRowInGrid();
        }

        public void BuildandAddSearchRow(string conj, string colName, int colIndex, string colValType, string searchOperator, string searchVal1 = "", string searchVal2 = "", int searchValYesNo = 0)
        {
            // adds a new search row based on the data

            if (dgvSearch.Rows.Count == 0)
            {
                this.AddNewSearchRowInGrid();
            }

            int k = this.dgvSearch.Rows.Count - 1;
            if (this.dgvSearch.Rows[k].Cells["colName"].Value != null && this.dgvSearch.Rows[k].Cells["colName"].Value.ToString() != "")
            {
                this.AddNewSearchRowInGrid();
                k++;
            }

            // we will write on the k-th row
            if (this.dgvSearch.Rows[k].Cells["conj"].Value == null || this.dgvSearch.Rows[k].Cells["conj"].Value.ToString() != Constants.ConjDeafult)
            {
                this.dgvSearch.Rows[k].Cells["conj"].Value = conj;
            }
            /*
            colName
            colValType
         operator
         searchVal
         searchVal2
         indexInGrid
         colNameInGrid
         */

            this.dgvSearch.Rows[k].Cells["colName"].Value = colName;
            this.dgvSearch.Rows[k].Cells["colValType"].Value = colValType;

            ////////////////

            DataGridViewComboBoxCell ComboCell_1 = new DataGridViewComboBoxCell();
            ComboCell_1.DisplayStyleForCurrentCellOnly = true;

            // based on the value type decide which search operators to add
            StaticFunctions.PopulateOperatorComboBox(ComboCell_1, colValType);

            dgvSearch.Rows[k].Cells["operator"].Dispose();
            dgvSearch.Rows[k].Cells["operator"] = ComboCell_1;
            dgvSearch.Rows[k].Cells["operator"].Value = searchOperator;

            if (colValType == Constants.ValueType_Bool && (searchValYesNo == 1 || searchValYesNo == 2))
            {
                DataGridViewComboBoxCell ComboCell_2 = new DataGridViewComboBoxCell();
                ComboCell_2.DisplayStyleForCurrentCellOnly = true;
                ComboCell_2.Items.Add(Constants.ValueBool_True);
                ComboCell_2.Items.Add(Constants.ValueBool_False);

                dgvSearch.Rows[k].Cells["searchVal"].Dispose();
                dgvSearch.Rows[k].Cells["searchVal"] = ComboCell_2;
            }

            ///////////////////


            if (searchValYesNo == 1)
            {
                this.dgvSearch.Rows[k].Cells["searchVal"].Value = Constants.ValueBool_True;
            }
            else if (searchValYesNo == 2)
            {
                this.dgvSearch.Rows[k].Cells["searchVal"].Value = Constants.ValueBool_False;
            }
            else
            {
                this.dgvSearch.Rows[k].Cells["searchVal"].Value = searchVal1;
                this.dgvSearch.Rows[k].Cells["searchVal2"].Value = searchVal2;
            }

            dgvSearch.Rows[k].Cells["indexInGrid"].Value = dgvToBeSearchedMeta.GetColumnIndexAt(colIndex);
        }

        public void AddNewSearchRowFromDict(Dictionary<string, string> singleRow)
        {
            // the dictionary contains all column names as keys
            /*
            colName
            colValType
            operator
            searchVal
            searchVal2
            indexInGrid
            colNameInGrid
            */

            this.AddNewSearchRowInGrid();

            int k = this.dgvSearch.Rows.Count - 1;

            // adjusting the list of operators
            DataGridViewComboBoxCell ComboCell_1 = new DataGridViewComboBoxCell();
            ComboCell_1.DisplayStyleForCurrentCellOnly = true;

            // based on the value type decide which search operators to add
            StaticFunctions.PopulateOperatorComboBox(ComboCell_1, singleRow["colValType"]);

            dgvSearch.Rows[k].Cells["operator"].Dispose();
            dgvSearch.Rows[k].Cells["operator"] = ComboCell_1;

            // adjusting values for boolean type
            if (singleRow["colValType"] == Constants.ValueType_Bool)
            {
                DataGridViewComboBoxCell ComboCell_2 = new DataGridViewComboBoxCell();
                ComboCell_2.DisplayStyleForCurrentCellOnly = true;
                ComboCell_2.Items.Add(Constants.ValueBool_True);
                ComboCell_2.Items.Add(Constants.ValueBool_False);

                dgvSearch.Rows[k].Cells["searchVal"].Dispose();
                dgvSearch.Rows[k].Cells["searchVal"] = ComboCell_2;
            }

            for (int i = 0; i < dgvSearch.ColumnCount; ++i)
            {
                dgvSearch.Rows[k].Cells[i].Value = singleRow[dgvSearch.Columns[i].Name];
            }
        }

        public void AddNewSearchRowInGrid()
        {
            DataGridViewRow RowX = new DataGridViewRow(); //the template for rows

            if (dgvSearch.Rows.Count == 0)
            {
                RowX.Cells.Add(new DataGridViewTextBoxCell());
                RowX.Cells[0].Value = Constants.ConjDeafult;  // "IF"
            }
            else
            {
                DataGridViewComboBoxCell ComboCell_1 = new DataGridViewComboBoxCell();

                ComboCell_1.Items.Add(Constants.ConjAnd);
                ComboCell_1.Items.Add(Constants.ConjOr);
                ComboCell_1.Items.Add(Constants.ConjNot);

                ComboCell_1.DisplayStyleForCurrentCellOnly = true;

                RowX.Cells.Add(ComboCell_1);
            }


            DataGridViewComboBoxCell ComboCell_2 = new DataGridViewComboBoxCell();

            ComboCell_2.DisplayStyleForCurrentCellOnly = true;

            /*
            for (int k = 0; k < col_names.Length; k++)
            {
                ComboCell_2.Items.Add(col_names[k]);
            }
            */

            for (int k = 0; k < dgvToBeSearchedMeta.ColumnHeaderTextLength; k++)
                ComboCell_2.Items.Add(dgvToBeSearchedMeta.GetColumnHeaderTextAt(k));

            RowX.Cells.Add(ComboCell_2);


            DataGridViewComboBoxCell ComboCell_3 = new DataGridViewComboBoxCell();
            ComboCell_3.DisplayStyleForCurrentCellOnly = true;

            // populating the combobox of valueTypes
            ComboCell_3.Items.Add(Constants.ValueType_Bool);
            ComboCell_3.Items.Add(Constants.ValueType_Date);
            ComboCell_3.Items.Add(Constants.ValueType_Numeric);
            ComboCell_3.Items.Add(Constants.ValueType_String);

            RowX.Cells.Add(ComboCell_3);  //the value type of the column


            //DataGridViewComboBoxCell ComboCell_4 = new DataGridViewComboBoxCell();
            //ComboCell_4.DisplayStyleForCurrentCellOnly = true;
            //RowX.Cells.Add( ComboCell_4 );  //the search operator (will become a combo)

            RowX.Cells.Add(new DataGridViewTextBoxCell()); // the operator; will later become a combobox


            RowX.Cells.Add(new DataGridViewTextBoxCell());  //the searched value; this might become a combo, if the value type is Boolean
            RowX.Cells.Add(new DataGridViewTextBoxCell());  //the 2nd searched value;
            RowX.Cells.Add(new DataGridViewTextBoxCell());  //the column index in the actual grid
            RowX.Cells.Add(new DataGridViewTextBoxCell());  //the name of the column in the actual grid

            dgvSearch.Rows.Add(RowX);

            dgvSearch.ClearSelection();
            dgvSearch.Rows[dgvSearch.RowCount - 1].Cells["conj"].Selected = true;

            if (dgvSearch.RowCount > 1)
            {
                dgvSearch.Rows[dgvSearch.RowCount - 1].Cells["conj"].Value = Constants.ConjAnd;
            }

            ComboinGridCell = null; // we destroy the reference (if any) to the current combo representing grid's cell
        }

        private void MoveSearchRowDown(int RowIndex = -1)
        {
            // move the selected row 1 step down the search grid
            // method: copy the row into a new row, insert the copy at the position currentRow + 2 
            // then remove the current row
            // when doing so, take care of the 1-st column for conjunctions which is a combo box;

            // we allow an optional @RowIndex which can overwrite the selected row index

            int k = RowIndex;

            if (k == -1)
                k = dgvSearch.SelectedCells[0].RowIndex;

            if (k == dgvSearch.RowCount - 1)
                MessageBox.Show("The last row cannot be moved down", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                DataGridViewRow tmp = new DataGridViewRow();

                DataGridViewComboBoxCell ComboCell_1 = new DataGridViewComboBoxCell();

                ComboCell_1.Items.Add(Constants.ConjAnd);
                ComboCell_1.Items.Add(Constants.ConjOr);
                ComboCell_1.Items.Add(Constants.ConjNot);
                ComboCell_1.DisplayStyleForCurrentCellOnly = true;

                tmp.Cells.Add(ComboCell_1);

                for (int i = 1; i < dgvSearch.ColumnCount; i++)
                {
                    tmp.Cells.Add(dgvSearch.Rows[k].Cells[i].Clone() as DataGridViewCell);
                    tmp.Cells[i].Value = dgvSearch.Rows[k].Cells[i].Value;
                }

                tmp.Cells[0].Value = dgvSearch.Rows[k].Cells[0].Value;
                if (tmp.Cells[0].Value.ToString() == Constants.ConjDeafult)
                    tmp.Cells[0].Value = Constants.ConjAnd;


                dgvSearch.Rows.Insert(k + 2, tmp);
                dgvSearch.Rows.RemoveAt(k);

                if (k == 0)
                {
                    // since the 1st search row contains the default conjunction, the cell is not a combobox
                    // we replace it with an ordinary text box cell and recover the default value of the cell
                    dgvSearch.Rows[0].Cells[0].Dispose();
                    dgvSearch.Rows[0].Cells[0] = new DataGridViewTextBoxCell();
                    dgvSearch.Rows[0].Cells[0].Value = Constants.ConjDeafult;
                }

                txtSearch.Text = MakeQueryText();
                HighLight_SearchText();  // apply the highlight on search text
            }
        }

        private void MoveSearchRowUp(int RowIndex = -1)
        {
            // move the selected row 1 step UP the search grid
            // method: copy the row into a new row, insert the copy at the position currentRow - 2 
            // then remove the current row
            // when doing so, take care of the 1-st column for conjunctions which is a combo box

            // we allow an optional @RowIndex which can overwrite the selected row index

            int k = RowIndex;

            if (k == -1)
                k = dgvSearch.SelectedCells[0].RowIndex;

            if (k == 0)
                MessageBox.Show("The first row cannot be moved up", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                // @tmp is a copy of the k-th row, which needs to be moved up
                DataGridViewRow tmp = new DataGridViewRow();

                DataGridViewComboBoxCell ComboCell_1 = new DataGridViewComboBoxCell();

                ComboCell_1.Items.Add(Constants.ConjAnd);
                ComboCell_1.Items.Add(Constants.ConjOr);
                ComboCell_1.Items.Add(Constants.ConjNot);
                ComboCell_1.DisplayStyleForCurrentCellOnly = true;

                tmp.Cells.Add(ComboCell_1);

                for (int i = 1; i < dgvSearch.ColumnCount; i++)
                {
                    tmp.Cells.Add(dgvSearch.Rows[k].Cells[i].Clone() as DataGridViewCell);
                    tmp.Cells[i].Value = dgvSearch.Rows[k].Cells[i].Value;
                }

                tmp.Cells[0].Value = dgvSearch.Rows[k].Cells[0].Value;

                dgvSearch.Rows.Insert(k - 1, tmp);
                dgvSearch.Rows.RemoveAt(k + 1);

                if (k == 1)
                {
                    // since the 1st search row contains the default conjunction, the cell is not a combobox
                    // we replace it with an ordinary text box cell and recover the default value of the cell
                    dgvSearch.Rows[0].Cells[0].Dispose();
                    dgvSearch.Rows[0].Cells[0] = new DataGridViewTextBoxCell();
                    dgvSearch.Rows[0].Cells[0].Value = Constants.ConjDeafult;

                    DataGridViewComboBoxCell ComboCell_2 = new DataGridViewComboBoxCell();

                    ComboCell_2.Items.Add(Constants.ConjAnd);
                    ComboCell_2.Items.Add(Constants.ConjOr);
                    ComboCell_2.Items.Add(Constants.ConjNot);
                    ComboCell_2.DisplayStyleForCurrentCellOnly = true;
                    dgvSearch.Rows[k].Cells[0].Dispose();
                    dgvSearch.Rows[k].Cells[0] = ComboCell_2;
                    dgvSearch.Rows[k].Cells[0].Value = Constants.ConjAnd;
                }

                txtSearch.Text = MakeQueryText();
                HighLight_SearchText();  // apply the highlight on search text
            }
        }

        private void DeleteSearchRow(int RowIndex = -1)
        {
            int k = RowIndex;

            if (k == -1)
                k = dgvSearch.SelectedCells[0].RowIndex;

            if (k == 0)
                MessageBox.Show("The first row cannot be removed", Constants.msgWarning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                dgvSearch.Rows.RemoveAt(k);

                txtSearch.Text = MakeQueryText();
                HighLight_SearchText();  // apply the highlight on search text
            }
        }

        private void ClearSearchGrid(bool addDefaultRow = true)
        {
            this.dgvSearch.Rows.Clear();
            this.txtSearch.Text = "";
            if (addDefaultRow == true)
            { this.AddNewSearchRowInGrid(); }
        }

        private void ComboinGridCell_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboinGridCell == null) //safety check
                return;

            if (ComboinGridCell.Text == "")
                return;


            if (ComboinGridCell.Name == "colName")
            {
                // we attempt to set the type of the value and column index in grid_to_search           
                dgvSearch.Rows[dgvSearch.CurrentRow.Index].Cells["indexInGrid"].Value = dgvToBeSearchedMeta.GetColumnIndexAt(ComboinGridCell.SelectedIndex);
                dgvSearch.Rows[dgvSearch.CurrentRow.Index].Cells["colValType"].Value = dgvToBeSearchedMeta.GetColumnValueTypeAt(ComboinGridCell.SelectedIndex);

                DataGridViewComboBoxCell ComboCell_1 = new DataGridViewComboBoxCell();
                ComboCell_1.DisplayStyleForCurrentCellOnly = true;

                // based on the value type decide which search operators to add
                StaticFunctions.PopulateOperatorComboBox(ComboCell_1, dgvToBeSearchedMeta.GetColumnValueTypeAt(ComboinGridCell.SelectedIndex));

                dgvSearch.Rows[dgvSearch.CurrentRow.Index].Cells["operator"].Dispose();
                dgvSearch.Rows[dgvSearch.CurrentRow.Index].Cells["operator"] = ComboCell_1;

                if (dgvToBeSearchedMeta.GetColumnValueTypeAt(ComboinGridCell.SelectedIndex) == Constants.ValueType_Bool)
                {
                    DataGridViewComboBoxCell ComboCell_2 = new DataGridViewComboBoxCell();
                    ComboCell_2.DisplayStyleForCurrentCellOnly = true;
                    ComboCell_2.Items.Add(Constants.ValueBool_True);
                    ComboCell_2.Items.Add(Constants.ValueBool_False);

                    dgvSearch.Rows[dgvSearch.CurrentRow.Index].Cells["searchVal"].Dispose();
                    dgvSearch.Rows[dgvSearch.CurrentRow.Index].Cells["searchVal"] = ComboCell_2;
                }
                else
                {
                    dgvSearch.Rows[dgvSearch.CurrentRow.Index].Cells["searchVal"].Dispose();
                    dgvSearch.Rows[dgvSearch.CurrentRow.Index].Cells["searchVal"] = new DataGridViewTextBoxCell();
                }
            }

            else if (ComboinGridCell.Name == "colValType")
            {
                DataGridViewComboBoxCell ComboCell_1 = new DataGridViewComboBoxCell();
                ComboCell_1.DisplayStyleForCurrentCellOnly = true;

                // based on the value type decide which search operators to add
                StaticFunctions.PopulateOperatorComboBox(ComboCell_1, ComboinGridCell.Text);

                dgvSearch.Rows[dgvSearch.CurrentRow.Index].Cells["operator"].Dispose();
                dgvSearch.Rows[dgvSearch.CurrentRow.Index].Cells["operator"] = ComboCell_1;

                if (ComboinGridCell.Text == Constants.ValueType_Bool)
                {
                    DataGridViewComboBoxCell ComboCell_2 = new DataGridViewComboBoxCell();
                    ComboCell_2.DisplayStyleForCurrentCellOnly = true;
                    ComboCell_2.Items.Add(Constants.ValueBool_True);
                    ComboCell_2.Items.Add(Constants.ValueBool_False);

                    dgvSearch.Rows[dgvSearch.CurrentRow.Index].Cells["searchVal"].Dispose();
                    dgvSearch.Rows[dgvSearch.CurrentRow.Index].Cells["searchVal"] = ComboCell_2;
                }
                else
                {
                    dgvSearch.Rows[dgvSearch.CurrentRow.Index].Cells["searchVal"].Dispose();
                    dgvSearch.Rows[dgvSearch.CurrentRow.Index].Cells["searchVal"] = new DataGridViewTextBoxCell();
                }
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnAddNewSearchRow_Click(object sender, EventArgs e)
        {
            AddNewSearchRowInGrid();
        }

        private void dgvSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvSearch_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            /*
          Debug.WriteLine(" CellBeginEdit (col, row) = ( " +
            Convert.ToString(e.ColumnIndex) + ", " + Convert.ToString(e.RowIndex) + ")"
            + " current cell (col, row) =" +
            Convert.ToString(dgvSearch.CurrentCell.ColumnIndex) + ", " + Convert.ToString(dgvSearch.CurrentCell.RowIndex) + ")");
          */

            if (suspend_Edit_Checks == true)
            {
                // we reserve an option of manually suspending default behviour, 
                // in cases when we might load the grid rows from saved search file.
                return;
            }

            if ((e.RowIndex == 0) && (e.ColumnIndex == 0))
            {
                e.Cancel = true;
                return;
            }

            if ((dgvSearch.Rows[e.RowIndex].Cells["colName"].Value == null) ||
                (dgvSearch.Rows[e.RowIndex].Cells["colName"].Value.ToString() == ""))
            {
                if ((dgvSearch.Columns[e.ColumnIndex].Name == "colValType") ||
                    (dgvSearch.Columns[e.ColumnIndex].Name == "operator") ||
                    (dgvSearch.Columns[e.ColumnIndex].Name == "searchVal"))
                {
                    MessageBox.Show("Please select a column name first", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    e.Cancel = true;
                    return;
                }
            }
        }

        private void dgvSearch_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            /*
            Debug.WriteLine(" CellEndEdit (col, row) = ( " + 
                Convert.ToString(e.ColumnIndex) + ", " + Convert.ToString(e.RowIndex) + ")"
                + " current cell (col, row) =" +
                Convert.ToString(dgvSearch.CurrentCell.ColumnIndex) + ", " + Convert.ToString(dgvSearch.CurrentCell.RowIndex) + ")" );
            */

            if (suspend_Edit_Checks == true)
            {
                return;
            }

            ComboinGridCell = null;

            // update the search query text and apply the highlight
            txtSearch.Text = MakeQueryText();
            HighLight_SearchText();
        }

        private void dgvSearch_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Debug.WriteLine(" EditingControlShowing (col, row) = ( " + Convert.ToString(dgvSearch.CurrentCell.ColumnIndex) + ", " + Convert.ToString(dgvSearch.CurrentCell.RowIndex) + ") ");

            if (dgvSearch.CurrentCell.OwningColumn.Name == "colName")
            {
                ComboinGridCell = e.Control as ComboBox; //we need to cast this control as combo
                ComboinGridCell.Name = "colName";
                ComboinGridCell.SelectedIndexChanged += new EventHandler(ComboinGridCell_SelectedIndexChanged);

                // Debug.WriteLine (" -- combo is set -- with N of items = " + Convert.ToString( ComboinGridCell.Items.Count  )  );
            }
            else if (dgvSearch.CurrentCell.OwningColumn.Name == "colValType")
            {
                ComboinGridCell = e.Control as ComboBox; //we need to cast this control as combo
                ComboinGridCell.Name = "colValType";
                ComboinGridCell.SelectedIndexChanged += new EventHandler(ComboinGridCell_SelectedIndexChanged);
            }
            else
            {
                ComboinGridCell = null;
            }
        }

        private void BtnUp_Click(object sender, EventArgs e)
        {
            MoveSearchRowUp();
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            if (dgvToBeSearchedMeta.dgv.RowCount == 0)
            {
                MessageBox.Show("The datagrid is empty, cannot apply this filer on empty set of rows", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int[][] SearchBlocks = SanitizeSearchGrid();
            if (SearchBlocks == null || SearchBlocks.Length == 0)
            {
                MessageBox.Show("Cannot invoke the filter with an empty search query", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            FreezeControls(false);

            progressBar1.Value = 0;
            progressBar1.Visible = true;

            StopOnFirstMatch = false; // signals the background worker to NOT stop on finding a match

            backgroundWorker1.RunWorkerAsync();

        }

        private void BtnWizard_Click(object sender, EventArgs e)
        {
            /*
            FrmWizard F_Wizard = new FrmWizard(this.dgvToBeSearchedMeta, this);
            F_Wizard.ShowDialog();

            F_Wizard.Dispose();
            F_Wizard = null;
            */
        }

        private void BtnCommands_Click(object sender, EventArgs e)
        {
            Point btnPosition = PointToScreen(BtnCommands.Location);

            Point startPoint = new Point(btnPosition.X, btnPosition.Y - contextMenuStrip1.Height);

            contextMenuStrip1.Show(startPoint);
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            MoveSearchRowDown();
        }

        private void BtnStopFilter_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
        }

        private void BtnFindNext_Click(object sender, EventArgs e)
        {
            if (dgvToBeSearchedMeta.dgv.RowCount == 0)
            {
                MessageBox.Show("The datagrid is empty, cannot apply this filer on empty set of rows", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int[][] SearchBlocks = SanitizeSearchGrid();
            if (SearchBlocks == null || SearchBlocks.Length == 0)
            {
                MessageBox.Show("Cannot invoke the filter with an empty search query", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // check if the search grid was changed since we last called Find Next
            string s = this.EncodeGridToString();
            if (s != this.GridLastState)
            {
                this.GridLastState = s;
                ResetFindNextParams();
            }


            StopOnFirstMatch = true;  // signals the background worker process (of search) to stop on the first match

            FreezeControls(false);

            progressBar1.Value = 0;
            progressBar1.Visible = true;

            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // sanity checks and search blocks buildup
            if (dgvToBeSearchedMeta.dgv.RowCount == 0)
            {
                MessageBox.Show("The datagrid is empty, cannot apply this filer on empty set of rows", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int[][] SearchBlocks = SanitizeSearchGrid();
            if (SearchBlocks == null || SearchBlocks.Length == 0)
            {
                MessageBox.Show("Cannot invoke the filter with an empty search query", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            // end of sanity checks

            if (StopOnFirstMatch == false)  // The FILTER; 
            {
                UnMatchedRows = new List<int>();
                MatchCount = 0;

                for (int i = 0; i < dgvToBeSearchedMeta.dgv.RowCount; ++i)
                {
                    if (!backgroundWorker1.CancellationPending)
                    {
                        if (dgvToBeSearchedMeta.dgv.Rows[i].Visible == true)
                        {
                            if (MatchGridRowAgainstSearchQuery(i, SearchBlocks))
                                MatchCount++;
                            else
                                UnMatchedRows.Add(i);
                        }
                        backgroundWorker1.ReportProgress(Convert.ToInt32(100 * i / (dgvToBeSearchedMeta.dgv.RowCount)));
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }

            // =======================================================

            if (StopOnFirstMatch == true) // The FIND NEXT
            {
                LastMatched_Row = -1;
                for (int i = LastSearched_Row + 1; i < dgvToBeSearchedMeta.dgv.RowCount; ++i)
                {
                    if (!backgroundWorker1.CancellationPending)
                    {
                        if (dgvToBeSearchedMeta.dgv.Rows[i].Visible == true)
                        {
                            if (MatchGridRowAgainstSearchQuery(i, SearchBlocks))
                            {
                                LastMatched_Row = i;
                                LastSearched_Row = i;
                                //e.Cancel = true;  // terminate the search
                                backgroundWorker1.CancelAsync();
                            }
                        }
                        backgroundWorker1.ReportProgress(Convert.ToInt32(100 * i / (dgvToBeSearchedMeta.dgv.RowCount)));
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                //lblStatus.Text = "Process was cancelled";
                if (StopOnFirstMatch == false || (StopOnFirstMatch == true && LastMatched_Row == -1))
                {
                    MessageBox.Show("Process was cancelled by the user", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (e.Error != null)
            {
                //lblStatus.Text = "There was an error running the process. The thread aborted";
                MessageBox.Show("Process was aborted because of an error", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //lblStatus.Text = "Process was completed";
                //MessageBox.Show("Process was completed successfully", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            progressBar1.Value = 0;
            progressBar1.Visible = false;

            FreezeControls(true);

            if (StopOnFirstMatch == false) // the FILTER results
            {
                if (MatchCount == 0)
                    MessageBox.Show("Nothing was found", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    MessageBox.Show("Found " + Convert.ToInt32(MatchCount) + " records matching the query", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // in the grid on which the search was run, hide all rows that did not match the query

                    this.Cursor = Cursors.WaitCursor;

                    CurrencyManager currencyManager1 = null;

                    if (dgvToBeSearchedMeta.dgv.DataSource != null)
                    {
                        currencyManager1 = (CurrencyManager)BindingContext[dgvToBeSearchedMeta.dgv.DataSource];
                        currencyManager1.SuspendBinding();
                    }

                    for (int i = 0; i < UnMatchedRows.Count; ++i)
                    {
                        dgvToBeSearchedMeta.dgv.Rows[UnMatchedRows[i]].Visible = false;
                    }

                    if (dgvToBeSearchedMeta.dgv.DataSource != null)
                        currencyManager1.ResumeBinding();

                    this.Cursor = Cursors.Default;
                }

                //MessageBox.Show("Found " + Convert.ToString( MatchedRows == null?0:MatchedRows.Count) + " matches in the datagrid", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // ===============================================

            if (StopOnFirstMatch == true) // the FIND NEXT results
            {
                // coming from Find Next action, need to select the matching row in the grid to be searched.
                if (LastMatched_Row == -1)
                {
                    if (LastSearched_Row != -1)
                    {
                        MessageBox.Show("The search has reached the end of the grid", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.LastSearched_Row = -1;
                    }
                    else
                    {
                        MessageBox.Show("Nothing was found", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // select the matched row and highlight the first visible column so the cursor will be moved to that row
                    dgvToBeSearchedMeta.dgv.CurrentCell = dgvToBeSearchedMeta.dgv.Rows[LastMatched_Row].Cells[dgvToBeSearchedMeta.FirstVisibleColumnIndex];
                    dgvToBeSearchedMeta.dgv.Rows[LastMatched_Row].Selected = true;
                }
            }

        }

        private void MenuWizard_Click(object sender, EventArgs e)
        {
            BtnWizard_Click(sender, e);
        }

        private void MenuSaveSearch_Click(object sender, EventArgs e)
        {
            string s = this.EncodeGridToString();

            List<Dictionary<string, string>> res = this.EncodeGridToListofDict();

            //saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Filter = "Grid Query Files|*.gridQ";
            saveFileDialog1.Title = "Save the search query to a file";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                //System.IO.File.WriteAllText( saveFileDialog1.FileName, s);
                //System.IO.Stream sw = System.IO.File.Open(saveFileDialog1.FileName + "_1", System.IO.FileMode.CreateNew);
                System.IO.Stream sw = System.IO.File.Open(saveFileDialog1.FileName, System.IO.FileMode.OpenOrCreate);


                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(sw, res);

                sw.Close();

                MessageBox.Show("The search query was saved successfully", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void MenuLoadFromFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "Grid Query Files|*.gridQ";
                openFileDialog1.Title = "Choose a search file to load in grid";
                openFileDialog1.ShowDialog();

                if (this.openFileDialog1.FileName != "")
                {
                    var ans = this.DecodeGridDictFromBinary(openFileDialog1.FileName);

                    // check if the search file is not corrputed
                    foreach (Dictionary<string, string> singleRow in ans)
                    {
                        if (singleRow.Count != this.dgvSearch.Columns.Count)
                        {
                            throw new Exception("The search file is not in a correct format.");
                        }

                        // the column count is the same as the dicitionary elements; so we will simply exhaust the columns
                        // ignoring a possible mixing of the order

                        for (int k = 0; k < this.dgvSearch.ColumnCount; ++k)
                        {
                            if (singleRow.ContainsKey(this.dgvSearch.Columns[k].Name) == false)
                                throw new Exception("The search file is not in a correct format.");
                            if (dgvSearch.Columns[k].Name == "conj")
                            {
                                string val = singleRow["conj"];
                                if (DGV_SearchMeta.IsConjText(val) == false)
                                    throw new Exception("The search file is not in a correct format.");
                            }
                            if (dgvSearch.Columns[k].Name == "colName")
                            {
                                string val = singleRow["colName"];
                                if (this.dgvToBeSearchedMeta.HasColumnNamed(val) == false)
                                    throw new Exception("The search file is not in a correct format.");
                            }
                            if (dgvSearch.Columns[k].Name == "colIndex")
                            {
                                string val = singleRow["colIndex"];
                                int x = -1;
                                if (Int32.TryParse(val, out x) == false || (x < 0) || (x >= this.dgvToBeSearchedMeta.ColumnIndexLength))
                                {
                                    throw new Exception("The search file is not in a correct format.");
                                }
                            }
                            if (dgvSearch.Columns[k].Name == "colValType")
                            {
                                string val = singleRow["colValType"];
                                if (DGV_SearchMeta.IsValueType(val) == false)
                                    throw new Exception("The search file is not in a correct format.");
                            }
                            if (dgvSearch.Columns[k].Name == "searchOperator")
                            {
                                string val = singleRow["searchOperator"];
                                if (DGV_SearchMeta.IsOperator(val) == false)
                                    throw new Exception("The search file is not in a correct format.");

                                string val2 = singleRow["colValType"];
                                if (val2 == Constants.ValueType_Bool && (val != Constants.operator_Eq && val != Constants.operator_NotEq))
                                    throw new Exception("The search file is not in a correct format.");
                            }
                            if (dgvSearch.Columns[k].Name == "searchVal1")
                            {
                                string val = singleRow["searchVal1"];
                                string val2 = singleRow["colValType"];
                                if (val2 == Constants.ValueType_Bool && (val != Constants.ValueBool_True && val != Constants.ValueBool_False))
                                    throw new Exception("The search file is not in a correct format.");
                            }
                        }
                    }

                    // populate the grid anew from the search file dictionary
                    this.ClearSearchGrid(addDefaultRow: false);
                    foreach (Dictionary<string, string> singleRow in ans)
                    {
                        AddNewSearchRowFromDict(singleRow);
                    }

                    UpdateQueryText();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The following error occured.\n" + ex.Message, Constants.msgError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void MenuClearAll_Click(object sender, EventArgs e)
        {
            this.ClearSearchGrid();
        }

        private void MenuMoveDown_Click(object sender, EventArgs e)
        {
            MoveSearchRowDown();
        }

        private void MenuMoveUp_Click(object sender, EventArgs e)
        {
            MoveSearchRowUp();
        }

        private void MenuNewSearchRow_Click(object sender, EventArgs e)
        {
            AddNewSearchRowInGrid();
        }

        private void MenuDeleteSearchRow_Click(object sender, EventArgs e)
        {
            DeleteSearchRow();
        }

        private void MenuPasteSearchRow_Click(object sender, EventArgs e)
        {
            if (CopiedSearchRow == null)
                MessageBox.Show("The clipboard is empty, cannot paste anything", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                dgvSearch.Rows.Add(CopiedSearchRow.Clone() as DataGridViewRow);
                for (int i = 0; i < dgvSearch.ColumnCount; i++)
                    dgvSearch.Rows[dgvSearch.RowCount - 1].Cells[i].Value = CopiedSearchRow.Cells[i].Value;

                txtSearch.Text = MakeQueryText();
                HighLight_SearchText();  // apply the highlight on search text
            }
        }

        private void MenuCopySearchRow_Click(object sender, EventArgs e)
        {
            int k = dgvSearch.SelectedCells[0].RowIndex;

            CopiedSearchRow = new DataGridViewRow();

            DataGridViewComboBoxCell ComboCell_1 = new DataGridViewComboBoxCell();

            ComboCell_1.Items.Add("AND");
            ComboCell_1.Items.Add("OR");
            ComboCell_1.Items.Add("NOT");
            ComboCell_1.DisplayStyleForCurrentCellOnly = true;

            CopiedSearchRow.Cells.Add(ComboCell_1);

            for (int i = 1; i < dgvSearch.ColumnCount; ++i)
            {
                CopiedSearchRow.Cells.Add(dgvSearch.Rows[k].Cells[i].Clone() as DataGridViewCell);
                CopiedSearchRow.Cells[i].Value = dgvSearch.Rows[k].Cells[i].Value;
            }

            CopiedSearchRow.Cells[0].Value = dgvSearch.Rows[k].Cells[0].Value;
            if (CopiedSearchRow.Cells[0].Value.ToString() == "IF")
                CopiedSearchRow.Cells[0].Value = "AND";

        }

        private void MenuFilter_Click(object sender, EventArgs e)
        {
            BtnFilter_Click(sender, e);
        }

        private void MenuFindNext_Click(object sender, EventArgs e)
        {
            BtnFindNext_Click(sender, e);
        }
    }
}
