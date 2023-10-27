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
    public partial class Form1 : Form
    {
        OleDbConnection dbConnection;
        public Form1()
        {
            InitializeComponent();
            dbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=SessionTrackingDatabase.mdb");
            dbConnection.Open();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите выйти из приложения?", "Закрытие программы", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Close();
            }
        }

        private void студентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGrupAndStud GrupAndStud = new FormGrupAndStud();
            GrupAndStud.Show();
        }

        private void предметыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FromPred Pred = new FromPred();
            Pred.Show();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FromSprav Sprav = new FromSprav();
            Sprav.Show();
        }

        private void техническиеХарактеристикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FromTexXar TexXar = new FromTexXar();
            TexXar.Show();
        }

        private void видСтипендииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FromWidStip WidStip = new FromWidStip();
            WidStip.Show();
        }

        private void видыОтчётностиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FromWidOtch WidOtch = new FromWidOtch();
            WidOtch.Show();
        }

        private void специальностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FromSpec Spec = new FromSpec();
            Spec.Show();
        }
    }
}
