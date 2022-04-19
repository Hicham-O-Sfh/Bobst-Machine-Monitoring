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

        public MachinesController(IMachineService machineService)
        {
            this._machineService = machineService;
        }

        [HttpGet("machines")]
        public async Task<ActionResult<IEnumerable<MachineDTO>>> GetMachines()
        {
            var machinesDetails = await this._machineService.GetMachines();
            return machinesDetails.Count > 0 ? Ok(machinesDetails) : NoContent();
        }

        [HttpGet("machine/{id}")]
        public async Task<ActionResult<MachineDTO>> GetMachine(int id)
        {
            var machine = await this._machineService.GetMachineById(id);
            return machine is not null ? Ok(machine) : NotFound();
        }

        [HttpGet("machine/totalproduction")]
        public async Task<ActionResult> GetMachineProduction(int id)
        {
            var data = await this._machineService.GetProductionMachineById(id);
            return data != -1 ?
            Ok(new { totalproduction = data }) :
            NotFound();
        }

        [HttpDelete("machine/{id}")]
        public async Task<ActionResult<int>> DeleteMachine(int id)
        {
            return await this._machineService.DeleteMachine(id);
        }
    }
}