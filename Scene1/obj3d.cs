using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;

namespace Scene1
{
    class obj3D
    {
        public int x_center;
        public int y_center;
        public int z_center;
        double angle_x;
        double angle_y;
        double angle_z;
        double zoom;



        public List<double[]> pointlist = new List<double[]>();
        public List<int[]> polygonlist = new List<int[]>();

        

        public obj3D(string adress,int x1,int y1, int z1, double rot_x,double rot_y, double rot_z, double zoom1)
        {
            x_center = x1;
            y_center = y1;
            z_center = z1;
            angle_x = rot_x;
            angle_y = rot_y;
            angle_z = rot_z;
            zoom = zoom1;
           
            pointlist = GetCoordFromFile(adress);
            polygonlist = GetPolyFromFile(adress);
        }

        
        public double[] PointCoordToGlobal(double[] pointcoord)
        {
            
            double[,] rotmatrX =
                  {
                {1,           0                     ,0 },
                {0,Math.Cos(angle_x),Math.Sin(angle_x) },
                {0,-Math.Sin(angle_x),Math.Cos(angle_x) }
            };
            double[,] rotmatrY =
            {
                {Math.Cos(angle_y),0,Math.Sin(angle_y) },
                {0,1,0 },
                {-Math.Sin(angle_y),0,Math.Cos(angle_y) }
            };
            double[,] rotmatrZ =
             {
                {Math.Cos(angle_z),-Math.Sin(angle_z),0 },
                {Math.Sin(angle_z),Math.Cos(angle_z),0 },
                {0,0,1 }
            };
            double[] globalcoord = pointcoord;
            double[] coord_rot_x = { 0, 0, 0 };
            double[] coord_rot_y = { 0, 0, 0 };
            double[] coord_rot_z = { 0, 0, 0 };


            for (int i = 0; i <= 2; i++)
            {
                coord_rot_x[i] = (rotmatrX[i, 0] * globalcoord[0]) + (rotmatrX[i, 1] * globalcoord[1]) + (rotmatrX[i, 2] * globalcoord[2]);
            }
            for (int i = 0; i <= 2; i++)
            {
                coord_rot_y[i] = (rotmatrY[i, 0] * coord_rot_x[0]) + (rotmatrY[i, 1] * coord_rot_x[1]) + (rotmatrY[i, 2] * coord_rot_x[2]);
            }
            for (int i = 0; i <= 2; i++)
            {
                coord_rot_z[i] = (rotmatrZ[i, 0] * coord_rot_y[0]) + (rotmatrZ[i, 1] * coord_rot_y[1]) + (rotmatrZ[i, 2] * coord_rot_y[2]);
            }
            double[] getglobalcoord = { coord_rot_z[0] + x_center, coord_rot_z[1] + y_center, coord_rot_z[2] + z_center};
            
            return getglobalcoord;

        }
        public double[] GlobalCoordToScreen(double[] pointcoord,int xcam, int ycam, int zcam,double camrotx,double camroty, double camrotz)
        {
            double angle_x1 = camrotx;
            double angle_y1 = camroty;
            double angle_z1 = camrotz;
            double[,] rotmatrX =
                  {
                {1,           0                     ,0 },
                {0,Math.Cos(angle_x1),-Math.Sin(angle_x1) },
                {0,Math.Sin(angle_x1),Math.Cos(angle_x1) }
            };
            double[,] rotmatrY =
            {
                {Math.Cos(angle_y1),0,-Math.Sin(angle_y1) },
                {0,1,0 },
                {Math.Sin(angle_y1),0,Math.Cos(angle_y1) }
            };
            double[,] rotmatrZ =
             {
                {Math.Cos(angle_z1),Math.Sin(angle_z1),0 },
                {-Math.Sin(angle_z1),Math.Cos(angle_z1),0 },
                {0,0,1 }
            };
            double[] globalcoord = pointcoord;
            double[] coord_rot_x = { 0, 0, 0 };
            double[] coord_rot_y = { 0, 0, 0 };
            double[] coord_rot_z = { 0, 0, 0 };


            for (int i = 0; i <= 2; i++)
            {
                coord_rot_x[i] = (rotmatrX[i, 0] * globalcoord[0]) + (rotmatrX[i, 1] * globalcoord[1]) + (rotmatrX[i, 2] * globalcoord[2]);
            }
            for (int i = 0; i <= 2; i++)
            {
                coord_rot_y[i] = (rotmatrY[i, 0] * coord_rot_x[0]) + (rotmatrY[i, 1] * coord_rot_x[1]) + (rotmatrY[i, 2] * coord_rot_x[2]);
            }
            for (int i = 0; i <= 2; i++)
            {
                coord_rot_z[i] = (rotmatrZ[i, 0] * coord_rot_y[0]) + (rotmatrZ[i, 1] * coord_rot_y[1]) + (rotmatrZ[i, 2] * coord_rot_y[2]);
            }
            double[] getglobalcoord = { coord_rot_z[0] - xcam, coord_rot_z[1] - ycam, coord_rot_z[2] - zcam };

            return getglobalcoord;

        }


        public int[] CameraCoordToScreen(double[] coord, int z_depth)
        {
            int x_screen = (int)(z_depth * (coord[0]/coord[2]));
            int y_screen = (int)(z_depth * (coord[1]/coord[2]));
            int[] screencoord = { x_screen+400, y_screen+200 };
            return screencoord;
        }


        public List<int[]> getcoordlist(int z_depth,int x_cam,int y_cam,int z_cam,double camanglex,double camangley,double camanglez)
        {
            List<int[]> screencoordlist = new List<int[]>();
            foreach (double[] i in pointlist)
            {
                double[] c = PointCoordToGlobal(i);
                double[] b = GlobalCoordToScreen(c,x_cam,y_cam,z_cam,camanglex,camangley,camanglez);
                int[] a = CameraCoordToScreen(b, z_depth);
               screencoordlist.Add(a);
            }
            return screencoordlist;

        }
        public void rotate_z(double delta)
        {
            
            angle_z = angle_z + delta;
        }
        public void rotate_y(double delta)
        {

            angle_y = angle_y + delta;
        }
        public void rotate_x(double delta)
        {

            angle_x = angle_x + delta;
        }
        public void set_start_rotate()
        {

            angle_x = 0;
            angle_y = 0;
            angle_z = 0;
        }

        public List<double[]> GetCoordFromFile(string adress)
        {
           // "C:/Users/1337/source/repos/ConsoleApp3/sphere.obj"
            List<double[]> pointlist = new List<double[]>();
            StreamReader sr = new StreamReader(adress);
            while (sr.Peek() != -1)
            {
                string a = sr.ReadLine();
                string b = "";
                string c = "";
                string d = "";
                if (a.StartsWith("#")) { continue; }
                if (a.StartsWith("v") & !a.StartsWith("vn") & !a.StartsWith("vt")) { b = a.Substring(1); } else { continue; }

                if (b.StartsWith("-")) { b = b.Substring(0, 9); } else { b = b.Substring(0, 10); }
                b = b.Trim();
                double b1 = Convert.ToDouble(b, System.Globalization.CultureInfo.InvariantCulture);

                c = a.Substring(1);
                c = c.Substring(b.Length + 1);
                if (c.StartsWith("-")) { c = c.Substring(0, 9); } else { c = c.Substring(0, 10); }
                c = c.Trim();
                double с1 = Convert.ToDouble(c, System.Globalization.CultureInfo.InvariantCulture);

                d = a.Substring(1);
                d = d.Substring(b.Length + c.Length + 2);

                if (d.StartsWith("-")) { d = d.Substring(0, 9); } else { d = d.Substring(0, d.Length); }
                d = d.Trim();
                double d1 = Convert.ToDouble(d, System.Globalization.CultureInfo.InvariantCulture);

                double[] point = { b1*zoom, с1*zoom, d1*zoom };
                pointlist.Add(point);
               



            }
            sr.Close();
            return pointlist;
        }
        public List<int[]> GetPolyFromFile(string adress)
        {
            // "C:/Users/1337/source/repos/ConsoleApp3/sphere.obj"
            List<int[]> polygonlist = new List<int[]>();
            StreamReader sr = new StreamReader(adress);
            while (sr.Peek() != -1)
            {
                string a = sr.ReadLine();
                string b = "";
                string c = "";
                string d = "";


                if (a.StartsWith("#")) { continue; }
                if (a.StartsWith("f")) { a = a.Substring(1); } else { continue; }
                //Console.WriteLine(a);

                a = a.Trim();
                a = a.Replace(" ", "*");
                Console.WriteLine(a);
                b = a.Substring(0, a.IndexOf('/'));
                c = a.Substring(a.IndexOf('*'));
                c = c.Substring(1, c.IndexOf('/') - 1);
                d = a.Substring(a.IndexOf('*') + 1);
                d = d.Substring(d.IndexOf('*') + 1);
                d = d.Substring(0, d.IndexOf('/'));

                int b1 = Convert.ToInt32(b);
                int c1 = Convert.ToInt32(c);
                int d1 = Convert.ToInt32(d);

                int[] poly = { b1, c1, d1 };
                polygonlist.Add(poly);
                Console.Write(b1);
                Console.Write("  ");
                Console.Write(c1);
                Console.Write("  ");
                Console.WriteLine(d1);
                Console.ReadLine();




            }
            sr.Close();
            return polygonlist;
        }



    }


        
    
}
