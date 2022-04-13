using Repository.Data.DTOs;
using Repository.Data.Models;

namespace Service.Interfaces
{
    public interface IMachineService
    {
        Task<List<Machine>> GetMachines();
        Task<MachineDTO> GetMachineById(int id);
        Task UpdateCollaborateur(int id, MachineDTO machine);
        Task<MachineDTO> AddMachine(MachineDTO machine);
        Task DeleteMachine(int id);
        Task<bool> IsMachineExistsById(int id);
    }
}