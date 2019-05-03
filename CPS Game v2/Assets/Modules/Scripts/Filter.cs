﻿using Assets.Interfaces.Modules;
using Assets.Modules.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Modules.Scripts
{
    public class Filter : AttackableModule, IControlPurity
    {
        [SerializeField]
        [Range(1, 3)]
        private int _PurityControl = 1; 
        public int PurityIndexToControl { get { return _PurityControl; } set { _PurityControl = value; } }

        private bool PurityBroken;
        private bool FlowBroken;

        public Filter()
        {
            this.PurityBroken = false;
            this.FlowBroken = false;
        }

        /// <summary>
        /// Filters the water, as a filter does
        /// </summary>
        /// <param name="water"></param>
        /// <returns></returns>
        public WaterObject FilterWater(WaterObject water)
        {
            if (water == null)
                return water; 

            if (this.PurityBroken)
                return water; // Do not filter it if purity is broken

            if (this.PurityIndexToControl < 1)
                this.PurityIndexToControl = 1;

            if (this.PurityIndexToControl > 3)
                this.PurityIndexToControl = 3;

            water.purity[this.PurityIndexToControl - 1] = true;
            return water;
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
                case Strings.AttackStrings.Filter.Purity:
                    this.PurityBroken = true; 
                    break;

                case Strings.AttackStrings.Filter.Flow:
                    this.FlowBroken = true;
                    this.Water = null;
                    break;
            }

            return true;
        }

        /// <summary>
        /// Fixes problems if broken
        /// </summary>
        /// <returns></returns>
        public override bool Fix(string FixMenuOption)
        {
            switch (FixMenuOption)
            {
                case Strings.FixStrings.Filter.Purity:
                    if(this.PurityBroken)
                    {
                        this.PurityBroken = false;
                        return base.Fix();
                    }
                    break;
                case Strings.FixStrings.Filter.Flow:
                    if(this.FlowBroken) 
                    {
                        this.FlowBroken = false;
                        return base.Fix();
                    }
                    break;
            }
            return false;
        }



        /// <summary>
        /// Apply filtration during flow
        /// </summary>
        /// <param name="inflow"></param>
        /// <returns></returns>
        public override WaterObject OnFlow(WaterObject inflow)
        {
            if (this.FlowBroken)
                return null; // Flow is broken

            var water = base.OnFlow(inflow); // Returns water that was inside the filter

            return FilterWater(water); // Filter it for the next module
        }

        /// <summary>
        /// Get information about the filter
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override MenuToDisplay GetInformation(MenuBuilder builder)
        {
            builder.AddStringItem(Strings.PurityControl, this.PurityIndexToControl.ToString());
            builder.AddBoolItem(Strings.HasFlow, this.HasFlow);
            builder.AddBoolItem(Strings.IsPurityAsExpected, this.IsPurityAsExpected);

            return builder.Build();
        }


        /// <summary>
        /// Build the menu for displaying attacking
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override MenuToDisplay GetAttackMenu(MenuBuilder builder)
        {
            builder.AddOption(Strings.AttackStrings.Filter.Purity);
            builder.AddOption(Strings.AttackStrings.Filter.Flow);
            return builder.Build();
        }

                /// <summary>
        /// Build the menu for displaying fixing
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override MenuToDisplay GetFixMenu(MenuBuilder builder)
        {
            builder.AddOption(Strings.FixStrings.Filter.Purity);
            builder.AddOption(Strings.FixStrings.Filter.Flow);
            return builder.Build();
        }
    }
}
