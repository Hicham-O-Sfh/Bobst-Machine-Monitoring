using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Data.Models;
using Repository.Interfaces;

namespace Repository
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

        public async Task DeleteMachine(int id)
        {
            var machineToDelete = await this._context.Machines.FindAsync(id);
            if (machineToDelete is not null)
                this._context.Machines.Remove(machineToDelete);
            await this._context.SaveChangesAsync();
        }

        public async Task<Machine> GetMachineById(int id)
        {
            return await this._context.Machines.FindAsync(id);
        }

        public async Task<List<Machine>> GetMachines()
        {
            return await this._context.Machines.ToListAsync();
        }

        public async Task UpdateMachine(int id, Machine machine)
        {
            this._context.Entry(machine).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }
    }
}