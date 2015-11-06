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
    public class ActGameForm
    {
        ServerManager srv;
        RegForm reg;
        Paint draw;
        StructMap map;
        public ActGameForm(int wh, int ht)
        {
            draw = new Paint(wh, ht);
            StructMap.BlockSize = 30;
            map = new StructMap();
        }
        public void Registration()
        {
            reg = new RegForm();
            reg.ShowDialog();
            srv = reg.Srv;
        }
        public Bitmap GetGrid()
        {
            return draw.DrawGrid();
        }
        public void AddChanges()
        {
            draw.FixImage();
        }
        public Bitmap GetShipImage(Point location, int size, bool hor)
        {
            return draw.Ship(location, size, hor);
        }
        public void AddShip(bool horizont, int size, Point startLocation)
        {
            map.FixMap(horizont, size, startLocation);
        }
        public bool CheckLocShip(Point location, bool horizont, int size)
        {
            return map.CheckLocation(location, horizont, size);
        }
        public void Clear()
        {
            map.Map = new int[10, 10];
        }
        public void NewGame()
        {
            Program.state = ClientState.Waiting;
            srv.SendMessage(new CreateField(new GameField(map.ships)));
        }
        public Bitmap GetFullMap(List<Ship> ships)
        {
            return draw.GetFullMap(ships);
        }
        public void InitGameForm(GameForm g)
        {
            srv.InitializeGameForm(g);
        }
    }
}
