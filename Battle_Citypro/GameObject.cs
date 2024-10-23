using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battle_Citypro
{
    abstract class GameObject
    {
        public int X { get; set; }//xiangsu
        public int Y { get; set; }
        public  int Width { get; set; }  
        public  int Height { get; set; }
       //o public int Width { get; set; }

       // public int Height { get; set; }
        protected abstract Image GetImage();//abstract
        public void DrawSelf()
        {
            Graphics g = GameFramework.g;

            g.DrawImage(GetImage(),X,Y);
        }
        public virtual void Update()
        {
            DrawSelf();
        }
        public Rectangle GetRectangle()
        {
            Rectangle rectangle = new Rectangle(X,Y,Width,Height);
            return rectangle;
        }
    }
}
