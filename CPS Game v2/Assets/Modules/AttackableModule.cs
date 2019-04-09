using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Modules
{
    public class AttackableModule : Module
    {
        [SerializeField]
        private bool _IsAttacked = false;
        public bool IsAttacked { get { return _IsAttacked; } set { _IsAttacked = value; } }
    }
}
