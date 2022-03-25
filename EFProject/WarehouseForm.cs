using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EFProject.DataBaseModel;


namespace EFProject
{
    public partial class WarehouseForm : Form
    {
        StoreDb _context;

        public WarehouseForm()
        {
            InitializeComponent();
            _context = new StoreDb();
            BindDataToDataGrid();
        }
        void BindDataToDataGrid()
        {
            var list = _context.Warehouses.ToList();
            dataGridView1.DataSource = list;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                int id = (int)dataGridView1.Rows[row].Cells[0].Value;



                var warehouse = _context.Warehouses.Find(id);
                if (warehouse != null)
                {
                    txbID.Text = warehouse.ID.ToString().Trim();
                    txbName.Text = warehouse.Name.Trim();
                    txbAdd.Text = warehouse.Address.Trim();
                    txbMgr.Text = warehouse.MgrName.ToString().Trim();

                }
            }
            catch (Exception)
            {

            }

        }
        private void Edit()
        {
            var warehouse = _context.Warehouses.Find(int.Parse(txbID.Text.Trim()));
            if (warehouse != null)
            {
                if (txbID.Text != "" && txbName.Text != "" && txbMgr.Text != "")
                {
                    warehouse.Name = txbName.Text;
                    warehouse.Address = txbAdd.Text;
                    warehouse.MgrName = txbMgr.Text;
                    


                }
            }
            _context.SaveChanges();
        }


        void Add()
        {

            if (txbID.Text != "" && txbName.Text != "" && txbMgr.Text != "")
            {
                var warehouse = _context.Warehouses.Find(int.Parse(txbID.Text));

                if (warehouse == null)
                {
                    warehouse = new Warehouse()
                    {
                        ID = int.Parse(txbID.Text),
                        Address = txbAdd.Text.Trim(),
                        Name = txbName.Text,
                        MgrName = txbMgr.Text,
                        
                        
                        
                        

                    };

                    _context.Warehouses.Add(warehouse);

                    MessageBox.Show("Action Is Done");

                }
                else
                {
                    MessageBox.Show("Action Is Not Done");

                }

            }

            _context.SaveChanges();



        }
        void MakeTextBoxBlank()
        {
            foreach (var control in panel1.Controls)
            {
                if (control is TextBox)
                {
                    var textBox = control as TextBox;
                    textBox.Text = string.Empty;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var supplier = _context.Suppliers.Find(int.Parse(txbID.Text.Trim()));
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
            }
            _context.SaveChanges();
            BindDataToDataGrid();
            MakeTextBoxBlank();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit();
            BindDataToDataGrid();
            MakeTextBoxBlank();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
            BindDataToDataGrid();
        }
    }
}
