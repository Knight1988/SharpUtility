using System;

namespace SharpUtility.MEF.Tests
{
    [Serializable]
    public class AutoResetWrapper : MarshalByRefObject
    {
        //public AutoResetEvent AutoResetEvent { get; set; }

        //public AutoResetWrapper()
        //{
        //    AutoResetEvent = new AutoResetEvent(false)
        //}

        public void Set()
        {
            //AutoResetEvent.Set();
        }

        public void WaitOne()
        {
            //AutoResetEvent.WaitOne();
        }
    }
}
