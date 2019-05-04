using Assets.Interfaces.Modules;
using Assets.Modules.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Modules.Scripts
{
    public class Reservoir : AttackableModule, IHaveCapacity
    {
        public bool IsStartingReservoir = false; 
        private bool FlowBroken;
        private bool SensorBroken;
        private String AttackToFix;
        [SerializeField]
        [Range(1, 3)]
        private int _MaxCapacity; 
        public int MaxCapacity { get { return _MaxCapacity; } set { _MaxCapacity = value; } }

        public int CurrentCapacity { get { return Water.Count; } }

        public bool IsFull { get { return CurrentCapacity == _MaxCapacity; } }
        public bool IsEmpty { get { return CurrentCapacity == 0; } }

        public new Queue<WaterObject> Water = new Queue<WaterObject>();

        public Reservoir()
        {
            if (this.IsStartingReservoir)
            {
                // Fill the reservoir
                for (int i = 0; i <= _MaxCapacity; i++)
                    Water.Enqueue(new WaterObject());
            }
            this.FlowBroken = false;
            this.SensorBroken = false;
            this.AttackToFix = null;
        }


        public void OnOverfill()
        {

        }

        public void OnEmpty()
        {

        }

        // Get water from reservoir
        public override WaterObject getWater()
        {
            if (IsEmpty)
                return null;

            return Water.Dequeue();
        }

        // Fill the reservoir
        public override WaterObject OnFlow(WaterObject inflow)
        {
            Water.Enqueue(inflow.Copy());

            return Water.Dequeue();
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
                case Strings.AttackStrings.Reservoir.Flow:
                    this.FlowBroken = true; 
                    break;

                case Strings.AttackStrings.Reservoir.Sensor:
                    this.SensorBroken = true;
                    break;
            }

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
            switch (this.AttackToFix)
            {
                case Strings.FixStrings.Reservoir.Flow:
                    if(this.FlowBroken)
                    {
                        this.FlowBroken = false;
                        return base.Fix();
                    }
                    break;
                case Strings.FixStrings.Reservoir.Sensor:
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
        /// Get information about the filter
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override MenuToDisplay GetInformation(MenuBuilder builder)
        {
            builder = base.GetInformation(builder).GetBuilder(); 

            builder.AddStringItem(Strings.Capacity, String.Format("{0}/{1}", CurrentCapacity, MaxCapacity));

            return builder.Build();
        }

        /// <summary>
        /// Build the menu for displaying attacking
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override MenuToDisplay GetAttackMenu(MenuBuilder builder)
        {
            builder.AddOption(Strings.AttackStrings.Reservoir.Flow);
            builder.AddOption(Strings.AttackStrings.Reservoir.Sensor);
            return builder.Build();
        }

        /// <summary>
        /// Build the menu for displaying fixing
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override MenuToDisplay GetFixMenu(MenuBuilder builder)
        {
            builder.AddOption(Strings.FixStrings.Reservoir.Flow);
            builder.AddOption(Strings.FixStrings.Reservoir.Sensor);
            return builder.Build();
        }
    }
}
