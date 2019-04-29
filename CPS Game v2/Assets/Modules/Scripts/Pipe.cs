using Assets.Interfaces.Modules;
using Assets.Modules.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Modules.Scripts
{
    public class Pipe : AttackableModule
    {
        /// <summary>
        /// Build the menu for displaying attack information
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override MenuToDisplay GetAttackMenu(MenuBuilder builder)
        {
            builder.AddOption("Meow");

            return builder.Build();
        }

        public override void OnStart()
        {
            Console.WriteLine("Hi");
        }

    }
}
