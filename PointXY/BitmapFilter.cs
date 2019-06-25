using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Geometory
{
    class BitmapFilter
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        public enum COLORELEMENT
        {
            HUE,
            SATURATION,
            BRIGHTNESS
        }

        delegate float ColorFunc(Color c);

        public static Bitmap ConvertToGrayScale(Bitmap bmp, COLORELEMENT ce = COLORELEMENT.BRIGHTNESS)
        {
            //読み込み元ビットマップをロック
            BitmapData src = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            IntPtr srcAddr = src.Scan0;
            int srcStride = src.Stride;

            //書き込み先ビットマップを作成
            Bitmap dstBmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format8bppIndexed);

            //グレースケールのパレットを設定
            var pal = dstBmp.Palette;
            for (int i = 0; i < 256; i++)
            {
                pal.Entries[i] = Color.FromArgb(i, i, i);
            }
            dstBmp.Palette = pal;

            //書き込み先のビットマップをロック
            BitmapData dst = dstBmp.LockBits(new Rectangle(0, 0, dstBmp.Width, dstBmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            IntPtr dstAddr = dst.Scan0;
            int dstStride = dst.Stride;

            //色を処理する無名関数をセット
            ColorFunc f;
            switch (ce)
            {
                case COLORELEMENT.HUE:
                    f = (c => { return c.GetHue() / 360.0F; });
                    break;
                case COLORELEMENT.SATURATION:
                    f = (c => { return c.GetSaturation(); });
                    break;
                case COLORELEMENT.BRIGHTNESS:
                    f = (c => { return c.GetBrightness(); });
                    break;
                default:
                    throw new Exception();
            }

            //ループ処理
            int width = bmp.Width;
            System.Threading.Tasks.Parallel.For(0, bmp.Height, y => { //これは必須ですね
                unsafe //禁断だけどシンプルで速い
                {
                    uint* p = (uint*)((byte*)srcAddr + srcStride * y);
                    int count = width;
                    byte* q = (byte*)dstAddr + dstStride * y;
                    while (count > 0)
                    {
                        uint dw0 = *p++;
                        //0-255のグレースケールに変換
                        *q++ = (byte)(f(Color.FromArgb((int)dw0)) * 255.0F);
                        if (--count == 0) break;

                        uint dw1 = *p++;
                        *q++ = (byte)(f(Color.FromArgb((int)((dw1 << 8) | ((dw0 >> 24) & 255)))) * 255.0F);
                        if (--count == 0) break;

                        uint dw2 = *p++;
                        *q++ = (byte)(f(Color.FromArgb((int)((dw2 << 16) | ((dw1 >> 16) & 0xffff)))) * 255.0F);
                        if (--count == 0) break;

                        *q++ = (byte)(f(Color.FromArgb((int)(dw2 >> 8))) * 255.0F);
                        count--;
                    }

                    //byte* p = (byte*)srcAddr + srcStride * y;
                    //byte* q = (byte*)dstAddr + dstStride * y;
                    //for (int x = 0; x < width; x++)
                    //{
                    //    //色情報を読み込む
                    //    int c = *(int*)p; <= ここでメモリ境界を超えることがある
                    //    p += 3;

                    //    //0-255のグレースケールに変換
                    //    *q++ = (byte)(Color.FromArgb(c).GetBrightness() * 255.0F);
                    //}
                }
            });

            //ビットマップのロックを解除
            bmp.UnlockBits(src);
            dstBmp.UnlockBits(dst);
            return dstBmp;
        }
        public static Bitmap ConvertToRGB24(Bitmap bmp)
        {
            if (bmp.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                BitmapData src = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
                IntPtr srcAddr = src.Scan0;
                int srcStride = src.Stride;

                Bitmap dstBmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format24bppRgb);
                Color[] cols = bmp.Palette.Entries;

                BitmapData dst = dstBmp.LockBits(new Rectangle(0, 0, dstBmp.Width, dstBmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                IntPtr dstAddr = dst.Scan0;
                int dstStride = dst.Stride;

                int width = bmp.Width;
                System.Threading.Tasks.Parallel.For(0, bmp.Height, y => {
                    unsafe
                    {
                        byte* p = (byte*)srcAddr + srcStride * y;
                        byte* q = (byte*)dstAddr + dstStride * y;
                        for (int x = 0; x < width; x++)
                        {
                            Color c = cols[*p++];
                            *q++ = c.B;
                            *q++ = c.G;
                            *q++ = c.R;
                        }
                    }
                });

                bmp.UnlockBits(src);
                dstBmp.UnlockBits(dst);
                return dstBmp;
            }
            else
            {
                BitmapData src = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                IntPtr srcAddr = src.Scan0;

                Bitmap dstBmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format24bppRgb);

                BitmapData dst = dstBmp.LockBits(new Rectangle(0, 0, dstBmp.Width, dstBmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                IntPtr dstAddr = dst.Scan0;

                if (dst.Stride != src.Stride)
                    throw new Exception("Stride of Bitmap not same");

                CopyMemory(dstAddr, srcAddr, (uint)(bmp.Height * src.Stride));

                bmp.UnlockBits(src);
                dstBmp.UnlockBits(dst);
                return dstBmp;
            }
        }

        //２値化
        public static Bitmap ConvertToBinary(Bitmap bmp, byte threshold)
        {
            if (bmp.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                BitmapData src = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
                IntPtr srcAddr = src.Scan0;
                int srcStride = src.Stride;

                Bitmap dstBmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format8bppIndexed);
                Color[] cols = bmp.Palette.Entries;

                BitmapData dst = dstBmp.LockBits(new Rectangle(0, 0, dstBmp.Width, dstBmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                IntPtr dstAddr = dst.Scan0;
                int dstStride = dst.Stride;

                int width = bmp.Width;
                System.Threading.Tasks.Parallel.For(0, bmp.Height, y => {
                    unsafe
                    {
                        byte* p = (byte*)srcAddr + srcStride * y;
                        byte* q = (byte*)dstAddr + dstStride * y;
                        for (int x = 0; x < width; x++)
                        {
                            Color c = cols[*p++];

                            //0-255のグレースケールに変換後しきい値と比較
                            *q++ = (byte)(c.GetBrightness() * 255.0F) < threshold ? (byte)0 : (byte)255;
                        }
                    }
                });

                bmp.UnlockBits(src);
                dstBmp.UnlockBits(dst);

                //グレースケールパレットに変更
                var pal = dstBmp.Palette;
                for (int i = 0; i < 256; i++)
                {
                    pal.Entries[i] = Color.FromArgb(i, i, i);
                }
                dstBmp.Palette = pal;

                return dstBmp;
            }
            else
            {
                BitmapData src = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                IntPtr srcAddr = src.Scan0;
                int srcStride = src.Stride;

                Bitmap dstBmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format8bppIndexed);
                var pal = dstBmp.Palette;
                for (int i = 0; i < 256; i++)
                {
                    pal.Entries[i] = Color.FromArgb(i, i, i);
                }
                dstBmp.Palette = pal;

                BitmapData dst = dstBmp.LockBits(new Rectangle(0, 0, dstBmp.Width, dstBmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                IntPtr dstAddr = dst.Scan0;
                int dstStride = dst.Stride;

                int width = bmp.Width;
                System.Threading.Tasks.Parallel.For(0, bmp.Height, y =>
                {
                    unsafe
                    {
                        uint* p = (uint*)((byte*)srcAddr + srcStride * y);
                        byte* q = (byte*)dstAddr + dstStride * y;
                        int count = width;
                        while (count > 0)
                        {
                            uint dw0 = *p++;
                            //0-255のグレースケールに変換後しきい値と比較
                            *q++ = (byte)(Color.FromArgb((int)dw0).GetBrightness() * 255.0F) < threshold ? (byte)0 : (byte)255;
                            if (--count == 0) break;

                            uint dw1 = *p++;
                            *q++ = (byte)(Color.FromArgb((int)((dw1 << 8) | ((dw0 >> 24) & 255))).GetBrightness() * 255.0F) < threshold ? (byte)0 : (byte)255;
                            if (--count == 0) break;

                            uint dw2 = *p++;
                            *q++ = (byte)(Color.FromArgb((int)((dw2 << 16) | ((dw1 >> 16) & 0xffff))).GetBrightness() * 255.0F) < threshold ? (byte)0 : (byte)255;
                            if (--count == 0) break;

                            *q++ = (byte)(Color.FromArgb((int)(dw2 >> 8)).GetBrightness() * 255.0F) < threshold ? (byte)0 : (byte)255;
                            count--;
                        }

                        //byte* p = (byte*)srcAddr + srcStride * y;
                        //byte* q = (byte*)dstAddr + dstStride * y;
                        //for (int x = 0; x < width; x++)
                        //{
                        //    //色情報を読み込む
                        //    int c = *(int*)p; <= ここでメモリ境界を超える可能性がある
                        //    p += 3;

                        //    //0-255のグレースケールに変換後しきい値と比較
                        //    *q++ = (byte)(Color.FromArgb(c).GetBrightness() * 255.0F) < threshold ? (byte)0 : (byte)255;
                        //}
                    }
                });

                bmp.UnlockBits(src);
                dstBmp.UnlockBits(dst);
                return dstBmp;
            }
        }

        //返される配列の大きさは256
        public static int[] GetHistgram(Bitmap bmp)
        {
            int[] hist = new int[256];
            for (int i = 0; i < hist.Length; i++)
            {
                hist[i] = 0;
            }

            if (bmp.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                BitmapData src = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
                IntPtr srcAddr = src.Scan0;
                int srcStride = src.Stride;

                int width = bmp.Width;

                //for (int y = 0; y < bmp.Height; y++)
                System.Threading.Tasks.Parallel.For(0, bmp.Height, y =>
                {
                    unsafe
                    {
                        byte* p = (byte*)srcAddr + srcStride * y;
                        for (int x = 0; x < width; x++)
                        {
                            Interlocked.Increment(ref hist[*p]);
                            p++;
                        }
                    }
                });

                bmp.UnlockBits(src);
            }
            else
            {
                BitmapData src = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                IntPtr srcAddr = src.Scan0;
                int srcStride = src.Stride;

                int width = bmp.Width;
                //for (int y = 0; y < bmp.Height; y++)
                System.Threading.Tasks.Parallel.For(0, bmp.Height, y =>
                {
                    unsafe
                    {
                        //byte* p = (byte*)srcAddr + srcStride * y;
                        //for (int x = 0; x < width; x++)
                        //{
                        //    int c = *(int*)p; <= ここでメモリ境界を超える可能性がある
                        //    p += 3;

                        //    hist[(byte)(Color.FromArgb(c).GetBrightness() * 255.0F)]++;
                        //}

                        uint* p = (uint*)((byte*)srcAddr + srcStride * y);
                        int count = width;
                        while (count > 0)
                        {
                            uint dw0 = *p++;
                            byte i = (byte)(Color.FromArgb((int)dw0).GetBrightness() * 255.0F);
                            Interlocked.Increment(ref hist[i]);
                            if (--count == 0) break;

                            uint dw1 = *p++;
                            i = (byte)(Color.FromArgb((int)((dw1 << 8) | ((dw0 >> 24) & 255))).GetBrightness() * 255.0F);
                            Interlocked.Increment(ref hist[i]);
                            if (--count == 0) break;

                            uint dw2 = *p++;
                            i = (byte)(Color.FromArgb((int)((dw2 << 16) | ((dw1 >> 16) & 0xffff))).GetBrightness() * 255.0F);
                            Interlocked.Increment(ref hist[i]);
                            if (--count == 0) break;

                            i = (byte)(Color.FromArgb((int)(dw2 >> 8)).GetBrightness() * 255.0F);
                            Interlocked.Increment(ref hist[i]);
                            count--;
                        }
                    }
                });

                bmp.UnlockBits(src);
            }

            return hist;
        }

        public static byte ThresholdOtsu(Bitmap bmp)
        {
            int[] hist = GetHistgram(bmp);
            //Debug.Print(hist.GetHashCode().ToString());

            //for (int i = 0; i < hist.Length; i++)
            //{
            //    hist[i] = Math.Sqrt(hist[i]);
            //}

            int max = int.MinValue;
            int t = 0;

            for (int i = 0; i < hist.Length; i++)
            {
                int w1 = 0;
                int w2 = 0;
                int sum1 = 0;
                int sum2 = 0;
                int m1 = 0;
                int m2 = 0;
                for (int j = 0; j <= i; j++)
                {
                    w1 += hist[j];
                    sum1 += j * hist[j];
                }
                for (int j = i + 1; j < hist.Length; j++)
                {
                    w2 += hist[j];
                    sum2 += j * hist[j];
                }
                if (w1 > 0)
                {
                    m1 = sum1 / w1;
                }
                if (w2 > 0)
                {
                    m2 = sum2 / w2;
                }
                int tmp = w1 * w2 * (m1 - m2) * (m1 - m2);
                if (tmp > max)
                {
                    max = tmp;
                    t = i;
                }
            }
            return (byte)t;
        }

        //ope1,ope2は同じサイズであること
        //
        private static Bitmap _filtering(Bitmap bmp, float[,] ope1, float[,] ope2 = null)
        {
            int xadj = -ope1.GetLength(0) / 2;
            int yadj = -ope1.GetLength(1) / 2;

            Bitmap dstBmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format8bppIndexed);
            var pal = dstBmp.Palette;
            for (int i = 0; i < 256; i++)
            {
                pal.Entries[i] = Color.FromArgb(i, i, i);
            }
            dstBmp.Palette = pal;

            BitmapEx src = BitmapEx.Begin(bmp, ImageLockMode.ReadOnly);
            BitmapEx dst = BitmapEx.Begin(dstBmp, ImageLockMode.WriteOnly);

            int bmpWidth = bmp.Width;
            int bmpHeight = bmp.Height;

            if (bmp.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                System.Threading.Tasks.Parallel.For(0, bmpHeight, i =>
                //for (int i = 0; i < bmp.Height; i++)
                {
                    for (int j = 0; j < bmpWidth; j++)
                    {
                        float sum1 = 0, sum2 = 0;
                        for (int k = 0; k < ope1.GetLength(1); k++)
                        {
                            int y = i + k + yadj;
                            for (int n = 0; n < ope1.GetLength(0); n++)
                            {
                                int x = j + n + xadj;
                                if (x >= 0 && x < bmpWidth && y >= 0 && y < bmpHeight)
                                {
                                    int d = src.GetPixelDirect(x, y);
                                    sum1 += d * ope1[n, k];
                                    if (ope2 != null) sum2 += d * ope2[n, k];
                                }
                            }
                        }

                        if (ope2 != null) sum1 = (float)Math.Sqrt(sum1 * sum1 + sum2 * sum2) / 2.0F;
                        if (sum1 < 0.0F) sum1 = 0.0F; else if (sum1 > 255.0F) sum1 = 255.0F;
                        dst.SetPixelDirect(j, i, (int)sum1);
                    }
                });
            }
            else
            {
                System.Threading.Tasks.Parallel.For(0, bmpHeight, i =>
                //for (int i = 0; i < bmp.Height; i++)
                {
                    for (int j = 0; j < bmpWidth; j++)
                    {
                        float sum1 = 0, sum2 = 0;
                        for (int k = 0; k < ope1.GetLength(1); k++)
                        {
                            int y = i + k + yadj;
                            for (int n = 0; n < ope1.GetLength(0); n++)
                            {
                                int x = j + n + xadj;
                                if (x >= 0 && x < bmpWidth && y >= 0 && y < bmpHeight)
                                {
                                    float br = src.GetPixel(x, y).GetBrightness() * 255.0F;
                                    sum1 += br * ope1[n, k];
                                    if (ope2 != null) sum2 += br * ope2[n, k];
                                }
                            }
                        }

                        if (ope2 != null) sum1 = (float)Math.Sqrt(sum1 * sum1 + sum2 * sum2) / 2.0F;
                        if (sum1 < 0.0F) sum1 = 0.0F; else if (sum1 > 255.0F) sum1 = 255.0F;
                        dst.SetPixelDirect(j, i, (int)sum1);
                    }
                });
            }

            src.End();
            dst.End();
            return dstBmp;
        }
        private static Bitmap _filteringRGB(Bitmap bmp, float[,] ope1, float[,] ope2 = null)
        {
            int xadj = -ope1.GetLength(0) / 2;
            int yadj = -ope1.GetLength(1) / 2;

            Bitmap dstBmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format8bppIndexed);
            var pal = dstBmp.Palette;
            for (int i = 0; i < 256; i++)
            {
                pal.Entries[i] = Color.FromArgb(i, i, i);
            }
            dstBmp.Palette = pal;

            BitmapEx src = BitmapEx.Begin(bmp, ImageLockMode.ReadOnly);
            BitmapEx dst = BitmapEx.Begin(dstBmp, ImageLockMode.WriteOnly);

            if (bmp.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                throw new Exception("For RGB Bitmap only");
            }
            else
            {
                int bmpWidth = bmp.Width;
                int bmpHeight = bmp.Height;

                System.Threading.Tasks.Parallel.For(0, bmpHeight, i =>
                //for (int i = 0; i < bmp.Height; i++)
                {
                    for (int j = 0; j < bmpWidth; j++)
                    {
                        float sum1B = 0, sum1G = 0, sum1R = 0;
                        float sum2B = 0, sum2G = 0, sum2R = 0;
                        for (int k = 0; k < ope1.GetLength(1); k++)
                        {
                            int y = i + k + yadj;
                            for (int n = 0; n < ope1.GetLength(0); n++)
                            {
                                int x = j + n + xadj;
                                if (x >= 0 && x < bmpWidth && y >= 0 && y < bmpHeight)
                                {
                                    Color c = src.GetPixel(x, y);

                                    sum1B += c.B * ope1[n, k];
                                    sum1G += c.G * ope1[n, k];
                                    sum1R += c.R * ope1[n, k];

                                    if (ope2 != null)
                                    {
                                        sum2B += c.B * ope2[n, k];
                                        sum2G += c.G * ope2[n, k];
                                        sum2R += c.R * ope2[n, k];
                                    }
                                }
                            }
                        }

                        float sum;
                        if (ope2 != null)
                        {
                            sum = (float)Math.Sqrt(sum1B * sum1B + sum1G * sum1G + sum1R * sum1R +
                                                    sum2B * sum2B + sum2G * sum2G + sum2R * sum2R) / 6.0F;
                        } else
                        {
                            sum = (float)Math.Sqrt(sum1B * sum1B + sum1G * sum1G + sum1R * sum1R) / 3.0F;
                        }
                        if (sum < 0.0F) sum = 0.0F; else if (sum > 255.0F) sum = 255.0F;
                        dst.SetPixelDirect(j, i, (int)sum);
                    }
                });
            }

            src.End();
            dst.End();
            return dstBmp;
        }

        private static Bitmap _filteringHSV(Bitmap bmp, float[,] ope1, float[,] ope2 = null)
        {
            int xadj = -ope1.GetLength(0) / 2;
            int yadj = -ope1.GetLength(1) / 2;

            Bitmap dstBmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format8bppIndexed);
            var pal = dstBmp.Palette;
            for (int i = 0; i < 256; i++)
            {
                pal.Entries[i] = Color.FromArgb(i, i, i);
            }
            dstBmp.Palette = pal;

            BitmapEx src = BitmapEx.Begin(bmp, ImageLockMode.ReadOnly);
            BitmapEx dst = BitmapEx.Begin(dstBmp, ImageLockMode.WriteOnly);

            if (bmp.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                throw new Exception("For RGB Bitmap only");
            }
            else
            {
                int bmpWidth = bmp.Width;
                int bmpHeight = bmp.Height;

                System.Threading.Tasks.Parallel.For(0, bmpHeight, i =>
                //for (int i = 0; i < bmp.Height; i++)
                {
                    for (int j = 0; j < bmpWidth; j++)
                    {
                        float sum1H = 0, sum1S = 0, sum1V = 0;
                        float sum2H = 0, sum2S = 0, sum2V = 0;
                        for (int k = 0; k < ope1.GetLength(1); k++)
                        {
                            int y = i + k + yadj;
                            for (int n = 0; n < ope1.GetLength(0); n++)
                            {
                                int x = j + n + xadj;
                                if (x >= 0 && x < bmpWidth && y >= 0 && y < bmpHeight)
                                {
                                    Color c = src.GetPixel(x, y);
                                    float h = c.GetHue() / 360.0F; //GetHue() 0.0-360.0
                                    float s = c.GetSaturation();    //GetSaturation() 0.0-1.0
                                    float v = c.GetBrightness();    //GetBrightness() 0.0-1.0

                                    sum1H += h * ope1[n, k];
                                    sum1S += s * ope1[n, k];
                                    sum1V += v * ope1[n, k];

                                    if (ope2 != null)
                                    {
                                        sum2H += h * ope2[n, k];
                                        sum2S += s * ope2[n, k];
                                        sum2V += v * ope2[n, k];
                                    }
                                }
                            }
                        }

                        float sum;
                        if (ope2 != null)
                        {
                            sum = (float)Math.Sqrt(sum1H * sum1H + sum1S * sum1S + sum1V * sum1V +
                                                        sum2H * sum2H + sum2S * sum2S + sum2V * sum2V) * 255.0F / 6.0F;
                        }
                        else
                        {
                            sum = (float)Math.Sqrt(sum1H * sum1H + sum1S * sum1S + sum1V * sum1V) * 255.0F / 3.0F;
                        }
                        if (sum < 0.0F) sum = 0.0F; else if (sum > 255.0F) sum = 255.0F;
                        dst.SetPixelDirect(j, i, (int)sum);
                    }
                });
            }

            src.End();
            dst.End();
            return dstBmp;
        }

        public enum COLORMODE
        {
            BRIGHTNESS,
            RGB,
            HSV
        }

        //ソーベルエッジ検出
        public static Bitmap Sobel(Bitmap bmp, COLORMODE cmode)
        {
            float[,] ope1 = new float[3, 3]{
                                {1,0,-1},
                                {2,0,-2},
                                {1,0,-1}};
            float[,] ope2 = new float[3, 3]{
                                {1,2,1},
                                {0,0,0},
                                {-1,-2,-1}};
            switch (cmode)
            {
                case COLORMODE.BRIGHTNESS:
                    return _filtering(bmp, ope1, ope2);
                case COLORMODE.RGB:
                    return _filteringRGB(bmp, ope1, ope2);
                case COLORMODE.HSV:
                    return _filteringHSV(bmp, ope1, ope2);
                default:
                    throw new Exception();
            }
        }

        //グラディエント(一次微分)エッジ検出
        public static Bitmap Gradient(Bitmap bmp, COLORMODE cmode)
        {
            float[,] ope1 = new float[3, 3]{
                                {-1,0,1},
                                {-1,0,1},
                                {-1,0,1}};
            float[,] ope2 = new float[3, 3]{
                                {-1,-1,-1},
                                {0,0,0},
                                {1,1,1}};
            switch (cmode)
            {
                case COLORMODE.BRIGHTNESS:
                    return _filtering(bmp, ope1, ope2);
                case COLORMODE.RGB:
                    return _filteringRGB(bmp, ope1, ope2);
                case COLORMODE.HSV:
                    return _filteringHSV(bmp, ope1, ope2);
                default:
                    throw new Exception();
            }
        }

        //ラプラシアン(二次微分)エッジ検出
        public static Bitmap Laplacian(Bitmap bmp, COLORMODE cmode)
        {
            float[,] ope = new float[3, 3]{
                                {1,1,1},
                                {1,-8,1},
                                {1,1,1}};
            switch (cmode)
            {
                case COLORMODE.BRIGHTNESS:
                    return _filtering(bmp, ope);
                case COLORMODE.RGB:
                    return _filteringRGB(bmp, ope);
                case COLORMODE.HSV:
                    return _filteringHSV(bmp, ope);
                default:
                    throw new Exception();
            }
        }

        //ガウスぼかし
        public static Bitmap Gaussian(Bitmap bmp)
        {
            float[,] ope = new float[3, 3]{
                               {1.0F/16.0F,2.0F/16.0F,1.0F/16.0F},
                                {2.0F/16.0F,4.0F/16.0F,2.0F/16.0F},
                                {1.0F/16.0F,2.0F/16.0F,1.0F/16.0F}};
            return _filtering(bmp, ope);
        }

        public static Bitmap Median(Bitmap bmp, int size = 3)
        {
            int adj = -size / 2;

            Bitmap dstBmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format8bppIndexed);
            var pal = dstBmp.Palette;
            for (int i = 0; i < 256; i++)
            {
                pal.Entries[i] = Color.FromArgb(i, i, i);
            }
            dstBmp.Palette = pal;

            BitmapEx src = BitmapEx.Begin(bmp, ImageLockMode.ReadOnly);
            BitmapEx dst = BitmapEx.Begin(dstBmp, ImageLockMode.WriteOnly);

            int bmpWidth = bmp.Width;
            int bmpHeight = bmp.Height;

            if (bmp.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                System.Threading.Tasks.Parallel.For(0, bmpHeight, i =>
                //for (int i = 0; i < bmp.Height; i++)
                {
                    byte[] brightness = new byte[size * size];

                    for (int j = 0; j < bmpWidth; j++)
                    {
                        int m = 0;
                        for (int k = 0; k < size; k++)
                        {
                            int y = i + k + adj;
                            for (int n = 0; n < size; n++)
                            {
                                int x = j + n + adj;
                                if (x >= 0 && x < bmpWidth && y >= 0 && y < bmpHeight)
                                {
                                    brightness[m++] = (byte)src.GetPixelDirect(x, y);
                                }
                            }
                        }

                        Array.Sort(brightness, 0, m);
                        dst.SetPixelDirect(j, i, brightness[m / 2]);
                    }
                });
            }
            else
            {
                System.Threading.Tasks.Parallel.For(0, bmpHeight, i =>
                //for (int i = 0; i < bmp.Height; i++)
                {
                    byte[] brightness = new byte[size * size];

                    for (int j = 0; j < bmpWidth; j++)
                    {
                        int m = 0;
                        for (int k = 0; k < size; k++)
                        {
                            int y = i + k + adj;
                            for (int n = 0; n < size; n++)
                            {
                                int x = j + n + adj;
                                if (x >= 0 && x < bmpWidth && y >= 0 && y < bmpHeight)
                                {
                                    brightness[m++] = (byte)(src.GetPixel(x, y).GetBrightness() * 255.0F);
                                }
                            }
                        }

                        Array.Sort(brightness, 0, m);
                        dst.SetPixelDirect(j, i, brightness[m / 2]);
                    }
                });
            }

            src.End();
            dst.End();
            return dstBmp;
        }

        public enum PIXELOPERATION
        {
            ADD,
            MUL
        }

        delegate Color ColorOperatorFunc(Color c1, Color c2);

        public static Bitmap Operation(Bitmap bmp1, Bitmap bmp2, PIXELOPERATION po)
        {
            if (bmp1.Size != bmp2.Size)
                throw new Exception("Image size not same");

            BitmapEx bex1 = BitmapEx.Begin(bmp1, ImageLockMode.ReadOnly);
            BitmapEx bex2 = BitmapEx.Begin(bmp2, ImageLockMode.ReadOnly);

            Bitmap bmp = new Bitmap(bmp1.Width, bmp1.Height, PixelFormat.Format24bppRgb);
            BitmapEx bex3 = BitmapEx.Begin(bmp, ImageLockMode.WriteOnly);

            //操作を処理する無名関数をセット
            ColorOperatorFunc f;
            switch (po)
            {
                case PIXELOPERATION.ADD:
                    f = ((c1, c2) => {
                        int r = c1.R + c2.R;
                        if (r > 255) r = 255;
                        int g = c1.G + c2.G;
                        if (g > 255) g = 255;
                        int b = c1.B + c2.B;
                        if (b > 255) b = 255;
                        return Color.FromArgb(r, g, b);
                    });
                    break;
                case PIXELOPERATION.MUL:
                    f = ((c1, c2) => {
                        int r = c1.R * c2.R;
                        if (r > 255) r = 255;
                        int g = c1.G * c2.G;
                        if (g > 255) g = 255;
                        int b = c1.B * c2.B;
                        if (b > 255) b = 255;
                        return Color.FromArgb(r, g, b);
                    });
                    break;
                default:
                    throw new Exception();
            }

            int bmpWidth = bmp1.Width;
            int bmpHeight = bmp1.Height;

            System.Threading.Tasks.Parallel.For(0, bmpHeight, y =>
            {
                for (int x = 0; x < bmpWidth; x++)
                {
                    bex3.SetPixel(x, y, f(bex1.GetPixel(x, y), bex2.GetPixel(x, y)));
                }
            });

            bex3.End();
            bex2.End();
            bex1.End();
            return bmp;
        }


    }
}
