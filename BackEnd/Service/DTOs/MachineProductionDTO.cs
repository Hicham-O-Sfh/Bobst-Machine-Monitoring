using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public class MachineProductionDTO : DtoBase
    {
        public int MachineProductionId { get; set; }
        public int MachineId { get; set; }
        public int TotalProduction { get; set; }
    }
}