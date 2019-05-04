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

        public String Title = ""; 

        public MenuBuilder builder; 


        public List<MenuItem<String>> GetAllAsStrings()
        {
            List<MenuItem<String>> list = new List<MenuItem<string>>();

            foreach (var item in this.StringMenuItems)
                list.Add(new MenuItem<string>(item.DisplayName, item.Value));

            foreach (var item in this.IntMenuItems)
                list.Add(new MenuItem<string>(item.DisplayName, item.Value.ToString()));

            foreach (var item in this.FloatMenuItems)
                list.Add(new MenuItem<string>(item.DisplayName, item.Value.ToString()));

            return list; 
        }

        public MenuBuilder GetBuilder()
        {
            return builder;
        }
    }
}
