using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Metrobank.Model.Models;
using Metrobank.DataAccessLibrary.Interfaces;

#nullable disable

namespace Metrobank.DataAccessLibrary.DataAccess
{
    public partial class MetrobankDataAccess : DbContext , IMetrobankDataAccess
    {
        public MetrobankDataAccess(DbContextOptions<MetrobankDataAccess> options)
            : base(options)
        {
        }

        public virtual DbSet<AppMain> AppMains { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AppMain>(entity =>
            {
                entity.ToTable("AppMain");

                entity.HasIndex(e => e.InputNumber, "UC_InputNumber")
                    .IsUnique();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
