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
    public abstract class Message
    {
        public int Id { get; set; }
    }

    [Serializable]
    public class RegistrationMessage : Message
    {
        public RegistrationMessage(string nick)
        {
            this.nick = nick;
        }

        public string nick;

    }
    [Serializable]
    public class RegistrationResultMessage : Message
    {
        public RegistrationResultMessage(int id)
        {
            
        }

        public bool ok;
    }

    [Serializable]
    public class CreateField : Message
    {
        public CreateField(GameField field)
        {
            this.field = field;
        }

        public GameField field;
    }

    [Serializable]
    public class SearchMessage : Message
    {
        public SearchMessage()
        {

        }
    }
    [Serializable]
    public class StartGameMessage : Message
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
    public class ShootMessage : Message
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
    public class FieldStateMessage : Message
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

    public class EndOfGameMessage : Message
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
    }
    [Serializable]
    public class Ship
    {

        public int count;
        public List<Deck> palub;
        public bool dead;
        public bool Shoot(int x, int y)
        {
            var tmp = palub.First(deck => deck.point.X == x && deck.point.Y == y && deck.type == DeckType.Live);
            if (tmp != null)
            {
                tmp.type = DeckType.Hurt;
                Test();
                return true;
            }

            return false;
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
    }


    [Serializable]
    public class GameField
    {
        public CellType[,] field;

        public List<Ship> ships;

        public bool Shoot(int x, int y)
        {
            if (field[x, y] == CellType.None)
            {
                field[x, y] = CellType.Point;
                return ships.Exists(ship => ship.Shoot(x, y));             
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
    }
}
