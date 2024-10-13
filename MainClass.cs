using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROM
{
    internal class MainClass
    {

        public static readonly string con_string = @"data source = MSI\PHUMMIN; database = ROM; Trusted_Connection=yes;";
        public static SqlConnection con = new SqlConnection(con_string);


        public static bool IsValidUser(string user, string pass)
        {
            bool isValid = false;

            string qry = @"Select * from users where username = '" + user + "'and upass='" + pass + "' ";
            SqlCommand cmd = new SqlCommand(qry, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                isValid = true;
                USER = dt.Rows[0]["uName"].ToString();

            }


            return isValid;
        }

        public static string user;

        public static string USER
        {
            get { return user; }
            private set { user = value; }
        }

        public static string ConnectionString { get; internal set; }

        public static int SQl(string qry, Hashtable ht)
        {
            int res = 0;

            try
            {
                // สร้างวัตถุ SqlCommand สำหรับคำสั่ง SQL ที่กำหนด
                SqlCommand cdm = new SqlCommand(qry, con);
                cdm.CommandType = CommandType.Text;

                // เพิ่มพารามิเตอร์จาก Hashtable ไปยัง SqlCommand
                foreach (DictionaryEntry item in ht)
                {
                    cdm.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                }

                // ตรวจสอบและเปิดการเชื่อมต่อกับฐานข้อมูลถ้ายังไม่ได้เปิด
                if (con.State == ConnectionState.Closed) { con.Open(); }

                // ExecuteNonQuery เพื่อดำเนินการคำสั่ง SQL (INSERT, UPDATE, DELETE) และรับจำนวนแถวที่ได้รับผลกระทบ
                res = cdm.ExecuteNonQuery();

                // ปิดการเชื่อมต่อถ้ายังเปิดอยู่
                if (con.State == ConnectionState.Open) { con.Close(); }
            }
            catch (Exception ex)
            {
                // จัดการข้อผิดพลาดและแสดงข้อความในกล่องข้อความ
                MessageBox.Show(ex.ToString());

                // ปิดการเชื่อมต่อถ้าเปิดอยู่เมื่อเกิดข้อผิดพลาด
                con.Close();
            }

            // ส่งคืนจำนวนแถวที่ได้รับผลกระทบจากคำสั่ง SQL
            return res;
        }


        public static void LoadData(string qry, DataGridView gv, ListBox lb)
        {
            // เพิ่มเหตุการณ์ CellFormatting ให้กับ DataGridView gv
            gv.CellFormatting += new DataGridViewCellFormattingEventHandler(gv_CellFormatting);

            try
            {
                // สร้าง SqlCommand ด้วยคำสั่ง SQL ที่กำหนด (qry) และการเชื่อมต่อ (con)
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.CommandType = CommandType.Text;

                // สร้าง SqlDataAdapter เพื่อดึงข้อมูลจากฐานข้อมูล
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                // สร้าง DataTable เพื่อเก็บข้อมูลจากฐานข้อมูล
                DataTable dt = new DataTable();

                // เติมข้อมูลจากฐานข้อมูลลงใน DataTable
                da.Fill(dt);

                // วนลูปผ่านแต่ละรายการใน ListBox (lb)
                for (int i = 0; i < lb.Items.Count; i++)
                {
                    // ดึงชื่อคอลัมน์จาก DataGridViewColumn ที่อยู่ใน ListBox
                    string colNam1 = ((DataGridViewColumn)lb.Items[i]).Name;

                    // กำหนด DataPropertyName ของคอลัมน์ใน DataGridView เพื่อเชื่อมต่อกับคอลัมน์ใน DataTable
                    gv.Columns[colNam1].DataPropertyName = dt.Columns[i].ToString();
                }

                // กำหนด DataSource ของ DataGridView ให้เป็น DataTable ที่เต็มไปด้วยข้อมูล
                gv.DataSource = dt;
            }
            catch (Exception ex)
            {
                // ถ้าเกิดข้อผิดพลาด จะแสดงข้อความแสดงข้อผิดพลาดใน MessageBox
                MessageBox.Show(ex.ToString());

                // ปิดการเชื่อมต่อกับฐานข้อมูลถ้ามันเปิดอยู่
                con.Close();
            }
        }

        // ซึ่งจะถูกเรียกใช้งานเมื่อเซลล์ใน DataGridView กำลังถูกจัดรูปแบบสำหรับการแสดงผล
        private static void gv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Guna.UI2.WinForms.Guna2DataGridView gv = (Guna.UI2.WinForms.Guna2DataGridView)sender;
            int count = 0;

            foreach (DataGridViewRow row in gv.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        public static void BlurBackground(Form model)
        {
            Form background = new Form
            {
                StartPosition = FormStartPosition.Manual,
                FormBorderStyle = FormBorderStyle.None,
                Opacity = 0.5d,
                BackColor = Color.Black,
                Size = frmMain.Instance.Size,
                Location = frmMain.Instance.Location,
                ShowInTaskbar = false
            };

            // แสดง background form
            background.Show();

            // ตั้ง owner ของ model form เป็น background form
            //model.Owner = background;

            // แสดง model form เป็น dialog
            model.ShowDialog();

            // ปิดและทำลาย background form หลังจากปิด model form
            background.Dispose();
        }


        public static void CBFill(string qry, ComboBox cb)
        {
            // สร้าง SqlCommand ด้วยคำสั่ง SQL และการเชื่อมต่อ
            SqlCommand cdm = new SqlCommand(qry, con);
            cdm.CommandType = CommandType.Text;

            // ใช้ SqlDataAdapter ดึงข้อมูลจากฐานข้อมูล
            SqlDataAdapter da = new SqlDataAdapter(cdm);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // ตั้งค่าคุณสมบัติของ ComboBox
            cb.DisplayMember = "name"; // คอลัมน์ที่แสดง
            cb.ValueMember = "id";     // คอลัมน์ที่ใช้เป็นค่า
            cb.DataSource = dt;        // กำหนด DataSource เป็น DataTable
            cb.SelectedIndex = -1;     // ไม่เลือกค่าเริ่มต้น
        }


        internal static DataTable GetData(string qry)
        {
            // Connection String สำหรับเชื่อมต่อกับฐานข้อมูล SQL Server
            string connectionString = "Data Source=MSI\\PHUMMIN; database = ROM; Trusted_Connection=yes";

            // สร้าง DataTable เพื่อเก็บข้อมูลที่จะคืนค่า
            DataTable dataTable = new DataTable();

            // ใช้ SqlConnection เพื่อเชื่อมต่อกับฐานข้อมูล
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // เปิดการเชื่อมต่อ
                connection.Open();

                // สร้าง SqlCommand เพื่อส่งคำสั่ง SQL ไปยังฐานข้อมูล
                using (SqlCommand command = new SqlCommand(qry, connection))
                {
                    // ใช้ SqlDataAdapter เพื่อดึงข้อมูลจากฐานข้อมูลและเติมลงใน DataTable
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }

                // ปิดการเชื่อมต่อ
                connection.Close();
            }

            // คืนค่า DataTable ที่เก็บข้อมูล
            return dataTable;
        }


    }

}
