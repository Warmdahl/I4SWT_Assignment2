using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal.Execution;


namespace Ladeskab.Moduls
{
    public class RFIDReadEventArgs : EventArgs
    {
        public int ID { get; set; }
    }
    public interface IRFidReader
    {
        event EventHandler<RFIDReadEventArgs> RFIDReadEvent;
    }


}
