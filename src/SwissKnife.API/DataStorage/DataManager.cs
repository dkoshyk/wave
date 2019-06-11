using System;

namespace SwissKnife.API.DataStorage
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