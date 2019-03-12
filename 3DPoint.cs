using System;

namespace _3D_Cube
{
    class _3DPoint
    {
        public float X;
        public float Y;
        public float Z;

        public _3DPoint()
        {

        }

        public _3DPoint(float[,] input)
        {
            X = input[0, 0];
            Y = input[1, 0];
        }

        public _3DPoint(float inputx, float inputy, float inputz)
        {
            X = inputx;
            Y = inputy;
            Z = inputz;
        }

        public _3DPoint(string inputx, string inputy, string inputz)
        {
            X = Convert.ToSingle(inputx) * 100;
            Y = Convert.ToSingle(inputy) * 100;
            Z = Convert.ToSingle(inputz) * 100;
        }

        public override string ToString()
        {
            string print = "[" + X + ", " + Y + ", " + Z  + "]";
            return print;
        }
    }
}