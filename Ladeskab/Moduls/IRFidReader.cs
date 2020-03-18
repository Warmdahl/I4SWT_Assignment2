using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Moduls;

namespace Ladeskab
{
    public interface IRFidReader
    {
        private event EventHandler<RFidReader.RFidChangedEventArgs> RFidChangedEvent;

        void SetID(int newID);
    }
}
