/*
  Author: Hayk Aleksanyan
  email:  hayk.aleksanyan@gmail.com
  web:    https://github.com/hayk314
*/

using System.Collections.Generic;
using System.Windows.Forms;

namespace DataGridView_withQuery
{
    public class DataGridView_withQuery : DataGridView
    {
        private FrmSimple_Search searchSimple_Form = null;
        private FrmAdvanced_Search searchAdvanced_Form = null;

        private ComboBox cmbSearch_PrevState = null;                               // stores the previous state of the FrmSimple_Search cmbSearch
        private List<Dictionary<string, string>> advancedSearch_PrevState = null;  // stores the previous state of the FrmAdvanced_Search searchgrid

        public DataGridView_withQuery()
        {
            this.cmbSearch_PrevState = new ComboBox();
            this.advancedSearch_PrevState = new List<Dictionary<string, string>>();
        }


        ~DataGridView_withQuery()
        {
            // the destructor must close the search forms if they are open
            if (this.searchSimple_Form != null && this.searchSimple_Form.IsDisposed)
            {
                this.searchSimple_Form.Close();
            }

            if (this.searchAdvanced_Form != null && this.searchAdvanced_Form.IsDisposed)
            {
                this.searchAdvanced_Form.Close();
            }
        }        

        private DataGridView GetBase()
        {
            return (DataGridView)this;  // this is necessary for the search forms
        }

        /// <summary>
        /// A string to be used in the caption of search forms.
        /// </summary>
        public string SearchFormTitle // the title to be passed on to search forms
        { get; set; } = "";


        //private ListBindingConverter<>

        public void SearchSimpleStart()
        {
            var dgv = GetBase();
            if (dgv == null || dgv.ColumnCount == 0 || dgv.RowCount == 0)
            {
                MessageBox.Show("Cannot perform search on empty datagrid", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (this.searchSimple_Form == null || this.searchSimple_Form.IsDisposed)
            {
                this.searchSimple_Form = new FrmSimple_Search(dgv, this.SearchFormTitle, cmbSearch_PrevState:this.cmbSearch_PrevState);
            }
            this.searchSimple_Form.Show();
        }

        public void SearchAdvancedStart()
        {
            var dgv = GetBase();
            if (dgv == null || dgv.ColumnCount == 0 || dgv.RowCount == 0)
            {
                MessageBox.Show("Cannot perform search on empty datagrid", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (this.searchAdvanced_Form == null || this.searchAdvanced_Form.IsDisposed)
            {
                this.searchAdvanced_Form = new FrmAdvanced_Search(dgv, this.SearchFormTitle, this.advancedSearch_PrevState);
            }
            this.searchAdvanced_Form.Show();
        }

    }
}
