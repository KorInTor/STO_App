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
    public partial class FormEmployees : Form
    {
        private static FormEmployees f;

        public static FormEmployees fe
        {
            get
            {
                if (f == null || f.IsDisposed)
                    f = new FormEmployees();
                return f;
            }
        }

        public void ShowForm()
        {
            Show();
            Activate();
        }

        public FormEmployees()
        {
            InitializeComponent();
        }

        private void employeeBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.EmployeeBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.sTO_dataBaseDataSet);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 229)
                {
                    MessageBox.Show("Отказано в доступе", "Ошибка!", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    this.sTO_dataBaseDataSet.RejectChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FormEmployees_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sTO_dataBaseDataSet.Employee". При необходимости она может быть перемещена или удалена.
            this.employeeTableAdapter.Fill(this.sTO_dataBaseDataSet.Employee);

        }

        string fileImage = "";
        private void buttonOpenPhoto_Click(object sender, EventArgs e)
        {
            openFileDialogPhoto.Title = "Укажите файл для фото"; if (openFileDialogPhoto.ShowDialog() == DialogResult.OK)
            {
                fileImage = openFileDialogPhoto.FileName;
                try
                {

                    photoPictureBox.Image = new Bitmap(openFileDialogPhoto.FileName);
                }
                catch
                {
                    MessageBox.Show("Выбран не тот формат файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            else fileImage = "";
        }
    }
}
