/*
  Author: Hayk Aleksanyan
  email:  hayk.aleksanyan@gmail.com
  web:    https://github.com/hayk314
*/

using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public OleDbConnection cnn = new OleDbConnection();
        public string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C://github//datagridview-with-query//Test//sampleDB.mdb";


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.cnn.ConnectionString = this.connString;
            this.cnn.Open();


            this.Datagrid_1.AllowUserToAddRows = false;
            this.Datagrid_1.AllowUserToDeleteRows = false;
            this.Datagrid_1.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.Datagrid_1.SelectionMode = DataGridViewSelectionMode.CellSelect;

            this.Datagrid_1.Rows.Clear();
            this.Datagrid_1.Columns.Clear();

            this.Datagrid_1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing; //fix the header row
            this.Datagrid_1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // fix the leftmost column

            Form1_Resize(sender, e);

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Datagrid_1.Left = btnLoad.Left;
            this.Datagrid_1.Top = btnLoad.Top + btnLoad.Height + 10;
            this.Datagrid_1.Width = Width - Datagrid_1.Left - 30;
            this.Datagrid_1.Height = Height - Datagrid_1.Top - 50;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.cnn != null && this.cnn.State == ConnectionState.Open )
            {
                cnn.Close();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            int RecCount = 0;
            int u = 0;

            string strSQL = "SELECT Count(Panama.ID) AS CountOfRows FROM Panama where Panama.ID<= " + this.txtDataRecords.Text;

            using (OleDbCommand cmd = new OleDbCommand(strSQL, cnn))
            {
                using (OleDbDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        rdr.Read();
                        if (rdr.IsDBNull(0) == false)
                        {
                            RecCount = Convert.ToInt32(rdr.GetValue(0));
                        }
                    }
                }
            }


            strSQL = "SELECT Panama.ID, Panama.address, Panama.fullName, Panama.text_ID, Panama.valid_until, ";
            strSQL += " Panama.country_code3, Panama.country, Panama.status, Panama.node_ID, Panama.source_ID, Panama.notes, ";
            strSQL += " Panama.check_status, Panama.test_date,  ";
            strSQL += "  Panama.test_byte, Panama.test_int, Panama.test_long, Panama.test_single, Panama.test_double, Panama.test_decimal ";
            strSQL += " FROM Panama ";
            strSQL += " where Panama.ID<=" + this.txtDataRecords.Text;

            using (OleDbCommand cmd = new OleDbCommand(strSQL, cnn))
            {
                using (OleDbDataReader rdr = cmd.ExecuteReader())
                {
                    this.Datagrid_1.Rows.Clear();
                    this.Datagrid_1.Columns.Clear();

                    for (int k = 0; k < rdr.FieldCount; k++)
                    {

                        switch (rdr.GetFieldType(k).Name)
                        {
                            case "Boolean":

                                this.Datagrid_1.Columns.Add(new DataGridViewCheckBoxColumn());
                                this.Datagrid_1.Columns[this.Datagrid_1.Columns.Count - 1].CellTemplate = new DataGridViewCheckBoxCell();
                                this.Datagrid_1.Columns[this.Datagrid_1.Columns.Count - 1].Name = rdr.GetName(k);
                                this.Datagrid_1.Columns[this.Datagrid_1.Columns.Count - 1].ValueType = rdr.GetFieldType(k);

                                break;

                            default:  // default value would be string

                                this.Datagrid_1.Columns.Add(new DataGridViewColumn());
                                this.Datagrid_1.Columns[this.Datagrid_1.Columns.Count - 1].CellTemplate = new DataGridViewTextBoxCell();
                                this.Datagrid_1.Columns[this.Datagrid_1.Columns.Count - 1].Name = rdr.GetName(k);
                                this.Datagrid_1.Columns[this.Datagrid_1.Columns.Count - 1].ValueType = rdr.GetFieldType(k);

                                break;

                        }

                        this.Datagrid_1.Columns[this.Datagrid_1.Columns.Count - 1].SortMode = DataGridViewColumnSortMode.Automatic;

                    }

                    if (RecCount > 0)
                    {
                        this.Datagrid_1.Rows.Add(RecCount);

                        while (rdr.Read())
                        {
                            for (int k = 0; k < rdr.FieldCount; k++)
                            {
                                this.Datagrid_1.Rows[u].Cells[k].Value = (rdr.IsDBNull(k)) ? "" : rdr.GetValue(k);
                            }
                            u++;
                        }
                    }
                }
            }


            MessageBox.Show("Data has been loaded");

        }

        private void btnSearchSimple_Click(object sender, EventArgs e)
        {
            this.Datagrid_1.SearchSimpleStart();
        }

        private void btnSearchAdvanced_Click(object sender, EventArgs e)
        {
            this.Datagrid_1.SearchAdvancedStart();
        }
    }
}
