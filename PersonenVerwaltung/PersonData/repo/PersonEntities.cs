using Microsoft.EntityFrameworkCore;
using PersonData.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonData
{
    public class PersonEntities : DbContext
    {
        public DbSet<Person> person { get; set; }
        public DbSet<Address> address { get; set; }
        public DbSet<AddressPerson> addressperson { get; set; }
        public DbSet<Contact> contact { get; set; }
        public DbSet<Comment> comment { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=personadmin;user=root");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
           
            //entitity for addressTable
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(x => x.id);
                entity.Property(x => x.createdAt);

            });
            //entity for personTable
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(x => x.id);
                entity.Property(x => x.name1).IsRequired();
                entity.Property(x => x.name2).IsRequired();
                entity.Property(x => x.function).IsRequired();
                entity.Property(x => x.aktiv).IsRequired();
                entity.Property(x => x.deleted_inaktiv).IsRequired();
                entity.Property(x => x.newsletter_flag).IsRequired();
                entity.Property(x => x.createdAt).IsRequired();
            });

            //realtion personTable=>adressPersonTable
            modelBuilder.Entity<Person>().HasMany(x => x.addresses).WithOne();
            //realtion  adressTable => adressPersonTable
            modelBuilder.Entity<Address>().HasMany(x => x.persons).WithOne();

            //realtion adressPersonTable id-adressId-personId
            modelBuilder.Entity<AdressPerson>(entity =>
                        {
                            entity.HasKey(x => x.id);
                        });


            modelBuilder.Entity<AdressPerson>()
                .HasOne(x => x.person)
                .WithMany(x => x.addresses)
                .HasForeignKey(x => x.personId);
            modelBuilder.Entity<AdressPerson>()
                .HasOne(x => x.address)
                .WithMany(x => x.persons)
                .HasForeignKey(x => x.adressId);



        }
    }
}
