using Project2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Game
{
    class GameController
    {
        IMainGameForm form;
        bool turn;
        Paint p;
        GameField oldYoursField;
        GameField oldEnemyField;

        Point tempLoc; //зачем?

        public GameController()
        {
            
        }
        public void Attach(IMainGameForm form)
        {
            this.form = form;

            form.yoursBoxClick += (x, y) => { };
            form.YoursBoxMouseMove += (x, y) => { };
            form.EnemyBoxMouseClick += EnemyMouseClick;
            form.EnemyBoxMouseMove += EnemyMouseMove;

            p = new Paint(form.YoursBox.Width, form.YoursBox.Height);
        }
        public void Detach()
        {
            form.yoursBoxClick -= (x, y) => { };
            form.YoursBoxMouseMove -= (x, y) => { };
            form.EnemyBoxMouseClick -= EnemyMouseClick;
            form.EnemyBoxMouseMove -= EnemyMouseMove;
        }


        public void NewField(GameField yours, GameField enemy, bool turn)
        {

            p.DrawField(yours, yours.field, true);
            p.DrawField(enemy, enemy.field, false);

            Point pnt = FindLastShoot(yours);
            if (!pnt.IsEmpty)
            {
                p.DrawCell(pnt);
            }

            oldEnemyField = enemy;
            oldYoursField = yours;

            Action<PictureBox, Image> tmp = (x, y) => x.Image = y;

            if (form.EnemyBox.InvokeRequired)
            {                
                form.EnemyBox.Invoke(tmp, form.EnemyBox, p.TempBitmapEnemy);
            }
            else
            {
                form.EnemyBox.Image = p.TempBitmapEnemy;
            }

            if (form.YoursBox.InvokeRequired)
            {               
                form.YoursBox.Invoke(tmp, form.YoursBox, p.TempBitmapYours);
            }
            else
            {
                form.YoursBox.Image = p.TempBitmapYours;
            }

            //form.YoursBox.Image = p.TempBitmapYours;
            //form.EnemyBox.Image = p.TempBitmapEnemy;

            this.turn = turn;
            form.MessageString = turn ? "Ваш ход" : "Ход противника";
        }
        void EnemyMouseClick(object sender, MouseEventArgs args)
        {
            if (turn)
            {
                if (args.Button == MouseButtons.Left)
                {
                    if (oldEnemyField.field[tempLoc.X, tempLoc.Y] == CellType.None)
                    {
                        Program.serverManager.SendMessage(new ShootMessage(tempLoc.X, tempLoc.Y));
                       // p.FixImage(); зачем?
                    }
                }
            }
        }
        void EnemyMouseMove(object sender, MouseEventArgs args)
        {
            if (turn)
            {
                var location = new Point(args.Location.X / StructMap.BlockSize, args.Location.Y / StructMap.BlockSize);
                if (location.X < 10 && location.Y < 10)
                {
                    tempLoc = location;
                    p.Point(location);

                    if (form.EnemyBox.InvokeRequired)
                    {
                        Action<PictureBox, Image> tmp = (x, y) => x.Image = y;
                        form.EnemyBox.Invoke(tmp, form.EnemyBox, p.TempBitmapEnemy);
                    }
                    else
                    {
                        form.EnemyBox.Image = p.TempBitmapEnemy;
                    }
                }
            }
        }

        Point FindLastShoot(GameField newField)
        {
            if(oldYoursField == null)
            {
                return new Point();
            }

            for (int i = 0; i < newField.field.GetLength(0); i++ )
            {
                for (int k = 0; k < newField.field.GetLength(1); k++)
                {
                    if (newField.field[i, k] != oldYoursField.field[i, k])
                    {
                        return new Point(i, k);
                    }
                }
            }

            return new Point();
        }

    }
}
