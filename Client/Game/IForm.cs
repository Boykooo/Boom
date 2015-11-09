using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Game
{
    public interface IForm
    {
        bool Turn { get; set; }
        RadioButton OneShip { get; set; }
        RadioButton TwoShip { get; set; }
        RadioButton ThreeShip { get; set; }
        RadioButton FourShip { get; set; }
        void InvalidateYou();
        void InvalidateEnemy();
    }
}
