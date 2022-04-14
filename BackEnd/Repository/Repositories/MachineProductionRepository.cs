using Microsoft.EntityFrameworkCore;
using Repository.Data.Models;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class MachineProductionRepository : IMachineProductionRepository
    {
        private readonly MachineMonitoringContext _context;

        public MachineProductionRepository(MachineMonitoringContext context)
        {
            this._context = context;
        }
        public async Task<MachineProduction> AddMachineProduction(MachineProduction machineProduction)
        {
            await this._context.MachineProductions.AddAsync(machineProduction);
            await this._context.SaveChangesAsync();
            return machineProduction;
        }

        public async Task<bool> IsMachineProductionExistsById(int id)
        {
            return await this._context.MachineProductions.AnyAsync(machineProd => machineProd.MachineProductionId == id);
        }

        public async Task DeleteMachineProduction(int id)
        {
            var machineProdToDelete = await this._context.MachineProductions.FindAsync(id);
            if (machineProdToDelete is not null)
                this._context.MachineProductions.Remove(machineProdToDelete);
            await this._context.SaveChangesAsync();
        }

        public async Task<MachineProduction> GetMachineProductionById(int id)
        {
            return await this._context.MachineProductions.FindAsync(id);
        }

        public async Task<List<MachineProduction>> GetMachinesProductions()
        {
            return await this._context.MachineProductions.ToListAsync();
        }

        public async Task UpdateMachineProduction(int id, MachineProduction machineProduction)
        {
            this._context.Entry(machineProduction).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }
    }
}