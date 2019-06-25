using System;
using System.Drawing;

namespace Geometory
{
    class PointXY
    {
        public float x, y;

        /*
         * コンストラクタ
         */
        public PointXY()
        {
            x = y = 0;
        }
        public PointXY(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public PointXY Clone()
        {
            return new PointXY(x, y);
        }

        /*
         * 型変換
         */
        public Point ToPoint()
        {
            return new Point((int)x, (int)y);
        }
        public PointF ToPointF()
        {
            return new PointF(x, y);
        }
        public override String ToString()
        {
            return $"({x},{y})";
        }

        /*
         * ベクトルのように扱う
         */
        public float Length()
        {
            return (float)Math.Sqrt( x * x + y * y);
        }
        public void Normalize()
        {
            float l = Length();
            x /= l;
            y /= l;
        }
        // 原点を中心とした回転
        public PointXY Rotate(float deg, bool clockwise = true)
        {
            float rad = deg / 180.0F * (float)Math.PI;
            if (!clockwise) rad = -rad;
            float sin_t = (float)Math.Sin(rad);
            float cos_t = (float)Math.Cos(rad);
            return new PointXY(
                x * cos_t - y * sin_t,
                x * sin_t + y * cos_t
                );
        }
        // 指定座標を中心とした回転
        public PointXY Rotate(float center_x, float center_y, float deg, bool clockwise = true)
        {
            float rad = deg / 180.0F * (float)Math.PI;
            if (!clockwise) rad = -rad;
            float sin_t = (float)Math.Sin(rad);
            float cos_t = (float)Math.Cos(rad);
            return new PointXY(
                (x - center_x) * cos_t - (y - center_y) * sin_t + center_x,
                (x - center_x) * sin_t + (y - center_y) * cos_t + center_y
                );
        }
        // 内積。クラスのベクトルとベクトルｂのなす角が９０で０、９０を超えると負の数を返します
        public float InnerProduct(PointXY b)
        {
            return x * b.x + y * b.y;
        }
        // 外積。ベクトルｂがこのクラスのベクトルの時計回り側にあると返り値はプラスになります
        public float OuterProduct(PointXY b)
        {
            return x * b.y - y * b.x;
        }
        // 0.0 <= angle <= 180.0の値を返します
        public float Angle(PointXY b)
        {
            float rad = (float)Math.Acos(InnerProduct(b) / (Length() * b.Length()));
            return rad * 180.0F / (float)Math.PI;
        }
        // -180.0 < angle <= 180.0の値を返します
        // ベクトルbが反時計周りにあるとき、値は負の数を返します
        public float SignedAngle(PointXY b)
        {
            float rad = (float)Math.Acos(InnerProduct(b) / (Length() * b.Length()));
            if (OuterProduct(b) < 0.0F) rad = -rad;
            return rad * 180.0F / (float)Math.PI;
        }


        /*
         * 演算子
         */
        public override bool Equals(object obj)
        {
            //objがnullか、型が違うときは、等価でない
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }
            return this == (PointXY)obj;
        }
        public override int GetHashCode()
        {
            return 0;
        }
        public static bool operator ==(PointXY a, PointXY b)
        {
            return a.x == b.x && a.y == b.y;
        }
        public static bool operator !=(PointXY a, PointXY b)
        {
            return a.x != b.x || a.y != b.y;
        }
        public static PointXY operator+ (PointXY a, PointXY b)
        {
            return new PointXY(a.x + b.x, a.y + b.y);
        }
        public static PointXY operator -(PointXY a, PointXY b)
        {
            return new PointXY(a.x - b.x, a.y - b.y);
        }
        public static PointXY operator *(PointXY a, float scale)
        {
            return new PointXY(a.x *scale, a.y *scale);
        }
        public static PointXY operator /(PointXY a, float scale)
        {
            return new PointXY(a.x / scale, a.y / scale);
        }
    }
}
