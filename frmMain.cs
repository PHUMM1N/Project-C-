﻿using ROM.Model;
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
    public partial class frmMain : Form
    {
        private static object instance;

        public static object GetInstance()
        {
            return instance;
        }

        internal static void SetInstance(object value)
        {
            instance = value;
        }

        public frmMain()
        {
            InitializeComponent();
        }

        static frmMain _obj;
        public static frmMain Instance
        {
            get { if (_obj == null) { _obj = new frmMain(); } return _obj; }
        }

        public void AddControls(Form f)
        {
            CenterPanel.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            CenterPanel.Controls.Add(f);
            f.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblUser.Text = MainClass.USER;
            _obj = this;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            AddControls(new frmHome());
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            AddControls(new frmCategoryView());
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            AddControls(new frmTableView());
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            AddControls(new frmStaffView());
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            AddControls(new frmProducts());
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            frmPOS frmPOS = new frmPOS();
            frmPOS.Show();
        }

        private void btnKitchen_Click(object sender, EventArgs e)
        {
            AddControls(new frmKitchenView());
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            AddControls(new frmincome());
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            AddControls(new frmReport());
        }
    }
}
