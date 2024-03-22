using Microsoft.EntityFrameworkCore;
using PharmaCity.Domain;
using System;
using System.Collections.Generic;

namespace PharmaCity.DataAccess.Context
{
    public class PharmaCityDbContext : DbContext
    {
        public PharmaCityDbContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<Invitation> Invitations { get; set; }
        public virtual DbSet<Petition> Petitions { get; set; }
        public virtual DbSet<StockRequest> StockRequests { get; set; }
        public virtual DbSet<Pharmacy> Pharmacies { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
