using System.Windows.Forms;

namespace DataGridView_withQuery
{
    public class DataGridView_withQuery : DataGridView
    {
        private FrmSimple_Search searchSimple_Form = null;
        private FrmAdvanced_Search advancedSearch_Form = null;

        ~DataGridView_withQuery()
        {
            // the destructor must close the search forms if they are open
            if (this.searchSimple_Form != null && this.searchSimple_Form.IsDisposed)
            {
                this.searchSimple_Form.Close();
            }

            if (this.advancedSearch_Form != null && this.advancedSearch_Form.IsDisposed)
            {
                this.advancedSearch_Form.Close();
            }
        }


        private DataGridView GetBase()
        {
            return (DataGridView)this;  // this is necessary for the search forms
        }

        public void SearchSimpleStart()
        {
            if (this.searchSimple_Form == null || this.searchSimple_Form.IsDisposed)
            {
                //this.searchSimple_Form = new FrmSimple_Search(GetBase(), "Test grid");
            }

            this.searchSimple_Form.Show();
        }


    }
}
