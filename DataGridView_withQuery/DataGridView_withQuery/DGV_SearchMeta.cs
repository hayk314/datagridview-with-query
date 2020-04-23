using System;
using System.Linq;

using System.Windows.Forms;


namespace DataGridView_withQuery
{
    class DGV_SearchMeta
    {
        public DataGridView dgv;

        public string title = "";     // the title of the grid to be searched

        // used for searching the grid -- refer to the grid_ToSearch
        private int[] col_indices = null;         // real indicies of the columns of the grid_ToSearch (takes into account that some columns might be hidden) 
        private string[] col_names = null;        // the real names of the columns of the grid_ToSearch 
        private string[] col_headerTexts = null;  // header names of the columns to be displayed in combo boxes
        private string[] col_valueTypes = null;   // 4 fixed types (see constants) Boolean, Date, Numeric, Stirng

        // constructors
        public DGV_SearchMeta()
        {
            this.dgv = null;
            this.title = "";

            this.CleanColumnInfo();
        }

        public DGV_SearchMeta(DataGridView dgv, string title)
        {
            this.dgv = dgv;
            this.title = title;

            this.ReadGridColumns();
        }

        // copy constructor
        public DGV_SearchMeta(DGV_SearchMeta oldSample)
        {
            this.dgv = oldSample.dgv;
            this.title = oldSample.title;
            this.ReadGridColumns();
        }

        // ==================  end of constructors ==================

        public int GetColumnIndexAt(int k)
        {
            if ((this.col_indices == null) || (k < 0) || (k >= this.col_indices.Length))
            {
                throw new System.IndexOutOfRangeException("The index column was not initialized or the index is out of its range");
            }

            return this.col_indices[k];
        }

        public int ColumnIndexLength // property
        { get; private set; } = 0;

        public string GetColumnNameAt(int k)
        {
            if ((this.col_names == null) || (k < 0) || (k >= this.col_names.Length))
            {
                throw new System.IndexOutOfRangeException("The name column was not initialized or the index is out of its range");
            }

            return this.col_names[k];
        }

        public int ColumnNameLength // property
        { get; private set; } = 0;

        public string GetColumnHeaderTextAt(int k)
        {
            if ((this.col_headerTexts == null) || (k < 0) || (k >= this.col_headerTexts.Length))
            {
                throw new System.IndexOutOfRangeException("The header text column was not initialized or the index is out of its range");
            }

            return this.col_headerTexts[k];
        }

        public int ColumnHeaderTextLength // property
        { get; private set; } = 0;

        public string GetColumnValueTypeAt(int k)
        {
            if ((this.col_valueTypes == null) || (k < 0) || (k >= this.col_valueTypes.Length))
            {
                throw new System.IndexOutOfRangeException("The value type column was not initialized or the index is out of its range");
            }

            return this.col_valueTypes[k];
        }

        public int ColumnValueTypeLength // property
        { get; private set; } = 0;

        public int FirstVisibleColumnIndex   // property
        { get; private set; } = -1;

        // ======================= santiy checks ===========================
        public bool HasColumnNamed(string colName)
        {
            for (int k = 0; k < this.col_names.Length; ++k)
            {
                if (this.col_names[k] == colName)
                    return true;
            }
            return false;
        }
        public static bool IsConjText(string s)
        {
            return Constants.List_of_Conj.Contains(s);
        }
        public static bool IsValueType(string s)
        {
            return Constants.List_of_ValueTypes.Contains(s);
        }
        public static bool IsOperator(string s)
        {
            return Constants.List_of_Operators.Contains(s);
        }
        // ==================================================================
        private void CleanColumnInfo()
        {
            this.col_indices = null;
            this.col_names = null;
            this.col_headerTexts = null;
            this.col_valueTypes = null;
            this.FirstVisibleColumnIndex = -1;
        }

        private void ReadGridColumns()
        {
            // reads the column names of the datagridview

            this.CleanColumnInfo();

            if (this.dgv == null || dgv.ColumnCount == 0)
            {
                return;
            }

            string s = "";
            int u = 0;   // will count only visible columns

            for (int k = 0; k < this.dgv.ColumnCount; k++)
            {
                if (this.dgv.Columns[k].Visible)
                {
                    u++;

                    if (this.FirstVisibleColumnIndex == -1) { this.FirstVisibleColumnIndex = k; }

                    Array.Resize(ref col_indices, u);
                    col_indices[u - 1] = k;

                    Array.Resize(ref col_names, u);
                    col_names[u - 1] = this.dgv.Columns[k].Name;

                    Array.Resize(ref col_headerTexts, u);
                    col_headerTexts[u - 1] = (this.dgv.Columns[k].HeaderText == "") ? this.dgv.Columns[k].Name : this.dgv.Columns[k].HeaderText;

                    Array.Resize(ref col_valueTypes, u);
                    s = this.dgv.Columns[k].ValueType.Name.ToLower();

                    if (StaticFunctions.IsSubstring(s, Constants.types_Bool))
                        col_valueTypes[u - 1] = Constants.ValueType_Bool;
                    else if (StaticFunctions.IsSubstring(s, Constants.types_DateTime))
                        col_valueTypes[u - 1] = Constants.ValueType_Date;
                    else if (StaticFunctions.IsSubstring(s, Constants.types_Numeric))
                        col_valueTypes[u - 1] = Constants.ValueType_Numeric;
                    else
                        col_valueTypes[u - 1] = Constants.ValueType_String;
                }
            }

            // setting the property values
            ColumnValueTypeLength = this.col_valueTypes.Length;
            ColumnHeaderTextLength = this.col_headerTexts.Length;
            ColumnNameLength = this.col_names.Length;
            ColumnIndexLength = this.col_indices.Length;
        }

    }
}
