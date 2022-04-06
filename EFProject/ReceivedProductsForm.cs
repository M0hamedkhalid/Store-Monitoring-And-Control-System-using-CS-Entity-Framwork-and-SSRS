using EFProject.DataBaseModel;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EFProject
{
    public partial class ReceivedProductsForm : Form
    {
        private StoreDb _context;

        public ReceivedProductsForm()
        {
            InitializeComponent();
            _context = new StoreDb();
            BindDataToDataGrid();
        }

        private void BindDataToDataGrid()
        {
            var list = _context.SupplierBills.ToList();
            dataGridView1.DataSource = list;
        }

        private void RemoveCBItems()
        {
            cbxWareID2.DataSource = null;
            cbxWareID.DataSource = null;
            cbxSuppID.DataSource = null;
            cbxProductID.DataSource = null;
        }

        private static void DisplayMessageBox(bool flag)
        {
            AbokhaledMBox messageBox = new AbokhaledMBox(flag);
            messageBox.ShowDialog();
        }

        private void Edit()
        {
            int id = Convert.ToInt32(txbID.Text.Trim());
            int prodID = int.Parse(cbxProductID.Text);
            int supID = int.Parse(cbxSuppID.Text);
            int warID = int.Parse(cbxWareID.Text);

            //var editThisBill = _context.SupplierBills.Where(b => b.SupplierID == supID && b.WarehouseID == warID
            //&& b.ProductID == prodID && b.BillNo == id).First();
            var bill = _context.SupplierBills.Where(b => b.BillNo == id).First();
            if (bill != null)
            {
                if (txbID.Text != "" && txbExpDate.Text != "" && txbProdDate.Text != "" && cbxSuppID.Text != "" && cbxSuppID.Text != "" && cbxWareID.Text != "" && txbCount.Text != "")

                {
                    bill.ProductionDate = DateTime.Parse(txbProdDate.Text);
                    bill.ExpireIN = DateTime.Parse(txbExpDate.Text); ;

                    bill.Count = int.Parse(txbCount.Text);
                }
                DisplayMessageBox(true);
            }
            else
            {
                DisplayMessageBox(true);
            }
            _context.SaveChanges();
        }

        private void Add()
        {
            if (txbID.Text != "" && txbExpDate.Text != "" && txbProdDate.Text != "" && cbxSuppID.Text != "" && cbxSuppID.Text != "" && cbxWareID.Text != "" && txbCount.Text != "")

            {
                var bill = _context.SupplierBills.Find(int.Parse(txbID.Text), int.Parse(cbxProductID.Text)
                    , int.Parse(cbxSuppID.Text), int.Parse(cbxWareID.Text));

                if (bill == null)
                {
                    bill = new SupplierBill()
                    {
                        BillNo = int.Parse(txbID.Text),
                        SupplierID = int.Parse(cbxSuppID.Text),
                        ProductionDate = DateTime.Parse(DateTime.Parse(txbProdDate.Text).ToString("MM/dd/yyyy")),
                        ExpireIN = DateTime.Parse(DateTime.Parse(txbExpDate.Text).ToString("MM/dd/yyyy")),
                        ProductID = int.Parse(cbxProductID.Text),
                        WarehouseID = int.Parse(cbxWareID.Text),
                        Count = int.Parse(txbCount.Text)
                    };

                    _context.SupplierBills.Add(bill);

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

                _context.SaveChanges();
            }
        }

        private void MakeTextBoxBlank()
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

        private void cbxProductID_Enter(object sender, EventArgs e)
        {
            var prodIDList = _context.Products.Select(s => s.ID).ToArray();
            cbxProductID.DataSource = prodIDList;
        }

        private void cbxSuppID_Enter(object sender, EventArgs e)
        {
            var supIDList = _context.Suppliers.Select(s => s.ID).ToArray();
            cbxSuppID.DataSource = supIDList;
        }

        private void cbxWareID_Enter(object sender, EventArgs e)
        {
            var warIDList = _context.Warehouses.Select(s => s.ID).ToArray();
            cbxWareID.DataSource = warIDList;
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            var warIDList = _context.Warehouses.Select(s => s.ID).ToArray();
            cbxWareID2.DataSource = warIDList;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                int id = (int)dataGridView1.Rows[row].Cells[0].Value;
                int prodID = (int)dataGridView1.Rows[row].Cells[3].Value;
                int supID = (int)dataGridView1.Rows[row].Cells[4].Value;
                int warID = (int)dataGridView1.Rows[row].Cells[5].Value;

                var wantedBill = _context.SupplierBills.Where(a => a.SupplierID == supID
                && a.ProductID == prodID && a.WarehouseID == warID
                && a.BillNo == id).First();
                if (wantedBill != null)
                {
                    txbID.Text = wantedBill.BillNo.ToString();
                    txbProdDate.Text = wantedBill.ProductionDate.ToString("MM/dd/yyyy");
                    txbExpDate.Text = wantedBill.ExpireIN.ToString("MM/dd/yyyy");
                    cbxSuppID.Text = wantedBill.SupplierID.ToString();
                    cbxWareID.Text = wantedBill.WarehouseID.ToString();
                    cbxProductID.Text = wantedBill.ProductID.ToString();
                    txbCount.Text = wantedBill.Count.ToString();
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit();
            BindDataToDataGrid();
            MakeTextBoxBlank();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var supplierBill = _context.SupplierBills.Find(int.Parse(txbID.Text), int.Parse(cbxProductID.Text)
                    , int.Parse(cbxSuppID.Text), int.Parse(cbxWareID.Text));
            if (supplierBill != null)
            {
                _context.SupplierBills.Remove(supplierBill);
                DisplayMessageBox(true);
            }
            else
            {
                DisplayMessageBox(false);
            }
            _context.SaveChanges();
            BindDataToDataGrid();
            MakeTextBoxBlank();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Add();
            }
            catch (Exception)
            {

                DisplayMessageBox(false);

            }
            BindDataToDataGrid();
            MakeTextBoxBlank();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (txbID.Text != "" && txbExpDate.Text != "" && txbProdDate.Text != "" && cbxSuppID.Text != "" && cbxSuppID.Text != "" && cbxWareID.Text != "" && txbCount.Text != "" && cbxWareID2.Text != "")
            {
                int RemovethisCount = int.Parse(txbCount.Text);

                int id = Convert.ToInt32(txbID.Text.Trim());
                int prodID = int.Parse(cbxProductID.Text);
                int supID = int.Parse(cbxSuppID.Text);
                int warID = int.Parse(cbxWareID.Text);

                var editThisBill = _context.SupplierBills.Where(b => b.SupplierID == supID && b.WarehouseID == warID
                && b.ProductID == prodID && b.BillNo == id).FirstOrDefault();

                int oldBillCount = editThisBill.Count;

                int warID2 = int.Parse(cbxWareID2.Text);

                SupplierBill existedBill = _context.SupplierBills.Where(b => b.SupplierID == supID
                && b.WarehouseID == warID2
                && b.ProductID == prodID && b.BillNo == id).FirstOrDefault();

                if (int.Parse(txbCount.Text) < oldBillCount)
                {
                    if (existedBill == null)
                    {
                        SupplierBill supplierBill = new SupplierBill()
                        {
                            BillNo = Convert.ToInt32(txbID.Text.Trim()),
                            SupplierID = int.Parse(cbxSuppID.Text),
                            ProductionDate = DateTime.Parse(DateTime.Parse(txbProdDate.Text).ToString("MM/dd/yyyy")),
                            ExpireIN = DateTime.Parse(DateTime.Parse(txbExpDate.Text).ToString("MM/dd/yyyy")),
                            ProductID = int.Parse(cbxProductID.Text),
                            WarehouseID = int.Parse(cbxWareID2.Text),
                            Count = int.Parse(txbCount.Text)
                        };
                        _context.SupplierBills.Add(supplierBill);
                    }
                    else
                    {
                        existedBill.Count += int.Parse(txbCount.Text);
                    }

                    editThisBill.Count = oldBillCount - int.Parse(txbCount.Text);
                    DisplayMessageBox(true);
                }
                else
                {
                    if (int.Parse(txbCount.Text) == oldBillCount)
                    {
                        if (existedBill == null)
                        {
                            SupplierBill supplierBill = new SupplierBill()
                            {
                                BillNo = Convert.ToInt32(txbID.Text.Trim()),
                                SupplierID = int.Parse(cbxSuppID.Text),
                                ProductionDate = DateTime.Parse(DateTime.Parse(txbProdDate.Text).ToString("MM/dd/yyyy")),
                                ExpireIN = DateTime.Parse(DateTime.Parse(txbExpDate.Text).ToString("MM/dd/yyyy")),
                                ProductID = int.Parse(cbxProductID.Text),
                                WarehouseID = int.Parse(cbxWareID2.Text),
                                Count = int.Parse(txbCount.Text)
                            };
                            _context.SupplierBills.Add(supplierBill);
                        }
                        else
                        {
                            existedBill.Count += int.Parse(txbCount.Text);
                        }
                        _context.SupplierBills.Remove(editThisBill);
                        DisplayMessageBox(true);
                    }
                    else
                    {
                        DisplayMessageBox(false);
                    }
                }

                _context.SaveChanges();

                BindDataToDataGrid();
                MakeTextBoxBlank();
            }
        }
    }
}