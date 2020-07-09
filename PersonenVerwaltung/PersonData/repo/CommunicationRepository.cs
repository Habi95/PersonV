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

        //
        public List<RelCommunicationClass> GetCommunications<T>(int Pid) where T : BaseClassCreatedModify
        {
            var person = entities.person.FirstOrDefault(c => c.id == Pid);

            if (person != null)
            {
                var classname = typeof(T).Name;
                var classes = entities.Set<T>();
                var communication = entities.communication_class.Include(x => x.Communication).Where(x => x.Communication.PersonId == Pid).ToList();
                communication.ForEach(x =>
                {
                    x.Communication.Document = entities.documents.FirstOrDefault(c => c.id == x.Communication.DocumentId);
                    x.sender = classes.FirstOrDefault(d => d.id == x.ClassId);
                });

                Console.WriteLine();
                return communication;
            }

            return null;
        }

        //List<RelCommunicationClass> communicationClasses;
        //MySqlCommand command = connection().CreateCommand();
        //MySqlDataReader dataReader;
        //command.CommandText = $"SELECT * FROM `communication_class` " +
        //    $"Inner JOIN communication On communication.id = communication_class.id  " +
        //    $"INNER JOIN documents ON communication.document_id = documents.id " +
        //    $"WHERE communication.person_id = {Pid}";
        //using (dataReader = command.ExecuteReader())
        //{
        //    while (dataReader.Read())
        //    {
        //        RelCommunicationClass communicationClass = new RelCommunicationClass()
        //        {
        //            Id = int.Parse(dataReader[0].ToString()),
        //            CommunicationId = int.Parse(dataReader[1].ToString()),
        //            Class = dataReader[2].ToString(),
        //            ClassId = int.Parse(dataReader[3].ToString())
        //        };
        //        Communication communication = new Communication()
        //        {
        //            Id = int.Parse(dataReader[4].ToString()),
        //            Channel = (EChannel)Enum.Parse(typeof(EChannel), dataReader[5].ToString(), true),
        //            PersonId = int.Parse(dataReader[6].ToString()),
        //            Date = DateTime.Parse(dataReader[7].ToString()),
        //            comment = dataReader[8].ToString(),
        //            DocumentId = int.Parse(dataReader[9].ToString()),
        //            ReminderId = int.Parse(dataReader[10].ToString()),
        //            CreatedAt = DateTime.Parse(dataReader[11].ToString()),
        //            CommunicationClasse = communicationClass
        //        };

        //        if (communication.DocumentId.HasValue)
        //        {
        //            communication.Document = new Document()
        //            {
        //                Id = communication.DocumentId.Value,
        //                Url = dataReader[12].ToString(),
        //                Name = dataReader[13].ToString(),
        //                Comment = dataReader[14].ToString(),
        //                ReminderId = string.IsNullOrEmpty(dataReader[14].ToString()) ? 0 : int.Parse(dataReader[14].ToString()),
        //                CreatedAt = (DateTime)dataReader[15],
        //                ModifiedAt = null,//string.IsNullOrEmpty(dataReader[16].ToString()) ? DateTime.Parse("01.01.2000 00:00:00") : DateTime.Parse(modify),
        //                type = (EDocumentType)Enum.Parse(typeof(EDocumentType), dataReader[17].ToString(), true)
        //            };
        //        }
        //    }
        //}
    }
}