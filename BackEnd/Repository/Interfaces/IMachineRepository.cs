using Repository.Data.Models;

namespace Repository.Interfaces
{
    public interface IMachineRepository
    {
        Task<List<Machine>> GetMachines();
        Task<Machine> GetMachineById(int id);
        Task UpdateMachine(int id, Machine machine);
        Task<Machine> AddMachine(Machine machine);
        Task DeleteMachine(int id);
        Task<bool> IsMachineExistsById(int id);
        Task<int> GetProductionMachineById(int id);
    }
}