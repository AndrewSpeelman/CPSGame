using Assets.Interfaces.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Modules.Scripts
{
    public class Filter : Module, IControlPurity
    {
        [SerializeField]
        [Range(1, 3)]
        private int _PurityControl; 
        public int PurityIndexToControl { get { return _PurityControl; } set { _PurityControl = value; } }

        
        public Filter()
        {
        }


        public WaterObject FilterWater(WaterObject water)
        {
            water.purity[this.PurityIndexToControl - 1] = true;
            return water;
        }


        /// <summary>
        /// Apply filtration during flow
        /// </summary>
        /// <param name="inflow"></param>
        /// <returns></returns>
        public override WaterObject OnFlow(WaterObject inflow)
        {
            var water = base.OnFlow(inflow);
            return FilterWater(water);
        }
    }
}
