using System.Windows.Forms;

namespace _3D_Cube
{
    class MatrixMulti
    {
        public MatrixMulti()
        {
           
            //_result = MatrixMultiply(_projection, _p);
            //_v = MatrixToVec(_result);
        }

                readonly float[,] _projection = new float[2, 3]
        {
            {1, 0, 0},
            {0, 1, 0}
        };
        private readonly float[,] _result;

                 _3DPoint _p = new _3DPoint(100 , 75, 50);
        private  _3DPoint _v;

        //Converts 3D Point to a float array
        float[,] VecToMatrix(_3DPoint v)
        {
            float[,] m = new float[3,1];

            m[0, 0] = v.X;
            m[1, 0] = v.Y;
            m[2, 0] = v.Z;

            return m;
        }

        //Converts Float array to a 3D Point 
        _3DPoint MatrixToVec(float[,] m)
        {
            _3DPoint v = new _3DPoint();

            v.X = m[0, 0];
            v.Y = m[1, 0];

            if (m.GetLength(0) > 2)
            {
                v.Z = m[2, 0];
            }
            
            return v;
        }

        //Multiples matrices together 
        public float[,] MatrixMultiply(float[,] a, float[,] b)
        {

            int colsA = a.GetLength(1);
            int rowsA = a.GetLength(0);

            int colsB = b.GetLength(1);
            int rowsB = b.GetLength(0);

            float[,] result = new float[rowsA, colsB];
      
            if (colsA != rowsB)
            {
                MessageBox.Show("Columuns of A must match rows of B");
                return null;
            }

            /*
             Runs through the rows and columns of the 2 inputted float arrays and 
            times them together to output 1 float array that can be converted to a 3D Point
            */
            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    float sum = 0;

                    for (int k = 0; k < colsA; k++)
                    { 
                            sum += a[i, k] * b[k, j];                      
                    }

                    result[i, j] = sum;                     
                }         
            }
            return result;
        }
        public _3DPoint MatrixMultiply(float[,] a, _3DPoint b)
        {
            float[,] m = VecToMatrix(b);
            return MatrixToVec(MatrixMultiply(a, m));
        }


        //Converts all the variables to string form to be outputted 
        private static string MatrixLog(float[,] m)
        {
            int cols = m.GetLength(1);
            int rows = m.GetLength(0);

            string output;

            output = rows + "x" + cols;
            output += "\n-------------------\n";            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {                                           
                        output += m[i, j] + " ";                                      
                }

                output += "\n";
            }

            return output;
        }
        public string StartMatrixLog()
        {
            string outputToBox = MatrixLog(_projection);
            outputToBox += "\n\n";
            outputToBox += MatrixLog(_result);
            outputToBox += "\n\n";
            outputToBox += _v.ToString();

            return outputToBox;

        }
    }
}