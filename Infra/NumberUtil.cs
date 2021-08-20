using System;
using System.Collections.Generic;
using System.Text;

namespace Infra
{
    public static class NumberUtil
    {
        private static readonly Random _random = new Random();

       /// <summary>
       /// use this method only in test
       /// </summary>
       /// <returns></returns>
        public static int GetNextNumber()
        {
            return _random.Next();
        }

      
    }
}
