using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Moduls
{
    public class RFidReader : IRFidReader
    {
        private int id;

        public event EventHandler<RFIDReadEventArgs> RFIDReadEvent;

        public void SimulateReadRFID(int newID)
        {
            OnRFidChanged(new RFIDReadEventArgs() { ID = newID});
        }

        public virtual void OnRFidChanged(RFIDReadEventArgs e)
        {
            RFIDReadEvent?.Invoke(this, e);
        }
    }
}
