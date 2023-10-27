using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SessionTrackEasyStrip
{
    public partial class FromSpec : Form
    {
        public FromSpec()
        {
            InitializeComponent();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void специальностьBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.специальностьBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.sessionTrackingDatabaseDataSet);

        }

        private void Spec_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sessionTrackingDatabaseDataSet.Группа". При необходимости она может быть перемещена или удалена.
            this.группаTableAdapter.Fill(this.sessionTrackingDatabaseDataSet.Группа);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sessionTrackingDatabaseDataSet.Специальность". При необходимости она может быть перемещена или удалена.
            this.специальностьTableAdapter.Fill(this.sessionTrackingDatabaseDataSet.Специальность);
            kol.Text = $"{специальностьDataGridView.Rows.Count}";
            kol2.Text = $"{группаDataGridView.Rows.Count}";
            toolStripComboBox1.SelectedIndex = 0;
            toolStripComboBox2.SelectedIndex = 0;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tbPoisk.Text = "";
            специальностьBindingSource.Filter = "";
            kol.Text = $"{специальностьDataGridView.Rows.Count}";
            kol2.Text = $"{группаDataGridView.Rows.Count}";
        }

        private void tbPoisk_TextChanged(object sender, EventArgs e)
        {
            string s = $"{toolStripComboBox1.Text} Like \'{tbPoisk.Text}*\'";
            try
            {
                специальностьBindingSource.Filter = s;
            }
            catch
            {
                // MessageBox.Show("Поисковое поле пустое!","Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            kol.Text = $"{специальностьDataGridView.Rows.Count}";
            kol2.Text = $"{группаDataGridView.Rows.Count}";
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            tbPoisk2.Text = "";
            группаBindingSource.Filter = "";
            kol.Text = $"{специальностьDataGridView.Rows.Count}";
            kol2.Text = $"{группаDataGridView.Rows.Count}";
        }

        private void tbPoisk2_TextChanged(object sender, EventArgs e)
        {
            string s = $"{toolStripComboBox2.Text} Like \'{tbPoisk2.Text}*\'";
            try
            {
                группаBindingSource.Filter = s;
            }
            catch
            {
                // MessageBox.Show("Поисковое поле пустое!","Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            kol.Text = $"{специальностьDataGridView.Rows.Count}";
            kol2.Text = $"{группаDataGridView.Rows.Count}";
        }
    }
}
