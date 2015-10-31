using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.ServerLogic
{
    public interface ILogger
    {
        void Log(string m);
    }
    public class ConsoleLogger : ILogger
    {
        public void Log(string m)
        {
            Console.WriteLine(m);
        }
    }
}
