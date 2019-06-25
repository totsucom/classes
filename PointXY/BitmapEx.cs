using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace Geometory
{
    class BitmapEx
    {
        /*
         * 使い方
         * BitmapEx bex = BitmapEx.Begin(bitmap, System.Drawing.Imaging.ImageLockMode.ReadWrite);
         * bex.xxx(); //ごにょごにょする
         * bex.End();
         */


        public static BitmapEx Begin(Bitmap bmp, System.Drawing.Imaging.ImageLockMode mode)
        {
            switch (bmp.PixelFormat)
            {
                case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
                    return new BitmapEx(bmp,
                        bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                        mode,
                        System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                        System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                    return new BitmapEx(bmp,
                        bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                        mode,
                        System.Drawing.Imaging.PixelFormat.Format24bppRgb),
                        System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                default:
                    throw new Exception("Unsupported pixcel format: " + bmp.PixelFormat.ToString());
            }
        }




        private Bitmap _bmp = null;
        private BitmapData _bd = null;
        private IntPtr _Scan0;
        private int _Stride;
        private System.Drawing.Imaging.PixelFormat _format;
        private Color[] PalleteEntries;

        private BitmapEx(Bitmap bmp,BitmapData bd, PixelFormat format)
        {
            _bmp = bmp;
            _bd = bd;
            _Scan0 = _bd.Scan0;
            _Stride = _bd.Stride;
            _format = format;
            if (_format == PixelFormat.Format8bppIndexed)
            {
                PalleteEntries = new Color[256];
                for (int i = 0; i < 256; i++)
                {
                    PalleteEntries[i] = _bmp.Palette.Entries[i];
                }
            }
        }

        public void End()
        {
            if (_bd != null)
            {
                _bmp.UnlockBits(_bd);
                _bd = null;
            }
        }

        //範囲チェックしてないので注意
        public Color GetPixel(int x, int y)
        {
            if (_format == PixelFormat.Format8bppIndexed)
            {
                byte b = System.Runtime.InteropServices.Marshal.ReadByte(_Scan0, _Stride * y + x);
                return PalleteEntries[b];
            }
            else //if (_format == PixelFormat.Format24bppRgb)
            {
                int pos = _Stride * y + x * 3;
                byte b = System.Runtime.InteropServices.Marshal.ReadByte(_Scan0, pos + 0);
                byte g = System.Runtime.InteropServices.Marshal.ReadByte(_Scan0, pos + 1);
                byte r = System.Runtime.InteropServices.Marshal.ReadByte(_Scan0, pos + 2);
                return Color.FromArgb(r, g, b);
            }
        }
        //範囲チェックしてないので注意
        //値を直接取り出します
        public int GetPixelDirect(int x, int y)
        {
            if (_format == PixelFormat.Format8bppIndexed)
            {
                unsafe
                {
                    return *((byte*)_Scan0 + _Stride * y + x);
                }
                //byte b = System.Runtime.InteropServices.Marshal.ReadByte(_Scan0, _Stride * y + x);
                //return (int)b;
            }
            else //if (_format == PixelFormat.Format24bppRgb)
            {
                unsafe
                {
                    byte* p = (byte*)_Scan0 + _Stride * y + x * 3;
                    return (int)((uint)*p | ((uint)*(p + 1) << 8) | ((uint)*(p + 2) << 16));
                }
                //IntPtr adr = _Scan0;
                //int pos = _Stride * y + x * 3;
                //byte b = System.Runtime.InteropServices.Marshal.ReadByte(adr, pos + 0);
                //byte g = System.Runtime.InteropServices.Marshal.ReadByte(adr, pos + 1);
                //byte r = System.Runtime.InteropServices.Marshal.ReadByte(adr, pos + 2);
                //return (int)(((uint)r << 16) | ((uint)g << 8) | b);
            }
        }

        //範囲チェックしてないので注意
        //8ビットインデックスの場合は明るさをカラーインデックスに変換して書き込むことに注意
        public void SetPixel(int x, int y, Color col)
        {
            if (_format == PixelFormat.Format8bppIndexed)
            {
                System.Runtime.InteropServices.Marshal.WriteByte(_Scan0, _Stride * y + x, (byte)(col.GetBrightness() * 255.0F));
            }
            else //if (_format == PixelFormat.Format24bppRgb)
            {
                int pos = _Stride * y + x * 3;
                System.Runtime.InteropServices.Marshal.WriteByte(_Scan0, pos + 0, col.B);
                System.Runtime.InteropServices.Marshal.WriteByte(_Scan0, pos + 1, col.G);
                System.Runtime.InteropServices.Marshal.WriteByte(_Scan0, pos + 2, col.R);
            }
        }

        //範囲チェックしてないので注意
        //直接値を書き込みます
        public void SetPixelDirect(int x, int y, int data)
        {
            if (_format == PixelFormat.Format8bppIndexed)
            {
                unsafe
                {
                    *((byte *)_Scan0 + _Stride * y + x) = (byte)(data & 255);
                }
                //System.Runtime.InteropServices.Marshal.WriteByte(_Scan0, _Stride * y + x, (byte)(data & 255));
            }
            else //if (_format == PixelFormat.Format24bppRgb)
            {
                unsafe
                {
                    byte* p = (byte*)_Scan0 + _Stride * y + x * 3;
                    *p++ = (byte)(data & 255);
                    *p++ = (byte)((data >> 8) & 255);
                    *p++ = (byte)((data >> 16) & 255);
                }
                //IntPtr adr = _Scan0;
                //int pos = _Stride * y + x * 3;
                //System.Runtime.InteropServices.Marshal.WriteByte(adr, pos + 0, (byte)(data & 255));
                //System.Runtime.InteropServices.Marshal.WriteByte(adr, pos + 1, (byte)((data >> 8) & 255));
                //System.Runtime.InteropServices.Marshal.WriteByte(adr, pos + 2, (byte)((data >> 16) & 255));
            }
        }

    }
}
