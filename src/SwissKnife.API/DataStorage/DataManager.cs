using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeMessenger.DataStorage
{
    public static class DataManager
    {
        public static int GetData()
        {
            var r = new Random();

            return r.Next(1, 100);
        }
    }
}
