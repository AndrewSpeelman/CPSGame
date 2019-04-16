using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Interfaces.Modules
{
    public interface IHaveCapacity : IHoldWater
    {
        int MaxCapacity { get; }
        int CurrentCapacity { get; }

        bool IsFull { get; }
        bool IsEmpty { get; }

        void OnOverfill(); // When CurrentCapacity > MaxCapacity
        void OnEmpty(); // When CurrentCapacity = 0
    }
}
