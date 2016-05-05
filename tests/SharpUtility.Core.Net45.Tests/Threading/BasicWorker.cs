using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpUtility.Threading;

namespace SharpUtility.Core.Tests.Threading
{
    public class BasicWorker : BackgroundWorker
    {
        public BasicWorker()
        {
            Action = DoWork;
        }

        private void DoWork()
        {
            for (int i = 0; i < 100; i++)
            {
                ReportProgress((i+1)/100d);
            }
        }
    }
}
