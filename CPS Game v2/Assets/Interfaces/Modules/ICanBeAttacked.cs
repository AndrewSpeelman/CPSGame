using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Interfaces.Modules
{
    public interface ICanBeAttacked
    {
        bool Attack();
        bool Fix();
    }
}
