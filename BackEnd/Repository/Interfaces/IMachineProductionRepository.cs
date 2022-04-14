using Repository.Data.Models;

namespace Repository.Interfaces
{
    public interface IMachineProductionRepository
    {
        Task<List<MachineProduction>> GetMachinesProductions();
        Task<MachineProduction> GetMachineProductionById(int id);
        Task UpdateMachineProduction(int id, MachineProduction machineProduction);
        Task<MachineProduction> AddMachineProduction(MachineProduction machineProduction);
        Task DeleteMachineProduction(int id);
        Task<bool> IsMachineProductionExistsById(int id);
    }
}