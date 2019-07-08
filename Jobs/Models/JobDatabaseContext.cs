using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Jobs.Models
{
    public class JobDatabaseContext : DbContext
    {
        public JobDatabaseContext() : base("name=JobConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<JobDatabaseContext, global::Jobs.Migrations.Configuration>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<JobDatabaseContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<HOSO> HOSOs { get; set; }
        public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public virtual DbSet<CONGVIEC> CONGVIECes { get; set; }
        public virtual DbSet<ADMIN> ADMINs { get; set; }
        public virtual DbSet<LOAITK> LOAITKs { get; set; }
    }
}