using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Modules.Menu
{
    public class MenuItem<T>
    {
        public T Value { get; set; }
        public String DisplayName { get; set; }


        public MenuItem(String display, T value)
        {
            this.Value = value;
            this.DisplayName = display;
        }
        
    }
}
