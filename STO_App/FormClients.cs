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
    public partial class FormClients : Form
    {
        private static FormClients f;

        public static FormClients fс
        {
            get
            {
                if (f == null || f.IsDisposed)
                    f = new FormClients();
                return f;
            }
        }

        public void ShowForm()
        {
            Show();
            Activate();
        }

        string GetSelectedFieldName()
        {
            return clientDataGridView.Columns[clientDataGridView.CurrentCell.ColumnIndex].DataPropertyName;
        }

        private void client_DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((clientDataGridView.Rows[e.RowIndex].Cells["GenderComboBox"].Value == null)
                || (clientDataGridView.Rows[e.RowIndex].Cells["GenderComboBox"].Value.ToString() == ""))
                e.CellStyle.BackColor = Color.LightGreen;
            else
            {
                if (clientDataGridView.Rows[e.RowIndex].Cells["GenderComboBox"].Value.ToString() == "Male  ")
                    e.CellStyle.BackColor = Color.SkyBlue;
                else
                    e.CellStyle.BackColor = Color.Pink;

            }
        }

        public FormClients()
        {
            InitializeComponent();
        }

        private void clientBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.clientBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.sTO_dataBaseDataSet);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 229)
                {
                    MessageBox.Show("Отказано в доступе", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }
        }

        private void FormClients_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: данная строка кода позволяет загрузить данные в таблицу "sTO_dataBaseDataSet.Auto". При необходимости она может быть перемещена или удалена.
                this.autoTableAdapter.Fill(this.sTO_dataBaseDataSet.Auto);
                // TODO: данная строка кода позволяет загрузить данные в таблицу "sTO_dataBaseDataSet.Client". При необходимости она может быть перемещена или удалена.
                this.clientTableAdapter.Fill(this.sTO_dataBaseDataSet.Client);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 229)
                {
                    MessageBox.Show("Отказано в доступе", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }
        }

        private void clientDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Get the value of the cell where the error occurred
            var cellValue = clientDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            // Log the value of the cell
            Console.WriteLine("Error occurred in cell with value: " + cellValue);

            e.Cancel = true;
        }

        private void toolStripButtonFind_Click(object sender, EventArgs e)
        {
            {
                if (toolStripTextBoxFind.Text == "")
                {
                    MessageBox.Show("Вы ничего не задали", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int indexPos;

                try
                {
                    indexPos = clientBindingSource.Find(GetSelectedFieldName(), toolStripTextBoxFind.Text);
                }
                catch (Exception err)
                {
                    MessageBox.Show("Ошибка поиска \n" + err.Message);
                    return;
                }

                if (indexPos > -1)
                    clientBindingSource.Position = indexPos;
                else
                {
                    MessageBox.Show("Таких клиентов нет", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clientBindingSource.Position = 0;
                }
            }
        }

        private void checkBoxFind_CheckedChanged(object sender, EventArgs e)
        {
            {
                if (checkBoxFind.Checked)
                {
                    if (toolStripTextBoxFind.Text == "") MessageBox.Show("Вы ничего не задали", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else try
                        {
                            clientBindingSource.Filter = GetSelectedFieldName() + "='" + toolStripTextBoxFind.Text + "'";

                        }
                        catch (Exception err) { MessageBox.Show("Ошибка фильтрации \n" + err.Message); }
                }
                else clientBindingSource.Filter = "";

                if (clientBindingSource.Count == 0)
                {
                    MessageBox.Show("Нет таких");
                    clientBindingSource.Filter = ""; checkBoxFind.Checked = false;
                }
            }
        }
    }
}
