using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Interfaces.Modules
{
    public interface IControlPurity
    {
        int PurityIndexToControl { get; set; }


        WaterObject FilterWater(WaterObject water);
    }
}
