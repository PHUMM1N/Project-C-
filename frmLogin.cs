using Guna.UI2.WinForms;
using ROM.RMS;
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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            txtUser.Text = "admin";
            txtPass.Text = "123";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (MainClass.IsValidUser(txtUser.Text, txtPass.Text) == false)
            {
                //guna2MessageDialog1.Show("ใส่ชื่อ หรือ รหัสผ่าน ให้ถูกต้อง");

                txtUser.Text = "";
                txtPass.Text = "";
             
                RMSLogin frm = new RMSLogin();
                frm.Show();
                return;
            }
            else
            {
                this.Hide();
                main frm = new main();
                frm.StartPosition = FormStartPosition.CenterScreen; // เพิ่มบรรทัดนี้ถ้าต้องการให้ฟอร์มใหม่แสดงตรงกลางเช่นกัน
                frm.Show();
            }
        }
    }
}
