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

            oldEnemyField = enemy;
            oldYoursField = yours;

            form.YoursBox.Image = p.TempBitmapYours;
            form.EnemyBox.Image = p.TempBitmapEnemy;

            this.turn = turn;
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
            var location = new Point(args.Location.X / StructMap.BlockSize, args.Location.Y / StructMap.BlockSize);
            if (location.X < 10 && location.Y < 10)
            {
                tempLoc = location;
                p.Point(location);

                form.EnemyBox.Image = p.TempBitmapEnemy;
            }
        }
    }
}
