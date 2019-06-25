using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointXY
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Stopwatch sw = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buttonReset.PerformClick();
        }

        void StartProcess()
        {
            toolStripStatusLabel1.Text = "処理しています";
            sw.Restart();
        }

        void UpdateInfo()
        {
            sw.Stop();
            toolStripStatusLabel1.Text = $"処理時間 {sw.ElapsedMilliseconds}ミリ秒";

            pictureBox1.Image = bmp;
            toolStripStatusLabel2.Text = bmp.PixelFormat.ToString();

            sw.Restart();
            int[] hist = Geometory.BitmapFilter.GetHistgram(bmp);
            sw.Stop();
            Debug.Print($"GetHistgram() {sw.ElapsedMilliseconds}ミリ秒");

            float[] fist = new float[256];
            float max = float.MinValue;
            for (int i = 0; i < 256; i++)
            {
                if (hist[i] > 0)
                {
                    fist[i] = (float)Math.Log(hist[i]);
                }
                else
                {
                    fist[i] = 0;
                }
                if (max < fist[i]) max = fist[i];
            }
            Bitmap b = new Bitmap(256, pictureBox2.Height);
            Graphics g = Graphics.FromImage(b);
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, b.Width, b.Height));
            for (int i = 0; i < 256; i++)
            {

                g.DrawLine(Pens.Black, i, b.Height, i, b.Height - (fist[i] / max * b.Height));
            }
            int t = (int)numericUpDownThreshold.Value;
            g.DrawLine(Pens.Red, t, b.Height, t, 0);
            g.Dispose();
            pictureBox2.Image = b;
        }

        private async void ButtonToGrayScale_Click(object sender, EventArgs e)
        {
            Geometory.BitmapFilter.COLORELEMENT ce;
            if (radioButtonH.Checked)
            {
                ce = Geometory.BitmapFilter.COLORELEMENT.HUE;
            }
            else if (radioButtonS.Checked)
            {
                ce = Geometory.BitmapFilter.COLORELEMENT.SATURATION;
            }
            else
            {
                ce = Geometory.BitmapFilter.COLORELEMENT.BRIGHTNESS;
            }

            StartProcess();
            bmp = await Task.Run(() => Geometory.BitmapFilter.ConvertToGrayScale(bmp, ce));
            UpdateInfo();
        }

        private async void ButtonToRGB24_Click(object sender, EventArgs e)
        {
            StartProcess();
            bmp = await Task.Run(() => Geometory.BitmapFilter.ConvertToRGB24(bmp));
            UpdateInfo();
        }

        private async void ButtonToBinary_Click(object sender, EventArgs e)
        {
            int t = (int)numericUpDownThreshold.Value;
            StartProcess();
            bmp = await Task.Run(() => Geometory.BitmapFilter.ConvertToBinary(bmp, (byte)t));
            UpdateInfo();
        }

        int picture_no = 1;

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            String path = $"sample{picture_no}.jpg";
            bmp = (Bitmap)Image.FromFile(path);
            UpdateInfo();
        }

        private void ButtonReplace_Click(object sender, EventArgs e)
        {
            picture_no++;

            String path = $"sample{picture_no}.jpg";
            if (!System.IO.File.Exists(path))
            {
                picture_no = 1;
                path = $"sample{picture_no}.jpg";
            }

            bmp = (Bitmap)Image.FromFile(path);
            UpdateInfo();
        }

        private async void ButtonOtsu_Click(object sender, EventArgs e)
        {
            StartProcess();
            int t = await Task.Run(() => (int)Geometory.BitmapFilter.ThresholdOtsu(bmp));
            sw.Stop();
            numericUpDownThreshold.Value = t;
            UpdateInfo();
        }

        private async void ButtonSobel_Click(object sender, EventArgs e)
        {
            Geometory.BitmapFilter.COLORMODE cm;
            if (radioButtonBrightness.Checked)
            {
                cm = Geometory.BitmapFilter.COLORMODE.BRIGHTNESS;
            }
            else if (radioButtonRGB.Checked)
            {
                cm = Geometory.BitmapFilter.COLORMODE.RGB;
                if (bmp.PixelFormat == PixelFormat.Format8bppIndexed)
                {
                    MessageBox.Show("グレースケール画像に対して無効な処理です");
                    return;
                }
            }
            else
            {
                cm = Geometory.BitmapFilter.COLORMODE.HSV;
                if (bmp.PixelFormat == PixelFormat.Format8bppIndexed)
                {
                    MessageBox.Show("グレースケール画像に対して無効な処理です");
                    return;
                }
            }

            StartProcess();
            bmp = await Task.Run(() => Geometory.BitmapFilter.Sobel(bmp, cm));
            UpdateInfo();
        }

        private async void ButtonRotateClock_Click(object sender, EventArgs e)
        {
            StartProcess();
            await Task.Run(() => bmp.RotateFlip(RotateFlipType.Rotate90FlipNone));
            UpdateInfo();
        }

        private async void ButtonMedian_Click(object sender, EventArgs e)
        {
            StartProcess();
            bmp = await Task.Run(() => Geometory.BitmapFilter.Median(bmp, 5));
            UpdateInfo();
        }

        private async void ButtonGauss_Click(object sender, EventArgs e)
        {
            StartProcess();
            bmp = await Task.Run(() => Geometory.BitmapFilter.Gaussian(bmp));
            UpdateInfo();
        }

        private async void ButtonGradient_Click(object sender, EventArgs e)
        {
            Geometory.BitmapFilter.COLORMODE cm;
            if (radioButtonBrightness.Checked)
            {
                cm = Geometory.BitmapFilter.COLORMODE.BRIGHTNESS;
            }
            else if (radioButtonRGB.Checked)
            {
                cm = Geometory.BitmapFilter.COLORMODE.RGB;
                if (bmp.PixelFormat == PixelFormat.Format8bppIndexed)
                {
                    MessageBox.Show("グレースケール画像に対して無効な処理です");
                    return;
                }
            }
            else
            {
                cm = Geometory.BitmapFilter.COLORMODE.HSV;
                if (bmp.PixelFormat == PixelFormat.Format8bppIndexed)
                {
                    MessageBox.Show("グレースケール画像に対して無効な処理です");
                    return;
                }
            }

            StartProcess();
            bmp = await Task.Run(() => Geometory.BitmapFilter.Gradient(bmp, cm));
            UpdateInfo();
        }

        private async void ButtonLaplacian_Click(object sender, EventArgs e)
        {
            Geometory.BitmapFilter.COLORMODE cm;
            if (radioButtonBrightness.Checked)
            {
                cm = Geometory.BitmapFilter.COLORMODE.BRIGHTNESS;
            }
            else if (radioButtonRGB.Checked)
            {
                cm = Geometory.BitmapFilter.COLORMODE.RGB;
                if (bmp.PixelFormat == PixelFormat.Format8bppIndexed)
                {
                    MessageBox.Show("グレースケール画像に対して無効な処理です");
                    return;
                }
            }
            else
            {
                cm = Geometory.BitmapFilter.COLORMODE.HSV;
                if (bmp.PixelFormat == PixelFormat.Format8bppIndexed)
                {
                    MessageBox.Show("グレースケール画像に対して無効な処理です");
                    return;
                }
            }

            StartProcess();
            bmp = await Task.Run(() => Geometory.BitmapFilter.Laplacian(bmp, cm));
            UpdateInfo();
        }

        private void ButtonCopy_Click(object sender, EventArgs e)
        {
            if (bmp != null)
                Clipboard.SetDataObject(bmp, true);
        }

        private void ButtonBackup_Click(object sender, EventArgs e)
        {
            if (bmp == null) return;

            ListViewItem lvi = new ListViewItem(DateTime.Now.ToLongTimeString());
            lvi.Tag = bmp.Clone();
            listView1.Items.Add(lvi);
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1) return;
            listView1.Items.Remove(listView1.SelectedItems[0]);
        }

        private void ButtonRestore_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1) return;
            bmp = (Bitmap)listView1.SelectedItems[0].Tag;
            UpdateInfo();
        }

        private async void ButtonOperation_Click(object sender, EventArgs e)
        {
            if (bmp == null) return;
            if (listView1.SelectedItems.Count != 1) return;

            Bitmap bo = (Bitmap)listView1.SelectedItems[0].Tag;
            if (bo.Size != bmp.Size)
            {
                MessageBox.Show("画像の大きさが一致しません");
                return;
            }

            Geometory.BitmapFilter.PIXELOPERATION po;
            if (radioButtonAdd.Checked)
            {
                po = Geometory.BitmapFilter.PIXELOPERATION.ADD;
            }
            else //if (radioButtonMul.Checked)
            {
                po = Geometory.BitmapFilter.PIXELOPERATION.MUL;
            }

            StartProcess();
            bmp = await Task.Run(() => Geometory.BitmapFilter.Operation(bmp, bo, po));
            UpdateInfo();
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool b = listView1.SelectedItems.Count == 1;
            buttonRestore.Enabled = b;
            buttonOperation.Enabled = b;
            buttonDelete.Enabled = b;
        }
    }
}
