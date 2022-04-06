using EFProject.DataBaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFProject
{
    public partial class ProductWithDraw : Form
    {
        StoreDb _context;

        public ProductWithDraw()
        {
            InitializeComponent();
            _context = new StoreDb();
            BindDataToDataGrid();
        }
        void BindDataToDataGrid()
        {
            var list = _context.CustomerBills.ToList();
            dataGridView1.DataSource = list;

        }
        private static void DisplayMessageBox(bool flag)
        {
            AbokhaledMBox messageBox = new AbokhaledMBox(flag);
            messageBox.ShowDialog();
        }
        void RemoveCBItems()
        {
            cbxWareID.DataSource = null;
            cbxWareID.Text = "";
            cbxCustID.DataSource = null;
            cbxCustID.Text = "";
            cbxProductID.DataSource = null;
            cbxProductID.Text = "";

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
            RemoveCBItems();

        }
        void Add()
        {
            if (txbID.Text != "" && cbxCustID.Text != "" && cbxCustID.Text != "" && cbxWareID.Text != "" && txbCount.Text != "")

            {
                var bill = _context.CustomerBills.Find(int.Parse(txbID.Text), int.Parse(cbxProductID.Text)
                    , int.Parse(cbxCustID.Text), int.Parse(cbxWareID.Text));

                if (bill == null)
                {
                    bill = new CustomerBill()
                    {
                        BillNo = int.Parse(txbID.Text),
                        BillDate = DateTime.Parse(DateTime.Parse(txbBillDate.Text).ToString("MM/dd/yyyy")),
                        ProductID = int.Parse(cbxProductID.Text),
                        CustomerID = int.Parse(cbxCustID.Text),
                        WarehouseID = int.Parse(cbxWareID.Text),
                        Count = int.Parse(txbCount.Text)

                    };

                    _context.CustomerBills.Add(bill);

                    DisplayMessageBox(true);


                }
                else
                {
                    DisplayMessageBox(false);


                }

            }
            else
            {
                DisplayMessageBox(false);


            }

            _context.SaveChanges();


        }
        private void Edit()
        {
            int id = Convert.ToInt32(txbID.Text.Trim());
            int prodID = int.Parse(cbxProductID.Text);
            int supID = int.Parse(cbxCustID.Text);
            int warID = int.Parse(cbxWareID.Text);


            //var editThisBill = _context.SupplierBills.Where(b => b.SupplierID == supID && b.WarehouseID == warID
            //&& b.ProductID == prodID && b.BillNo == id).First();
            var bill = _context.CustomerBills.Find(int.Parse(txbID.Text), int.Parse(cbxProductID.Text)
                    , int.Parse(cbxCustID.Text), int.Parse(cbxWareID.Text));
            if (bill != null)
            {
                if (txbID.Text != "" && cbxCustID.Text != "" && cbxCustID.Text != "" && cbxWareID.Text != "" && txbCount.Text != "")

                {
                    bill.BillDate = DateTime.Parse(DateTime.Parse(txbBillDate.Text).ToString("MM/dd/yyyy"));

                    bill.Count = int.Parse(txbCount.Text);



                }
                _context.SaveChanges();

                DisplayMessageBox(false);
            }
            else
            {
                    DisplayMessageBox(false);

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                int id = (int)dataGridView1.Rows[row].Cells[0].Value;
                int prodID = (int)dataGridView1.Rows[row].Cells[2].Value;
                int supID = (int)dataGridView1.Rows[row].Cells[3].Value;
                int warID = (int)dataGridView1.Rows[row].Cells[4].Value;

                var bill = _context.CustomerBills.Find(id, prodID
                         , supID, warID);
                if (bill != null)
                {
                    txbID.Text = bill.BillNo.ToString();
                    txbBillDate.Text = bill.BillDate.ToString("MM/dd/yyyy");
                    cbxCustID.Text = bill.CustomerID.ToString();
                    cbxWareID.Text = bill.WarehouseID.ToString();
                    cbxProductID.Text = bill.ProductID.ToString();
                    txbCount.Text = bill.Count.ToString();

                }
            }
            catch (Exception)
            {

            }
        }

        private void cbxProductID_Click(object sender, EventArgs e)
        {
            var prodIDList = new List<int>();
            prodIDList= _context.Products.Select(s => s.ID).ToList();

            cbxProductID.DataSource = prodIDList;
        }

        private void cbxCustID_Click(object sender, EventArgs e)
        {
            var custIDList = new List<int>();
            custIDList = _context.Customers.Select(s => s.ID).ToList();

            cbxCustID.DataSource = custIDList;
        }

        private void cbxWareID_Click(object sender, EventArgs e)
        {
            var warIDList = new List<int>();
            warIDList = _context.Warehouses.Select(s => s.ID).ToList();

            cbxWareID.DataSource = warIDList;
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
            MakeTextBoxBlank();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var custBill = _context.CustomerBills.Find(int.Parse(txbID.Text), int.Parse(cbxProductID.Text)
                    , int.Parse(cbxCustID.Text), int.Parse(cbxWareID.Text));
            if (custBill != null)
            {
                _context.CustomerBills.Remove(custBill);
                _context.SaveChanges();
                DisplayMessageBox(true);

            }
            else
            {
                DisplayMessageBox(false);
            }

            BindDataToDataGrid();
            MakeTextBoxBlank();
        }
    }
}
