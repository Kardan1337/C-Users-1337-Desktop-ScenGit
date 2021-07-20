using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scene1
{
    class camera
    {
        public int x_center;
        public int y_center;
        public int z_center;
        public int z_depth;

        public double angle_x;
        public double angle_y;
        public double angle_z;
                       
        
        public camera(int x1, int y1, int z1, double rot_x, double rot_y, double rot_z, int z_depth1)
        {
            x_center = x1;
            y_center = y1;
            z_center = z1;
            angle_x = rot_x;
            angle_y = rot_y;
            angle_z = rot_z;
            z_depth = z_depth1;
        }

        public void moveright()
        {
            x_center = x_center + 10;
        }
        public void moveleft()
        {
            x_center = x_center - 10;
        }
        public void moveup()
        {
            y_center = y_center - 10;
        }
        public void movedown()
        {
            y_center = y_center + 10;
        }
        public void moveforvard()
        {
            z_center = z_center + 10;
        }
        public void moveback()
        {
            z_center = z_center - 10;
        }

        public void rotright()
        {
            angle_y = angle_y + 0.1;
        }
        public void rotleft()
        {
            angle_y = angle_y - 0.1;
        }
        public void rotup()
        {
            angle_x = angle_x - 0.1;
        }
        public void rotdown()
        {
            angle_x = angle_x + 0.1;
        }
        public void krenright()
        {
            angle_z = angle_z + 0.1;
        }
        public void krenleft()
        {
            angle_z = angle_z - 0.1;
        }



    }
}
