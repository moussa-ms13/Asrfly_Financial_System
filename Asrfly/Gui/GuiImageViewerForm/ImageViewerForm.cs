using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Asrfly.Gui.GuiImageViewer
{
    public partial class ImageViewerForm : Form
    {
        private const string SupabaseBaseUrl = "https://ehhkkdryilinvleqanqs.supabase.co/storage/v1/object/public/receipts/";

        public ImageViewerForm()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        public void SetImage(string imageString)
        {
            if (!string.IsNullOrEmpty(imageString))
            {
                try
                {
                    if (IsUrlOrFileName(imageString))
                    {
                        string fullUrl = SupabaseBaseUrl + imageString;

                        var request = WebRequest.Create(fullUrl);
                        using (var response = request.GetResponse())
                        using (var stream = response.GetResponseStream())
                        {
                            MemoryStream ms = new MemoryStream();
                            stream.CopyTo(ms);
                            ms.Position = 0;
                            pictureBox1.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        byte[] imageBytes = Convert.FromBase64String(imageString);
                        MemoryStream ms = new MemoryStream(imageBytes);
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("حدث خطأ أثناء تحميل الصورة: \n" + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool IsUrlOrFileName(string input)
        {
            string lowerInput = input.ToLower();
            return lowerInput.EndsWith(".jpg") ||
                   lowerInput.EndsWith(".jpeg") ||
                   lowerInput.EndsWith(".png") ||
                   lowerInput.EndsWith(".bmp") ||
                   lowerInput.Contains("http");
        }
    }
}
