using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Interfaces.Modules
{
    public interface IDetectPurity
    {
        bool ExpectedPurity1 { get; set; }
        bool ExpectedPurity2 { get; set; }
        bool ExpectedPurity3 { get; set; }

        bool IsPurityAsExpected();
    }
}
