using Battle_Citypro.Properties;
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
    internal class MyTank:Moverthing
    {
        public int HP { get; set; }

        public bool IsMoving { get; set; }

        private int orix;
        private int oriy;
        public MyTank(int x,int y,int speed)
        {
            HP = 1;
            orix=x; oriy=y;
            IsMoving = false;
            this.X = x;
            this.Y = y; 
            this.Speed = speed;
            this.Dir = Direction.Up;
            BitmapDown = Resources.mytank_down;
            BitmapUp = Resources.mytank_up;
            BitmapLeft = Resources.mytank_left;
            BitmapRight= Resources.mytank_right;
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
            if(Dir==Direction.Up)
            {
                if (Y - Speed < 0)
                {
                    IsMoving = false;return;
                }
              
            }else if (Dir == Direction.Down)
            {
                if(Y+Speed > 480 - 32 * 2-32) 
                {
                    IsMoving = false;return;
                }
            }else if(Dir == Direction.Left)
            {
                if(X-Speed < 0)
                {
                    IsMoving = false;return;
                }
            }else if (Dir == Direction.Right)
                {
                if(X+Speed > 412 -32+8)
                {
                    IsMoving=false;return;  
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
            if (GameObjectmanager.IsCollidedWall(rect)!=null)
            {   
                IsMoving = false;return;
            }
            if (GameObjectmanager.IscollidedBoss(rect))
            {
                IsMoving = false;return;
            }
        }

        private void Move()
        {
            if(IsMoving==false)return;
            switch (Dir)
            {
                case Direction.Up:
                    Y-=Speed; break;
                 case Direction.Down:
                    Y+=Speed; break;
                 case Direction.Left:
                    X-=Speed; break;
                 case Direction.Right:
                    X+=Speed; break;

            }

        }
        public  void KeyDown(KeyEventArgs atgs)
        {
            switch (atgs.KeyCode)
            {
                case Keys.W:
                    Dir = Direction.Up;
                    IsMoving = true;
                    break;
                case Keys.S:
                    Dir=Direction.Down;
                    IsMoving = true;
                    break;
                case Keys.D:
                    Dir=Direction.Right;
                    IsMoving = true;
                    break;
                case Keys.A:
                    Dir=Direction.Left;
                    IsMoving = true;
                    break;
                case Keys.Space:
                    //shot

                        Attack();
                    
                    
                    
                    break;
            }
        }

        private void Attack()
        {
            int x = this.X; 
            int y = this.Y;

            switch (Dir)
            {
                case Direction.Up:
                    x = x+Width / 2-4;
                    y-=8;
                    break;
                case Direction.Down:
                    x = x+Width / 2-4;
                    y += Height;
                    break;
                case Direction.Left:
                    y = y + Height / 2-4;
                    x -= 8;
                    break;
                case Direction.Right:
                    x += Width;
                    y =y+ Height/2-4;
                    break;
            }

                GameObjectmanager.CreateBullet(x, y, Tag.MyTank, Dir);
            
        }
        public  void KeyUp(KeyEventArgs atgs)
        {
            switch (atgs.KeyCode)
            {
                case Keys.W:

                    IsMoving = false;
                    break;
                case Keys.S:
                    
                    IsMoving = false;
                    break;
                case Keys.D:
                    
                    IsMoving = false;
                    break;
                case Keys.A:
                    
                    IsMoving = false;
                    break;
            }
        }
        public void TakeDamage()
        {
            HP--;
            X = orix;
            Y = oriy;
            if(HP <= 0)
            {
                //X = orix;
               // Y =  orix;
                //HP = 1;
            }
        }

    }
}
