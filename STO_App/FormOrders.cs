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
    public partial class FormOrders : Form
    {
        private static FormOrders f;

        public static FormOrders fo
        {
            get
            {
                if (f == null || f.IsDisposed)
                    f = new FormOrders();
                return f;
            }
        }

        public void ShowForm()
        {
            Show();
            Activate();
        }

        public FormOrders()
        {
            InitializeComponent();
        }

        private void ordersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.ordersBindingSource.EndEdit();
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
            catch
            {
                MessageBox.Show("Ошибка при заполнении данных заказа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Orders_EmployeeBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.orders_Employee_RelBindingSource.EndEdit();
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
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Orders_Employee_RelDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Ошибка при заполнении данных о работниках участвующих в заказах", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FormOrders_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sTO_dataBaseDataSet.Employee". При необходимости она может быть перемещена или удалена.
            this.employeeTableAdapter.Fill(this.sTO_dataBaseDataSet.Employee);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sTO_dataBaseDataSet.Auto". При необходимости она может быть перемещена или удалена.
            this.autoTableAdapter.Fill(this.sTO_dataBaseDataSet.Auto);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sTO_dataBaseDataSet.Used_Parts". При необходимости она может быть перемещена или удалена.
            this.used_PartsTableAdapter.Fill(this.sTO_dataBaseDataSet.Used_Parts);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sTO_dataBaseDataSet.Orders_Employee_Rel". При необходимости она может быть перемещена или удалена.
            this.orders_Employee_RelTableAdapter.Fill(this.sTO_dataBaseDataSet.Orders_Employee_Rel);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sTO_dataBaseDataSet.Orders". При необходимости она может быть перемещена или удалена.
            this.ordersTableAdapter.Fill(this.sTO_dataBaseDataSet.Orders);
            sTO_dataBaseDataSet.Orders.Columns["Date_Issued"].DefaultValue = DateTime.Now.Date;
            sTO_dataBaseDataSet.Orders_Employee_Rel.Columns["Start_Date"].DefaultValue = DateTime.Now.Date;
        }
    }
}
