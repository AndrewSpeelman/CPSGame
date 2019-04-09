using Assets.Interfaces.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Modules.Scripts
{
    public class Reservoir : Module, IHaveCapacity
    {
        [SerializeField]
        [Range(1, 3)]
        private int _MaxCapacity; 
        public int MaxCapacity { get { return _MaxCapacity; } set { _MaxCapacity = value; } }

        [SerializeField]
        [Range(0, 3)]
        private int _CurrentCapacity; 
        public int CurrentCapacity { get { return _CurrentCapacity; } set { _CurrentCapacity = value; } }

        public bool IsFull { get { return _CurrentCapacity == _MaxCapacity; } }
        public bool IsEmpty { get { return _CurrentCapacity == 0; } }

        public void OnOverfill()
        {

        }

        public void OnEmpty()
        {

        }
    }
}
