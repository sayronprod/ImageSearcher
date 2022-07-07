using ImageSearcher.Services;
using System.Diagnostics;

namespace ImageSearcher
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private byte[] ImageToByteArray(Image image)
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(image, typeof(byte[]));
            return xByte;
        }

        private async void notifyIcon1_Click(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            this.TopMost = true;
            await HanleScreenhot();
            this.TopMost = false;
        }

        private async Task HanleScreenhot()
        {
            Image bmp = SnippingTool.Snip();
            if (bmp != null)
            {
                PictureBox pb = new PictureBox();
                pb.Image = bmp;
                Clipboard.SetDataObject(bmp);
                ImageService imageService = new ImageService();
                byte[] imageData = ImageToByteArray(bmp);
                string googleUrl = await imageService.GetImageUrl(imageData);
                Process.Start(new ProcessStartInfo(googleUrl.Replace("&", "^&")) { UseShellExecute = true });
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AutoStart_Click(object sender, EventArgs e)
        {
            bool isInStartup = StartupService.IsInStartup();
            if (isInStartup)
            {
                StartupService.DeleteFromStartup();
            }
            else
            {
                StartupService.AddToSturtup();
            }
        }

        private void ContextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool isInStartup = StartupService.IsInStartup();
            toolStripMenuItem1.Checked = isInStartup;
        }
    }
}
