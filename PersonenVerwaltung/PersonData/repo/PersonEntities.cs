using Microsoft.EntityFrameworkCore;
using PersonData.repo;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonData
{
    public class PersonEntities : BaseEntities
    {
        public DbSet<master_file> master_file { get; set; }

       /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=personadmin;user=root");
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<master_file>(entity =>
            {
                entity.HasKey(x => x.id);
                entity.Property(x => x.firstname).IsRequired();
                entity.Property(x => x.lastname).IsRequired();
                entity.Property(x => x.title);
                entity.Property(x => x.sv_nr);
                entity.Property(x => x.geb_date);
                entity.Property(x => x.gender);
                entity.Property(x => x.busy);
                entity.Property(x => x.busy_by);
                entity.Property(x => x.picture);
                entity.Property(x => x.function).IsRequired();
                entity.Property(x => x.aktiv).IsRequired();
                entity.Property(x => x.deleted_inaktiv).IsRequired();
                entity.Property(x => x.newsletter_flag).IsRequired();
                entity.Property(x => x.createdAt).IsRequired();
                entity.Property(x => x.modifyAt);

            });
        }
    }
}
