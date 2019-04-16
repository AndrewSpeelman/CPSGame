using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Modules.Menu
{
    public class MenuToDisplay
    {
        public List<MenuItem<String>> StringMenuItems = new List<MenuItem<string>>();
        //public List<MenuItem<bool>> BoolMenuItems = new List<MenuItem<bool>>();
        public List<MenuItem<int>> IntMenuItems = new List<MenuItem<int>>();
        public List<MenuItem<float>> FloatMenuItems = new List<MenuItem<float>>();
        public List<MenuItem<string>> MenuChoices = new List<MenuItem<string>>();
    }
}
