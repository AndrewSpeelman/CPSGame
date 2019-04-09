using Assets.Interfaces.Modules;
using Assets.Modules.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Modules.Scripts
{
    public class Pipe : AttackableModule, ICanBeAttacked
    {
        public bool Attack()
        {
            return true;
        }

        public bool Fix()
        {
            return true;
        }

        /// <summary>
        /// Get information about the pipe
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override MenuToDisplay GetInformation(MenuBuilder builder)
        {
            builder.AddBoolItem(Strings.HasFlow, this.HasFlow);
            builder.AddBoolItem(Strings.IsPurityAsExpected, this.IsPurityAsExpected);

            return builder.Build();
        }
    }
}
