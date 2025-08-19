using Microsoft.EntityFrameworkCore;
using Pacientes.DataBase.Models.pacientes;
using Pacientes.DataBase.Models.Usuarios;
using System;
using System.Collections.Generic;

namespace Pacientes.DataBase.Utils
{
    public class DbContextAPI : DbContext
    {
        public DbContextAPI(DbContextOptions<DbContextAPI> options) : base(options) { }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Cita> Citas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuarios>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Paciente>()
            .HasKey(p => p.Cedula);

            modelBuilder.Entity<Cita>()
            .Property(u => u.Id).ValueGeneratedOnAdd();
        }
    }
}
