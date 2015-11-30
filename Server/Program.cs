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
            //IPHostEntry host = Dns.GetHostEntry("192.168.1.1");
            //IPAddress adress = host.AddressList[0];

            IPAddress adress = Dns.GetHostEntry("localhost").AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 8888); //!!!

            Console.WriteLine(adress.MapToIPv4().ToString());

            ConnectManager cnm = new ConnectManager(adress, endPoint);

            cnm.Start();

            Console.ReadLine();
        }
    }
}
