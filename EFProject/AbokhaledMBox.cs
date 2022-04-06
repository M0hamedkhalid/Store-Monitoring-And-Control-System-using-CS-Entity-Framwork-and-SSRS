using System;
using System.Drawing;
using System.Windows.Forms;

namespace EFProject
{
    public partial class AbokhaledMBox : Form
    {
        public AbokhaledMBox(bool flag)
        {
            InitializeComponent();
            if (flag)
            {
                RightOperation();
            }
            else
            {
                WrongOperation();
            }
        }

        private void RightOperation()
        {
            pictureBox2.Image = imageList1.Images[1];
            pictureBox2.Refresh();
            lblMessage.Text = "Operation Done Successfully!";
            pnlDown.BackColor = Color.FromArgb(0, 122, 204);
            splitter5.BackColor = splitter6.BackColor = splitter7.BackColor = Color.FromArgb(0, 122, 204);
        }

        private void WrongOperation()
        {
            pictureBox2.Image = imageList1.Images[0];
            pictureBox2.Refresh();
            lblMessage.Text = "Failed To Do This Operation!";
            pnlDown.BackColor = Color.Crimson;
            splitter5.BackColor = splitter6.BackColor = splitter7.BackColor = Color.Crimson;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}