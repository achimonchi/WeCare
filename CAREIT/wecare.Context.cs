﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CAREIT
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ucareEntities1 : DbContext
    {
        public ucareEntities1()
            : base("name=ucareEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<employee> employees { get; set; }
        public virtual DbSet<insurance> insurances { get; set; }
        public virtual DbSet<medicine> medicines { get; set; }
        public virtual DbSet<patient> patients { get; set; }
        public virtual DbSet<queue> queues { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<transaction> transactions { get; set; }
    }
}
