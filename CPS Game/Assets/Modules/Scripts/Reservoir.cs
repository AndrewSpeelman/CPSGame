﻿using Assets.Interfaces.Modules;
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
        private bool DrainBroken;
        private bool SensorBroken;

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
            this.DrainBroken = false;
            this.SensorBroken = false;
            this.AttackToFix = null;
        }

        /// <summary>
        /// When everything is starting fill to capacity
        /// </summary>
        public override void OnStart()
        {
            base.OnStart();

            // Fill the reservoir
            for (int i = 0; i < _MaxCapacity; i++)
                Water.Enqueue(new WaterObject());
        }

        public void OnOverfill()
        {

        }

        public void OnEmpty()
        {

        }


        // Fill the reservoir
        public override WaterObject OnFlow(WaterObject inflow)
        {
            if (inflow != null)
            {
                Water.Enqueue(inflow.Copy());
            }

            if (this.IsEmpty || this.DrainBroken)
                return null; 

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
                case Strings.AttackStrings.Reservoir.Drain:
                    this.DrainBroken = true; 
                    break;

                case Strings.AttackStrings.Reservoir.Sensor:
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
                case Strings.FixStrings.Reservoir.Drain:
                    if(this.DrainBroken)
                    {
                        this.DrainBroken = false;
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
            if (this.SensorBroken)
            {
                builder.AddStringItem(Strings.HasFlow, Strings.Hacked);
                builder.AddStringItem(Strings.IsPurityAsExpected, Strings.Hacked);
                builder.AddStringItem(Strings.Capacity, Strings.Hacked);
            }
            else
            {
                builder.AddBoolItem(Strings.HasFlow, this.HasFlow);
                builder.AddBoolItem(Strings.IsPurityAsExpected, this.IsPurityAsExpected);
                builder.AddStringItem(Strings.Capacity, String.Format("{0}/{1}", CurrentCapacity, MaxCapacity));
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

            builder.AddOption(Strings.AttackStrings.Reservoir.Drain);
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
            builder = base.GetFixMenu(builder).GetBuilder(); 
            
            builder.AddOption(Strings.FixStrings.Reservoir.Drain);
            builder.AddOption(Strings.FixStrings.Reservoir.Sensor);
            return builder.Build();
        }
    }
}
