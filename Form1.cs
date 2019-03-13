//Copyright 2018 - Matthew Pye 

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace _3D_Cube
{
    public partial class Form1 : Form
    {
        //Variables ----------------------------------------------------------------
            //Graphics
            Bitmap _backBuffer;
            Graphics _backBufferGr;
            Graphics _gr;
            private readonly bool _isRunning = true;
            private int _resWidth;
            private int _resHeight;

            //FPS
            private int _tSec = DateTime.Now.Second;
            private int _tTicks = 0;
            private int _tMax = 0;

            //Maths
            _3DPoint[] points;
            int size = 100;
            float[,] _projection = new float[2, 3]
        {
            {1, 0, 0},
            {0, 1, 0}
        };
            readonly MatrixMulti Matrix = new MatrixMulti();
            

            //Angle Speeds
            private static double angle = 0.02;
            private double AngleX, AngleY, AngleZ;
            private double AutoAngleX, AutoAngleY, AutoAngleZ;

            //Others
            private int ShapeNum = 0;
            private List<List<string>> ListOfFaces = new List<List<string>>();
            private List<_3DPoint> ImportedVertices = new List<_3DPoint>();
             
        //--------------------------------------------------------------------------

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            _resWidth = 800;
            _resHeight = 600;

            Show();
            Focus();

            _gr = CreateGraphics();
            _backBuffer = new Bitmap(_resWidth, _resHeight);
            points = new _3DPoint[8];

            points[0] = new _3DPoint(-size,-size, -size);
            points[1] = new _3DPoint(size, -size , -size);
            points[2] = new _3DPoint(size, size, -size);
            points[3] = new _3DPoint(-size, size, -size);
            points[4] = new _3DPoint(-size, -size, size);
            points[5] = new _3DPoint(size, -size, size);
            points[6] = new _3DPoint(size, size, size);
            points[7] = new _3DPoint(-size, size, size);

            DrawLoop();
        }

        private void DrawLoop()
        {
            do
            {
                Application.DoEvents();
                switch (ShapeNum)
                {
                    case 0:
                        DrawCube();
                        break;

                    case 1:
                        DrawPyramid();
                        break;

                    case 2:
                        DrawSqrPyramid();
                        break;

                    case 3:
                        DrawDodechedron();
                        break;

                    case 4:
                        DrawOctohedron();
                        break;

                    case 5:
                        DrawImported();
                        break;

                }
                
                FpsCounter();
                FPSLabel.Text = "FPS:" + _tMax;
                EndFrame();

            } while (_isRunning);

        }

        void DrawCube()
        {
           
            float[,] rotationX =
            {
                {1,0, 0},
                {0, Convert.ToSingle(Math.Cos(AngleX)), Convert.ToSingle(-Math.Sin(AngleX))},
                {0, Convert.ToSingle(Math.Sin(AngleX)), Convert.ToSingle(Math.Cos(AngleX))}
                
            };
            float[,] rotationAutoX =
            {
                {1,0, 0},
                {0, Convert.ToSingle(Math.Cos(AutoAngleX)), Convert.ToSingle(-Math.Sin(AutoAngleX))},
                {0, Convert.ToSingle(Math.Sin(AutoAngleX)), Convert.ToSingle(Math.Cos(AutoAngleX))}

            };

            float[,] rotationZ =
            {
                {Convert.ToSingle(Math.Cos(AngleZ)), Convert.ToSingle(-Math.Sin(AngleZ)), 0},
                {Convert.ToSingle(Math.Sin(AngleZ)), Convert.ToSingle(Math.Cos(AngleZ)), 0},
                {0, 0, 1}
            };
            float[,] rotationAutoZ =
            {              
                {Convert.ToSingle(Math.Cos(AutoAngleZ)), Convert.ToSingle(-Math.Sin(AutoAngleZ)), 0},
                {Convert.ToSingle(Math.Sin(AutoAngleZ)), Convert.ToSingle(Math.Cos(AutoAngleZ)), 0},
                {0, 0, 1}

            };

            float[,] rotationY =
            {
                {Convert.ToSingle(Math.Cos(AngleY)), 0, Convert.ToSingle(-Math.Sin(AngleY))},
                {0, 1, 0},
                {Convert.ToSingle(Math.Sin(AngleY)), 0, Convert.ToSingle(Math.Cos(AngleY))}
                
            };
            float[,] rotationAutoY =
            {
                {Convert.ToSingle(Math.Cos(AutoAngleY)), 0, Convert.ToSingle(-Math.Sin(AutoAngleY))},
                {0, 1, 0},
                {Convert.ToSingle(Math.Sin(AutoAngleY)), 0, Convert.ToSingle(Math.Cos(AutoAngleY))}

            };

            _3DPoint[] Projected = new _3DPoint[8];

            int index = 0;
            foreach (var i in points)
            {
                _3DPoint rotated = i;

                if (AutoXTick.Checked)
                {
                    rotated = Matrix.MatrixMultiply(rotationAutoX, rotated);
                    try
                    {
                        AutoAngleX += Convert.ToDouble(XNum.Text)/10000;
                    }
                    catch (Exception e)
                    {
                                              
                    }
                    
                }
                if (AutoYTick.Checked)
                {
                    rotated = Matrix.MatrixMultiply(rotationAutoY, rotated);
                    try
                    {
                        AutoAngleY += Convert.ToDouble(YNum.Text)/10000;
                    }
                    catch (Exception e)
                    {
                                             
                    }
                }
                if (AutoZTick.Checked)
                {
                    rotated = Matrix.MatrixMultiply(rotationAutoZ, rotated);
                    try
                    {
                        AutoAngleZ += Convert.ToDouble(ZNum.Text)/10000;
                    }
                    catch (Exception e)
                    {
                                            
                    }
                }

                rotated = Matrix.MatrixMultiply(rotationY, rotated);
                rotated = Matrix.MatrixMultiply(rotationX, rotated);
                rotated = Matrix.MatrixMultiply(rotationZ, rotated);
                _3DPoint projected2D = Matrix.MatrixMultiply(_projection, rotated);

                Projected[index] = projected2D;
                index++;
                                           
            }

            int PickColour = 0;
            for (int i = 0; i < 4; i++)
            {
                PickColour++;
                ConnectPoints(i, (i+1) % 4, Projected, PickColour);

                PickColour++;
                ConnectPoints(i+4, ((i+1)%4)+4, Projected, PickColour);

                PickColour++;
                ConnectPoints(i, i+4, Projected, PickColour);
            }


            /*
                ConnectPoints(0, 1, Projected);
                ConnectPoints(1, 2, Projected);
                ConnectPoints(2, 3, Projected);
                ConnectPoints(3, 0, Projected);

                ConnectPoints(4, 5, Projected);
                ConnectPoints(5, 6, Projected);
                ConnectPoints(6, 7, Projected);
                ConnectPoints(7, 4, Projected);

                ConnectPoints(0, 4, Projected);
                ConnectPoints(1, 5, Projected);
                ConnectPoints(2, 6, Projected);
                ConnectPoints(3, 7, Projected);
            */

        }
        void DrawPyramid()
        {
            float[,] rotationX =
            {
                {1,0, 0},
                {0, Convert.ToSingle(Math.Cos(AngleX)), Convert.ToSingle(-Math.Sin(AngleX))},
                {0, Convert.ToSingle(Math.Sin(AngleX)), Convert.ToSingle(Math.Cos(AngleX))}

            };
            float[,] rotationAutoX =
            {
                {1,0, 0},
                {0, Convert.ToSingle(Math.Cos(AutoAngleX)), Convert.ToSingle(-Math.Sin(AutoAngleX))},
                {0, Convert.ToSingle(Math.Sin(AutoAngleX)), Convert.ToSingle(Math.Cos(AutoAngleX))}

            };

            float[,] rotationZ =
            {
                {Convert.ToSingle(Math.Cos(AngleZ)), Convert.ToSingle(-Math.Sin(AngleZ)), 0},
                {Convert.ToSingle(Math.Sin(AngleZ)), Convert.ToSingle(Math.Cos(AngleZ)), 0},
                {0, 0, 1}
            };
            float[,] rotationAutoZ =
            {
                {Convert.ToSingle(Math.Cos(AutoAngleZ)), Convert.ToSingle(-Math.Sin(AutoAngleZ)), 0},
                {Convert.ToSingle(Math.Sin(AutoAngleZ)), Convert.ToSingle(Math.Cos(AutoAngleZ)), 0},
                {0, 0, 1}

            };

            float[,] rotationY =
            {
                {Convert.ToSingle(Math.Cos(AngleY)), 0, Convert.ToSingle(-Math.Sin(AngleY))},
                {0, 1, 0},
                {Convert.ToSingle(Math.Sin(AngleY)), 0, Convert.ToSingle(Math.Cos(AngleY))}

            };
            float[,] rotationAutoY =
            {
                {Convert.ToSingle(Math.Cos(AutoAngleY)), 0, Convert.ToSingle(-Math.Sin(AutoAngleY))},
                {0, 1, 0},
                {Convert.ToSingle(Math.Sin(AutoAngleY)), 0, Convert.ToSingle(Math.Cos(AutoAngleY))}

            };

            _3DPoint[] Projected = new _3DPoint[4];

            int index = 0;
            foreach (var i in points)
            {
                _3DPoint rotated = i;

                if (AutoXTick.Checked)
                {
                    rotated = Matrix.MatrixMultiply(rotationAutoX, rotated);
                    try
                    {
                        AutoAngleX += Convert.ToDouble(XNum.Text) / 10000;
                    }
                    catch (Exception e)
                    {

                    }

                }
                if (AutoYTick.Checked)
                {
                    rotated = Matrix.MatrixMultiply(rotationAutoY, rotated);
                    try
                    {
                        AutoAngleY += Convert.ToDouble(YNum.Text) / 10000;
                    }
                    catch (Exception e)
                    {

                    }
                }
                if (AutoZTick.Checked)
                {
                    rotated = Matrix.MatrixMultiply(rotationAutoZ, rotated);
                    try
                    {
                        AutoAngleZ += Convert.ToDouble(ZNum.Text) / 10000;
                    }
                    catch (Exception e)
                    {

                    }
                }

                rotated = Matrix.MatrixMultiply(rotationY, rotated);
                rotated = Matrix.MatrixMultiply(rotationX, rotated);
                rotated = Matrix.MatrixMultiply(rotationZ, rotated);
                _3DPoint projected2D = Matrix.MatrixMultiply(_projection, rotated);

                Projected[index] = projected2D;
                index++;

            }

            /*
            int PickColour = 0;
            for (int i = 0; i < 4; i++)
            {
                PickColour++;
                ConnectPoints(i, (i + 1) % 4, Projected, PickColour);

                PickColour++;
                ConnectPoints(i + 4, ((i + 1) % 4) + 4, Projected, PickColour);

                PickColour++;
                ConnectPoints(i, i + 4, Projected, PickColour);
            }
            */

            
                ConnectPoints(0, 1, Projected,0);         
                ConnectPoints(0, 2, Projected,0);         
                ConnectPoints(0, 3, Projected,0);

                ConnectPoints(1, 2, Projected, 0);
                ConnectPoints(2, 3, Projected, 0);
                ConnectPoints(3, 1, Projected, 0);

        }
        void DrawSqrPyramid()
        {
            float[,] rotationX =
            {
                {1,0, 0},
                {0, Convert.ToSingle(Math.Cos(AngleX)), Convert.ToSingle(-Math.Sin(AngleX))},
                {0, Convert.ToSingle(Math.Sin(AngleX)), Convert.ToSingle(Math.Cos(AngleX))}

            };
            float[,] rotationAutoX =
            {
                {1,0, 0},
                {0, Convert.ToSingle(Math.Cos(AutoAngleX)), Convert.ToSingle(-Math.Sin(AutoAngleX))},
                {0, Convert.ToSingle(Math.Sin(AutoAngleX)), Convert.ToSingle(Math.Cos(AutoAngleX))}

            };

            float[,] rotationZ =
            {
                {Convert.ToSingle(Math.Cos(AngleZ)), Convert.ToSingle(-Math.Sin(AngleZ)), 0},
                {Convert.ToSingle(Math.Sin(AngleZ)), Convert.ToSingle(Math.Cos(AngleZ)), 0},
                {0, 0, 1}
            };
            float[,] rotationAutoZ =
            {
                {Convert.ToSingle(Math.Cos(AutoAngleZ)), Convert.ToSingle(-Math.Sin(AutoAngleZ)), 0},
                {Convert.ToSingle(Math.Sin(AutoAngleZ)), Convert.ToSingle(Math.Cos(AutoAngleZ)), 0},
                {0, 0, 1}

            };

            float[,] rotationY =
            {
                {Convert.ToSingle(Math.Cos(AngleY)), 0, Convert.ToSingle(-Math.Sin(AngleY))},
                {0, 1, 0},
                {Convert.ToSingle(Math.Sin(AngleY)), 0, Convert.ToSingle(Math.Cos(AngleY))}

            };
            float[,] rotationAutoY =
            {
                {Convert.ToSingle(Math.Cos(AutoAngleY)), 0, Convert.ToSingle(-Math.Sin(AutoAngleY))},
                {0, 1, 0},
                {Convert.ToSingle(Math.Sin(AutoAngleY)), 0, Convert.ToSingle(Math.Cos(AutoAngleY))}

            };

            _3DPoint[] Projected = new _3DPoint[5];

            int index = 0;
            foreach (var i in points)
            {
                _3DPoint rotated = i;

                if (AutoXTick.Checked)
                {
                    rotated = Matrix.MatrixMultiply(rotationAutoX, rotated);
                    try
                    {
                        AutoAngleX += Convert.ToDouble(XNum.Text) / 10000;
                    }
                    catch (Exception e)
                    {

                    }

                }
                if (AutoYTick.Checked)
                {
                    rotated = Matrix.MatrixMultiply(rotationAutoY, rotated);
                    try
                    {
                        AutoAngleY += Convert.ToDouble(YNum.Text) / 10000;
                    }
                    catch (Exception e)
                    {

                    }
                }
                if (AutoZTick.Checked)
                {
                    rotated = Matrix.MatrixMultiply(rotationAutoZ, rotated);
                    try
                    {
                        AutoAngleZ += Convert.ToDouble(ZNum.Text) / 10000;
                    }
                    catch (Exception e)
                    {

                    }
                }

                rotated = Matrix.MatrixMultiply(rotationY, rotated);
                rotated = Matrix.MatrixMultiply(rotationX, rotated);
                rotated = Matrix.MatrixMultiply(rotationZ, rotated);
                _3DPoint projected2D = Matrix.MatrixMultiply(_projection, rotated);

                Projected[index] = projected2D;
                index++;

            }

            /*
            int PickColour = 0;
            for (int i = 0; i < 4; i++)
            {
                PickColour++;
                ConnectPoints(i, (i + 1) % 4, Projected, PickColour);

                PickColour++;
                ConnectPoints(i + 4, ((i + 1) % 4) + 4, Projected, PickColour);

                PickColour++;
                ConnectPoints(i, i + 4, Projected, PickColour);
            }
            */


            ConnectPoints(0, 1, Projected, 0);
            ConnectPoints(1, 3, Projected, 0);
            ConnectPoints(3, 2, Projected, 0);
            ConnectPoints(2, 0, Projected, 0);

            ConnectPoints(0, 4, Projected, 0);
            ConnectPoints(1, 4, Projected, 0);
            ConnectPoints(2, 4, Projected, 0);
            ConnectPoints(3, 4, Projected, 0);

        }
        void DrawDodechedron()
        {
            float[,] rotationX =
            {
                {1,0, 0},
                {0, Convert.ToSingle(Math.Cos(AngleX)), Convert.ToSingle(-Math.Sin(AngleX))},
                {0, Convert.ToSingle(Math.Sin(AngleX)), Convert.ToSingle(Math.Cos(AngleX))}

            };
            float[,] rotationAutoX =
            {
                {1,0, 0},
                {0, Convert.ToSingle(Math.Cos(AutoAngleX)), Convert.ToSingle(-Math.Sin(AutoAngleX))},
                {0, Convert.ToSingle(Math.Sin(AutoAngleX)), Convert.ToSingle(Math.Cos(AutoAngleX))}

            };

            float[,] rotationZ =
            {
                {Convert.ToSingle(Math.Cos(AngleZ)), Convert.ToSingle(-Math.Sin(AngleZ)), 0},
                {Convert.ToSingle(Math.Sin(AngleZ)), Convert.ToSingle(Math.Cos(AngleZ)), 0},
                {0, 0, 1}
            };
            float[,] rotationAutoZ =
            {
                {Convert.ToSingle(Math.Cos(AutoAngleZ)), Convert.ToSingle(-Math.Sin(AutoAngleZ)), 0},
                {Convert.ToSingle(Math.Sin(AutoAngleZ)), Convert.ToSingle(Math.Cos(AutoAngleZ)), 0},
                {0, 0, 1}

            };

            float[,] rotationY =
            {
                {Convert.ToSingle(Math.Cos(AngleY)), 0, Convert.ToSingle(-Math.Sin(AngleY))},
                {0, 1, 0},
                {Convert.ToSingle(Math.Sin(AngleY)), 0, Convert.ToSingle(Math.Cos(AngleY))}

            };
            float[,] rotationAutoY =
            {
                {Convert.ToSingle(Math.Cos(AutoAngleY)), 0, Convert.ToSingle(-Math.Sin(AutoAngleY))},
                {0, 1, 0},
                {Convert.ToSingle(Math.Sin(AutoAngleY)), 0, Convert.ToSingle(Math.Cos(AutoAngleY))}

            };

            _3DPoint[] Projected = new _3DPoint[20];
            List<_3DPoint> Vertices = MakeDodecahedron(size);

            int index = 0;
            foreach (var i in Vertices)
            {
                _3DPoint rotated = i;

                if (AutoXTick.Checked)
                {
                    rotated = Matrix.MatrixMultiply(rotationAutoX, rotated);
                    try
                    {
                        AutoAngleX += Convert.ToDouble(XNum.Text) / 10000;
                    }
                    catch (Exception e)
                    {

                    }

                }
                if (AutoYTick.Checked)
                {
                    rotated = Matrix.MatrixMultiply(rotationAutoY, rotated);
                    try
                    {
                        AutoAngleY += Convert.ToDouble(YNum.Text) / 10000;
                    }
                    catch (Exception e)
                    {

                    }
                }
                if (AutoZTick.Checked)
                {
                    rotated = Matrix.MatrixMultiply(rotationAutoZ, rotated);
                    try
                    {
                        AutoAngleZ += Convert.ToDouble(ZNum.Text) / 10000;
                    }
                    catch (Exception e)
                    {

                    }
                }

                rotated = Matrix.MatrixMultiply(rotationY, rotated);
                rotated = Matrix.MatrixMultiply(rotationX, rotated);
                rotated = Matrix.MatrixMultiply(rotationZ, rotated);
                _3DPoint projected2D = Matrix.MatrixMultiply(_projection, rotated);

                Projected[index] = projected2D;

                //_gr.DrawString(index.ToString(), Font, Brushes.Red, Projected[index].X, Projected[index].Y);

                index++;
            }

            /*
            int PickColour = 0;
            for (int i = 0; i < 4; i++)
            {
                PickColour++;
                ConnectPoints(i, (i + 1) % 4, Projected, PickColour);

                PickColour++;
                ConnectPoints(i + 4, ((i + 1) % 4) + 4, Projected, PickColour);

                PickColour++;
                ConnectPoints(i, i + 4, Projected, PickColour);
            }
            */

            ConnectPoints(17, 12, Projected, 0);
            ConnectPoints(12, 18, Projected, 0);
            ConnectPoints(18, 16, Projected, 0);
            ConnectPoints(16, 19, Projected, 0);
            ConnectPoints(19, 17, Projected, 0);
            
            ConnectPoints(2, 7, Projected, 0);                    
            ConnectPoints(7, 9, Projected, 0);                    
            ConnectPoints(9, 6, Projected, 0);                    
            ConnectPoints(6, 8, Projected, 0);                    
            ConnectPoints(8, 2, Projected, 0);

            ConnectPoints(11, 14, Projected, 0);
            ConnectPoints(14, 17, Projected, 0);
            ConnectPoints(17, 12, Projected, 0);
            ConnectPoints(12, 13, Projected, 0);
            ConnectPoints(13, 11, Projected, 0);

            ConnectPoints(14, 5, Projected, 0);
            ConnectPoints(5, 15, Projected, 0);
            ConnectPoints(15, 19, Projected, 0);
            ConnectPoints(19, 17, Projected, 0);
            ConnectPoints(17, 14, Projected, 0);

            ConnectPoints(15, 9, Projected, 0);
            ConnectPoints(9, 6, Projected, 0);
            ConnectPoints(6, 16, Projected, 0);
            ConnectPoints(16, 19, Projected, 0);
            ConnectPoints(19, 15, Projected, 0);

            ConnectPoints(3, 2, Projected, 0);
            ConnectPoints(4, 1, Projected, 0);
            ConnectPoints(13, 0, Projected, 0);
            ConnectPoints(0, 10, Projected, 0);
            ConnectPoints(10, 18, Projected, 0);

            ConnectPoints(10, 8, Projected, 0);
            ConnectPoints(0, 3, Projected, 0);
            ConnectPoints(3, 1, Projected, 0);
            ConnectPoints(1, 11, Projected, 0);
            ConnectPoints(7, 4, Projected, 0);

            ConnectPoints(4, 5, Projected, 0);

        }
        void DrawOctohedron()
        {
            float[,] rotationX =
            {
                {1,0, 0},
                {0, Convert.ToSingle(Math.Cos(AngleX)), Convert.ToSingle(-Math.Sin(AngleX))},
                {0, Convert.ToSingle(Math.Sin(AngleX)), Convert.ToSingle(Math.Cos(AngleX))}

            };
            float[,] rotationAutoX =
            {
                {1,0, 0},
                {0, Convert.ToSingle(Math.Cos(AutoAngleX)), Convert.ToSingle(-Math.Sin(AutoAngleX))},
                {0, Convert.ToSingle(Math.Sin(AutoAngleX)), Convert.ToSingle(Math.Cos(AutoAngleX))}

            };

            float[,] rotationZ =
            {
                {Convert.ToSingle(Math.Cos(AngleZ)), Convert.ToSingle(-Math.Sin(AngleZ)), 0},
                {Convert.ToSingle(Math.Sin(AngleZ)), Convert.ToSingle(Math.Cos(AngleZ)), 0},
                {0, 0, 1}
            };
            float[,] rotationAutoZ =
            {
                {Convert.ToSingle(Math.Cos(AutoAngleZ)), Convert.ToSingle(-Math.Sin(AutoAngleZ)), 0},
                {Convert.ToSingle(Math.Sin(AutoAngleZ)), Convert.ToSingle(Math.Cos(AutoAngleZ)), 0},
                {0, 0, 1}

            };

            float[,] rotationY =
            {
                {Convert.ToSingle(Math.Cos(AngleY)), 0, Convert.ToSingle(-Math.Sin(AngleY))},
                {0, 1, 0},
                {Convert.ToSingle(Math.Sin(AngleY)), 0, Convert.ToSingle(Math.Cos(AngleY))}

            };
            float[,] rotationAutoY =
            {
                {Convert.ToSingle(Math.Cos(AutoAngleY)), 0, Convert.ToSingle(-Math.Sin(AutoAngleY))},
                {0, 1, 0},
                {Convert.ToSingle(Math.Sin(AutoAngleY)), 0, Convert.ToSingle(Math.Cos(AutoAngleY))}

            };

            _3DPoint[] Projected = new _3DPoint[6];

            int index = 0;
            foreach (var i in points)
            {
                _3DPoint rotated = i;

                if (AutoXTick.Checked)
                {
                    rotated = Matrix.MatrixMultiply(rotationAutoX, rotated);
                    try
                    {
                        AutoAngleX += Convert.ToDouble(XNum.Text) / 10000;
                    }
                    catch (Exception e)
                    {

                    }

                }
                if (AutoYTick.Checked)
                {
                    rotated = Matrix.MatrixMultiply(rotationAutoY, rotated);
                    try
                    {
                        AutoAngleY += Convert.ToDouble(YNum.Text) / 10000;
                    }
                    catch (Exception e)
                    {

                    }
                }
                if (AutoZTick.Checked)
                {
                    rotated = Matrix.MatrixMultiply(rotationAutoZ, rotated);
                    try
                    {
                        AutoAngleZ += Convert.ToDouble(ZNum.Text) / 10000;
                    }
                    catch (Exception e)
                    {

                    }
                }

                rotated = Matrix.MatrixMultiply(rotationY, rotated);
                rotated = Matrix.MatrixMultiply(rotationX, rotated);
                rotated = Matrix.MatrixMultiply(rotationZ, rotated);
                _3DPoint projected2D = Matrix.MatrixMultiply(_projection, rotated);

                Projected[index] = projected2D;
                index++;

            }

            /*
            int PickColour = 0;
            for (int i = 0; i < 4; i++)
            {
                PickColour++;
                ConnectPoints(i, (i + 1) % 4, Projected, PickColour);

                PickColour++;
                ConnectPoints(i + 4, ((i + 1) % 4) + 4, Projected, PickColour);

                PickColour++;
                ConnectPoints(i, i + 4, Projected, PickColour);
            }
            */


            ConnectPoints(0, 1, Projected, 0);
            ConnectPoints(1, 3, Projected, 0);
            ConnectPoints(3, 2, Projected, 0);
            ConnectPoints(2, 0, Projected, 0);

            ConnectPoints(0, 4, Projected, 0);
            ConnectPoints(1, 4, Projected, 0);
            ConnectPoints(2, 4, Projected, 0);
            ConnectPoints(3, 4, Projected, 0);

            ConnectPoints(0, 5, Projected, 0);
            ConnectPoints(1, 5, Projected, 0);
            ConnectPoints(2, 5, Projected, 0);
            ConnectPoints(3, 5, Projected, 0);

        }
        void DrawImported()
        {
            float[,] rotationX =
            {
                {1,0, 0},
                {0, Convert.ToSingle(Math.Cos(AngleX)), Convert.ToSingle(-Math.Sin(AngleX))},
                {0, Convert.ToSingle(Math.Sin(AngleX)), Convert.ToSingle(Math.Cos(AngleX))}

            };
            float[,] rotationAutoX =
            {
                {1,0, 0},
                {0, Convert.ToSingle(Math.Cos(AutoAngleX)), Convert.ToSingle(-Math.Sin(AutoAngleX))},
                {0, Convert.ToSingle(Math.Sin(AutoAngleX)), Convert.ToSingle(Math.Cos(AutoAngleX))}

            };

            float[,] rotationZ =
            {
                {Convert.ToSingle(Math.Cos(AngleZ)), Convert.ToSingle(-Math.Sin(AngleZ)), 0},
                {Convert.ToSingle(Math.Sin(AngleZ)), Convert.ToSingle(Math.Cos(AngleZ)), 0},
                {0, 0, 1}
            };
            float[,] rotationAutoZ =
            {
                {Convert.ToSingle(Math.Cos(AutoAngleZ)), Convert.ToSingle(-Math.Sin(AutoAngleZ)), 0},
                {Convert.ToSingle(Math.Sin(AutoAngleZ)), Convert.ToSingle(Math.Cos(AutoAngleZ)), 0},
                {0, 0, 1}

            };

            float[,] rotationY =
            {
                {Convert.ToSingle(Math.Cos(AngleY)), 0, Convert.ToSingle(-Math.Sin(AngleY))},
                {0, 1, 0},
                {Convert.ToSingle(Math.Sin(AngleY)), 0, Convert.ToSingle(Math.Cos(AngleY))}

            };
            float[,] rotationAutoY =
            {
                {Convert.ToSingle(Math.Cos(AutoAngleY)), 0, Convert.ToSingle(-Math.Sin(AutoAngleY))},
                {0, 1, 0},
                {Convert.ToSingle(Math.Sin(AutoAngleY)), 0, Convert.ToSingle(Math.Cos(AutoAngleY))}

            };

            _3DPoint[] Projected = new _3DPoint[ImportedVertices.Count];

            int index = 0;
            foreach (var i in ImportedVertices)
            {
                _3DPoint rotated = i;

                if (AutoXTick.Checked)
                {
                    rotated = Matrix.MatrixMultiply(rotationAutoX, rotated);
                    try
                    {
                        if (XNum.Text == "")
                        {
                            
                        }
                        else
                        {
                            AutoAngleX += Convert.ToDouble(XNum.Text) / 1000000;
                        }                       
                    }
                    catch (Exception e)
                    {

                    }

                }
                if (AutoYTick.Checked)
                {
                    rotated = Matrix.MatrixMultiply(rotationAutoY, rotated);
                    try
                    {
                        if (YNum.Text == "")
                        {
                            
                        }
                        else
                        {
                            AutoAngleY += Convert.ToDouble(YNum.Text) / 1000000;
                        }                       
                    }
                    catch (Exception e)
                    {

                    }
                }
                if (AutoZTick.Checked)
                {
                    rotated = Matrix.MatrixMultiply(rotationAutoZ, rotated);
                    try
                    {
                        if (ZNum.Text == "")
                        {
                            
                        }
                        else
                        {
                            AutoAngleZ += Convert.ToDouble(ZNum.Text) / 1000000;
                        }                     
                    }
                    catch (Exception e)
                    {

                    }
                }

                rotated = Matrix.MatrixMultiply(rotationY, rotated);
                rotated = Matrix.MatrixMultiply(rotationX, rotated);
                rotated = Matrix.MatrixMultiply(rotationZ, rotated);
                _3DPoint projected2D = Matrix.MatrixMultiply(_projection, rotated);

                Projected[index] = projected2D;
                index++;

            }

            for (int i = 0; i < ListOfFaces.Count; )
            {
                for (int j = 0; j < ListOfFaces[i].Count;)
                {
                    if (ListOfFaces[i][j] == ListOfFaces[i][ListOfFaces[i].Count - 1])
                    {
                        int PointA = Convert.ToInt16(ListOfFaces[i][j]);
                        int PointB = Convert.ToInt16(ListOfFaces[i][0]);
                        ImportedConnectPoints(PointA, PointB, Projected, 0);
                    }
                    else
                    {
                        int PointA = Convert.ToInt16(ListOfFaces[i][j]);
                        int PointB = Convert.ToInt16(ListOfFaces[i][j + 1]);
                        ImportedConnectPoints(PointA, PointB, Projected, 0);
                    }

                    j++;
                }

                i++;
            }
        }

        void ConnectPoints(int i, int j, _3DPoint[] points, int colour)
        {
            Point a = new Point(0,0);
            Point b = new Point(0,0);
           
                a.X = Convert.ToInt32(points[i].X);
                a.Y = Convert.ToInt32(points[i].Y);

                b.X = Convert.ToInt32(points[j].X);
                b.Y = Convert.ToInt32(points[j].Y);

                _gr.DrawLine(Pens.White, a, b);
            
        /*  switch (colour)
            {
                case 1:
                    _gr.DrawLine(Pens.White, a, b);
                    break;
                case 2:
                    _gr.DrawLine(Pens.Red, a, b);
                    break;
                case 3:
                    _gr.DrawLine(Pens.Yellow, a, b);
                    break;
                case 4:
                    _gr.DrawLine(Pens.Green, a, b);
                    break;
                case 5:
                    _gr.DrawLine(Pens.Blue, a, b);
                    break;
                case 6:
                    _gr.DrawLine(Pens.Indigo, a, b);
                    break;
                case 7:
                    _gr.DrawLine(Pens.Violet, a, b);
                    break;
                case 8:
                    _gr.DrawLine(Pens.LawnGreen, a, b);
                    break;
                case 9:
                    _gr.DrawLine(Pens.DeepSkyBlue, a, b);
                    break;
                case 10:
                    _gr.DrawLine(Pens.DarkRed, a, b);
                    break;
                case 11:
                    _gr.DrawLine(Pens.DarkGray, a, b);
                    break;
                case 12:
                    _gr.DrawLine(Pens.LightGray, a, b);
                    break;
            }
            */
        }
        void ImportedConnectPoints(int i, int j, _3DPoint[] points, int colour)
        {
            Point a = new Point(0, 0);
            Point b = new Point(0, 0);

            a.X = Convert.ToInt32(points[i - 1].X);
            a.Y = Convert.ToInt32(points[i - 1].Y);

            b.X = Convert.ToInt32(points[j - 1].X);
            b.Y = Convert.ToInt32(points[j - 1].Y);

            _gr.DrawLine(Pens.White, a, b);

            /*  switch (colour)
                {
                    case 1:
                        _gr.DrawLine(Pens.White, a, b);
                        break;
                    case 2:
                        _gr.DrawLine(Pens.Red, a, b);
                        break;
                    case 3:
                        _gr.DrawLine(Pens.Yellow, a, b);
                        break;
                    case 4:
                        _gr.DrawLine(Pens.Green, a, b);
                        break;
                    case 5:
                        _gr.DrawLine(Pens.Blue, a, b);
                        break;
                    case 6:
                        _gr.DrawLine(Pens.Indigo, a, b);
                        break;
                    case 7:
                        _gr.DrawLine(Pens.Violet, a, b);
                        break;
                    case 8:
                        _gr.DrawLine(Pens.LawnGreen, a, b);
                        break;
                    case 9:
                        _gr.DrawLine(Pens.DeepSkyBlue, a, b);
                        break;
                    case 10:
                        _gr.DrawLine(Pens.DarkRed, a, b);
                        break;
                    case 11:
                        _gr.DrawLine(Pens.DarkGray, a, b);
                        break;
                    case 12:
                        _gr.DrawLine(Pens.LightGray, a, b);
                        break;
                }
                */
        }
        void FpsCounter()
        {
            if(_tSec == DateTime.Now.Second && _isRunning)
            {
                _tTicks = _tTicks + 1;
            }
            else
            { 
                    _tMax = _tTicks;
                _tTicks = 0;
                _tSec = DateTime.Now.Second;
            }
        }
        void EndFrame()
        {
            _gr = Graphics.FromImage(_backBuffer);
            _gr.TranslateTransform(_resWidth / 2, _resHeight / 2);
            try
            {
                _backBufferGr = CreateGraphics();
                _backBufferGr.DrawImage(_backBuffer, 0, 0, _resWidth, _resHeight);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR\n" + ex.ToString());
            }
            _gr.Clear(Color.Black);
        }

        private void CubeButton_Click(object sender, EventArgs e)
        {
            points = new _3DPoint[8];
            points[0] = new _3DPoint(-size, -size, -size);
            points[1] = new _3DPoint(size, -size, -size);
            points[2] = new _3DPoint(size, size, -size);
            points[3] = new _3DPoint(-size, size, -size);
            points[4] = new _3DPoint(-size, -size, size);
            points[5] = new _3DPoint(size, -size, size);
            points[6] = new _3DPoint(size, size, size);
            points[7] = new _3DPoint(-size, size, size);
            ShapeNum = 0;
        }
        private void PyramidButton_Click(object sender, EventArgs e)
        {
            points = new _3DPoint[4];
            points[0] = new _3DPoint( 
                                        ((8/9) * (8/9)) * size,
                                        0, 
                                        -((1/3) * size)
                                    );

            points[1] = new _3DPoint(
                                        -(((2 / 9) * (2 / 9)) * size),
                                        ((2 / 3) * (2 / 3)) * size, 
                                        -((1 / 3) * size)
                                    );

            points[2] = new _3DPoint(
                                        -(((2/9)*(2/9)) * size), 
                                        -(((2/3)*(2/3)) * size), 
                                        -((1/3) * size)
                                    );

            points[3] = new _3DPoint(0, 0, size);
            ShapeNum = 1;
        }
        private void Dodecohedron_Click(object sender, EventArgs e)
        {

            ShapeNum = 3;

        }
        private void SqrPyramid_Click(object sender, EventArgs e)
        {
            points = new _3DPoint[5];
            points[0] = new _3DPoint(size, size, -size);
            points[1] = new _3DPoint(-size, size, -size);
            points[2] = new _3DPoint(size, size, size);
            points[3] = new _3DPoint(-size, size, size);
            points[4] = new _3DPoint(0, -size, 0);
            ShapeNum = 2;
        }
        private void Octahedron_Click(object sender, EventArgs e)
        {
            points = new _3DPoint[6];
            points[0] = new _3DPoint(size, 0, -size);
            points[1] = new _3DPoint(-size, 0, -size);
            points[2] = new _3DPoint(size, 0, size);
            points[3] = new _3DPoint(-size, 0, size);
            points[4] = new _3DPoint(0, -size, 0);
            points[5] = new _3DPoint(0, size, 0);
            ShapeNum = 4;
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            int size = -1;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                Read_objfile(openFileDialog1.FileName);             
            }
        }

        private void hScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {
            AngleX = hScrollBar3.Value/100f;
        }
        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            AngleY = hScrollBar2.Value/100f;
        }
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            AngleZ = hScrollBar1.Value/100f;
        }

        private static List<_3DPoint> MakeDodecahedron(double r)
        {
            // Calculate constants that will be used to generate vertices
            var phi = (float)(Math.Sqrt(5) - 1) / 2; // The golden ratio

            var a = 1 / Math.Sqrt(3);
            var b = a / phi;
            var c = a * phi;

            // Generate each vertex
            var vertices = new List<_3DPoint>();
            foreach (var i in new[] { -1, 1 })
            {
                foreach (var j in new[] { -1, 1 })
                {
                    vertices.Add(new _3DPoint(
                        0,
                        Convert.ToSingle(i * c * r),
                        Convert.ToSingle(j * b * r)
                                             ));

                    vertices.Add(new _3DPoint(
                        Convert.ToSingle(i * c * r),
                        Convert.ToSingle(j * b * r),
                        0
                                             ));

                    vertices.Add(new _3DPoint(
                        Convert.ToSingle(i * b * r),
                        0,
                        Convert.ToSingle(j * c * r)
                                            ));

                    foreach (var k in new[] { -1, 1 })
                        vertices.Add(new _3DPoint(
                            Convert.ToSingle(i * a * r),
                            Convert.ToSingle(j * a * r),
                            Convert.ToSingle(k * a * r)
                                                ));
                }
            }
            return vertices;
        }

        void Read_objfile(string filename)
        {
            ImportedVertices = null;
            ImportedVertices = new List<_3DPoint>();

            ListOfFaces = null;
            ListOfFaces = new List<List<string>>();

            Lastopenedfile.Text = filename;

            StreamReader sr = new StreamReader(filename);

            do
            {
                string line = sr.ReadLine();

                string vX = "";
                string vY = "";
                string vZ = "";

                List<string> facenumbers = new List<string>();

                int axisSwitch = 0;
                int faceSwitch = -1;

                for (int i = 0; i < line.Length; i++)
                {
                    if (line[0] == '#' || line[0] == 'o' || line[0] == 's')
                    {
                        
                    }
                    else if (line[0] == 'v')
                    {
                        if (line[i] == 'v')
                        {
                            
                        }
                        else if(line[i] == ' ')
                        {
                            axisSwitch++;
                        }
                        else
                        {
                            switch (axisSwitch)
                            {
                                case 1:
                                    vX += line[i];
                                    break;

                                case 2:
                                    vY += line[i];
                                    break;

                                case 3:
                                    vZ += line[i];
                                    break;
                            }
                        }
                    }
                    else if (line[0] == 'f')
                    {
                        if (line[i] == 'f')
                        {
                            
                        }
                        else if (line[i] == ' ')
                        {
                            faceSwitch++;
                            facenumbers.Add("");
                        }
                        else
                        {
                            facenumbers[faceSwitch] += line[i];
                        }
                    }
                }

                if (line[0] == 'v')
                {
                    ImportedVertices.Add(new _3DPoint(vX,vY,vZ));
                }

                if (line[0] == 'f')
                {
                    ListOfFaces.Add(facenumbers);
                }


            } while (!sr.EndOfStream);

            sr.Close();

            ShapeNum = 5;

        }
    }
}