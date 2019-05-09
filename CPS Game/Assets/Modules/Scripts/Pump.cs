using Assets.Interfaces.Modules;
using Assets.Modules.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Modules.Scripts
{
    public class Pump : AttackableModule, IPumpWater
    {
        [SerializeField]
        private bool _PumpBroken = true; 
        public bool PumpBroken { get { return _PumpBroken; } protected set { _PumpBroken = value; } }

        private bool SensorBroken; 

        public Pump()
        {
            this.SensorBroken = false;
            this.PumpBroken = false;
            this.AttackToFix = null;
        }


        public void Off()
        {
            this.PumpBroken = true;
        }

        public void On()
        {
            this.PumpBroken = false;
        }


        /// <summary>
        /// Pump the Water
        /// </summary>
        /// <param name="inflow"></param>
        /// <returns></returns>
        public override WaterObject OnFlow(WaterObject inflow)
        {
            if (this.PumpBroken)
                return null; // Do not continue the flow of water if pumping is off

            return base.OnFlow(inflow);
        }

        /// <summary>
        /// Attack the object
        /// </summary>
        /// <param name="AttackMenuOption"></param>
        /// <returns></returns>
        public override bool Attack(string AttackMenuOption)
        {
            base.Attack(AttackMenuOption); // Mark as attacked 

            switch (AttackMenuOption)
            {
                case Strings.AttackStrings.Pump.Flow:
                    this.PumpBroken = true; 
                    break;

                case Strings.AttackStrings.Pump.Sensor:
                    this.SensorBroken = true;
                    break;
            }

            return true;
        }

        /// <summary>
        /// Fixes problems if broken
        /// </summary>
        /// <returns></returns>
        public override bool Fix()
        {
            switch (this.AttackToFix)
            {
                case Strings.FixStrings.Pump.Flow:
                    if(this.PumpBroken)
                    {
                        this.PumpBroken = false;
                        return base.Fix();
                    }
                    break;
                case Strings.FixStrings.Pump.Sensor:
                    if(this.SensorBroken) 
                    {
                        this.SensorBroken = false;
                        return base.Fix();
                    }
                    break;
            }
            return false;
        }

        /// <summary>
        /// Get information about the pump
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
                builder.AddStringItem(Strings.IsPumping, Strings.Hacked);
            }
            else
            {
                builder.AddBoolItem(Strings.HasFlow, this.HasFlow);
                builder.AddBoolItem(Strings.IsPurityAsExpected, this.IsPurityAsExpected);
                builder.AddBoolItem(Strings.IsPumping, !this.PumpBroken);
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
            builder = base.GetAttackMenu(builder).GetBuilder(); 

            builder.AddOption(Strings.AttackStrings.Pump.Flow);
            builder.AddOption(Strings.AttackStrings.Pump.Sensor);
            return builder.Build();
        }

        /// <summary>
        /// Build the menu for displaying fixing
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override MenuToDisplay GetFixMenu(MenuBuilder builder)
        {
            builder = base.GetFixMenu(builder).GetBuilder(); 
            
            builder.AddOption(Strings.FixStrings.Pump.Flow);
            builder.AddOption(Strings.FixStrings.Pump.Sensor);
            return builder.Build();
        }
    }
}
