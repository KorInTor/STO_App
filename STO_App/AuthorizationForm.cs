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


namespace STO_App
{
    public partial class AuthorizationForm : Form
    {
        public AuthorizationForm()
        {
            InitializeComponent();
        }

        string ConnectionString;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string server = "DESKTOP-17V5OSI\\SQLEXPRESS";
            string database = "STO_dataBase";
            //string username = txtUsername.Text;
            //string password = txtPassword.Text;

            //ConnectionString = $"Data Source={server};Initial Catalog={database};User Id={username};Password={password};";

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
