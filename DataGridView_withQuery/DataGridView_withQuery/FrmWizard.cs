/*
  Author: Hayk Aleksanyan
  email:  hayk.aleksanyan@gmail.com
  web:    https://github.com/hayk314
*/

using System;
using System.Windows.Forms;

namespace DataGridView_withQuery
{
    public partial class FrmWizard : Form
    {
        private FrmAdvanced_Search searchForm = null;
        private DGV_SearchMeta dgvToBeSearchedMeta = null;
        private int currentStep = 0;

        private QueryDeatils queryMeta;

        private struct QueryDeatils
        {
            public string conj;
            public string colName;
            public string searchCondition;
            public string searchValue1;
            public string searchValue2;

            public void ResetValues()
            {
                conj = Constants.ConjAnd;
                colName = "";
                searchCondition = "";
                searchValue1 = "";
                searchValue2 = "";
            }

            public string ToQueryText()
            {
                // creates a query text based on the user input
                string s = "";

                if (colName == "")
                    return "";

                if (this.conj != Constants.ConjAnd)
                {
                    s = this.conj + s + " ";
                }
                s += "<" + this.colName + ">";

                switch (this.searchCondition)
                {
                    case Constants.operator_Eq:
                        s += " equals to '" + this.searchValue1 + "'";
                        break;
                    case Constants.operator_GEq:
                        s += " greater or equal " + this.searchValue1;
                        break;
                    case Constants.operator_LEq:
                        s += " less or equal " + this.searchValue1;
                        break;
                    case Constants.operator_Like:
                        s += " is Like '" + this.searchValue1 + "'";
                        break;
                    case Constants.operator_NotEq:
                        s += " is not equal to '" + this.searchValue1 + "'";
                        break;
                    case Constants.operator_Between:
                        s += " is between ";
                        s += "'" + this.searchValue1 + "'";
                        s += " and '" + this.searchValue2 + "'";
                        break;
                    case Constants.operator_Edit_Distance:
                        s += " is within edit distance of ";
                        s += this.searchValue2;
                        s += " from '" + this.searchValue1 + "'";
                        break;
                    default:
                        break;
                }

                return s;
            }

        }

        // constructors
        public FrmWizard()
        {
            InitializeComponent();
        }

        public FrmWizard(DataGridView dgv, string title)
        {
            InitializeComponent();
            this.dgvToBeSearchedMeta = new DGV_SearchMeta(dgv, title);
            this.FillGridColumnNames();
        }

        public FrmWizard(DGV_SearchMeta dgvSearchMeta)
        {
            InitializeComponent();
            this.dgvToBeSearchedMeta = new DGV_SearchMeta(dgvSearchMeta);

            this.FillGridColumnNames();
        }

        public FrmWizard(DGV_SearchMeta dgvSearchMeta, FrmAdvanced_Search searchForm)
        {
            InitializeComponent();
            this.searchForm = searchForm;
            this.dgvToBeSearchedMeta = new DGV_SearchMeta(dgvSearchMeta);

            this.FillGridColumnNames();
        }

        // end of constructors

        private void FillGridColumnNames()
        {
            // fill in the column names of the grid into the ListGridColumns

            this.ListGridColumns.Items.Clear();

            if (this.dgvToBeSearchedMeta != null && this.dgvToBeSearchedMeta.ColumnHeaderTextLength > 0)
            {
                for (int i = 0; i < this.dgvToBeSearchedMeta.ColumnHeaderTextLength; ++i)
                    this.ListGridColumns.Items.Add(this.dgvToBeSearchedMeta.GetColumnHeaderTextAt(i));
            }
        }

        private void FrmWizard_Load(object sender, EventArgs e)
        {
            ActivePanel(0);

            radioBtnYes.Text = Constants.ValueBool_True;
            radioBtnNo.Text = Constants.ValueBool_False;

            radioBtnAnd.Text = Constants.ConjAnd;
            radioBtnNot.Text = Constants.ConjNot;
            radioBtnOr.Text = Constants.ConjOr;

            // value types
            ComboDataType.Items.Add(Constants.ValueType_Bool);
            ComboDataType.Items.Add(Constants.ValueType_Date);
            ComboDataType.Items.Add(Constants.ValueType_Numeric);
            ComboDataType.Items.Add(Constants.ValueType_String);

            PlaceControls();
            this.CenterToScreen();

            this.queryMeta = new QueryDeatils();
            queryMeta.ResetValues();

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PlaceControls()
        {
            Panel_Step1.Left = Panel_Step0.Left;
            Panel_Step1.Top = Panel_Step0.Top;
            Panel_Step1.Height = Panel_Step0.Height;
            Panel_Step1.Width = Panel_Step0.Width;

            Panel_Step2.Left = Panel_Step0.Left;
            Panel_Step2.Top = Panel_Step0.Top;
            Panel_Step2.Height = Panel_Step0.Height;
            Panel_Step2.Width = Panel_Step0.Width;

            BtnBack.Left = Panel_Step0.Left;
            //BtnBack.Top = Panel_Step0.Top + Panel_Step0.Height + 10;
            BtnBack.Top = TxtSearchQuery.Top + TxtSearchQuery.Height + 10;
            BtnNext.Left = BtnBack.Left + BtnBack.Width + 5;
            BtnNext.Top = BtnBack.Top;

            BtnExit.Top = BtnBack.Top;
            BtnExit.Left = Panel_Step0.Left + Panel_Step0.Width - BtnExit.Width;

            this.Width = Panel_Step0.Width + 2 * Panel_Step0.Left + 20;
            this.Height = BtnBack.Top + BtnBack.Height + 50;
        }


        private void ActivePanel(int i)
        {
            if (i < 0 || i > 2)
                return;

            this.Panel_Step0.Visible = (i == 0);
            this.Panel_Step1.Visible = (i == 1);
            this.Panel_Step2.Visible = (i == 2);
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (this.currentStep == 0)
            {
                if (this.ListGridColumns.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a column from the list on which the search query will run", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                int k = ComboDataType.FindStringExact(dgvToBeSearchedMeta.GetColumnValueTypeAt(this.ListGridColumns.SelectedIndex));
                if (k != ComboDataType.SelectedIndex)
                {
                    ComboDataType.SelectedIndex = k;
                    ComboSearchCondition.Items.Clear();
                    StaticFunctions.PopulateOperatorComboBox(ComboSearchCondition, ComboDataType.Text);
                }

                ActivePanel(1);
            }
            else if (this.currentStep == 1)
            {
                if (this.ComboDataType.Text == "")
                {
                    MessageBox.Show("Please choose the data type of the column", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (this.ComboSearchCondition.Text == "")
                {
                    MessageBox.Show("Please choose the search column condition", Constants.msgAttention, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                ActivePanel(2);
            }
            else if (this.currentStep == 2)
            {
                ActivePanel(0);

                // this is the last step, move the collected results to searchgrid
                string conj = this.radioBtnAnd.Text;
                if (this.radioBtnNot.Checked == true)
                {
                    conj = radioBtnNot.Text;
                }
                else if (this.radioBtnOr.Checked == true)
                {
                    conj = radioBtnOr.Text;
                }
                this.searchForm.BuildandAddSearchRow(conj, this.ListGridColumns.SelectedItem.ToString(),
                    ListGridColumns.SelectedIndex, ComboDataType.Text, ComboSearchCondition.Text,
                    TxtSearchVal1.Text, TxtSearchVal2.Text, this.ComboDataType.Text == Constants.ValueType_Bool ? 1 : 0);

                this.searchForm.UpdateQueryText();  // update the query text of the searchForm
                this.queryMeta.ResetValues();
                if (ListGridColumns.SelectedIndex > -1)
                {
                    queryMeta.colName = ListGridColumns.SelectedItem.ToString();
                }
                this.TxtSearchQuery.Text = queryMeta.ToQueryText();
            }


            this.currentStep++;
            if (this.currentStep == 3)
            {
                this.currentStep = 0;
                BtnBack.Enabled = false;
            }
            else
            {
                BtnBack.Enabled = true;
            }

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (this.currentStep == 0)
            {
                return;
            }
            else if (this.currentStep == 1)
            {
                ActivePanel(0);
            }
            else if (this.currentStep == 2)
            {
                ActivePanel(1);
            }

            this.currentStep--;
            if (this.currentStep == 0)
            {
                BtnBack.Enabled = false;
            }
        }

        private void ComboDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string searchCondition_old = "";

            if (ComboSearchCondition.SelectedIndex > -1)
                searchCondition_old = ComboSearchCondition.SelectedText;

            ComboSearchCondition.Items.Clear();
            StaticFunctions.PopulateOperatorComboBox(ComboSearchCondition, ComboDataType.Text);

            if (searchCondition_old != "")
            {
                ComboSearchCondition.SelectedIndex = ComboDataType.FindStringExact(searchCondition_old);
            }


            if (ComboDataType.Text == Constants.ValueType_Bool)
            {
                radioBtnYes.Visible = true;
                radioBtnYes.Left = TxtSearchVal1.Left;
                radioBtnYes.Top = TxtSearchVal1.Top;

                radioBtnNo.Visible = true;
                radioBtnNo.Left = radioBtnYes.Left + radioBtnYes.Width + 10;
                radioBtnNo.Top = radioBtnYes.Top;

                TxtSearchVal1.Visible = false;
                TxtSearchVal2.Visible = false;
                Label_SearchValue2.Visible = false;
            }
            else
            {
                TxtSearchVal1.Visible = true;
                TxtSearchVal2.Visible = (ComboSearchCondition.Text == Constants.operator_Between);
                Label_SearchValue2.Visible = (ComboSearchCondition.Text == Constants.operator_Between);
                radioBtnYes.Visible = false;
                radioBtnNo.Visible = false;
            }

            queryMeta.searchCondition = ComboSearchCondition.Text;
            if (ComboSearchCondition.Text == "")
            {
                this.queryMeta.searchValue1 = "";
                this.queryMeta.searchValue2 = "";
            }
            if (ComboDataType.Text == Constants.ValueType_Bool)
            {
                this.queryMeta.searchValue1 = (this.radioBtnYes.Checked == true ? radioBtnYes.Text : radioBtnNo.Text);
                this.queryMeta.searchValue2 = "";
            }
            this.TxtSearchQuery.Text = this.queryMeta.ToQueryText();

        }

        private void ComboSearchCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboSearchCondition.Text == Constants.operator_Between || ComboSearchCondition.Text == Constants.operator_Edit_Distance)
            {
                TxtSearchVal2.Visible = true;
                Label_SearchValue2.Visible = true;
            }
            else
            {
                TxtSearchVal2.Visible = false;
                Label_SearchValue2.Visible = false;
            }
            

            this.queryMeta.searchCondition = ComboSearchCondition.Text;
            if (TxtSearchVal2.Visible == false)
                this.queryMeta.searchValue2 = "";

            this.TxtSearchQuery.Text = this.queryMeta.ToQueryText();

        }

        private void ListGridColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.queryMeta.colName = ListGridColumns.SelectedItem.ToString();
            this.TxtSearchQuery.Text = this.queryMeta.ToQueryText();
        }

        private void radioBtnAnd_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnAnd.Checked == true)
            {
                this.queryMeta.conj = radioBtnAnd.Text;
                this.TxtSearchQuery.Text = this.queryMeta.ToQueryText();
            }
        }

        private void radioBtnOr_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnOr.Checked == true)
            {
                this.queryMeta.conj = radioBtnOr.Text;
                this.TxtSearchQuery.Text = this.queryMeta.ToQueryText();
            }
        }

        private void radioBtnNot_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnNot.Checked == true)
            {
                this.queryMeta.conj = radioBtnNot.Text;
                this.TxtSearchQuery.Text = this.queryMeta.ToQueryText();
            }
        }

        private void TxtSearchVal1_TextChanged(object sender, EventArgs e)
        {
            this.queryMeta.searchValue1 = TxtSearchVal1.Text;
            this.TxtSearchQuery.Text = this.queryMeta.ToQueryText();
        }

        private void TxtSearchVal2_TextChanged(object sender, EventArgs e)
        {
            this.queryMeta.searchValue2 = TxtSearchVal2.Text;
            this.TxtSearchQuery.Text = this.queryMeta.ToQueryText();
        }

        private void radioBtnYes_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnYes.Checked == true)
            {
                this.queryMeta.searchValue1 = radioBtnYes.Text;
                this.queryMeta.searchValue2 = "";
                this.TxtSearchQuery.Text = this.queryMeta.ToQueryText();
            }
        }

        private void radioBtnNo_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnNo.Checked == true)
            {
                this.queryMeta.searchValue1 = radioBtnNo.Text;
                this.queryMeta.searchValue2 = "";
                this.TxtSearchQuery.Text = this.queryMeta.ToQueryText();
            }
        }
    }
}
