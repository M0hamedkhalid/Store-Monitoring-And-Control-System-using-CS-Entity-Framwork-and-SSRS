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
    public partial class SupplierForm : Form
    {
        StoreDb _context;

        public SupplierForm()
        {

            InitializeComponent();
            _context = new StoreDb();

            BindDataToDataGrid();
        }
        void BindDataToDataGrid()
        {
            var list = _context.Suppliers.ToList();
            dataGridView1.DataSource = list;

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                int id = (int)dataGridView1.Rows[row].Cells[0].Value;

                var wantedSupp = _context.Suppliers.Find(id);
                if (wantedSupp != null)
                {
                    txbName.Text = wantedSupp.Name;
                    txbID.Text = wantedSupp.ID.ToString();
                    txbAddress.Text = wantedSupp.Address;
                    txbFax.Text = wantedSupp.Fax.ToString();
                    txbMail.Text = wantedSupp.Mail;
                    txbMobile.Text = wantedSupp.Mobile.ToString();
                    txbPhone.Text = wantedSupp.Phone.ToString();
                    txbWebsite.Text = wantedSupp.Website;

                }
            }
            catch (Exception)
            {

            }
        }

        private void Edit()
        {
            var supp = _context.Suppliers.Find(int.Parse(txbID.Text.Trim()));
            if (supp != null)
            {
                if (txbID.Text != "" && txbName.Text != "" && txbMobile.Text != "" && txbAddress.Text != "" && txbFax.Text != "" && txbPhone.Text != "")
                {
                    supp.Name = txbName.Text;
                    supp.Address = txbAddress.Text;
                    supp.Fax = double.Parse(txbFax.Text);
                    supp.Mail = txbMail.Text;
                    supp.Website = txbWebsite.Text.Trim();
                    supp.Mobile = double.Parse(txbMobile.Text);
                    supp.Phone = double.Parse(txbPhone.Text);


                }
            }
            _context.SaveChanges();
        }


        void Add()
        {

            if (txbID.Text != "" && txbName.Text != "" && txbMobile.Text != "" && txbAddress.Text != "" && txbFax.Text != "" && txbPhone.Text != "")
            {
                var supplier = _context.Suppliers.Find(int.Parse(txbID.Text));

                if (supplier == null)
                {
                    supplier = new Supplier()
                    {
                        ID = int.Parse(txbID.Text),
                        Address = txbAddress.Text.Trim(),
                        Name = txbName.Text,
                        Mail = txbMail.Text.Trim(),
                        Website = txbWebsite.Text.Trim(),
                        Mobile = double.Parse(txbMobile.Text.Trim()),
                        Phone = double.Parse(txbPhone.Text.Trim()),
                        Fax = double.Parse(txbFax.Text.Trim()),

                    };

                    _context.Suppliers.Add(supplier);

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
      
     
        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            Add();
            BindDataToDataGrid();
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
    }
}
