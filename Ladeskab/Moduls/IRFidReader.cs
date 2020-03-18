using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Moduls
{
    public interface IRFidReader
    {
        private event EventHandler<RFidChangedEventArgs> RFidChangedEvent;

        void SetID(int newID);
    }
}
