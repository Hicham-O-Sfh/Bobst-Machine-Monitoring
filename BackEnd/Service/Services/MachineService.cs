using AutoMapper;
using Repository.Data.Models;
using Repository.Interfaces;
using Service.DTOs;
using Service.Interfaces;

namespace Service.Services
{
    public class MachineService : IMachineService
    {
        private readonly IMachineRepository _machineRepository;
        private readonly IMapper _mapper;

        public MachineService(IMachineRepository machineRepository, IMapper mapper)
        {
            this._machineRepository = machineRepository;
            this._mapper = mapper;
        }

        public async Task<MachineDTO> AddMachine(MachineDTO machine)
        {
            var returnedMachine = await this._machineRepository.AddMachine(this._mapper.Map<Machine>(machine));
            return this._mapper.Map<MachineDTO>(returnedMachine);
        }

        public async Task<int> DeleteMachine(int id)
        {
            return await this._machineRepository.DeleteMachine(id);
        }

        public async Task<MachineDTO> GetMachineById(int id)
        {
            return this._mapper.Map<MachineDTO>(await this._machineRepository.GetMachineById(id));
        }

        public async Task<List<MachineDTO>> GetMachines()
        {
            return this._mapper.Map<List<MachineDTO>>(await this._machineRepository.GetMachines());
        }

        public async Task<int> GetProductionMachineById(int id)
        {
            return await this._machineRepository.GetProductionMachineById(id);
        }

        public async Task<bool> IsMachineExistsById(int id)
        {
            return await this._machineRepository.IsMachineExistsById(id);
        }

        public async Task UpdateMachine(int id, MachineDTO machine)
        {
            await this._machineRepository.UpdateMachine(id, this._mapper.Map<Machine>(machine));
        }
    }
}