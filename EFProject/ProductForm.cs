using EFProject.DataBaseModel;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EFProject
{
    public partial class ProductForm : Form
    {
        private StoreDb _context;

        public ProductForm()
        {
            InitializeComponent();
            _context = new StoreDb();
            BindDataToDataGrid();
        }

        private void BindDataToDataGrid()
        {
            //var list =

            //(from prod in _context.Products
            // join unit in _context.ProductUnits on prod.ID equals unit.ProductID
            // select new { ID = prod.ID, Name = prod.Name, SupplierID = prod.SupplierID, Unit = (unit.Unit) }).ToList();
            //List
            //{ ID = 1, Name = "Cola                                              ", SupplierID = 1, Unit = "Bottle    " }
            //{ ID = 1, Name = "Cola                                              ", SupplierID = 1, Unit = "liter     " }
            //{ ID = 2, Name = "7Up                                               ", SupplierID = 2, Unit = "liter     " }
            //{ ID = 3, Name = "Pepsi                                             ", SupplierID = 3, Unit = "liter     " }

            var list2 = _context.Products.
                Select(a => new
                {
                    ID = a.ID,
                    Name = a.Name,
                    SupplierID = a.SupplierID,
                    Unit = a.ProductUnits
                .Select(e => e.Unit)
                }).ToList()
                .Select(s => new { ID = s.ID, Name = s.Name, SupplierID = s.SupplierID, Units = string.Join(",", s.Unit) })
                .ToList();

            ///List2
            ///

            //{ ID = 1, Name = "Cola  ", SupplierID = 1, Units = "Bottle    ,liter     " }
            //{ ID = 2, Name = "7Up  ", SupplierID = 2, Units = "liter     " }
            //{ ID = 3, Name = "Pepsi   ", SupplierID = 3, Units = "liter     " }

            dataGridView1.DataSource = list2;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                int id = (int)dataGridView1.Rows[row].Cells[0].Value;

                var wantedProduct = _context.Products.Find(id);
                if (wantedProduct != null)
                {
                    txbProductID.Text = wantedProduct.ID.ToString().Trim();
                    txbProductName.Text = wantedProduct.Name;

                    LoadDataToComboBox();

                    cbxProductSuppliers.Text = "";
                    cbxProductSuppliers.Text = wantedProduct.SupplierID.ToString();

                    txbProductUnits.Text = string.Join(",", wantedProduct.ProductUnits.Select(s => s.Unit.Trim()));
                }
            }
            catch (Exception)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var wantedProduct = _context.Products.Find(int.Parse(txbProductID.Text));

            if (wantedProduct != null)
            {
                wantedProduct.SupplierID = int.Parse(cbxProductSuppliers.Text.Trim());
                wantedProduct.Name = txbProductName.Text.Trim();
                string[] units = txbProductUnits.Text.Trim().Split(',');
                for (int i = 0; i < units.Length; i++)
                {
                    if (i == 0)
                    {
                        wantedProduct.ProductUnits.Clear();
                    }
                    wantedProduct.ProductUnits.Add(new ProductUnit { Unit = units[i].Trim(), Product = wantedProduct, ProductID = wantedProduct.ID });
                }

                _context.SaveChangesAsync();
                DisplayMessageBox(true);
            }
            else
            {
                DisplayMessageBox(false);
            }
            BindDataToDataGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var product = _context.Products.Find(int.Parse(txbProductID.Text.Trim()));
            if (product == null)
            {
                product = new Product() { ID = int.Parse(txbProductID.Text.Trim()), Name = txbProductName.Text.Trim(), SupplierID = int.Parse(cbxProductSuppliers.Text) };
                string[] units = txbProductUnits.Text.Trim().Split(',');
                for (int i = 0; i < units.Length; i++)
                {
                    if (i == 0)
                    {
                        product.ProductUnits.Clear();
                    }
                    product.ProductUnits.Add(new ProductUnit { Unit = units[i].Trim(), Product = product, ProductID = product.ID });
                }
                _context.Products.Add(product);
                _context.SaveChangesAsync();
                DisplayMessageBox(true);
            }
            else
            {
                DisplayMessageBox(false);
            }
            BindDataToDataGrid();
        }

        private static void DisplayMessageBox(bool flag)
        {
            AbokhaledMBox messageBox = new AbokhaledMBox(flag);
            messageBox.ShowDialog();
        }

        private void cbxProductSuppliers_Click(object sender, EventArgs e)
        {
            LoadDataToComboBox();
        }

        private void LoadDataToComboBox()
        {
            var supIDList = _context.Suppliers.Select(s => s.ID).ToArray();
            cbxProductSuppliers.DataSource = supIDList;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var wantedProduct = _context.Products.Find(int.Parse(txbProductID.Text));
            if (wantedProduct != null)
            {
                _context.Products.Remove(wantedProduct);
                await _context.SaveChangesAsync();
                DisplayMessageBox(true);
            }
            else
            {
                DisplayMessageBox(false);
            }

            BindDataToDataGrid();
        }
    }
}