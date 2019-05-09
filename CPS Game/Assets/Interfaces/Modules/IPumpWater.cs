using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Interfaces.Modules
{
    public interface IPumpWater
    {
        bool PumpBroken { get; }

        void Off();
        void On(); 
    }
}
