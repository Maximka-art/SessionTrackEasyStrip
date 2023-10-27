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
    public partial class FromWidOtch : Form
    {
        OleDbConnection dbConnection;
        public FromWidOtch()
        {
            InitializeComponent();
            dbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=SessionTrackingDatabase.mdb");
            dbConnection.Open();
        }

        private void вид_отчётностиBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.вид_отчётностиBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.sessionTrackingDatabaseDataSet);

        }

        private void WidOtch_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sessionTrackingDatabaseDataSet.Вид_отчётности". При необходимости она может быть перемещена или удалена.
            this.вид_отчётностиTableAdapter.Fill(this.sessionTrackingDatabaseDataSet.Вид_отчётности);
            kol.Text = $"{dgvOtch.Rows.Count}";
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbDob.Visible = true;
            lbRed.Visible = false;
            panel1.Visible = true;
            tbOt.Text = "";
            Size = new Size(905, 421);//ширина высота 512/905 421
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbDob.Visible = false;
            lbRed.Visible = true;
            Size = new Size(905, 421);
            panel1.Visible = true;
            tbOt.Text = dgvOtch.CurrentRow.Cells[1].Value.ToString();
            tbId.Text = dgvOtch.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Size = new Size(512, 421);
            panel1.Visible = false;
            lbRed.Visible = false;
            lbDob.Visible = false;
            tbOt.Text = "";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lbRed.Visible == true)
            {
                string query = $"UPDATE Вид_отчётности SET отчётность = '{tbOt.Text}' WHERE ном_отчётности = {tbId.Text}";
                OleDbCommand command = new OleDbCommand(query, dbConnection);
                try
                {
                    command.ExecuteNonQuery();
                    Size = new Size(512, 421);
                    panel1.Visible = false;
                    lbRed.Visible = false;
                    MessageBox.Show("Данные обновлены. Чтобы увидеть их, закройте и откройте окно \"Виды отчётности\".", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Непредвиденная ошибка!", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else
            {
                string query = $"INSERT INTO Вид_отчётности (отчётность) VALUES (\"{tbOt.Text}\")";
                OleDbCommand command = new OleDbCommand(query, dbConnection);
                try
                {
                    command.ExecuteNonQuery();
                    Size = new Size(512, 421);
                    panel1.Visible = false;
                    lbDob.Visible = false;
                    MessageBox.Show("Данные добавлены. Чтобы увидеть их, закройте и откройте окно \"Виды отчётности\".", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show($"Непредвиденная ошибка!{query}", "Ошибка", MessageBoxButtons.OK);
                }
               
            }
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            string s = $"отчётность Like \'{tbPoisk.Text}*\'";
            try
            {
                вид_отчётностиBindingSource.Filter = s;
            }
            catch
            {
                // MessageBox.Show("Поисковое поле пустое!","Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            kol.Text = $"{dgvOtch.Rows.Count}";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tbPoisk.Text = "";
            вид_отчётностиBindingSource.Filter = "";
            kol.Text = $"{dgvOtch.Rows.Count}";
        }
    }
}
