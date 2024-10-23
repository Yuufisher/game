using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battle_Citypro
{
    public partial class Form1 : Form
    {
        private Thread t1;
        private static Graphics windowG;
        private static Bitmap tempBmp;
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; ;

            windowG = this.CreateGraphics();
            

            tempBmp = new Bitmap(540+16, 480);
            Graphics bmpG=Graphics.FromImage(tempBmp);
            GameFramework.g=bmpG;

            t1 = new Thread(new ThreadStart(GamemainThread));
            t1.Start();
        }

        private static void GamemainThread()
        {
            //GameFramework
            GameFramework.Start();

            int sleeptime = 1000 / 60;

            while (true)
            {
                GameFramework.g.Clear(Color.Black);

                GameFramework.Update();//fps=60

                windowG.DrawImage(tempBmp, 0, 0);   

                Thread.Sleep(sleeptime);

            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            t1.Abort();//after close form kill thread
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
             GameObjectmanager.KeyDown(e);  
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            GameObjectmanager.KeyUp(e); 
        }

       // private void Form1_Load(object sender, EventArgs e)
    //    {

      //  }
    }
}
