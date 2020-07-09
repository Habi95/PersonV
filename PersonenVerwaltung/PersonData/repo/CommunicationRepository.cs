using MySql.Data.MySqlClient;
using PersonData.model.course;
using PersonData.model.ENUM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using K4os.Hash.xxHash;

namespace PersonData.repo
{
    public class CommunicationRepository : IRepository<Communication>
    {
        private PersonEntities entities;
        private DocumentRepository docRepo;

        public CommunicationRepository(PersonEntities personEntities)
        {
            entities = personEntities;
            docRepo = new DocumentRepository(personEntities);
        }

        private MySqlConnection connection()
        {
            var conn = new MySqlConnection(entities.DbServer);
            conn.Open();
            return conn;
        }

        public int Create(Communication entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteOne(Communication entity)
        {
            throw new NotImplementedException();
        }

        public List<Communication> FindAll()
        {
            throw new NotImplementedException();
        }

        public Communication FindOne(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Communication entity)
        {
            throw new NotImplementedException();
        }

        public List<RelCommunicationClass> GetCommunications<T>(int Pid) where T : BaseClassCreatedModify
        {
            var person = entities.person.FirstOrDefault(c => c.Id == Pid);

            if (person != null)
            {
                var classname = typeof(T).Name;
                var classes = entities.Set<T>();
                var communication = entities.communication_class.Include(x => x.Communication).Where(x => x.Communication.PersonId == Pid).ToList();
                communication.ForEach(x =>
                {
                    x.Communication.Document = entities.documents.FirstOrDefault(c => c.Id == x.Communication.DocumentId);
                    x.sender = classes.FirstOrDefault(d => d.Id == x.ClassId);
                });

                Console.WriteLine();
                return communication;
            }

            return null;
        }
    }
}