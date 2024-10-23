using Battle_Citypro.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_Citypro
{
    internal class EnemyTank:Moverthing
    {
        public int AttackSpeed = 80;
        private int attackCount = 0;
        public int MoveSpeed = 90;
        public int MoveCount= 0;

        private Random r = new Random();
        public EnemyTank(int x, int y, int speed,Bitmap bmpDown,Bitmap bmpUp,Bitmap bmpRight,Bitmap bmpLeft)
        {   
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            this.Dir = Direction.Down;
            BitmapDown = bmpDown;
            BitmapUp = bmpUp;
            BitmapLeft = bmpLeft;
            BitmapRight = bmpRight;

        }
        public override void Update()
        {
            Movercheck();
            Move();
            AttackCheck();

            base.Update();
        }
        private void Movercheck()
        {
            //chao chu form
            MoveCount++;
            if (MoveCount > MoveSpeed)
            {
                MoveCount = 0;
                ChangeDirection();return;
            }
            if (Dir == Direction.Up)
            {
                if (Y - Speed < 0)
                {
                    ChangeDirection(); return;
                }

            }
            else if (Dir == Direction.Down)
            {
                if (Y + Speed > 480 - 32 * 2 - 32)
                {
                    ChangeDirection(); return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    ChangeDirection(); return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Speed > 412 - 32 + 8)
                {
                    ChangeDirection(); return;
                }
            }

            //wu li peng zhuang

            Rectangle rect = GetRectangle();
            switch (Dir)
            {
                case Direction.Up:
                    rect.Y -= Speed;
                    break;
                case Direction.Down:
                    rect.Y += Speed;
                    break;
                case Direction.Left:
                    rect.X -= Speed;
                    break;
                case Direction.Right:
                    rect.X += Speed;
                    break;

            }
            if (GameObjectmanager.IsCollidedWall(rect) != null)
            {
                ChangeDirection(); return;
            }
            if (GameObjectmanager.IscollidedBoss(rect))
            {
                ChangeDirection(); return;
            }
            
        }

        private void ChangeDirection()
        {
            //Random r= new Random(); 
            while (true)
            {
                Direction dir = (Direction)r.Next(0, 4);
                if (dir!=Dir)
                {
                    Dir = dir;
                    break;
                }
            }
            Movercheck();
           
        }
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

        private void AttackCheck()
        {
            attackCount++;
            if(attackCount <AttackSpeed) 
            {
                return;
            }

            Attack();
            attackCount = 0;
           
        }
        private void Attack()
        {
            int x = this.X;
            int y = this.Y;

            switch (Dir)
            {
                case Direction.Up:
                    x = x + Width / 2 - 4;
                    y -= 8;
                    break;
                case Direction.Down:
                    x = x + Width / 2 - 4;
                    y += Height;
                    break;
                case Direction.Left:
                    y = y + Height / 2 - 4;
                    x -= 8;
                    break;
                case Direction.Right:
                    x += Width;
                    y = y + Height / 2 - 4;
                    break;
            }

            GameObjectmanager.CreateBullet(x, y, Tag.EnemyTank, Dir);

        }


    }
}
