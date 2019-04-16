using Assets.Interfaces.Modules;
using Assets.Modules.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Interfaces
{
    public interface IModule
    {
        WaterObject getWater();

        void Tick();

        MenuToDisplay GetInformation(MenuBuilder builder);
    }
}
