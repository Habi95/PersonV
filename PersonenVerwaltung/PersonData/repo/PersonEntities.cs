﻿using Microsoft.EntityFrameworkCore;
using PersonData.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonData
{
    public class PersonEntities : DbContext
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
        public DbSet<Address> address { get; set; }
        public DbSet<AddressPerson> addressperson { get; set; }
        public DbSet<Contact> contact { get; set; }
        public DbSet<Comment> comment { get; set; }
        public DbSet<Document> documents { get; set; }
        public DbSet<DocumentClass> document_class { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseMySQL(DbLocal);
            //optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            PrimKeys(modelBuilder);
            RealtionComment(modelBuilder);
            RealtionContact(modelBuilder);
            RealtionAddresses(modelBuilder);
        }
        private void RealtionComment(ModelBuilder modelBuilder)
        {
            //realation commenTable with personTable
            modelBuilder.Entity<Comment>()
                .HasOne(x => x.person)
                .WithMany(x => x.comments)
                .HasForeignKey(x => x.person_id);
        }
        private void RealtionContact(ModelBuilder modelBuilder)
        {
            //realtion contactTable with personTable
            modelBuilder.Entity<Contact>()
                .HasOne(x => x.person)
                .WithMany(x => x.contacts)
                .HasForeignKey(x => x.person_id);
        }
        private void RealtionAddresses(ModelBuilder modelBuilder)
        {
            //realtion personTable=>adressPersonTable 1:N
            modelBuilder.Entity<Person>()
                .HasMany(x => x.addresses)
                .WithOne();
            //realtion  adressTable => adressPersonTable 1:N
            modelBuilder.Entity<Address>()
                .HasMany(x => x.persons)
                .WithOne();
            //realtion adressPersonTable id-adressId-personId
            modelBuilder.Entity<AddressPerson>()
                .HasOne(x => x.person)
                .WithMany(x => x.addresses)
                .HasForeignKey(x => x.personId);
            //M:M realtion
            modelBuilder.Entity<AddressPerson>()
                .HasOne(x => x.address)
                .WithMany(x => x.persons)
                .HasForeignKey(x => x.addressId);
        }

        private void PrimKeys(ModelBuilder modelBuilder)
        {
            //entitity for addressTable
            modelBuilder.Entity<Address>().HasKey(x => x.id);
            //entity for personTable
            modelBuilder.Entity<Person>().HasKey(x => x.id);
            //entity for contactTable
            modelBuilder.Entity<Contact>().HasKey(x => x.id);
            //entity for commentTable
            modelBuilder.Entity<Comment>().HasKey(x => x.id);
            //entity for addressPersonTable
            modelBuilder.Entity<AddressPerson>().HasKey(x => x.id);
            //entity for documents
            modelBuilder.Entity<Document>().HasKey(x => x.Id);
            //entity for documentperson
            modelBuilder.Entity<DocumentClass>().HasKey(x => x.id);
        }
    }
}
