using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Font = iTextSharp.text.Font;

namespace ROM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        public static readonly string con_string = @"data source = MSI\PHUMMIN; database = ROM; Trusted_Connection=yes;";
        public static SqlConnection con = new SqlConnection(con_string);

        private void LoadData(DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                // เปิดการเชื่อมต่อ
                con.Open();

                // SQL query เพื่อเลือกข้อมูลจาก tblMain พร้อมเงื่อนไขการกรองสถานะ "Paid"
                string query = "SELECT * FROM tblMain WHERE status = 'Paid'";

                // เพิ่มเงื่อนไขการกรองวันที่ถ้ามีการระบุ startDate และ endDate
                if (startDate.HasValue && endDate.HasValue)
                {
                    query += " AND aDate >= @startDate AND aDate <= @endDate";
                }

                // สร้าง data adapter
                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    // เพิ่ม parameters ไปที่ data adapter ถ้ามีการกรองวันที่
                    if (startDate.HasValue && endDate.HasValue)
                    {
                        da.SelectCommand.Parameters.AddWithValue("@startDate", startDate.Value);
                        da.SelectCommand.Parameters.AddWithValue("@endDate", endDate.Value);
                    }

                    // สร้าง DataTable เพื่อเก็บข้อมูล
                    DataTable dt = new DataTable();

                    // เติมข้อมูลจาก database ลงใน DataTable
                    da.Fill(dt);

                    // ผูก DataTable กับ DataGridView
                    dataGridView1.DataSource = dt;

                    // คำนวณผลรวมของคอลัมน์ 'total'
                    var total = dt.AsEnumerable().Sum(row => row.Field<float?>("total") ?? 0);
                    LabelTotal.Text = $"Total: {total:C}";
                }
            }
            catch (Exception ex)
            {
                // จัดการข้อผิดพลาดและแสดงข้อความ
                //MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // ปิดการเชื่อมต่อ
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void guna2ButtonSearch_Click(object sender, EventArgs e)
        {
            // รับวันที่เริ่มต้นและสิ้นสุดจากตัวเลือกวันที่
            DateTime startDate = dateTimePicker1.Value.Date;
            DateTime endDate = dateTimePicker2.Value.Date;

            // โหลดข้อมูลตามช่วงวันที่ที่ระบุ
            LoadData(startDate, endDate);

            // คำนวณและแสดงผลรวม
            CalculateTotal(startDate, endDate);
        }

        private void CalculateTotal(DateTime startDate, DateTime endDate)
        {
            try
            {
                // เปิดการเชื่อมต่อ
                con.Open();

                // SQL query เพื่อคำนวณผลรวมของคอลัมน์ 'total' สำหรับรายการที่มีสถานะ 'Paid' ภายในช่วงวันที่ที่กำหนด
                string query = @"
                                SELECT SUM(total) 
                                FROM tblMain 
                                WHERE status = 'Paid' AND aDate >= @startDate AND aDate <= @endDate";

                // สร้างคำสั่ง SqlCommand
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);

                // ดำเนินการคำสั่งและรับผลลัพธ์
                var result = cmd.ExecuteScalar();
                var total = result != DBNull.Value ? Convert.ToDecimal(result) : 0;

                // แสดงผลรวมในป้ายกำกับ
                LabelTotal.Text = $"Total: {total:C}";
            }
            catch (Exception)
            {
                // จัดการข้อยกเว้นโดยไม่ต้องแจ้งให้ทราบ หรือบันทึกไว้ที่ใดที่หนึ่งหากจำเป็น
            }
            finally
            {
                // ปิดการเชื่อมต่อ
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }


        // ตัวจัดการเหตุการณ์สำหรับการคลิกปุ่มค้นหา


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            main frm = new main();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;

            string connectionString = @"data source = MSI\PHUMMIN; database = ROM; Trusted_Connection=yes;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {   //aDate >= @StartDate: เลือกแถวที่มีค่าในคอลัมน์ aDate มากกว่าหรือเท่ากับค่าของพารามิเตอร์ 
                string query = "SELECT * FROM tblMain WHERE aDate >= @StartDate AND aDate <= @EndDate AND status = 'Paid'";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // สร้างไฟล์ PDF
                if (dataTable.Rows.Count > 0)
                {
                    string filePath = @"D:\Project C# Now\Bill\IncomeReport.pdf";
                    Document document = new Document();
                    document.SetPageSize(PageSize.A4.Rotate()); // เพิ่มบรรทัดนี้เพื่อกำหนดเป็นแนวนอน
                    PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                    document.Open();

                    BaseFont baseFont = BaseFont.CreateFont("D:\\Project C# Now\\ROM\\Resources\\THSarabunNew.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    Font thaiFont = new Font(baseFont, 12);

                    PdfPTable pdfTable = new PdfPTable(dataTable.Columns.Count);
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        pdfTable.AddCell(new Phrase(dataTable.Columns[i].ColumnName, thaiFont));
                    }

                    pdfTable.HeaderRows = 1;

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            pdfTable.AddCell(new Phrase(dataTable.Rows[i][j].ToString(), thaiFont));
                        }
                    }

                    // คำนวณค่ารวมของรายได้
                    double totalIncome = 0;
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        totalIncome += Convert.ToDouble(dataTable.Rows[i]["total"]);
                    }

                    // คำนวณ VAT 7%
                    double vatAmount = totalIncome * 0.07;
                    double totalWithVAT = totalIncome + vatAmount;

                    // เพิ่มเซลล์ที่แสดงรายได้รวม
                    PdfPCell totalIncomeCell = new PdfPCell(new Phrase("Total Income: " + totalIncome.ToString("N2"), thaiFont));
                    totalIncomeCell.Colspan = dataTable.Columns.Count;
                    totalIncomeCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    pdfTable.AddCell(totalIncomeCell);

                    // เพิ่มเซลล์ที่แสดง VAT
                    PdfPCell vatCell = new PdfPCell(new Phrase("VAT (7%): " + vatAmount.ToString("N2"), thaiFont));
                    vatCell.Colspan = dataTable.Columns.Count;
                    vatCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    pdfTable.AddCell(vatCell);

                    // เพิ่มเซลล์ที่แสดงรายได้รวม + VAT
                    PdfPCell totalWithVATCell = new PdfPCell(new Phrase("Total Income with VAT: " + totalWithVAT.ToString("N2"), thaiFont));
                    totalWithVATCell.Colspan = dataTable.Columns.Count;
                    totalWithVATCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    pdfTable.AddCell(totalWithVATCell);

                    // เพิ่มตารางลงในเอกสาร
                    document.Add(pdfTable);

                    // ปิดเอกสาร
                    document.Close();

                    MessageBox.Show("PDF ถูกสร้างเรียบร้อยแล้วที่ " + filePath, "สร้าง PDF เสร็จสิ้น", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลที่จะสร้าง PDF", "ไม่พบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }

}
