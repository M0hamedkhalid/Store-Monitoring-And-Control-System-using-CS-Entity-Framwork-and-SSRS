using EFProject.DataBaseModel;
using System;
using System.Linq;
using System.Windows.Forms;

namespace EFProject
{
    public partial class CustomerForm : Form
    {
        private StoreDb _context;

        public CustomerForm()
        {
            InitializeComponent();
            _context = new StoreDb();

            BindDataToDataGrid();
        }

        private void BindDataToDataGrid()
        {
            var list = _context.Customers.ToList();
            dataGridView1.DataSource = list;
        }

        private static void DisplayMessageBox(bool flag)
        {
            AbokhaledMBox messageBox = new AbokhaledMBox(flag);
            messageBox.ShowDialog();
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
        }

        private void Add()
        {
            if (txbCutID.Text != null && txbCustName.Text != "" && txbMobile.Text != "" && txbAddress.Text != "" && txbFax.Text != "" && txbPhone.Text != "")
            {
                var newCustomer = _context.Customers.Find(int.Parse(txbCutID.Text));

                if (newCustomer == null)
                {
                    newCustomer = new Customer()
                    {
                        ID = int.Parse(txbCutID.Text),
                        Address = txbAddress.Text.Trim(),
                        Name = txbCustName.Text,
                        Mail = txbMail.Text.Trim(),
                        Website = txbWebsite.Text.Trim(),
                        Mobile = double.Parse(txbMobile.Text.Trim()),
                        Phone = double.Parse(txbPhone.Text.Trim()),
                        Fax = double.Parse(txbFax.Text.Trim()),
                    };

                    _context.Customers.Add(newCustomer);

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                int id = (int)dataGridView1.Rows[row].Cells[0].Value;

                var wantedCustomer = _context.Customers.Find(id);
                if (wantedCustomer != null)
                {
                    txbCustName.Text = wantedCustomer.Name.Trim();
                    txbCutID.Text = wantedCustomer.ID.ToString().Trim();
                    txbAddress.Text = wantedCustomer.Address.Trim();
                    txbFax.Text = wantedCustomer.Fax.ToString().Trim();
                    txbMail.Text = wantedCustomer.Mail.Trim();
                    txbMobile.Text = wantedCustomer.Mobile.ToString().Trim();
                    txbPhone.Text = wantedCustomer.Phone.ToString().Trim();
                    txbWebsite.Text = wantedCustomer.Website.Trim();
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

        private void Edit()
        {
            var customer = _context.Customers.Find(int.Parse(txbCutID.Text.Trim()));
            if (customer != null)
            {
                if (txbCutID.Text != "" && txbCustName.Text != "" && txbMobile.Text != "" && txbAddress.Text != "" && txbFax.Text != "" && txbPhone.Text != "")
                {
                    customer.Name = txbCustName.Text;
                    customer.Address = txbAddress.Text;
                    customer.Fax = double.Parse(txbFax.Text);
                    customer.Mail = txbMail.Text;
                    customer.Website = txbWebsite.Text.Trim();
                    customer.Mobile = double.Parse(txbMobile.Text);
                    customer.Phone = double.Parse(txbPhone.Text);
                }
                DisplayMessageBox(true);
            }
            else
            {
                DisplayMessageBox(false);
            }
            _context.SaveChanges();
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
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var customer = _context.Customers.Find(int.Parse(txbCutID.Text.Trim()));
            if (customer != null)
            {
                _context.Customers.Remove(customer);
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
    }
}