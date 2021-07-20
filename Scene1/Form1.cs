using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Scene1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string adress= "C:/Users/1337/source/repos/Scene1/sphere.obj";
        
       
        camera cam1 = new camera(0, 0, 0, 0, 0, 0, 1000);
        bool cadr = false;
        bool rotateX = false;
        bool rotateY = false;
        bool rotateZ = false;
        double fps1=0;
        int newobj_x = 0;
        int newobj_y = 0;
        int newobj_z = 0;
        double newobj_xang = 0;
        double newobj_yang = 0;
        double newobj_zang = 0;
        double newobj_zoom = 20;
        

        List<obj3D> objectlist = new List<obj3D>();
       



        private void Form1_Load(object sender, EventArgs e)
        {

            





        }

        private void Button1_Click(object sender, EventArgs e)
        {
            cam1.moveright();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

            DrawScreen();


        }

        private void Button3_Click(object sender, EventArgs e)
        {
            cam1.moveforvard();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            cam1.movedown();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            cam1.moveleft();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            cam1.moveup();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            cam1.moveback();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            cam1.rotup();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            cam1.rotright();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            cam1.rotleft();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            cam1.rotdown();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            cam1.krenright();
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            cam1.krenleft();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            rotateX = !rotateX;
        }

        private void DrawObj()
        {
            foreach (obj3D j in objectlist)
            {
                foreach (int[] i in j.polygonlist)
                { 
                    Graphics gr = this.CreateGraphics();
                    string x1 ="Полигонов:"+ Convert.ToString(j.polygonlist.Count);
                    
                    Font font1 = new Font("Arial", 10);
                    SolidBrush brush1 = new SolidBrush(Color.Red);
                    gr.DrawString(x1, font1, brush1, 900, 20);
                    
                    Pen pen5 = new Pen(Color.Green, 1);
                    int a1 = j.getcoordlist(cam1.z_depth, cam1.x_center, cam1.y_center, cam1.z_center, cam1.angle_x, cam1.angle_y, cam1.angle_z)[i[0] - 1][0];
                    int a2 = j.getcoordlist(cam1.z_depth, cam1.x_center, cam1.y_center, cam1.z_center, cam1.angle_x, cam1.angle_y, cam1.angle_z)[i[0] - 1][1]; ;
                    int a3 = j.getcoordlist(cam1.z_depth, cam1.x_center, cam1.y_center, cam1.z_center, cam1.angle_x, cam1.angle_y, cam1.angle_z)[i[1] - 1][0]; ;
                    int a4 = j.getcoordlist(cam1.z_depth, cam1.x_center, cam1.y_center, cam1.z_center, cam1.angle_x, cam1.angle_y, cam1.angle_z)[i[1] - 1][1]; ;
                    int a5 = j.getcoordlist(cam1.z_depth, cam1.x_center, cam1.y_center, cam1.z_center, cam1.angle_x, cam1.angle_y, cam1.angle_z)[i[2] - 1][0]; ;
                    int a6 = j.getcoordlist(cam1.z_depth, cam1.x_center, cam1.y_center, cam1.z_center, cam1.angle_x, cam1.angle_y, cam1.angle_z)[i[2] - 1][1]; ;
                    Point p1 = new Point(a1, a2);
                    Point p2 = new Point(a3, a4);
                    Point p3 = new Point(a5, a6);

                    gr.DrawLine(pen5, p1, p2);
                    gr.DrawLine(pen5, p1, p3);
                    gr.DrawLine(pen5, p3, p2);
                    

                }
            }
            
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            foreach(obj3D i in objectlist)
            {
                i.set_start_rotate();
            }
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "obj files(*.obj)|*.obj";
            // opf.ShowDialog();
            if (opf.ShowDialog() == DialogResult.OK) { adress = opf.FileName;  };
            
        }
        public void CreateObj(string adress)
        {
            obj3D dot1 = new obj3D(adress, newobj_x, newobj_y, newobj_z, newobj_xang, newobj_yang, newobj_zang, newobj_zoom);
            objectlist.Add(dot1);
        }

        private void Button15_Click(object sender, EventArgs e)
        {

            objectlist.Clear();
            
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            newobj_x = Convert.ToInt32(textBox1.Text);
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            newobj_y = Convert.ToInt32(textBox2.Text);
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            newobj_z = Convert.ToInt32(textBox3.Text);
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            newobj_xang = Convert.ToDouble(textBox4.Text);
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            newobj_yang = Convert.ToDouble(textBox5.Text);
        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {
            newobj_zang = Convert.ToDouble(textBox6.Text);
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            CreateObj(adress);
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            rotateY = !rotateY;
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            rotateZ = !rotateZ;
        }

        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            cadr = !cadr;
            if (!cadr) { timer1.Start(); }
        }
              private void Button17_Click(object sender, EventArgs e)
        {
            if (cadr) { DrawScreen();  }
            timer1.Stop();
        }

        public void DrawScreen()
        {
            System.Diagnostics.Stopwatch fps = new System.Diagnostics.Stopwatch();
            fps.Start();
            Graphics gr = this.CreateGraphics();
            Font font1 = new Font("Arial", 10);
            SolidBrush brush1 = new SolidBrush(Color.Red);
            string x1 = "FPS:" + Convert.ToString(fps1);



            Refresh();
            gr.DrawString(x1, font1, brush1, 900, 50);
            DrawObj();


            if (rotateX) { foreach (obj3D i in objectlist) { i.rotate_x(0.1); }; };
            if (rotateY) { foreach (obj3D i in objectlist) { i.rotate_y(0.1); }; };
            if (rotateZ) { foreach (obj3D i in objectlist) { i.rotate_z(0.1); }; };



            fps.Stop();
            fps1 = 1000 / fps.ElapsedMilliseconds;

            if (cadr) { timer1.Stop(); } else { timer1.Start(); }
        }

        private void TextBox7_TextChanged(object sender, EventArgs e)
        {
            newobj_zoom = Convert.ToDouble(textBox7.Text);
        }

       
    }
}
