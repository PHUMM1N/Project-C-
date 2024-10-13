using Guna.UI2.WinForms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using ROM.Model;
using System;
using System.Collections;
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
using System.Diagnostics;

namespace ROM
{
    public partial class frmincome : Form
    {
        public frmincome()
        {
            InitializeComponent();
        }

        public int MainID = 0;

        private void frmincome_Load(object sender, EventArgs e)
        {
            GetData();
        }

        public void GetData()
        {
            if (MainID == 0)
            {
                string qry = "SELECT MainID, aDate, aTime, WaiterName, orderType, CustName, CustPhone, total " +
                             "FROM tblMain WHERE (CustName LIKE '%" + txtSearch.Text + "%' OR CustPhone LIKE '%" + txtSearch.Text +
                             "%' OR orderType LIKE '%" + txtSearch.Text + "%' OR WaiterName LIKE '%" + txtSearch.Text + "%')" +
                             " AND status = 'Paid' ORDER BY aDate DESC";

                ListBox lb = new ListBox();
                lb.Items.Add(dgvid);
                lb.Items.Add(dgvDate);
                lb.Items.Add(dgvTime);
                lb.Items.Add(dgvWaiter);
                lb.Items.Add(dgvOrderType);
                lb.Items.Add(dgvCustName);
                lb.Items.Add(dgvCusPhone);
                lb.Items.Add(dgvTotal);

                MainClass.LoadData(qry, guna2DataGridView1, lb);
            }
        }




        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ตรวจสอบว่าไม่ได้คลิกที่หัวคอลัมน์หรือแถว
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // ตรวจสอบว่า CurrentRow ไม่เป็น null
                var currentRow = guna2DataGridView1.CurrentRow;
                if (currentRow != null)
                {
                    // ตรวจสอบว่าเซลล์ไม่เป็น null และมีคอลัมน์ตามที่ระบุ
                    var currentCell = guna2DataGridView1.CurrentCell;
                    if (currentCell != null && currentCell.OwningColumn != null)
                    {
                        if (currentCell.OwningColumn.Name == "dgvPrint")
                        {
                            int mainID = Convert.ToInt32(currentRow.Cells["dgvid"].Value);
                            string folderPath = @"D:\Project C# Now\Bill";
                            string fileName = $"Bill_{mainID}.pdf";
                            string filePath = Path.Combine(folderPath, fileName);

                            // ตรวจสอบว่ามีไฟล์ชื่อซ้ำหรือไม่
                            int fileIndex = 1;
                            while (File.Exists(filePath))
                            {
                                fileName = $"Bill_{mainID}_{fileIndex}.pdf";
                                filePath = Path.Combine(folderPath, fileName);
                                fileIndex++;
                            }

                            // ดำเนินการบันทึกบิลไปยังโฟลเดอร์ที่ระบุ
                            SaveBill(mainID, filePath);

                            // เปิดไฟล์ PDF ที่บันทึกไว้ทันที
                            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                        }
                        else if (currentCell.OwningColumn.Name == "dgvdel")
                        {
                            DialogResult result = MessageBox.Show("คุณแน่ใจหรือไม่ที่จะลบรายการนี้?", "ยืนยันการลบ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                int mainID = Convert.ToInt32(currentRow.Cells["dgvid"].Value);
                                DeleteRecord(mainID); // เรียกใช้งานฟังก์ชันลบข้อมูล
                                GetData(); // โหลดข้อมูลใหม่หลังจากลบ
                            }
                        }
                    }
                }
            }
        }



        private void DeleteRecord(int mainID)
        {
            string qry = "DELETE FROM tblMain WHERE MainID = @MainID";
            Hashtable ht = new Hashtable();
            ht.Add("@MainID", mainID);
            MainClass.SQl(qry, ht);
            MessageBox.Show("ลบข้อมูลเรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void SaveBill(int mainID, string directory)
        {
            // สร้างชื่อไฟล์บิลโดยใช้ MainID
            string fileName = $"Bill_{mainID}.pdf";

            // ตรวจสอบว่าโฟลเดอร์ที่ระบุมีอยู่หรือไม่ ถ้าไม่มีให้สร้างโฟลเดอร์
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // เตรียมข้อมูลที่จะเขียนลงในไฟล์ PDF
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(Path.Combine(directory, fileName), FileMode.Create));
            document.Open();

            // โหลดฟอนท์ภาษาไทย
            BaseFont baseFont = BaseFont.CreateFont("D:\\Project C# Now\\ROM\\Resources\\THSarabunNew.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font thaiFont = new Font(baseFont, 16);

            // เพิ่มโลโก้
            string logoPath = "D:\\Project C# Now\\logo new.png";
            if (File.Exists(logoPath))
            {
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                logo.ScaleToFit(100f, 100f); // ปรับขนาดของโลโก้ให้เหมาะสม
                logo.Alignment = iTextSharp.text.Image.ALIGN_CENTER; // จัดให้อยู่กึ่งกลาง
                document.Add(logo);
            }


            // เพิ่มข้อมูล TableName, WaiterName, orderType, และ status จากฐานข้อมูล tblMain
            string mainQuery = $@"SELECT TableName, WaiterName, orderType, status, total 
                        FROM tblMain
                        WHERE MainID = {mainID}";
            DataTable mainData = MainClass.GetData(mainQuery);

            if (mainData.Rows.Count > 0)
            {
                DataRow mainRow = mainData.Rows[0];
                Paragraph mainInfoParagraph = new Paragraph();
                mainInfoParagraph.Add(new Chunk($"OrderType: {mainRow["orderType"]}\n", thaiFont));
                mainInfoParagraph.Add(new Chunk($"TableName: {mainRow["TableName"]}\n", thaiFont));
                mainInfoParagraph.Add(new Chunk($"WaiterName: {mainRow["WaiterName"]}\n\n", thaiFont));
                document.Add(mainInfoParagraph); // เพิ่มข้อมูลทั่วไปก่อนตารางสินค้า
            }

            // เพิ่มข้อมูลรายการสินค้า (products) ที่เกี่ยวข้องกับบิล (MainID)
            string detailQuery = $@"SELECT p.pID, p.pName, d.qty, d.price
                        FROM tblDetails d
                        INNER JOIN products p ON p.pID = d.proID
                        WHERE d.MainID = {mainID}";

            DataTable detailData = MainClass.GetData(detailQuery);

            if (detailData.Rows.Count > 0)
            {
                Paragraph productListParagraph = new Paragraph("Product List:\n", thaiFont);
                document.Add(productListParagraph);

                PdfPTable productTable = new PdfPTable(3); // สร้างตารางสำหรับแสดงรายการสินค้า
                productTable.DefaultCell.Border = PdfPCell.NO_BORDER; // ตั้งค่าเส้นขอบให้เป็น NO_BORDER

                productTable.AddCell(new PdfPCell(new Phrase("Name", thaiFont)) { Border = PdfPCell.NO_BORDER });
                productTable.AddCell(new PdfPCell(new Phrase("qty", thaiFont)) { Border = PdfPCell.NO_BORDER });
                productTable.AddCell(new PdfPCell(new Phrase("price", thaiFont)) { Border = PdfPCell.NO_BORDER });

                foreach (DataRow detailRow in detailData.Rows)
                {
                    PdfPCell cell1 = new PdfPCell(new Phrase(detailRow["pName"].ToString(), thaiFont)) { Border = PdfPCell.NO_BORDER };
                    PdfPCell cell2 = new PdfPCell(new Phrase(detailRow["qty"].ToString(), thaiFont)) { Border = PdfPCell.NO_BORDER };
                    PdfPCell cell3 = new PdfPCell(new Phrase(detailRow["price"].ToString(), thaiFont)) { Border = PdfPCell.NO_BORDER };

                    productTable.AddCell(cell1);
                    productTable.AddCell(cell2);
                    productTable.AddCell(cell3);
                }

                document.Add(productTable); // เพิ่มตารางสินค้าในเอกสาร

                // เพิ่มข้อมูล Total หลังจากตารางสินค้า
                if (mainData.Rows.Count > 0)
                {
                    DataRow mainRow = mainData.Rows[0];
                    PdfPTable totalTable = new PdfPTable(1);
                    totalTable.DefaultCell.Border = PdfPCell.NO_BORDER;

                    // คำนวณ VAT 7%
                    double totalAmount = Convert.ToDouble(mainRow["total"]);
                    double vatAmount = Math.Round(totalAmount * 0.07, 0, MidpointRounding.AwayFromZero); // ปัดทศนิยมตามที่ต้องการ
                    double totalWithVAT = totalAmount + vatAmount;

                    PdfPCell totalCell = new PdfPCell(new Phrase($"Total: {totalAmount.ToString("0.00")}\n", thaiFont))
                    {
                        Border = PdfPCell.NO_BORDER,
                        HorizontalAlignment = Element.ALIGN_RIGHT // ชิดฝั่งขวา
                    };

                    PdfPCell vatCell = new PdfPCell(new Phrase($"VAT (7%): {vatAmount.ToString("0.00")}\n", thaiFont))
                    {
                        Border = PdfPCell.NO_BORDER,
                        HorizontalAlignment = Element.ALIGN_RIGHT // ชิดฝั่งขวา
                    };

                    PdfPCell totalWithVATCell = new PdfPCell(new Phrase($"Total with VAT: {totalWithVAT.ToString("0.00")}\n", thaiFont))
                    {
                        Border = PdfPCell.NO_BORDER,
                        HorizontalAlignment = Element.ALIGN_RIGHT // ชิดฝั่งขวา
                    };

                    totalTable.AddCell(totalCell);
                    totalTable.AddCell(vatCell);
                    totalTable.AddCell(totalWithVATCell);
                    document.Add(totalTable);
                }
            }

            // ปิดเอกสาร
            document.Close();
            // แสดงข้อความยืนยันการบันทึก
            MessageBox.Show("บิลถูกบันทึกเป็นไฟล์ PDF เรียบร้อยแล้ว");
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            main frm = new main();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }


    }
}
