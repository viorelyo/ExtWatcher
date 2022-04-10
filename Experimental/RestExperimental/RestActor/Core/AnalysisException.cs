using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestActor.Core
{
    public class AnalysisException : Exception
    {
        public AnalysisException()
        {
        }

        public AnalysisException(string message)
            : base(message)
        {
        }

        public AnalysisException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
