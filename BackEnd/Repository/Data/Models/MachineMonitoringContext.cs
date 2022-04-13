using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Repository.Data.Models
{
    public partial class MachineMonitoringContext : DbContext
    {
        public MachineMonitoringContext()
        {
        }

        public MachineMonitoringContext(DbContextOptions<MachineMonitoringContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Machine> Machines { get; set; } = null!;
        public virtual DbSet<MachineProduction> MachineProductions { get; set; } = null!;

        //         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //         {
        //             if (!optionsBuilder.IsConfigured)
        //             {
        // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                 optionsBuilder.UseSqlServer("Server=.;Database=MachineMonitoring;Trusted_Connection=True;");
        //             }
        //         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Machine>(entity =>
            {
                entity.ToTable("Machine");

                entity.Property(e => e.MachineId).HasColumnName("machineId");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<MachineProduction>(entity =>
            {
                entity.ToTable("MachineProduction");

                entity.Property(e => e.MachineId).HasColumnName("machineId");

                entity.Property(e => e.TotalProduction).HasColumnName("totalProduction");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.MachineProductions)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MachinePr__machi__3B40CD36");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
