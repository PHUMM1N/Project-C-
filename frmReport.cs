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
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = iTextSharp.text.Font;

namespace ROM
{
    public partial class frmReport : Form
    {
        public frmReport()
        {
            InitializeComponent();
        }


        private void btnPrintPdf_Click_1(object sender, EventArgs e)
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




        private void btnPrintPdfToDay_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker3.Value.Date; // รับวันที่จาก dateTimePicker3 และใช้เฉพาะวันที่

            string connectionString = @"data source = MSI\PHUMMIN; database = ROM; Trusted_Connection=yes;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM tblMain WHERE CONVERT(date, aDate) = @SelectedDate AND status = 'Paid'";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SelectedDate", selectedDate);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // ตรวจสอบจำนวนแถวที่ได้จากการ query
                //MessageBox.Show("Rows retrieved: " + dataTable.Rows.Count, "Debug Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                BaseFont baseFont = BaseFont.CreateFont("D:\\Project C# Now\\ROM\\Resources\\THSarabunNew.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font thaiFont = new Font(baseFont, 12);

                // คำนวณรายได้รวมในวันนั้น
                double totalIncomeToday = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    totalIncomeToday += Convert.ToDouble(row["total"]);
                }

                // คำนวณ VAT 7%
                double vatAmount = totalIncomeToday * 0.07;
                double totalWithVAT = totalIncomeToday + vatAmount;

                // สร้างไฟล์ PDF
                string filePath = @"D:\Project C# Now\Bill\IncomeReportToday.pdf";
                Document document = new Document();
                document.SetPageSize(PageSize.A4.Rotate());
                PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();

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

                // เพิ่มเซลล์ที่แสดงรายได้รวมในวันนั้น, VAT และรวม VAT
                PdfPCell totalIncomeCell = new PdfPCell(new Phrase("Total Income Today: " + totalIncomeToday.ToString("N2"), thaiFont));
                totalIncomeCell.Colspan = dataTable.Columns.Count;
                totalIncomeCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                pdfTable.AddCell(totalIncomeCell);

                PdfPCell vatCell = new PdfPCell(new Phrase("VAT (7%): " + vatAmount.ToString("N2"), thaiFont));
                vatCell.Colspan = dataTable.Columns.Count;
                vatCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                pdfTable.AddCell(vatCell);

                PdfPCell totalWithVATCell = new PdfPCell(new Phrase("Total Income with VAT: " + totalWithVAT.ToString("N2"), thaiFont));
                totalWithVATCell.Colspan = dataTable.Columns.Count;
                totalWithVATCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                pdfTable.AddCell(totalWithVATCell);

                document.Add(pdfTable);
                document.Close();

                MessageBox.Show("PDF ถูกสร้างเรียบร้อยแล้วที่ " + filePath, "สร้าง PDF เสร็จสิ้น", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
