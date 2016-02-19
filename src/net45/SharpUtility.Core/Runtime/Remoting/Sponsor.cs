using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;

namespace SharpUtility.Runtime.Remoting
{
    public class Sponsor : MarshalByRefObject
    {
        public override object InitializeLifetimeService()
        {
            return null;
        }

        ~Sponsor()
        {
            RemotingServices.Disconnect(this);
        }
    }
}
