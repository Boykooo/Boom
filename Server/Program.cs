using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress adress = host.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(adress, 8888);

            ConnectManager cnm = new ConnectManager(adress, endPoint);

            cnm.Start();

            Console.ReadLine();
        }
    }
}
