using ROM.Model;
using ROM.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROM
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCategoryView frm = new frmCategoryView();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmProducts frm = new frmProducts();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmPOS frm = new frmPOS();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmTableView frm = new frmTableView();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmStaffView frm = new frmStaffView();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void btnKitchen_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmKitchenView frm = new frmKitchenView();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmincome frm = new frmincome();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmReport frm = new frmReport();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmHome frm = new frmHome();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin frm = new frmLogin();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void butform1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 frm = new Form1();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        
    }
}
