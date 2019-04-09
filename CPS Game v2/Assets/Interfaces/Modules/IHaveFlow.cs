using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Interfaces.Modules
{
    public interface IHaveFlow
    {
        bool HasFlow { get; }

        WaterObject OnFlow(WaterObject inFlow); // Happens when water flows through this
    }
}
