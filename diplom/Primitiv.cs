using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace diplom
{
    abstract class Vector
    {
        public abstract float Norm();
        public abstract Vector Normir();
        public abstract string getCoord();
        public abstract string ToString();
        public abstract int getNameSpace();
    }
    class Vector2D:Vector
    {
        public float X, Y;
        private int NameSpace = 2;
        public Vector2D(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public Vector2D()
        {
            this.X = 0;
            this.Y = 0;
        }
        public abstract override float Norm()
        {
            return (float)Math.Sqrt(X * X + Y * Y);
        }
        public override Vector Normir()
        {
            return new Vector2D(this.X/this.Norm(), this.Y/this.Norm());
        }
        public override string getCoord()
        {
            return Convert.ToString(this.X + " " + this.Y);
        }
        public override string ToString()
        {
            return "(" + this.getCoord() + ")";
        }

        public override int getNameSpace()
        {
            return NameSpace;
        }
    }
}
