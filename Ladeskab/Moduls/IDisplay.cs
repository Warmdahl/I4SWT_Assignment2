using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Moduls
{
    public interface IDisplay
    {
        void DisplayUserInstructions(string s);
        void DisplayChargingMessage(string s);
    }
}
