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

        public override string ToString()
        {
            string print = "[" + X + ", " + Y + ", " + Z  + "]\n";
            return print;
        }
    }
}