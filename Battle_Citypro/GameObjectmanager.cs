using Battle_Citypro.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battle_Citypro
{
    internal class GameObjectmanager
    {
        protected static List<NotMoverthing> walllist = new List<NotMoverthing>();
        public static List<NotMoverthing> steellist =new List<NotMoverthing>();
        protected static NotMoverthing boss;
        private static MyTank mytank;

        private static List<Explosion> explist = new List<Explosion>(); 
        private static  List<EnemyTank> tanklist= new List<EnemyTank>(); 
        private static List<Bullet> bulletlist = new List<Bullet>();

        private static int enemyBornSpeed = 60;//fps60
        private static int enemyuBornCount = 60;

        public static bool IsDestorysteel = false;
       
        private static Point[] points = new Point[3];

        //public static bool AttackWall = false;
        

        public static void Start()
        {
           
            points[0].X = 0; points[0].Y = 0;
            points[1].X = 6 * 32; points[1].Y = 0;  
            points[2].X = 12 * 32; points[2].Y = 0;
        }
        public static void Update()
        {
            foreach (NotMoverthing nm in walllist)
            {
                nm.Update();
            }
            foreach (NotMoverthing nm in steellist)
            {
                nm.Update();
            }
            foreach(EnemyTank tank in tanklist)
            {
                tank.Update();
            }
          //  foreach (Bullet bullet in bulletlist)
           // {
           //     bullet.Update();
          //  }
            for(int i=0;i < explist.Count;i++)
            {
                explist[i].Update();
            }
                for (int i = 0; i < bulletlist.Count; i++)
                {
                    bulletlist[i].Update();
                }
            CheckDestoryExplosion();
            boss.Update();

            mytank.Update();

            EnemyBorn();
        }

        public static void CheckDestoryExplosion()
        {
            List<Explosion> needToDestory = new List<Explosion>();
            foreach (Explosion exp in explist)
            {
                if (exp.isDestoryex == true)
                {
                    needToDestory.Add(exp);
                }
            }
            //explist.Remove(exp);
            foreach(Explosion exp in needToDestory)
            {
                explist.Remove(exp);
            }
        }
        public static void CreateExplosion(int x, int y)
        {
            Explosion exp= new Explosion(x, y);
            explist.Add(exp);

        }
        public static void CreateBullet(int x,int y,Tag tag,Direction dir)
        {
            Bullet bullet = new Bullet(x, y, 5, dir, tag);
            bulletlist.Add(bullet);
        }

        public static void DestoryBullet(Bullet bullet)
        {
            bulletlist.Remove(bullet);
        }
        
        public static void DestoryWall(NotMoverthing wall)
        {
            if(walllist.Contains(wall))
                walllist.Remove(wall);
            ////////////////////////////////
        }
        public static void Destroytank(EnemyTank tank)
        {
            tanklist.Remove(tank);
        }
        private static void EnemyBorn()
        {
            enemyuBornCount++;
            if (enemyuBornCount < enemyBornSpeed) return;

            Random rd= new Random();
            int index=rd.Next(0,3);//0~2
            Point position = points[index];

            int enemyType = rd.Next(1,5);

            switch (enemyType)
            {
                case 1:
                    CreateEnemyTank1(position.X, position.Y); break;
                case 2:
                    CreateEnemyTank2(position.X, position.Y);break;
                case 3:
                    CreateEnemyTank3(position.X, position.Y); break;
                case 4:
                    CreateEnemyTank4(position.X, position.Y); break;

            }
            enemyuBornCount = 0;
        }

        private static void CreateEnemyTank1(int x,int y )
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.grayenemy1_down,Resources.grayenemy1_up,Resources.grayenemy1_right,Resources.grayenemy1_left);
            tanklist.Add(tank);
        }
        private static void CreateEnemyTank2(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 1, Resources.greenenemy_down, Resources.greenenemy_up, Resources.greenenemy_right, Resources.greenenemy_left);
            tanklist.Add(tank);
        }
        private static void CreateEnemyTank3(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 4, Resources.grayenemy2_down, Resources.grayenemy2_up, Resources.grayenemy2_right, Resources.grayenemy2_left);
            tanklist.Add(tank);//high speed tank
        }
        private static void CreateEnemyTank4(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 1, Resources.yellowenemy_down, Resources.yellowenemy_up, Resources.yellowenemy_right, Resources.yellowenemy_left);
            tanklist.Add(tank);
        }
        public static NotMoverthing IsCollidedWall(Rectangle rt)
        {
            foreach(NotMoverthing wall in walllist)
            {
                if (wall.GetRectangle().IntersectsWith(rt))
                {
                    return wall;
                }
              
            }
            foreach (NotMoverthing wall in steellist)
            {
                if (wall.GetRectangle().IntersectsWith(rt))
                {
                    return wall;
                }

            }
                          
            return null;
        }
        public static bool IscollidedBoss(Rectangle rt)
        {
            return boss.GetRectangle().IntersectsWith(rt);
        }
      /*  public static void DrawMap()
        {
            foreach(NotMoverthing nm in walllist)
            {
                nm.DrawSelf();
            }
            foreach(NotMoverthing nm in steellist)
            {
                nm.DrawSelf();
            }
            boss.DrawSelf();
        }
        public static void DrawMytank()
        {
            mytank.DrawSelf();  
        }*/
      public static EnemyTank IsCollidedEnemyTank(Rectangle rt)
        {
            foreach(EnemyTank tank in tanklist)
            {
                if (tank.GetRectangle().IntersectsWith(rt))
                {
                    return tank;
                }
            }
            return null;
        }
        
        public static MyTank IsCollidedMyTank(Rectangle rt)
        {
           if(mytank.GetRectangle().IntersectsWith(rt))return mytank;
           else return null;
        }
        public static void CreateMap()
        {
            CreateWall(1, 1, 5-1,Resources.blocknew,walllist);
            CreateWall(3, 1, 5-1, Resources.blocknew, walllist);
            CreateWall(5, 1, 4-1, Resources.blocknew, walllist);
            CreateWall(7, 1, 4 - 1, Resources.blocknew, walllist);
            CreateWall(6,4-1,1,Resources.iron_wall,steellist);
            CreateWall(9, 1, 5-1, Resources.blocknew, walllist);
            CreateWall(11, 1, 5-1, Resources.blocknew, walllist);

            CreateWall(2, 7-1, 1, Resources.blocknew, walllist);
            CreateWall(3, 7-1, 1, Resources.blocknew, walllist);

            CreateWall(5, 6-1, 1, Resources.blocknew, walllist);
            CreateWall(7, 6-1, 1, Resources.blocknew, walllist);

            CreateWall(9, 7-1, 1, Resources.blocknew, walllist);
            CreateWall(10, 7 - 1, 1, Resources.blocknew, walllist);

            CreateWall(1, 9-1, 5 - 1, Resources.blocknew, walllist);
            CreateWall(3, 9-1, 5-1, Resources.blocknew, walllist);

            CreateWall(5, 8 - 1, 4 - 1, Resources.blocknew, walllist);
            CreateWall(7, 7, 3, Resources.blocknew, walllist);

            CreateWall(9, 8, 4, Resources.blocknew, walllist);
            CreateWall(11, 8, 4, Resources.blocknew, walllist);

            CreateBoss(6,12, Resources.Boss);
        }
        public static void CreateMytank()
        {
            int x = 4 * 32;
            int y =12 * 32;

           mytank = new MyTank(x, y, 2);
        }
        
        private static void CreateWall(int x,int y,int count,Image img,List<NotMoverthing> walllist)
        {
            //List<NotMoverthing> walllist=new List<NotMoverthing>();
            int xPosition = x * 32;
            int yPosition = y * 32;
            for(int i = yPosition; i < yPosition + count * 32; i += 16)
            {
                NotMoverthing wall1 =new NotMoverthing(xPosition,i,img);
                NotMoverthing wall2 = new NotMoverthing(xPosition+16, i, img);
                walllist.Add(wall1);
                walllist.Add(wall2);
            }
        }
        private static void CreateBoss(int x, int y, Image img)
        {
            int xPosition = x * 32;
            int yPosition = y * 32;
            boss = new NotMoverthing(xPosition, yPosition, img);
        }

        public static void KeyDown(KeyEventArgs atgs)
        {
            mytank.KeyDown(atgs);   
        }
        public static void KeyUp(KeyEventArgs atgs)
        {
            mytank.KeyUp(atgs);
        }
    }

    
}
