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
        private bool SensorBroken;

        public Pipe()
        {
            this.SensorBroken = false;
        }

        /// <summary>
        /// Attack the object
        /// </summary>
        /// <param name="AttackMenuOption"></param>
        /// <returns></returns>
        public override bool Attack(string AttackMenuOption)
        {
            base.Attack(AttackMenuOption); // Mark as attacked 

            this.SensorBroken = true;

            return true;
        }

        /// <summary>
        /// Fixes problems if broken
        /// </summary>
        /// <returns></returns>
        public override bool Fix(string FixMenuOption)
        {
            this.SensorBroken = false;
            if(this.SensorBroken)
            {
                this.SensorBroken = false;
                return base.Fix();
            }
            return false;
        }

        /// <summary>
        /// Build the menu for displaying sensor information
        /// Dependent on if sensor is attacked
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override MenuToDisplay GetInformation(MenuBuilder builder)
        {
            if(this.SensorBroken)
            {
                builder.AddStringItem(Strings.HasFlow, Strings.BooleanStrings.Bad);
            }
            else
            {
                builder.AddBoolItem(Strings.HasFlow, this.HasFlow);
                builder.AddBoolItem(Strings.IsPurityAsExpected, this.IsPurityAsExpected);
            }
            return builder.Build();
        }

        /// <summary>
        /// Build the menu for displaying attack information
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override MenuToDisplay GetAttackMenu(MenuBuilder builder)
        {
            builder.AddOption(Strings.AttackStrings.Pipe.Sensor);
            return builder.Build();
        }

        /// <summary>
        /// Build the menu for displaying fix options
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override MenuToDisplay GetFixMenu(MenuBuilder builder)
        {
            builder.AddOption(Strings.FixStrings.Pipe.Sensor);
            return builder.Build();
        }
    }
}
