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
        public bool Turn { get; set; }
        ServerManager srv;
        StructMap map;
        Point tempLoc;
        Paint draw;
        public IForm form { get; set; }
        public ActGame(GameForm gameForm, int wh, int ht, StructMap sMap, ServerManager srv)
        {
            form = gameForm;
            draw = new Paint(wh, ht);
            map = sMap;
            this.srv = srv;
            srv.InitializeActGame(this);
        }
        public Bitmap GetGrid()
        {
            return draw.DrawGrid();
        }
        public Bitmap GetImage()
        {
            return draw.TempBitmap;
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
                        map.AddPoint(tempLoc);
                        draw.FixImage();
                        srv.SendMessage(new ShootMessage(tempLoc.X, tempLoc.Y));
                        Turn = false;
                    }
                }
            }
        }
        public void ReDraw(GameField you, GameField enemy)
        {
            if (Turn)
            {
                draw.DrawMapYou(you);
                form.InvalidateYou();
            }
            else
            {
                draw.DrawMapEnemy(enemy, you.field);
                form.InvalidateEnemy();
            }
        }
    }
}
