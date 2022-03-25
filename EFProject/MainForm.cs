using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFProject
{
    public partial class MainForm : Form
    {
        Form _activeForm;
        public MainForm()
        {
            InitializeComponent();
        }


        // To Make Form Movable
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        //
        private void OpenChildForm(Form childForm)
        {
            string n;
            try
            {
                n = _activeForm.Name;
            }
            catch (Exception)
            {
                n = "0";


            }
             
            if (_activeForm != null && n != childForm.Name)
            {
                _activeForm.Close();
                

            }

            if (n != childForm.Name)
            {
                _activeForm = childForm;
                _activeForm.TopLevel = false;
                _activeForm.FormBorderStyle = FormBorderStyle.None;
                _activeForm.Dock = DockStyle.Fill;
                this.pnlMain.Controls.Add(_activeForm);
                _activeForm.BringToFront();
                _activeForm.Show();
                panelLeft.Width = 58;
            }



        }
        //
        // handle Close/mini/maxmi Icons
        private void iconNormal_Click(object sender, EventArgs e)
        {
            this.iconMaximize.Visible = true;
            this.iconNormal.Visible=false;
            this.WindowState = FormWindowState.Normal;
        }

        private void iconMaximize_Click(object sender, EventArgs e)
        {
            this.iconMaximize.Visible = false;
            this.iconNormal.Visible = true;
            this.WindowState = FormWindowState.Maximized;
        }

        private void iconMinimiz_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OpenMainForm()
        {
            OpenChildForm(new HomeForm());
            panelLeft.Width = 250;
        }

        //Menubtn
        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (panelLeft.Width == 250)
            {
                panelLeft.Width = 58;
            }
            else
                panelLeft.Width = 250;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            OpenMainForm();

        }

        
        private void btnProd_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ProductForm());
        }

        private void btnlogo_Click(object sender, EventArgs e)
        {
            OpenMainForm();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            OpenChildForm(new CustomerForm());
        }

        private void btnWarehouse_Click(object sender, EventArgs e)
        {
            OpenChildForm(new WarehouseForm());
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            OpenChildForm(new SupplierForm());
        }

        private void btnReceivedProducts_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ReceivedProductsForm());

        }

        private void btnProductWithDraw_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ProductWithDraw());
        }

        private void btnReports_Click(object sender, EventArgs e)
        {

            OpenChildForm(new ReportsForm());

        }
    }
}
