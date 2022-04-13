using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Data.DTOs
{
    public class MachineDTO : DtoBase
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}