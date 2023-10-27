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
    public partial class FromPred : Form
    {
        public FromPred()
        {
            InitializeComponent();
        }

        private void предметBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.предметBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.sessionTrackingDatabaseDataSet);

        }

        private void Pred_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sessionTrackingDatabaseDataSet.Предмет". При необходимости она может быть перемещена или удалена.
            this.предметTableAdapter.Fill(this.sessionTrackingDatabaseDataSet.Предмет);
            kol.Text = $"{предметDataGridView.Rows.Count}";

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tbPoisk.Text = "";
            предметBindingSource.Filter = "";
            kol.Text = $"{предметDataGridView.Rows.Count}";
        }

        private void tbPoisk_TextChanged(object sender, EventArgs e)
        {
            string s = $"название Like \'{tbPoisk.Text}*\'";
            try
            {
                предметBindingSource.Filter = s;
            }
            catch
            {
                // MessageBox.Show("Поисковое поле пустое!","Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            kol.Text = $"{предметDataGridView.Rows.Count}";
        }
    }
}
