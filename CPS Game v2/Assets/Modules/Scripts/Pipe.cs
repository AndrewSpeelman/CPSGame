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
        private String AttackToFix;

        public Pipe()
        {
            this.SensorBroken = false;
            this.AttackToFix = null;
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
        /// Sets what problem to be fixed
        /// </summary>
        /// <returns></returns>
        public override void SetAttackToFix(string FixMenuOption)
        {
            AttackToFix = FixMenuOption;
        }

        /// <summary>
        /// Fixes problems if broken
        /// </summary>
        /// <returns></returns>
        public override bool Fix()
        {
            if(this.SensorBroken && this.AttackToFix == Strings.FixStrings.Pipe.Sensor)
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
            builder = base.GetInformation(builder).GetBuilder();
            if (this.SensorBroken)
            {
                builder.AddStringItem(Strings.HasFlow, Strings.Hacked);
                builder.AddStringItem(Strings.IsPurityAsExpected, Strings.Hacked);
            }
            else
            {
                builder.AddBoolItem(Strings.HasFlow, this.HasFlow);
                builder.AddBoolItem(Strings.IsPurityAsExpected, this.IsPurityAsExpected);
            }
            return builder.Build();
        }

        /// <summary>
        /// Build the menu for displaying attacking
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override MenuToDisplay GetAttackMenu(MenuBuilder builder)
        {
            builder.AddOption(Strings.AttackStrings.Pipe.Sensor);
            return builder.Build();
        }

        /// <summary>
        /// Build the menu for displaying fixing
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
