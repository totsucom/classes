using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorFont
{
    class VectorFont
    {
        private byte[] dict;
        private byte[] vect;

        private VectorFont(ref byte[] dict, ref byte[] vect)
        {
            this.dict = dict;
            this.vect = vect;
        }

        public VectorArray GetVectors(int utf16)
        {
            if (utf16 != 32)
            {
                int loc = UTF16ToLocation(utf16);
                if (loc < 0) return null;

                int n = BitConverter.ToInt16(vect, loc);
                if (n < 0) return null;

                VectorArray va = new VectorArray(n);
                loc += 2;

                while (n-- > 0)
                {
                    VectorUnit.Command c;
                    if (vect[loc] == (byte)'M')
                        c = VectorUnit.Command.MOVETO;
                    else if (vect[loc] == (byte)'L')
                        c = VectorUnit.Command.LINETO;
                    else
                        throw new Exception("Unknown command");

                    float x = BitConverter.ToSingle(vect, loc + 1);
                    float y = BitConverter.ToSingle(vect, loc + 5);
                    va.Add(c, x, y);

                    loc += 9;
                }
                return va;
            }
            else
            {
                //スペースの処理
                VectorArray va = new VectorArray(2);
                va.Add(VectorUnit.Command.NONE, 0.0F, 0.0F);
                va.Add(VectorUnit.Command.NONE, 0.3F, 0.0F);
                return va;
            }
        }

        /*
         * 編集用
         * VectorArrayの内容をバッファに書き込むが、配列の大きさは変更してはならない。
         * パラメータのVectorArrayは変更しません=const
         */
        public bool SetVectors(int utf16, ref VectorArray va)
        {
            int loc = UTF16ToLocation(utf16);
            if (loc < 0) return false;

            int n = BitConverter.ToInt16(vect, loc);
            if (n < 0 || n != va.Count) return false;

            loc += 2;

            for (int i = 0; i < va.Count; i++)
            {
                if (va[i].c != VectorUnit.Command.MOVETO && va[i].c != VectorUnit.Command.LINETO)
                    return false;
            }

            for (int i = 0; i < va.Count; i++)
            {
                if (va[i].c == VectorUnit.Command.MOVETO)
                    vect[loc++] = (byte)'M';
                else
                    vect[loc++] = (byte)'L';

                byte[] bs = BitConverter.GetBytes(va[i].x);
                for (int j = 0; j < 4; j++)
                {
                    vect[loc++] = bs[j];
                }

                bs = BitConverter.GetBytes(va[i].y);
                for (int j = 0; j < 4; j++)
                {
                    vect[loc++] = bs[j];
                }
            }

            return true;
        }

        /*
         * UTF16コードから文字ベクトルデータの位置を返す
         * エラーまたは存在しないコードの場合は負の数を返す
         * 
         * 該当する文字が存在するか調べるのにも使える
         * ちなみに
         */
        public int UTF16ToLocation(int utf16)
        {
            if (utf16 < 0) return -1;
            int i;
            if (utf16 <= 0x3D5)
                i = utf16;
            else if (utf16 >= 0x3D6 && utf16 <= 0x2103)
                return -2;
            else if (utf16 >= 0x2104 && utf16 <= 0x22C2)
                i = utf16 - 7470;
            else if (utf16 >= 0x22C3 && utf16 <= 0x2E8B)
                return -3;
            else if (utf16 >= 0x2E8C && utf16 <= 0x30FC)
                i = utf16 - 7470 - 3017;
            else if (utf16 >= 0x30FD && utf16 <= 0x471E)
                return -4;
            else if (utf16 == 0x471F)
                i = utf16 - 7470 - 3017 - 5666;
            else if (utf16 >= 0x4720 && utf16 <= 0x4DFF)
                return -5;
            else if (utf16 >= 0x4E00 && utf16 <= 0x9FA0)
                i = utf16 - 7470 - 3017 - 5666 - 1760;
            else if (utf16 >= 0x9FA1 && utf16 <= 0xFA10)
                return -6;
            else if (utf16 >= 0xFA11 && utf16 <= 0xFA66)
                i = utf16 - 7470 - 3017 - 5666 - 1760 - 23152;
            else if (utf16 >= 0xFA67 && utf16 <= 0xFF20)
                return -7;
            else if (utf16 >= 0xFF21 && utf16 <= 0xFFFF)
                i = utf16 - 7470 - 3017 - 5666 - 1760 - 23152 - 1210;
            else
                return -8;

            int loc = (int)(BitConverter.ToUInt32(dict, i * 3) & 0xffffff);
            if (loc == 0xFFFFFF)
                return -9;
            else
                return loc;
        }

        /*
         * フォント編集用
         * ファイルが存在していたら上書きしてしまうので注意
         */
        public void Save(String dict_path, String vect_path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(
                dict_path,
                System.IO.FileMode.Create,
                System.IO.FileAccess.Write);
            fs.Write(dict, 0, dict.Length - 1);
            fs.Close();

            fs = new System.IO.FileStream(
                vect_path,
                System.IO.FileMode.Create,
                System.IO.FileAccess.Write);
            fs.Write(vect, 0, vect.Length - 1);
            fs.Close();
        }

        private Encoding enc = Encoding.GetEncoding("utf-16");

        public static int CharToUTF16(char c)
        {
            char[] cs = { c }; 
            byte[] bs = Encoding.GetEncoding("utf-16").GetBytes(cs);
            return (int)((uint)bs[0] | (((uint)bs[1]) << 8));
        }

        /*
         * 辞書とベクトルファイルを読み込んでVectorFontクラスを生成する
         * この関数から始める
         */
        public static VectorFont Load(String dict_path, String vect_path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(
                dict_path,
                System.IO.FileMode.Open,
                System.IO.FileAccess.Read);
            byte[] dict = new byte[fs.Length + 1]; //データは３バイトだが32ビットでアクセスするので+1
            fs.Read(dict, 0, dict.Length - 1);
            fs.Close();

            fs = new System.IO.FileStream(
                vect_path,
                System.IO.FileMode.Open,
                System.IO.FileAccess.Read);
            byte[] vect = new byte[fs.Length + 1];
            fs.Read(vect, 0, vect.Length - 1);
            fs.Close();

            return new VectorFont(ref dict, ref vect);
        }
    }

    /*
     * ベクトル配列を管理するクラス
     * 通常、このクラス一つで１文字を表す
     */
    class VectorArray
    {
        private VectorUnit[] ar;
        private int dataCount;
        private float height = 1.0F;//文字の初期高さ

        public VectorArray(int array_size)
        {
            ar = new VectorUnit[array_size];
            dataCount = 0;
        }

        public int Count { get => dataCount; }

        public VectorUnit this[int index]
        {
            set { ar[index] = value; }
            get { return this.ar[index]; }
        }

        public void Add(VectorUnit.Command c, float x, float y)
        {
            ar[dataCount++] = new VectorUnit(c, x, y);
        }

        public void Offset(float dx, float dy)
        {
            for (int i = 0; i < dataCount; i++)
            {
                ar[i].x += dx;
                ar[i].y += dy;
            }
        }

        public void Scale(float sx, float sy)
        {
            for (int i = 0; i < dataCount; i++)
            {
                ar[i].x *= sx;
                ar[i].y *= sy;
            }
            height *= sy;
        }

        /*
         * 文字サイズを得る
         */
        public SizeF GetSize()
        {
            float xmin = 0.0F, xmax = 0.0F;

            for (int i = 0; i < dataCount; i++)
            {
                if (i > 0)
                {
                    if (xmin > ar[i].x)
                        xmin = ar[i].x;
                    else if (xmax < ar[i].x)
                        xmax = ar[i].x;
                }
                else
                {
                    xmin = xmax = ar[i].x;
                }
            }

            //xminがleft marginを表しているので、xmax+xminで文字幅になる
            //因みにnormalized_fontはmargin=0
            return new SizeF(xmax + xmin, height);
        }

        /*
         * 座標の範囲を取得する
         */
        public RectangleF GetBound()
        {
            float xmin = 0.0F, xmax = 0.0F;
            float ymin = 0.0F, ymax = 0.0F;

            for (int i = 0; i < dataCount; i++)
            {
                if (i > 0)
                {
                    if (xmin > ar[i].x)
                        xmin = ar[i].x;
                    else if (xmax < ar[i].x)
                        xmax = ar[i].x;
                    if (ymin > ar[i].y)
                        ymin = ar[i].y;
                    else if (ymax < ar[i].y)
                        ymax = ar[i].y;
                }
                else
                {
                    xmin = xmax = ar[i].x;
                    ymin = ymax = ar[i].y;
                }
            }

            return new RectangleF(xmin, ymin, xmax - xmin + 1.0F, ymax - ymin + 1.0F);
        }

        public void Draw(Graphics g, Pen pen, float x_scale, float y_scale, float x_offset, float y_offset)
        {
            float x0 = 0.0F, y0 = 0.0F;
            for (int i = 0; i < dataCount; i++)
            {
                if (ar[i].c != VectorUnit.Command.NONE)
                {
                    float x = ar[i].x * x_scale + x_offset;
                    float y = ar[i].y * y_scale + y_offset;

                    if (ar[i].c == VectorUnit.Command.LINETO)
                    {
                        g.DrawLine(pen, x0, y0, x, y);
                    }

                    x0 = x;
                    y0 = y;
                }
            }
        }

        /*
         * 書き順などを可視化して描画。ペンの開始(MOVETO)には〇、ペンの移動方向は→で表す
         */
        public void DrawWithMark(Graphics g, Pen pen, float x_scale, float y_scale, float x_offset, float y_offset,
            Pen moveMarkPen, float moveMarkRadius, Pen arrowMarkPen, float arrowLength)
        {
            Geometory.PointXY p1 = new Geometory.PointXY(-arrowLength, 0).Rotate(30.0F, true);
            Geometory.PointXY p2 = new Geometory.PointXY(-arrowLength, 0).Rotate(-30.0F, true);

            float x0 = 0.0F, y0 = 0.0F;
            for (int i = 0; i < dataCount; i++)
            {
                if (ar[i].c != VectorUnit.Command.NONE)
                {
                    float x = ar[i].x * x_scale + x_offset;
                    float y = ar[i].y * y_scale + y_offset;

                    if (ar[i].c == VectorUnit.Command.LINETO)
                    {
                        g.DrawLine(pen, x0, y0, x, y);

                        float ang = new Geometory.PointXY(1.0F, 0.0F).SignedAngle(new Geometory.PointXY(x - x0, y - y0));
                        Geometory.PointXY q1 = p1.Rotate(ang) + new Geometory.PointXY(x, y);
                        Geometory.PointXY q2 = p2.Rotate(ang) + new Geometory.PointXY(x, y);
                        g.DrawLine(arrowMarkPen, q1.x, q1.y, x, y);
                        g.DrawLine(arrowMarkPen, q2.x, q2.y, x, y);
                    }
                    else //VectorUnit.Command.MOVETO
                    {
                        g.DrawEllipse(moveMarkPen, new RectangleF(x - moveMarkRadius,　y - moveMarkRadius, moveMarkRadius * 2.0F, moveMarkRadius * 2.0F));
                    }

                    x0 = x;
                    y0 = y;
                }
            }
        }
    };

    /*
     * ひとつの頂点座標を表す
     */
    class VectorUnit
    {
        public enum Command
        {
            NONE,
            MOVETO,
            LINETO
        }

        public Command c;
        public float x, y;

        public VectorUnit(Command c, float x, float y)
        {
            this.c = c;
            this.x = x;
            this.y = y;
        }
    };
}
