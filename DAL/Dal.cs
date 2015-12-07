using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebCoEPhase2.Models;

namespace WebCoEPhase2.DAL
{
    public class Dal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Login>().ToTable("login_details");
            modelBuilder.Entity<UserDetails>().ToTable("user_details");
            modelBuilder.Entity<CountryDetails>().ToTable("country_details");
            modelBuilder.Entity<bank>().ToTable("bank");
            modelBuilder.Entity<transaction>().ToTable("tbltransaction");
            //modelBuilder.Entity<Login>().MapToStoredProcedures(
            //    a => a.Insert(i => i.HasName("usp_InsertUserCredentials"))
            //    );
        }

       
        public DbSet<Login> Logins { get; set; }
        public DbSet<UserDetails> Users { get; set; }
        public DbSet<CountryDetails> countries { get; set; }
        public DbSet<bank> banks { get; set; }
        public DbSet<transaction> transactions { get; set; }

    }

     
}