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
    public partial class FromWidStip : Form
    {
        OleDbConnection dbConnection;
        public FromWidStip()
        {
            InitializeComponent();
            dbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=SessionTrackingDatabase.mdb");
            dbConnection.Open();
        }

        private void WidStip_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sessionTrackingDatabaseDataSet.Вид_стипендии". При необходимости она может быть перемещена или удалена.
            this.вид_стипендииTableAdapter.Fill(this.sessionTrackingDatabaseDataSet.Вид_стипендии);
            kol.Text = $"{dgvWid.Rows.Count}";
            cbPoisk.SelectedIndex = 0;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbDob.Visible = false;
            lbRed.Visible = true;
            Size = new Size(955, 382);//ширина высота 558/955 382
            panel1.Visible = true;
            tbStip.Text = dgvWid.CurrentRow.Cells[1].Value.ToString();
            tbId.Text = dgvWid.CurrentRow.Cells[0].Value.ToString();
            dtpDat.Text = dgvWid.CurrentRow.Cells[3].Value.ToString();
            tbSum.Text = dgvWid.CurrentRow.Cells[2].Value.ToString();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbDob.Visible = true;
            lbRed.Visible = false;
            panel1.Visible = true;
            tbStip.Text = "";
            dtpDat.Text = "";
            tbSum.Text = "";
            Size = new Size(955, 382);
        }

        private void tbSum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || (int)e.KeyChar == 8 || e.KeyChar == ','))
                e.KeyChar = (char)0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            lbRed.Visible = false;
            lbDob.Visible = false;
            tbStip.Text = "";
            dtpDat.Text = "";
            tbSum.Text = "";
            Size = new Size(558, 382);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
             if (lbRed.Visible == true)
             {
                 string query = $"UPDATE Вид_стипендии SET название = \"{tbStip.Text}\", размер = \"{tbSum.Text}\", дата_ввода = \"{dtpDat.Value.ToString("dd.MM.yyyy")}\" WHERE ном_вида = {tbId.Text}";
                 OleDbCommand command = new OleDbCommand(query, dbConnection);
                 try
                 {
                     command.ExecuteNonQuery();
                     Size = new Size(558, 382);
                     panel1.Visible = false;
                     lbRed.Visible = false;
                     tbStip.Text = "";
                     dtpDat.Text = "";
                     tbSum.Text = "";
                     MessageBox.Show("Данные обновлены. Чтобы увидеть их, закройте и откройте окно \"Виды стипендий\".", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 }
                 catch
                 {
                     MessageBox.Show("Непредвиденная ошибка!", "Ошибка", MessageBoxButtons.OK);
                 }
             }
             else
             {
                 string query = $"INSERT INTO Вид_стипендии (название, размер, дата_ввода) VALUES (\"{tbStip.Text}\", {tbSum.Text}, \"{dtpDat.Value.ToString("dd.MM.yyyy")}\")";
                 OleDbCommand command = new OleDbCommand(query, dbConnection);
                 try
                 {
                     command.ExecuteNonQuery();
                     Size = new Size(558, 328);
                     panel1.Visible = false;
                     lbDob.Visible = false;
                     MessageBox.Show("Данные добавлены. Чтобы увидеть их, закройте и откройте окно \"Виды стипендий\".", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 }
                 catch
                 {
                     MessageBox.Show("Непредвиденная ошибка!", "Ошибка", MessageBoxButtons.OK);
                 }

             }
        }

        private void tbPoisk_TextChanged(object sender, EventArgs e)
        {
            string s;
            if (cbPoisk.Text != "название")
            {
                s = $"{cbPoisk.Text} >= \'{tbPoisk.Text}\'";
            }
            else
            {
                s = $"{cbPoisk.Text} Like \'{tbPoisk.Text}*\'";
            }
            try
            {
                вид_стипендииBindingSource.Filter = s;
            }
            catch
            {
               // MessageBox.Show("Поисковое поле пустое!","Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            kol.Text = $"{dgvWid.Rows.Count}";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tbPoisk.Text = "";
            вид_стипендииBindingSource.Filter = "";
            kol.Text = $"{dgvWid.Rows.Count}";
        }
    }
}
