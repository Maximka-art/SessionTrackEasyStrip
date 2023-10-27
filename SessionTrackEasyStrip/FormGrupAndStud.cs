using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace SessionTrackEasyStrip
{
    public partial class FormGrupAndStud : Form
    {
        OleDbConnection dbConnection;
        public FormGrupAndStud()
        {
            dbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=SessionTrackingDatabase.mdb");
            dbConnection.Open();
            InitializeComponent();
        }

        private void студентBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.групСтудСпецBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.sessionTrackingDatabaseDataSet);

        }

        private void GrupAndStud_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sessionTrackingDatabaseDataSet.ГрупСтудСпец". При необходимости она может быть перемещена или удалена.
            this.групСтудСпецTableAdapter.Fill(this.sessionTrackingDatabaseDataSet.ГрупСтудСпец);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sessionTrackingDatabaseDataSet.Группа". При необходимости она может быть перемещена или удалена.
            this.группаTableAdapter.Fill(this.sessionTrackingDatabaseDataSet.Группа);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sessionTrackingDatabaseDataSet.Студент". При необходимости она может быть перемещена или удалена.
            this.студентTableAdapter.Fill(this.sessionTrackingDatabaseDataSet.Студент);
            TreeNode Node = new TreeNode("Группы");
            DataTable topics = new DataTable();
            topics = sessionTrackingDatabaseDataSet.Группа;
            foreach (DataRow row in topics.Rows)
            {
                //   MessageBox.Show(row["nazvanie"].ToString());
                Node.Nodes.Add(new TreeNode(row["название"].ToString()));
            }
            treeView1.Nodes.Add(Node);
            treeView1.ExpandAll();
            cmbPoisk.SelectedIndex = 0;
            kol.Text = $"{групСтудСпецDataGridView.Rows.Count}";
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Text != "Группы")
            {
                групСтудСпецBindingSource.Filter = $"название = '{treeView1.SelectedNode.Text}'";
                //MessageBox.Show($"{treeView1.SelectedNode.Text}", "Ошибка", MessageBoxButtons.OK);
            }
            else
            {
                групСтудСпецBindingSource.Filter = "";
            }
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            int ind = cmbPoisk.SelectedIndex;
            string s = "";
            switch (ind)
            {
                case 0: s = $"{cmbPoisk.Text} Like \'*{tbPoisk.Text}*\'"; break;
                case 1: case 2: s = $"{cmbPoisk.Text} = {tbPoisk.Text}"; break;
                case 4: s = $"{cmbPoisk.Text} >= \'{tbPoisk.Text}\'"; break;
                default: s = $"{cmbPoisk.Text} Like \'{tbPoisk.Text}*\'"; break;
            }
            try
            { 
            групСтудСпецBindingSource.Filter = s;
            }
            catch
            {
                // MessageBox.Show("Поисковое поле пустое!","Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            kol.Text = $"{групСтудСпецDataGridView.Rows.Count}";
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tbPoisk.Text = "";
            групСтудСпецBindingSource.Filter = "";
            kol.Text = $"{групСтудСпецDataGridView.Rows.Count}";
        }
    }
}
