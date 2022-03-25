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
    public partial class Form1 : ProductForm
    {
        public Form1()
        {
            InitializeComponent();
            label2.BringToFront();
            label5.BringToFront();
            txbProductID.BringToFront();
        }
    }
}
