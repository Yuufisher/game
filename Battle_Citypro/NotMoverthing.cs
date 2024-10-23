using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battle_Citypro
{
    internal class NotMoverthing:GameObject//block...
    {
        private Image img;
        public Image Img { get { return img; } 
             set {
                 img = value;
                 Width = img.Width;
                 Height = img.Height;
             }
        }

        //public Image Img { get; set; }
        protected override Image GetImage()
        {
            return Img;
        }
        public NotMoverthing(int x,int y,Image img)
        {
            this.X = x;
            this.Y = y;
            this.Img = img;
        }
    }
}
