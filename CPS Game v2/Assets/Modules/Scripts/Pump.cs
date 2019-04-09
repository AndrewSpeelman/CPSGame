using Assets.Interfaces.Modules;
using Assets.Modules.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Modules.Scripts
{
    public class Pump: Module, IPumpWater
    {
        [SerializeField]
        private bool _isPumping = true; 
        public bool IsPumping { get { return _isPumping; } protected set { _isPumping = value; } }


        public Pump()
        {
        }


        public void Off()
        {
            this.IsPumping = false;
        }

        public void On()
        {
            this.IsPumping = true;
        }


        /// <summary>
        /// Pump the Water
        /// </summary>
        /// <param name="inflow"></param>
        /// <returns></returns>
        public override WaterObject OnFlow(WaterObject inflow)
        {
            if (!this.IsPumping)
                return null; // Do not continue the flow of water if pumping is off

            return base.OnFlow(inflow);
        }

        /// <summary>
        /// Get information about the pump
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override MenuToDisplay GetInformation(MenuBuilder builder)
        {
            builder.AddBoolItem(Strings.IsPumping, this.IsPumping);
            builder.AddBoolItem(Strings.HasFlow, this.HasFlow);
            builder.AddBoolItem(Strings.IsPurityAsExpected, this.IsPurityAsExpected);

            return builder.Build();
        }
    }
}
