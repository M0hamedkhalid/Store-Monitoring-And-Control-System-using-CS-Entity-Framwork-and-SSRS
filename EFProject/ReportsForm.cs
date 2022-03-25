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
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
        }

        //private void ReportsForm_Load(object sender, EventArgs e)
        //{

        //    this.reportViewer1.RefreshReport();
        //}

        private void btnEdit_Click(object sender, EventArgs e)
        {

            this.reportViewer1.ServerReport.ReportPath = "/Report Project3/Report1";
            this.reportViewer1.RefreshReport();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.reportViewer1.ServerReport.ReportPath = "/Report 2/Report2";
            this.reportViewer1.RefreshReport();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.reportViewer1.ServerReport.ReportPath = "/Report3/Report3";
            this.reportViewer1.RefreshReport();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.reportViewer1.ServerReport.ReportPath = "/Report Project4/Report4";
            this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.reportViewer1.ServerReport.ReportPath = "/Report5/Report5";
            this.reportViewer1.RefreshReport();
        }
    }
}
