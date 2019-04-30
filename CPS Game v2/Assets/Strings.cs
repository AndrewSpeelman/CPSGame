using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class Strings
    {
        public const String PurityControl = "Purity Control Index";
        public const String HasFlow = "Has Flow";
        public const String IsPurityAsExpected = "Purity As Expected";
        public const String IsFull = "Is Full";
        public const String IsEmpty = "Is Empty";
        public const String Capacity = "Capacity";
        public const String IsPumping = "Is Pumping";


        public static class BooleanStrings
        {
            public const String Yes = "Yes";
            public const String No = "No";
            public const String Good = "Good";
            public const String Bad = "Bad";
            public const String True = "True";
            public const String False = "False";
        }


        public static class AttackStrings
        {
            public static class Filter
            {
                public const String Flow = "Attack Flow";
                public const String Purity = "Attack Purity";
            }
        }

        public static class FixStrings
        {
            public static class Filter
            {
                public const String FixFlow = "Fix Flow";
                public const String FixPurity = "Fix Purity";
            }
        }
    }
}
