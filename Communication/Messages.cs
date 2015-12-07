using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    [Serializable]
    public abstract class Messages
    {
        public int Id { get; set; }
    }

    [Serializable]
    public class RegistrationMessage : Messages
    {
        public RegistrationMessage(string nick)
        {
            this.nick = nick;
        }

        public string nick;

    }
    [Serializable]
    public class RegistrationResultMessage : Messages
    {
        public RegistrationResultMessage(int id)
        {
            
        }

        public bool ok;
    }

    [Serializable]
    public class CreateField : Messages
    {
        public CreateField(GameField field)
        {
            this.field = field;
        }
        public GameField field;
    }
    [Serializable]
    public class SearchMessage : Messages
    {
        public SearchMessage(GameField field)
        {
            this.field = field;
        }

        public GameField field;
    }
    [Serializable]
    public class StartGameMessage : Messages
    {
        public StartGameMessage(GameField you, GameField enemy, bool turn)
        {
            this.you = you;
            this.enemy = enemy;
            this.turn = turn;
        }
        public GameField you;
        public GameField enemy;
        public bool turn;
    }

    [Serializable]
    public class ShootMessage : Messages
    {
        public ShootMessage(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int x;
        public int y;
    }
    [Serializable]
    public class FieldStateMessage : Messages
    {
        public FieldStateMessage(GameField you, GameField enemy, bool turn)
        {
            this.you = you;
            this.enemy = enemy;
            this.turn = turn;
        }

        public GameField you;
        public GameField enemy;
        public bool turn;
    }
    [Serializable]
    public class EndOfGameMessage : Messages
    {
        public EndOfGameMessage(bool win)
        {
            this.win = win;
        }

        public bool win;
    }
    [Serializable]
    public enum CellType
    {
        None, Point
    }
    [Serializable]
    public enum DeckType
    {
        Live, Hurt, Dead
    }
    [Serializable]
    public class Deck
    {
        public Point point;
        public DeckType type;
        public Deck()
        { 
        }
        public Deck(Point p)
        {
            point = p;
            type = DeckType.Live;
        }

    }
    [Serializable]
    public class Ship
    {
        public int count;
        public List<Deck> palub;
        public bool dead;
        public bool Shoot(int x, int y)
        {
            var tmp = palub.FirstOrDefault(deck => deck.point.X == x && deck.point.Y == y && deck.type == DeckType.Live);

            if (tmp != null)
            {
                tmp.type = DeckType.Hurt;
                Test();
                return true;
            }

            return false;
        }
        public Ship GetForEnemy()
        {
            Ship res = (Ship)MemberwiseClone();
            res.palub = palub.Where(x => x.type != DeckType.Live).ToList();

            return res;
        }
        void Test()
        {
            bool ok = true;
            for (int i = 0; i < palub.Count && ok; i++)
            {
                ok = palub[i].type == DeckType.Hurt;
            }

            if (ok)
            {
                for (int i = 0; i < palub.Count; i++)
                {
                    palub[i].type = DeckType.Dead;
                }
                dead = true;
            }
        }
        public Ship()
        {

        }
        public Ship(int count)
        {
            this.count = count;
            palub = new List<Deck>();
        }
    }
    [Serializable]
    public class GameField
    {
        public CellType[,] field;

        public List<Ship> ships;
        public Point lastShoot;

        public bool Shoot(int x, int y)
        {
            if (field[x, y] == CellType.None)
            {
                field[x, y] = CellType.Point;

                Ship tmpShip = ships.Find(ship => ship.Shoot(x, y));
                if (tmpShip == null)
                {
                    return false;
                }
                else
                {
                    if (tmpShip.dead)
                    {
                        Point okr = new Point();
                        foreach (var palup in tmpShip.palub)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                for (int k = -1; k <= 1; k++)
                                {
                                    okr.X = palup.point.X + i;
                                    okr.Y = palup.point.Y + k;

                                    if (okr.X >= 0 && okr.Y >= 0
                                        && okr.X <= 9 && okr.Y <= 9)
                                    {
                                        field[okr.X, okr.Y] = CellType.Point;
                                    }
                                }
                            }
                        }
                    }

                    return true;
                }
            }

            return true;
        }

        public bool IsGameOver()
        {
            bool ok = true;

            for (int i = 0; i < ships.Count && ok; i++)
            {
                ok = ships[i].dead;
            }

            return ok;
        }

        public GameField GetForEnemy()
        {
            GameField res = new GameField();
            res.ships = this.ships.Select(x => x.GetForEnemy()).ToList();
            res.field = this.field;

            return res;
        }
        public GameField()
        {
            ships = new List<Ship>();
            field = new CellType[10, 10];
        }
        public GameField(List<Ship> ships)
        {
            this.ships = ships;
            field = new CellType[10, 10];
        }
    }
}
