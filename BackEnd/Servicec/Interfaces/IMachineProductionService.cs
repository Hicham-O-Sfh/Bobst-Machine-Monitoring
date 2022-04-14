using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.DTOs;

namespace Service.Interfaces
{
    public interface IMachineProductionService
    {
        Task<List<MachineProductionDTO>> GetMachinesProductions();
        Task<MachineProductionDTO> GetMachineProductionById(int id);
        Task UpdateMachineProduction(int id, MachineProductionDTO machineProduction);
        Task<MachineProductionDTO> AddMachineProduction(MachineProductionDTO machineProduction);
        Task DeleteMachineProduction(int id);
        Task<bool> IsMachineProductionExistsById(int id);
    }
}