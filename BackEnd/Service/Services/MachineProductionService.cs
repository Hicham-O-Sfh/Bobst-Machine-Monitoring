using AutoMapper;
using Repository.Data.Models;
using Repository.Interfaces;
using Service.DTOs;
using Service.Interfaces;

namespace Service.Services
{
    public class MachineProductionService : IMachineProductionService
    {
        private readonly IMachineProductionRepository _machineProductionRepository;
        private readonly IMapper _mapper;

        public MachineProductionService(IMachineProductionRepository machineProductionRepository, IMapper mapper)
        {
            this._machineProductionRepository = machineProductionRepository;
            this._mapper = mapper;
        }

        public async Task<MachineProductionDTO> AddMachineProduction(MachineProductionDTO machineProd)
        {
            var returnedMachine = await this._machineProductionRepository.AddMachineProduction(this._mapper.Map<MachineProduction>(machineProd));
            return this._mapper.Map<MachineProductionDTO>(returnedMachine);
        }

        public async Task DeleteMachineProduction(int id)
        {
            await this._machineProductionRepository.DeleteMachineProduction(id);
        }

        public async Task<MachineProductionDTO> GetMachineProductionById(int id)
        {
            return this._mapper.Map<MachineProductionDTO>(await this._machineProductionRepository.GetMachineProductionById(id));
        }

        public async Task<List<MachineProductionDTO>> GetMachines()
        {
            return this._mapper.Map<List<MachineProductionDTO>>(await this._machineProductionRepository.GetMachinesProductions());
        }

        public Task<List<MachineProductionDTO>> GetMachinesProductions()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsMachineProductionExistsById(int id)
        {
            return await this._machineProductionRepository.IsMachineProductionExistsById(id);
        }

        public async Task UpdateMachineProduction(int id, MachineProductionDTO machineProd)
        {
            await this._machineProductionRepository.UpdateMachineProduction(id, this._mapper.Map<MachineProduction>(machineProd));
        }
    }
}