using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Game
{
    public interface IMauseAction
    {
        void MouseMove(object sender, MouseEventArgs e);
        void MouseClick(object sender, MouseEventArgs e);
        IForm form { get; set; }
    }
}
