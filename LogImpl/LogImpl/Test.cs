using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogImpl
{
    public class Test
    {
        public void Do()
        {
            Thread.Sleep(3000);
            Logger.WriteToLog("worldworld");
        }
    }
}
