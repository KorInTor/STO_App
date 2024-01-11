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
    public partial class FormParts : Form
    {
        private static FormParts f;

        public static FormParts fp
        {
            get
            {
                if (f == null || f.IsDisposed)
                    f = new FormParts();
                return f;
            }
        }

        public void ShowForm()
        {
            Show();
            Activate();
        }

        public FormParts()
        {
            InitializeComponent();
        }

        private void partsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.partsBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.sTO_dataBaseDataSet);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 229)
                {
                    MessageBox.Show("Отказано в доступе", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.sTO_dataBaseDataSet.RejectChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormParts_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sTO_dataBaseDataSet.Parts". При необходимости она может быть перемещена или удалена.
            this.partsTableAdapter.Fill(this.sTO_dataBaseDataSet.Parts);
        }
    }
}
