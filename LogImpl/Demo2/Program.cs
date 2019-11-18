using LogImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo2
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 1000;

            while (i < 2000)
            {
                Logger.WriteToLog(String.Format("hello {0}", i));
                i++;
                Console.WriteLine(i);
            }
        }
    }
}
