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

            IPAddress adress = Dns.GetHostEntry("localhost").AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 8888); //!!!


            ConnectManager cnm = new ConnectManager(endPoint);

            cnm.Start();

            Console.ReadLine();
        }
    }
}
