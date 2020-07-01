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
        public DbSet<AdressPerson> adressperson { get; set; }
        public DbSet<Contact> contact { get; set; }
        public DbSet<Comment> comment { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             optionsBuilder.UseMySQL("server=localhost;database=personadmin;user=root");
         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(x => x.id);
                
            });

            modelBuilder.Entity<Person>(entity =>
            {
                //entity.HasKey(x => x.id);
                entity.Property(x => x.name1).IsRequired();
                entity.Property(x => x.name2).IsRequired();
                //entity.Property(x => x.title);
                //entity.Property(x => x.sv_nr);
                //entity.Property(x => x.date);
                //entity.Property(x => x.gender);
                //entity.Property(x => x.busy);
                //entity.Property(x => x.busy_by);
                //entity.Property(x => x.picture);
                entity.Property(x => x.function).IsRequired();
                entity.Property(x => x.aktiv).IsRequired();
                entity.Property(x => x.deleted_inaktiv).IsRequired();
                entity.Property(x => x.newsletter_flag).IsRequired();
                entity.Property(x => x.createdAt).IsRequired();
                //entity.Property(x => x.modifyAt);

            });
        }
    }
}
