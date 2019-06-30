using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VectorFont
{
    public partial class Form1 : Form
    {
        VectorFont vf = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            vf = VectorFont.Load("normalized_font.dict", "normalized_font.vector");
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                int utf16 = VectorFont.CharToUTF16(textBox1.Text[0]);

                Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                Graphics g = Graphics.FromImage(bmp);
                g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
                VectorArray va = vf.GetVectors(utf16);
                if (va != null)
                {
                    SizeF sz = va.GetSize();
                    g.DrawRectangle(Pens.Gray, 0.0F, 0.0F, sz.Width * 200.0F, sz.Height * 200.0F);

                    va.DrawWithMark(g, Pens.Black, 200.0F, 200.0F, 0.0F, 0.0F, Pens.Red, 5.0F, Pens.Blue, 5.0F);
                }
                g.Dispose();
                pictureBox1.Image = bmp;
            }
        }
    }
}
