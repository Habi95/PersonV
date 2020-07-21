using Microsoft.EntityFrameworkCore;
using SecurityData.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityData.repo
{
    public class Entities : DbContext
    {
        private string dbServer = "server=192.168.0.94;database=dcv;user=root;Convert Zero Datetime=True";

        public string DbServer
        {
            get
            {
                return dbServer;
            }
        }

        private string dbLocal = "server=localhost;database=dcv;user=root;Convert Zero Datetime=True";

        public string DbLocal
        {
            get
            {
                return dbLocal;
            }
        }

        public DbSet<Person> person { get; set; }
        public DbSet<Contact> contact { get; set; }
        public DbSet<User> user { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var f = optionsBuilder.UseMySQL(DbServer);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            PrimKeys(modelBuilder);
            RealtionContact(modelBuilder);
            RealtionUser(modelBuilder);
        }

        private void RealtionContact(ModelBuilder modelBuilder)
        {
            //realtion contactTable with personTable
            modelBuilder.Entity<Contact>()
                .HasOne(x => x.person)
                .WithMany(x => x.contacts)
                .HasForeignKey(x => x.person_id);
        }

        private void RealtionUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(x => x.person)
                .WithOne(x => x.user)
                .HasForeignKey<Person>(x => x.user_id);
        }

        private void PrimKeys(ModelBuilder modelBuilder)
        {
            //entity for personTable
            modelBuilder.Entity<Person>().HasKey(x => x.Id);
            //entity for contactTable
            modelBuilder.Entity<Contact>().HasKey(x => x.Id);
            //entity for user
            modelBuilder.Entity<User>().HasKey(x => x.Id);
        }
    }
}