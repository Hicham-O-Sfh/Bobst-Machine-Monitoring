using System;
using System.Collections.Generic;

namespace Repository.Data.Models
{
    public partial class MachineProduction
    {
        public int MachineProductionId { get; set; }
        public int MachineId { get; set; }
        public int TotalProduction { get; set; }

        public virtual Machine Machine { get; set; } = null!;
    }
}
