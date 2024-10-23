using Battle_Citypro.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_Citypro
{
    internal class Explosion : GameObject
    {
        public bool isDestoryex { get; set; }
        private int PlaySpeed = 3;
        private int PlayCount = -1;
        private int index = 0;
        private Bitmap[] bmpArray = new Bitmap[]
        {
            Resources.e1,
            Resources.e2,
            Resources.e3,
            Resources.e4,
            Resources.ex5

        };
        public Explosion(int x,int y)
        {
            foreach (Bitmap bmp in bmpArray)
            {
                bmp.MakeTransparent(Color.Black);
            }

            this.X = x - bmpArray[0].Width / 2-8;
            this.Y = y - bmpArray[0].Height / 2;
            isDestoryex = false;
        }
        protected override Image GetImage()
        {
            return bmpArray[index];
        }

        public override void Update()
        {
            PlayCount++;
            index = (PlayCount - 1) / PlaySpeed;//)//%3;
            if (index > 2)
            {
                isDestoryex = true;
            }
            base.Update();
        }


    }
}
