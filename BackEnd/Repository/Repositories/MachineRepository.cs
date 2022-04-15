using Microsoft.EntityFrameworkCore;
using Repository.Data.Models;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class MachineRepository : IMachineRepository
    {
        private readonly MachineMonitoringContext _context;

        public MachineRepository(MachineMonitoringContext context)
        {
            this._context = context;
        }

        public async Task<Machine> AddMachine(Machine machine)
        {
            await this._context.Machines.AddAsync(machine);
            await this._context.SaveChangesAsync();
            return machine;
        }

        public async Task<bool> IsMachineExistsById(int id)
        {
            return await this._context.Machines.AnyAsync(machine => machine.MachineId == id);
        }

        public async Task<int> DeleteMachine(int id)
        {
            var machineToDelete = await this._context.Machines.FindAsync(id);
            if (machineToDelete is null) return 0;
            // machineToDelete.MachineProductions.
            this._context.Machines.Remove(machineToDelete);
            await this._context.SaveChangesAsync();
            return 1;
        }

        public async Task<Machine> GetMachineById(int id)
        {
            return await this._context.Machines.FindAsync(id);
        }

        public async Task<int> GetProductionMachineById(int id)
        {
            var machine = await this._context.Machines.Include(machine => machine.MachineProductions).Where(machine => machine.MachineId == id).FirstOrDefaultAsync();
            return machine is not null ? machine.MachineProductions.Sum(machineProd => machineProd.TotalProduction) : -1;
        }

        public async Task<List<Machine>> GetMachines()
        {
            return await this._context.Machines.Include(machine => machine.MachineProductions).ToListAsync();
        }

        public async Task UpdateMachine(int id, Machine machine)
        {
            this._context.Entry(machine).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }
    }
}