using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometory
{
    class Circle
    {
        public float x, y, r;

        /*
         * コンストラクタ
         */
        public Circle()
        {
            x = y = r = 0;
        }
        public Circle(float x, float y, float r)
        {
            this.x = x;
            this.y = y;
            this.r = r;
        }
        public Circle Clone()
        {
            return new Circle(x, y, r);
        }

        /*
         * 型変換
         */
        public override String ToString()
        {
            return $"({x},{y},r={r})";
        }

        /*
         * 描画補助
         */
        public void Draw(Graphics g, Pen pen,float center_x=0.0F, float center_y = 0.0F)
        {
            const float angle_step = 10.0F;

            float x0=0.0F, y0=0.0F;
            for (float ang = 0.0F; ang <= 360.0F; ang += angle_step)
            {
                float rad = ang * (float)Math.PI / 180.0F;
                float xx = r * (float)Math.Cos(rad) + x + center_x;
                float yy = r * (float)Math.Sin(rad) + y + center_y;
                if (ang > 0.0F) g.DrawLine(pen, x0, y0, xx, yy);
                x0 = xx;
                y0 = yy;
            }
        }

        public bool IsCrossing(Circle b)
        {
            return (float)Math.Sqrt((b.x - x) * (b.x - x) + (b.y - y) * (b.y - y)) <= r + b.r;
        }

        //円と円の交点を求める。成功したときは必ず長さ２の配列(２点)、失敗したときは長さ０の配列を返す
        public PointXY[] CrossPoints(Circle b)
        {
            //b座標を原点として計算
            float xx = x - b.x;
            float yy = y - b.y;

            float w = xx * xx + yy * yy;
            if (w < 0.00001F) return new PointXY[0];

            float a = (w + b.r * b.r - r * r) / 2.0F;
            float v = (float)Math.Sqrt(w * b.r * b.r - a * a);
            if (float.IsNaN(v)) return new PointXY[0];


            PointXY[] result = new PointXY[2];
            result[0] = new PointXY(
                (a * xx + yy * v) / w + b.x,
                (a * yy - xx * v) / w + b.y
                );
            result[1] = new PointXY(
                (a * xx - yy * v) / w + b.x,
                (a * yy + xx * v) / w + b.y
                );

            return result;
        }
    }
}
