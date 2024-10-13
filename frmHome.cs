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
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            main frm = new main();
            frm.StartPosition = FormStartPosition.CenterScreen; // เพิ่มบรรทัดนี้ถ้าต้องการให้ฟอร์มใหม่แสดงตรงกลางเช่นกัน
            frm.Show();
        }
    }
}
