using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Game
{
    public enum ShipCount
    {
        One, Two, Three, Four
    }
    public interface IMainGameForm
    {
        ShipCount Ships { get; }
        PictureBox YoursBox { get; set; }
        PictureBox EnemyBox { get; set; }
        string MessageString { get; set; }
        bool ClearButtonState { get; set; }

        void SetNameButton(ShipCount ship, string name);
        void SwitchButton(ShipCount ship, bool state);
       
        event Action<object, MouseEventArgs> yoursBoxClick;
        event Action<object, MouseEventArgs> EnemyBoxMouseClick;
        event Action<object, MouseEventArgs> YoursBoxMouseMove;
        event Action<object, MouseEventArgs> EnemyBoxMouseMove;

        event Action StartGame;
        event Action ClearField;
    }
}
