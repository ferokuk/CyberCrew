﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CyberCrew.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CyberCrewEntities : DbContext
    {
        public CyberCrewEntities()
            : base("name=CyberCrewEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Computer> Computer { get; set; }
        public virtual DbSet<CPU> CPU { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<GPU> GPU { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<Money> Money { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Software> Software { get; set; }
        public virtual DbSet<Storage> Storage { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}