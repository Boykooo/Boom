using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Game
{
    class ActGame : IMauseAction
    {
        public ActGame()
        {

        }
        public void MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
        }
        public void MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
        }
        public void MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
        }

        public IForm form { get; set; }
    }
}
