using Microsoft.EntityFrameworkCore;
using PersonData.model;
using System;
using System.Collections.Generic;
using System.Linq;
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
            optionsBuilder.UseMySQL("server=192.168.0.94;database=dcv;user=root");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
           
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

            //realtion contactTable with personTable
            modelBuilder.Entity<Contact>()
                .HasOne(x => x.person)
                .WithMany(x => x.contacts)
                .HasForeignKey(x => x.person_id);

            //realation commenTable with personTable
            modelBuilder.Entity<Comment>()
                .HasOne(x => x.person)
                .WithMany(x => x.comments)
                .HasForeignKey(x => x.person_id);


            //realtion personTable=>adressPersonTable 1:N,03

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
    }
}
