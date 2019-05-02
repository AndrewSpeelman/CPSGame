using Assets.Interfaces.Modules;
using Assets.Modules.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Modules.Scripts
{
    public class Reservoir : Module, IHaveCapacity
    {
        public bool IsStartingReservoir = false; 

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
    }
}
