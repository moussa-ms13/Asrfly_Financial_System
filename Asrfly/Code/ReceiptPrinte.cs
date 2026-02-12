using Asrfly.Core;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace Asrfly.Code
{
    public class ReceiptPrinter
    {
        private object _transactionObject;
        private string _title;

        private Font fontTitle = new Font("Arial", 16, FontStyle.Bold);
        private Font fontHeader = new Font("Arial", 12, FontStyle.Bold);
        private Font fontBody = new Font("Arial", 11, FontStyle.Regular);

        private Brush brush = Brushes.Black;

        public void Print(object transactionData)
        {
            _transactionObject = transactionData;

            if (_transactionObject is Income)
                _title = "سند قبض";
            else if (_transactionObject is Outcome)
                _title = "سند صرف";
            else
            {
                MessageBox.Show("نوع البيانات غير مدعوم للطباعة");
                return;
            }

            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;

            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = printDocument;

            previewDialog.Width = 800;
            previewDialog.Height = 600;
            previewDialog.StartPosition = FormStartPosition.CenterScreen;

            previewDialog.RightToLeft = RightToLeft.Yes;
            previewDialog.RightToLeftLayout = true;

            previewDialog.ShowDialog();
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            float margin = 50;
            float currentY = margin;
            float pageWidth = e.PageBounds.Width;
            float centerX = pageWidth / 2;

            if (!string.IsNullOrEmpty(Properties.Settings.Default.CompanyLogo))
            {
                try
                {
                    byte[] imageBytes = Convert.FromBase64String(Properties.Settings.Default.CompanyLogo);
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        Image logo = Image.FromStream(ms);
                        graphics.DrawImage(logo, centerX - 50, currentY, 100, 100);
                        currentY += 110;
                    }
                }
                catch { /* تجاهل الخطأ إذا كانت الصورة تالفة */ }
            }

            string companyName = Properties.Settings.Default.CompanyName;
            string date = DateTime.Now.ToString("dd/MM/yyyy");

            StringFormat rightAlign = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            graphics.DrawString(companyName, fontHeader, brush, pageWidth - margin, currentY, rightAlign);

            graphics.DrawString("تاريخ: " + date, fontBody, brush, margin, currentY);

            if (currentY < 100) currentY = 120;

            SizeF titleSize = graphics.MeasureString(_title, fontTitle);
            graphics.DrawString(_title, fontTitle, Brushes.DarkBlue, centerX - (titleSize.Width / 2), currentY);
            currentY += 50;

            graphics.DrawLine(Pens.Black, margin, currentY, pageWidth - margin, currentY);
            currentY += 30;

            StringFormat rtlFormat = new StringFormat(StringFormatFlags.DirectionRightToLeft);

            if (_transactionObject is Income income)
            {
                DrawRow(graphics, "رقم الوصل:", income.RecNo, ref currentY, margin, pageWidth, rtlFormat);
                DrawRow(graphics, "تاريخ العملية:", income.IncomeDate.ToString("dd/MM/yyyy"), ref currentY, margin, pageWidth, rtlFormat);
                DrawRow(graphics, "العميل:", income.SupplierName, ref currentY, margin, pageWidth, rtlFormat);
                DrawRow(graphics, "الصنف:", income.CategoryName, ref currentY, margin, pageWidth, rtlFormat);
                DrawRow(graphics, "المبلغ:", income.Amount.ToString("N2") + " د.ج", ref currentY, margin, pageWidth, rtlFormat);
                DrawRow(graphics, "التفاصيل:", income.Details, ref currentY, margin, pageWidth, rtlFormat);
            }
            else if (_transactionObject is Outcome outcome)
            {
                DrawRow(graphics, "رقم الوصل:", outcome.RecNo, ref currentY, margin, pageWidth, rtlFormat);
                DrawRow(graphics, "تاريخ العملية:", outcome.OutcomeDate.ToString("dd/MM/yyyy"), ref currentY, margin, pageWidth, rtlFormat);
                DrawRow(graphics, "المورد:", outcome.SupplierName, ref currentY, margin, pageWidth, rtlFormat);
                DrawRow(graphics, "الصنف:", outcome.CategoryName, ref currentY, margin, pageWidth, rtlFormat);
                DrawRow(graphics, "المبلغ:", outcome.Amount.ToString("N2") + " د.ج", ref currentY, margin, pageWidth, rtlFormat);
                DrawRow(graphics, "التفاصيل:", outcome.Details, ref currentY, margin, pageWidth, rtlFormat);
            }

            currentY += 80;
            graphics.DrawString("توقيع المستلم:", fontHeader, brush, margin + 50, currentY);
            graphics.DrawString("توقيع المحاسب:", fontHeader, brush, pageWidth - margin, currentY, rightAlign);

            Pen borderPen = new Pen(Color.Black, 2);
            graphics.DrawRectangle(borderPen, margin / 2, margin / 2, pageWidth - margin, e.PageBounds.Height - margin);
        }

        private void DrawRow(Graphics g, string label, string value, ref float y, float margin, float pageWidth, StringFormat format)
        {
            g.DrawString(label, fontHeader, Brushes.DimGray, pageWidth - margin, y, format);

            g.DrawString(value, fontBody, Brushes.Black, pageWidth - margin - 150, y, format);

            y += 40;
        }
    }
}
