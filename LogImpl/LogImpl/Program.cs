using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogImpl
{
    class Program
    {
        static void Main(string[] args)
        {
            //Thread t = new Thread(new ThreadStart(() =>
            //{
            //    Test test = new Test();
            //    test.Do();
            //}))
            //{
            //    Priority = ThreadPriority.AboveNormal,
            //    IsBackground = true
            //};
            //t.Start();

            //int i = 0;

            //while (i < 1000)
            //{
            //    Logger.WriteToLog(String.Format("hello {0}", i));
            //    i++;
            //}

            //t.Join();

            ConcurrentDictionary<int, string> personColl = new ConcurrentDictionary<int, string>();

            personColl.TryAdd(0, "Dave");
            personColl.TryAdd(1, "Jastinder");

            foreach (var display in personColl)
            {
                Console.WriteLine(display.Key);
                Console.WriteLine(display.Value);
            }
        }
    }
}
