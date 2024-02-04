using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace OGL
{
    public partial class OGL : Form
    {
        private Point fixPt;

        private bool isA = true;
        private int start = 99999, end = 99999;
        private int bndSelect = 0, smPtsChoose = 1, smPts = 4;

        private double zoom = 1, sX = 0, sY = 0;

        //public vec3 pint = new vec3(0.0, 1.0, 0.0);

        public List<vec2> secList = new List<vec2>();

        public List<CSeg> segList = new List<CSeg>();
        private Random rnd = new Random();

        private double camDistance = -500;

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 50;

            InitializeVectors();
        }

        public OGL()
        {
            InitializeComponent();
        }

        private void glWin_Paint(object sender, PaintEventArgs e)
        {
            glWin.MakeCurrent();

            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
            GL.LoadIdentity();                  // Reset The View
            GL.Translate(0, 0, -camDistance);


            //translate to that spot in the world
            //if (cboxIsZoom.Checked) GL.Translate(-segList[segList.Count - 1].ptA.easting, -segList[segList.Count - 1].ptA.northing, 0);
            //GL.Rotate(ang, 0, 0, 1);
            for (int i = 0; i < segList.Count; i++)
            {
                //first is not carried
                if (i == 0)
                {
                    segList[i].ptB = new vec2(segList[i].ptA.easting + segList[i].length * Math.Sin(glm.toRadians(segList[i].iAngle)),
                        segList[i].ptA.northing + segList[i].length * Math.Cos(glm.toRadians(segList[i].iAngle)));
                }
                else
                {
                    segList[i].ptA = new vec2(segList[i - 1].ptB);

                    segList[i].ptB = new vec2(segList[i].ptA.easting + segList[i].length * Math.Sin(glm.toRadians(segList[i].iAngle)),
                        segList[i].ptA.northing + segList[i].length * Math.Cos(glm.toRadians(segList[i].iAngle)));
                }

                if (segList[i].direction > 0)
                {
                    segList[i].iAngle += segList[i].rate;
                    if (segList[i].iAngle > 359) segList[i].iAngle -= 360;
                }
                else
                {
                    segList[i].iAngle -= segList[i].rate;
                    if (segList[i].iAngle < 0) segList[i].iAngle += 360;
                }
            }

            GL.LineWidth(1);
            GL.Color3(0.725f, 0.95f, 0.950f);
            GL.Begin(PrimitiveType.LineStrip);

            for (int i = 0; i < secList.Count; i++)
                GL.Vertex3(secList[i].easting, secList[i].northing, 0);
            GL.End();

            if (secList.Count > 20 && segList[0].iAngle == last)
            {
                GL.Flush();
                glWin.SwapBuffers();
                System.Threading.Thread.Sleep(2000);
                secList?.Clear();
                InitializeVectors();
            }
            else
            {
                secList.Add(new vec2(segList[segList.Count - 1].ptB));

                if ((Math.Abs(secList[secList.Count - 1].easting*2) > camDistance))
                    camDistance = Math.Abs(secList[secList.Count - 1].easting)*2;

                if ((Math.Abs(secList[secList.Count - 1].northing*2) > camDistance))
                    camDistance = Math.Abs(secList[secList.Count - 1].northing)*2;

                GL.LineWidth(1);
                GL.Begin(PrimitiveType.Lines);

                for (int i = 0; i < segList.Count; i++)
                {
                    GL.Color3(0.0, 0.2f, 0.20f);
                    GL.Vertex3(segList[i].ptA.easting, segList[i].ptA.northing, 0);
                    GL.Color3(1.0f, 0.250f, 0.250f);
                    GL.Vertex3(segList[i].ptB.easting, segList[i].ptB.northing, 0);
                }
                GL.End();

                GL.PointSize(2);
                GL.Color3(1.0f, 1.0f, 0);

                GL.Begin(PrimitiveType.Points);
                GL.Vertex3(segList[0].ptA.easting, segList[0].ptA.northing, 0);

                for (int i = 0; i < segList.Count; i++)
                {
                    GL.Vertex3(segList[i].ptB.easting, segList[i].ptB.northing, 0);
                }
                GL.End();

                GL.PointSize(8);
                GL.Color3(0.5f, 1.0f, 1.0);

                GL.Begin(PrimitiveType.Points);
                GL.Vertex3(segList[segList.Count - 1].ptB.easting, segList[segList.Count - 1].ptB.northing, 0);
                GL.End();
                glWin.SwapBuffers();
                GL.Flush();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (Height > Width)
            {

            }
            else
            {

            }
        }

        private int last;

        private void InitializeVectors()
        {
            segList?.Clear();
            int num = rnd.Next(50) + 2;
            for (int i = 0; i < num; i++)
            {
                segList.Add(new CSeg());
            }

            camDistance = 0;
            for (int i = 0; i < segList.Count; i++)
            {
                if (i == 0)
                {
                    segList[i].rate = 1;
                    segList[i].direction = 1;
                    segList[i].length = rnd.Next(10) + 10;
                    segList[i].iAngle = rnd.Next(359);
                }
                else
                {
                    segList[i].rate = rnd.Next(5) + 1;
                    segList[i].length = rnd.Next(40) + 2;

                    int dir = rnd.Next(2);
                    if (dir == 0) segList[i].direction = -1;
                    else segList[i].direction = dir;

                    segList[i].iAngle = rnd.Next(359);
                }
            }
            lblSegments.Text = camDistance.ToString("N1");
            last = segList[0].iAngle + 3;
            if (last > 359) last -= 360;

            camDistance = 100;

            timer1.Interval = rnd.Next(30) + 10;

            lblSegments.Text = segList.Count.ToString();

            for (int i = 0; i < segList.Count; i++)
            {

                //first is not carried
                if (i == 0)
                {
                    segList[i].ptA = new vec2(); //set to origin
                    segList[i].ptB = new vec2(segList[i].ptA.easting + segList[i].length * Math.Sin(glm.toRadians(segList[i].iAngle)),
                        segList[i].ptA.northing + segList[i].length * Math.Cos(glm.toRadians(segList[i].iAngle)));
                }
                else
                {
                    segList[i].ptA = new vec2(segList[i - 1].ptB);

                    segList[i].ptB = new vec2(segList[i].ptA.easting + segList[i].length * Math.Sin(glm.toRadians(segList[i].iAngle)),
                        segList[i].ptA.northing + segList[i].length * Math.Cos(glm.toRadians(segList[i].iAngle)));
                }
            }

        }


        private void glWin_Load(object sender, EventArgs e)
        {
            glWin.MakeCurrent();
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        }

        private void glWin_Resize(object sender, EventArgs e)
        {
            glWin.MakeCurrent();
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            //58 degrees view
            Matrix4 mat = Matrix4.CreatePerspectiveFieldOfView(1.01f, 1.0f, 1.0f, 20000);
            GL.LoadMatrix(ref mat);

            GL.MatrixMode(MatrixMode.Modelview);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            glWin.Refresh();
        }
    }

    public struct vec2
    {
        public double easting; //easting
        public double northing; //northing

        public vec2(double easting, double northing)
        {
            this.easting = easting;
            this.northing = northing;
        }

        public vec2(vec2 v)
        {
            easting = v.easting;
            northing = v.northing;
        }
    }

    public class CSeg
    {
        public int iAngle;
        public int initAngle;
        public int rate;
        public bool isVisible;
        public vec2 ptA;
        public vec2 ptB;
        public int direction;
        public double length;

        public CSeg()
        {
            iAngle = 0;
            initAngle = 0;
            isVisible = true;
            ptA = new vec2();
            ptB = new vec2();
            direction = 1;
            length = 50;
            rate = 1;
        }

        public CSeg(CSeg _Seg)
        {
            iAngle = _Seg.iAngle;
            initAngle = _Seg.initAngle;
            isVisible = _Seg.isVisible;
            ptA = _Seg.ptA;
            ptB = _Seg.ptB;
            direction = _Seg.direction;
            length = _Seg.length;
            rate = _Seg.rate;
        }
    }

    public static class glm
    {
        //Regex file expression
        public const string fileRegex = " /^(?!.{256,})(?!(aux|clock\\$|con|nul|prn|com[1-9]|lpt[1-9])(?:$|\\.))[^ ][ \\.\\w-$()+=[\\];#@~,&amp;']+[^\\. ]$/i";

        //inches to meters
        public const double in2m = 0.0254;

        //meters to inches
        public const double m2in = 39.3701;

        //meters to feet
        public const double m2ft = 3.28084;

        //feet to meters
        public const double ft2m = 0.3048;

        //Hectare to Acres
        public const double ha2ac = 2.47105;

        //Acres to Hectare
        public const double ac2ha = 0.404686;

        //Meters to Acres
        public const double m2ac = 0.000247105;

        //Meters to Hectare
        public const double m2ha = 0.0001;

        // liters per hectare to us gal per acre
        public const double galAc2Lha = 9.35396;

        //us gal per acre to liters per hectare
        public const double LHa2galAc = 0.106907;

        //Liters to Gallons
        public const double L2Gal = 0.264172;

        //Gallons to Liters
        public const double Gal2L = 3.785412534258;

        //the pi's
        public const double twoPI = 6.28318530717958647692;

        public const double PIBy2 = 1.57079632679489661923;

        //Degrees Radians Conversions
        public static double toDegrees(double radians)
        {
            return radians * 57.295779513082325225835265587528;
        }

        public static double toRadians(double degrees)
        {
            return degrees * 0.01745329251994329576923690768489;
        }
    }
}
