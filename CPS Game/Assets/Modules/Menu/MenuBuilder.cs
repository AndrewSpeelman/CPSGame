using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Modules.Menu
{
    /// <summary>
    /// This class if for building menus to display things like what can be attacked, and displaying information on the module.
    /// </summary>
    public class MenuBuilder
    {
        private MenuToDisplay menu;


        public MenuBuilder()
        {
            this.menu = new MenuToDisplay();
        }



        public MenuBuilder AddIntItem(String display, int value)
        {
            menu.IntMenuItems.Add(new MenuItem<int>(display, value));
            return this;
        }

        public MenuBuilder AddStringItem(String display, String value)
        {
            menu.StringMenuItems.Add(new MenuItem<string>(display, value));
            return this;
        }

        public MenuBuilder AddFloatItem(String display, float value)
        {
            menu.FloatMenuItems.Add(new MenuItem<float>(display, value));
            return this;
        }

        public MenuBuilder AddBoolItem(String display, bool value, String trueValue = Strings.BooleanStrings.Yes, String falseValue = Strings.BooleanStrings.No)
        {
            menu.StringMenuItems.Add(new MenuItem<string>(display, value ? trueValue : falseValue));
            return this;
        }

        public MenuBuilder AddChoice(String display, String value)
        {
            menu.MenuChoices.Add(new MenuItem<string>(display, value));
            return this;
        }

        public MenuBuilder AddOption(String display)
        {
            this.AddChoice(display, "");
            return this;
        }


        public MenuBuilder SetTitle(String title)
        {
            this.menu.Title = title;
            return this;
        }

        /**
         * Creates the menu from the builder 
         */
        public MenuToDisplay Build()
        {
            this.menu.builder = this; 
            return menu;
        }
    }
}
