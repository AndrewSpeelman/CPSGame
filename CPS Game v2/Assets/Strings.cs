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
        public const String Hacked = "-";


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
                public const String Sensor = "Attack Sensor";
                public const String Purity = "Attack Purity";
            }

            public static class Pipe
            {
                public const String Sensor = "Attack Sensor";
            }

            public static class Pump
            {
                public const String Flow = "Attack Flow";
                public const String Sensor = "Attack Sensor";
            }

            public static class Reservoir
            {
                public const String Drain = "Attack Drain";
                public const String Sensor = "Attack Sensor";
            }
        }

        public static class FixStrings
        {
            public static class Filter
            {
                public const String Sensor = "Fix Sensor";
                public const String Purity = "Fix Purity";
                
            }

            public static class Pipe
            {
                public const String Sensor = "Fix Sensor";
            }

            public static class Pump
            {
                public const String Flow = "Fix Flow";
                public const String Sensor = "Fix Sensor";
            }

            public static class Reservoir
            {
                public const String Drain = "Fix Drain";
                public const String Sensor = "Fix Sensor";
            }
        }
    }
}
