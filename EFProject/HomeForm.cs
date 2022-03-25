using EFProject.DataBaseModel;
using System;
using System.Linq;
using System.Windows.Forms;

namespace EFProject
{
    public partial class HomeForm : Form
    {
        private StoreDb _context;

        public HomeForm()
        {
            InitializeComponent();
            _context = new StoreDb();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHour.Text = DateTime.Now.ToString("hh:mm:ss");

            lbDate.Text = DateTime.Now.ToLongDateString();
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            lbCustomersCount.Text = _context.Customers.Count().ToString();
            lbProductsCount.Text = _context.Products.Count().ToString();
            lbSuppliersCount.Text = _context.Suppliers.Count().ToString();
            lbWarehouseCount.Text = _context.Warehouses.Count().ToString();
        }
    }
}