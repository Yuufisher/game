using Battle_Citypro.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Battle_Citypro
{
    enum Tag
    {
        MyTank,
        EnemyTank
    }
    internal class Bullet : Moverthing
    {
        public Tag Tag { get; set; }

        public Bullet(int x, int y, int speed,Direction dir,Tag tag)
        {
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            BitmapDown = Resources.bullet_down;
            BitmapUp = Resources.bullet_up;
            BitmapLeft = Resources.bullet_left;
            BitmapRight = Resources.bullet_right;
            this.Dir= dir;
            this.Tag = tag;
        }
        public override void Update()
        {
            Movercheck();
            Move();

            base.Update();
        }
        private void Movercheck()
        {
            //chao chu form
            if (Dir == Direction.Up)
            {
                if (Y +Height/2 +3< 0)
                {
                    GameObjectmanager.DestoryBullet(this); return;
                }

            }
            else if (Dir == Direction.Down)
            {
                if (Y +Height-3  > 480 - 32 * 2 + 32)
                {
                    GameObjectmanager.DestoryBullet(this); return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X +Width/2+3< 0)
                {
                    GameObjectmanager.DestoryBullet(this); return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Width/2-3 > 412 - 32 + 8+8)
                {
                    GameObjectmanager.DestoryBullet(this); return;
                }
            }

            //wu li peng zhuang

            Rectangle rect = GetRectangle();

            rect.X = X + Width / 2 - 3;
            rect.Y=Y+ Height / 2 - 3;
            rect.Width = 3;
            rect.Height = 3;

            NotMoverthing wall = null;
            //peng zhuang wu
            int xExplosion=this.X+Width/2;
            int yExplosion =this.Y+Height/2;
            if ((wall=GameObjectmanager.IsCollidedWall(rect) )!= null) { 
                if (GameObjectmanager.steellist.Contains(GameObjectmanager.IsCollidedWall(rect)))
                {
                    if (GameObjectmanager.IsDestorysteel)
                    {
                        GameObjectmanager.CreateExplosion(xExplosion, yExplosion);
                    }
                    else
                    {
                        GameObjectmanager.CreateExplosion(xExplosion, yExplosion);
                        GameObjectmanager.DestoryBullet(this);
                        return;
                    }
                }
                else
                {
                    GameObjectmanager.DestoryBullet(this);
                    GameObjectmanager.DestoryWall(wall);
                   // GameObjectmanager.AttackWall = true;
                    GameObjectmanager.CreateExplosion(xExplosion, yExplosion);
                   // GameObjectmanager.DestoryExplosion(explosion);



                    return;
                }
           
            }
            if (GameObjectmanager.IscollidedBoss(rect))
            {
               GameObjectmanager.DestoryBullet(this);
                GameObjectmanager.CreateExplosion(xExplosion, yExplosion);
                Thread.Sleep(300);

                GameFramework.ChangeToGameOver();
                
               return;
            }
          if(Tag == Tag.MyTank)
            {
                EnemyTank tank = null;
                if ((tank=GameObjectmanager.IsCollidedEnemyTank(rect)) != null)
                {
                    GameObjectmanager.DestoryBullet(this);
                    GameObjectmanager.CreateExplosion(xExplosion, yExplosion);
                    GameObjectmanager.Destroytank(tank);
                    return;
                }
            }else if(Tag == Tag.EnemyTank)
            {
                MyTank mytank = null;
                if((mytank=GameObjectmanager.IsCollidedMyTank(rect)) != null)
                {
                    GameObjectmanager.DestoryBullet(this);
                    GameObjectmanager.CreateExplosion(xExplosion, yExplosion);

                    mytank.TakeDamage();

                    return;

                }
            }
        }
        //private void ChangeDirection() { }
        private void Move()
        {

            switch (Dir)
            {
                case Direction.Up:
                    Y -= Speed; break;
                case Direction.Down:
                    Y += Speed; break;
                case Direction.Left:
                    X -= Speed; break;
                case Direction.Right:
                    X += Speed; break;

            }

        }
    }
}
