using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MachineDTO>>> GetMachines()
        {
            var machines = await this._machineService.GetMachines();
            return Ok(machines);
        }
    }
}