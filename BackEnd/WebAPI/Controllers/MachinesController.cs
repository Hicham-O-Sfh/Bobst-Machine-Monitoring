using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class MachinesController : ControllerBase
    {
        private readonly IMachineService _machineService;
        private readonly IMapper _mapper;

        public MachinesController(IMachineService machineService, IMapper mapper)
        {
            this._machineService = machineService;
            this._mapper = mapper;
        }

        [HttpGet("machines")]
        public async Task<ActionResult<IEnumerable<MachineDTO>>> GetMachines()
        {
            var machinesDetails = await this._machineService.GetMachines();
            return Ok(machinesDetails);
        }

        [HttpGet("machine/{id}")]
        public async Task<ActionResult<MachineDTO>> GetMachine(int id)
        {
            var machine = await this._machineService.GetMachineById(id);
            return machine is not null ? Ok(machine) : NotFound();
        }

        [HttpGet("machine/totalproduction")]
        public async Task<ActionResult<MachineDTO>> GetMachineProduction(int id)
        {
            var data = await this._machineService.GetProductionMachineById(id);
            return data != -1 ? Ok(data) : NotFound();
        }
    }
}