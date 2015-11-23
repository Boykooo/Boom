using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project2;

namespace Client.Game
{
    public class ActGame : IMauseAction
    {
        public bool Turn { get { return form.Turn; }}
        StructMap map;
        Point tempLoc;
        Paint draw;
        public IForm form { get; set; }
        public ActGame(GameForm gameForm, int wh, int ht, StructMap sMap)
        {
            form = gameForm;
            draw = new Paint(wh, ht);
            map = sMap;
        }

        public Bitmap GetGrid()
        {
            return draw.grid;
        }
        public Bitmap GetImageYours()
        {
            return draw.TempBitmapYours;
        }
        public Bitmap GetImageEnemy()
        {
            return draw.TempBitmapEnemy;
        }

        public void MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (Turn)
            {
                var location = new Point(e.Location.X / StructMap.BlockSize, e.Location.Y / StructMap.BlockSize);
                if (location.X < 10 && location.Y < 10)
                {
                    tempLoc = location;
                    draw.Point(location);

                    form.InvalidateEnemy();
                }
            }
        }
        public void MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (Turn)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (map.point[tempLoc.X, tempLoc.Y] == CellType.None)
                    {
                        Program.serverManager.SendMessage(new ShootMessage(tempLoc.X, tempLoc.Y));
                        draw.FixImage();
                        map.AddPoint(tempLoc);
                    }
                }
            }
        }

        public void ReDraw(GameField you, GameField enemy)
        {
                draw.DrawField(you, you.field, true);
                form.InvalidateYou();

                draw.DrawField(enemy, enemy.field, false);
                form.InvalidateEnemy();
        }
    }
}
