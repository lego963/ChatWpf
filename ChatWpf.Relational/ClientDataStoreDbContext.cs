using System;
using System.Collections.Generic;
using System.Text;
using ChatWpf.Core.DataModels;
using Microsoft.EntityFrameworkCore;

namespace ChatWpf.Relational
{
    public class ClientDataStoreDbContext : DbContext
    {
        public DbSet<LoginCredentialsDataModel> LoginCredentials { get; set; }

        public ClientDataStoreDbContext(DbContextOptions<ClientDataStoreDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API

            // Configure LoginCredentials
            // --------------------------
            //
            // Set Id as primary key
            modelBuilder.Entity<LoginCredentialsDataModel>().HasKey(a => a.Id);

            // TODO: Set up limits
            //modelBuilder.Entity<LoginCredentialsDataModel>().Property(a => a.FirstName).HasMaxLength(50);
        }
    }
}
