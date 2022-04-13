using Repository.Data.DTOs;
using Repository.Data.Models;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Services
{
    public class MachineService : IMachineService
    {
        private readonly IMachineRepository _machineRepository;
        private readonly IMapper _mapper;


        public Task<MachineDTO> AddMachine(MachineDTO machine)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMachine(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MachineDTO> GetMachineById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Machine>> GetMachines()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsMachineExistsById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCollaborateur(int id, MachineDTO machine)
        {
            throw new NotImplementedException();
        }
    }
}