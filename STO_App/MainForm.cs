using STO_App.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace STO_App
{
    public partial class MainForm : Form
    {
        private static string _connectionString;

        public static string ConnectionString 
        { 
            get
            {
                return _connectionString;
            }
            private set 
            { 
                _connectionString = value; 
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = MessageBox.Show("Вы хотите закрыть программу?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes;
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("(C)ТУСУР,КСУП,Пряхин Д.С.,группа 571-1 ,2023", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();
        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEmployees.fe.ShowForm();
        }
        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClients.fс.ShowForm();
        }

        private void деталиНаСкладеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormParts.fp.ShowForm();
        }

        private void заказыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOrders.fo.ShowForm();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = "DESKTOP-17V5OSI",
                InitialCatalog = "STO_dataBase",
                PersistSecurityInfo = true,
                UserID = UsernameTextBox.Text,
                Password = PasswordTextBox.Text
            };
            ConnectionString = builder.ConnectionString;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Подключение открыто");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Ошибка подключения: " + ex.Message);
                return;
            }
            PasswordTextBox.Enabled = false;
            UsernameTextBox.Enabled = false;
            ConnectButton.Enabled = false;
            ConnectButton.Text = "Успешно!";
            menuStripMain.Enabled = true;

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings["STO_App.Properties.Settings.STO_dataBaseConnectionString"].ConnectionString = ConnectionString;
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");

        }
    }
}
