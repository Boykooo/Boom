﻿using Project2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Game
{
    public class GameController
    {
        private IMainGameForm form;
        private bool turn;
        private Paint p;
        private GameField oldYoursField;
        private GameField oldEnemyField;

        private Point tempLoc; //зачем?
        private bool ok = true;
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

            form.ClearButtonState = false;

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
            ok = false;
            p.DrawField(yours, yours.field, true);
            p.DrawField(enemy, enemy.field, false);

            if (!yours.lastShoot.IsEmpty)
            {
                p.DrawCell(yours.lastShoot);
            }

            oldEnemyField = enemy;
            oldYoursField = yours;

            SetImage(form.YoursBox, p.TempBitmapYours);
            SetImage(form.EnemyBox, p.TempBitmapEnemy);

            //form.YoursBox.Image = p.TempBitmapYours;
            //form.EnemyBox.Image = p.TempBitmapEnemy;
           
            form.MessageString = turn ? "Ваш ход" : "Ход противника";
            this.turn = turn;
            ok = true;

        }
        private void EnemyMouseClick(object sender, MouseEventArgs args)
        {
            if (turn && ok)
            {

                if (args.Button == MouseButtons.Left)
                {
                    if (oldEnemyField.field[tempLoc.X, tempLoc.Y] == CellType.None)
                    {
                        turn = false;

                        Program.serverManager.SendMessage(new ShootMessage(tempLoc.X, tempLoc.Y));                   
                    }
                }

            }
        }
        private void EnemyMouseMove(object sender, MouseEventArgs args)
        {
            if (turn && ok)
            {
               
                var location = new Point(args.Location.X / StructMap.BlockSize, args.Location.Y / StructMap.BlockSize);
                if (location.X < 10 && location.Y < 10)
                {
                    tempLoc = location;
                    p.Point(location);

                    SetImage(form.EnemyBox, p.TempBitmapEnemy);

                }

            }
        }




        private Point FindLastShoot(GameField newField)
        {
            if (oldYoursField == null)
            {
                return new Point();
            }

            for (int i = 0; i < newField.field.GetLength(0); i++)
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
        private void SetImage(PictureBox box, Image img)
        {
            Action<PictureBox, Image> tmp = (x, y) => x.Image = y;

            if (box.InvokeRequired)
            {
                box.Invoke(tmp, box, img);
            }
            else
            {
                box.Image = img;
            }
        }

    }
}
