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

        public event EventHandler<RFIDReadEventArgs> RFidChangedEvent;

        public void SetID(int newID)
        {
            OnRFidChanged(new RFidChangedEventArgs {ID = newID});
        }

        public virtual void OnRFidChanged(RFidChangedEventArgs e)
        {
            RFidChangedEvent?.Invoke(this, e);
        }

        public class RFidChangedEventArgs : EventArgs
        {
            public int ID { get; set; }
        }

    }
}
