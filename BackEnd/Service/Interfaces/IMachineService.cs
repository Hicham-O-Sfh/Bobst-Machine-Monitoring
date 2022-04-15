using Service.DTOs;

namespace Service.Interfaces
{
    public interface IMachineService
    {
        Task<List<MachineDTO>> GetMachines();
        Task<MachineDTO> GetMachineById(int id);
        Task<int> GetProductionMachineById(int id);
        Task UpdateMachine(int id, MachineDTO machine);
        Task<MachineDTO> AddMachine(MachineDTO machine);
        Task DeleteMachine(int id);
        Task<bool> IsMachineExistsById(int id);
    }
}