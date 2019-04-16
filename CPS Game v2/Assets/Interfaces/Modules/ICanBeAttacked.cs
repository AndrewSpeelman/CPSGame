using Assets.Modules.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Interfaces.Modules
{
    public interface ICanBeAttacked
    {
        bool IsAttacked { get; set; }
        bool Attack();
        bool Fix();

        //MenuToDisplay GetAttackMenu(MenuBuilder builder);
    }
}
