﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaChamadoTecnico.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ChamadoTecnicoEntities : DbContext
    {
        public ChamadoTecnicoEntities()
            : base("name=ChamadoTecnicoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Atendente> Atendente { get; set; }
        public virtual DbSet<Chamado> Chamado { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<PersonUser> PersonUser { get; set; }
    }
}
