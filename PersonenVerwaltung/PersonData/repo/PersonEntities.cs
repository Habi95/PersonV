using Data.Models;
using Microsoft.EntityFrameworkCore;
using PersonData.model;
using PersonData.model.course;
using PersonData.model.material;

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
        public DbSet<RelCourseTrainer> course_trainer { get; set; }
        public DbSet<RelCourseParticipant> course_participants { get; set; }
        public DbSet<Course> course { get; set; }
        public DbSet<book> book { get; set; }
        public DbSet<equipment> equipment { get; set; }
        public DbSet<notebook> notebook { get; set; }
        public DbSet<RelCommunicationClass> communication_class { get; set; }
        public DbSet<Communication> communication { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var f = optionsBuilder.UseMySQL(DbServer);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            PrimKeys(modelBuilder);
            RealtionComment(modelBuilder);
            RealtionContact(modelBuilder);
            RealtionAddresses(modelBuilder);
            RealtionCourseTrainer(modelBuilder);
            RealtionCourseParticipants(modelBuilder);
            RealtionBook(modelBuilder);
            RealtionEquipment(modelBuilder);
            RealtionNotebook(modelBuilder);
            RealtionDocument(modelBuilder);
            RealtionCommunicaton(modelBuilder);
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

        private void RealtionCourseTrainer(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                            .HasMany(x => x.RelCourseTrainers)
                            .WithOne(x => x.Course)
                            .HasForeignKey(x => x.CourseId);

            modelBuilder.Entity<RelCourseTrainer>()
                            .HasOne(x => x.Course)
                            .WithMany(x => x.RelCourseTrainers)
                            .HasForeignKey(x => x.CourseId);

            modelBuilder.Entity<RelCourseTrainer>()
                .HasOne(x => x.Person)
                .WithMany(x => x.courseTrainers)
                .HasForeignKey(x => x.TrainerID);
            modelBuilder.Entity<Person>()
                            .HasMany(x => x.courseTrainers)
                            .WithOne(x => x.Person);
        }

        private void RealtionCourseParticipants(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasMany(x => x.RelCourseParticipants)
                .WithOne(x => x.Course)
                .HasForeignKey(x => x.CourseId);

            modelBuilder.Entity<RelCourseParticipant>()
                            .HasOne(x => x.Course)
                            .WithMany(x => x.RelCourseParticipants)
                            .HasForeignKey(x => x.CourseId);

            modelBuilder.Entity<RelCourseParticipant>()
                .HasOne(x => x.Person)
                .WithMany(x => x.courseParticipants)
                .HasForeignKey(x => x.ParticipantId);
            modelBuilder.Entity<Person>()
                           .HasMany(x => x.courseParticipants)
                           .WithOne(x => x.Person);
        }

        private void RealtionBook(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<book>()
               .HasOne(x => x.person)
               .WithMany(x => x.book)
               .HasForeignKey(x => x.person_id);
        }

        private void RealtionEquipment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<equipment>()
               .HasOne(x => x.person)
               .WithMany(x => x.equipment)
               .HasForeignKey(x => x.person_id);
        }

        private void RealtionNotebook(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<notebook>()
               .HasOne(x => x.person)
               .WithMany(x => x.notebook)
               .HasForeignKey(x => x.person_id);
        }

        private void RealtionDocument(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentClass>()
               .HasOne(x => x.Document)
               .WithMany(x => x.Classes)
               .HasForeignKey(x => x.doc_id);
        }

        private void RealtionCommunicaton(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RelCommunicationClass>()
                .HasOne(x => x.Communication)
                .WithMany(x => x.CommunicationClass)
                .HasForeignKey(x => x.Id);
        }

        private void PrimKeys(ModelBuilder modelBuilder)
        {
            //entitity for addressTable
            modelBuilder.Entity<Address>().HasKey(x => x.Id);
            //entity for personTable
            modelBuilder.Entity<Person>().HasKey(x => x.Id);
            //entity for contactTable
            modelBuilder.Entity<Contact>().HasKey(x => x.Id);
            //entity for commentTable
            modelBuilder.Entity<Comment>().HasKey(x => x.Id);
            //entity for addressPersonTable
            modelBuilder.Entity<AddressPerson>().HasKey(x => x.id);
            //entity for documents
            modelBuilder.Entity<Document>().HasKey(x => x.Id);
            //entity for documentperson
            modelBuilder.Entity<DocumentClass>().HasKey(x => x.id);
            //entity for course
            modelBuilder.Entity<Course>().HasKey(x => x.Id);
            //entity for course_trainer
            modelBuilder.Entity<RelCourseTrainer>().HasKey(x => x.Id);
            //entity for course_participant
            modelBuilder.Entity<RelCourseParticipant>().HasKey(x => x.Id);
            //entity for book
            modelBuilder.Entity<book>().HasKey(x => x.id);
            //enetity for equipment
            modelBuilder.Entity<equipment>().HasKey(x => x.id);
            //entity for notebook
            modelBuilder.Entity<notebook>().HasKey(x => x.id);
            //enity for communicaton
            modelBuilder.Entity<Communication>().HasKey(x => x.Id);
            //entity for communicaton_class
            modelBuilder.Entity<RelCommunicationClass>().HasKey(x => x.Id);
        }
    }
}