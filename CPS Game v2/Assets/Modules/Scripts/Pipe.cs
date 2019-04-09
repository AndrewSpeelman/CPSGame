using Assets.Interfaces.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Modules.Scripts
{
    public class Pipe : Module, ICanBeAttacked
    {
        [SerializeField]
        private bool _IsAttacked = false; 
        public bool IsAttacked { get { return _IsAttacked; } set { _IsAttacked = value; } }


        public bool Attack()
        {
            return true;
        }

        public bool Fix()
        {
            return true;
        }
    }
}
