using Battle_Citypro.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battle_Citypro
{
    enum GameState
    {
        Running,
        GameOver
    }
    internal class GameFramework
    {
        public static Graphics g;
        private static GameState gameSate = GameState.Running;
        public static void Start()
            
        {
            GameObjectmanager.Start();
            GameObjectmanager.CreateMap();
            GameObjectmanager.CreateMytank();
        }
        public static void Update()//huizhi fps :60>?
        {
          //  GameObjectmanager.DrawMap();
           // GameObjectmanager.DrawMytank();
            
            if( gameSate == GameState.Running )
            {
                GameObjectmanager.Update();
            }else if( gameSate == GameState.GameOver)
            {
                GamneOverUpdate();
            }
        }
        private static void GamneOverUpdate()
        {
            int x = 556 / 2 + Resources.gameover.Width / 2 - 32*9;
            int y = 480 /2+Resources.gameover.Height / 2 - 32 * 8;
            g.DrawImage(Resources.gameover, x, y);
        }
        public static void KeyDown(KeyEventArgs atgs)
        {
            
        }
        public static void KeyUp(KeyEventArgs atgs)
        {

        }
        public static void ChangeToGameOver()
        {
            gameSate = GameState.GameOver;
        }
    }
}
