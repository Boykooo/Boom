using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class Check
    {
        public static bool CheckNickName(string nick)
        {
            return nick != "" || nick.Length < 10;
        }
    }
