using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using PersonData.model.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonData.repo
{
    public class DocumentRepository : IRepository<Document>
    {
        private PersonEntities entities;
        private PersonRepository PersonRepository;

        public DocumentRepository(PersonEntities entities)
        {
            this.entities = entities;
        }

        public int Create(Document entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteOne(Document entity)
        {
            throw new NotImplementedException();
        }

        public List<Document> FindAll()
        {
            throw new NotImplementedException();
        }

        public Document FindOne(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Document entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// generic select from document T class is class is document_class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<Document> GetDocuments<T>(int Pid) where T : BaseClassCreatedModify
        {
            //entities.documents.Include(x => x.Classes);
            //entities.document_class.Include(x => x.Document);
            var className = typeof(T).Name;
            var classes = entities.Set<T>();
            var documentClasses = entities.document_class.Include(x => x.Document).Where(c => c.class_id == Pid && c.classValue == className).ToList();
            documentClasses.ForEach(x =>
            {
                x.Document.DocumentOwner = classes.FirstOrDefault(x => x.Id == Pid);
            });

            //List<Document> doclist = new List<Document>();
            //MySqlCommand command = connection().CreateCommand();
            //MySqlDataReader dataReader;
            //command.CommandText =
            //$"SELECT * FROM `document_class`" +
            //$" Inner JOIN documents on doc_id = documents.id " +
            //$"WHERE class = '{typeof(T).Name}' And class_id = {Pid}";
            //using (dataReader = command.ExecuteReader())
            //{
            //    while (dataReader.Read())
            //    {
            //        int id = (int)(int?)dataReader[3];
            //        var reminder = dataReader[8].ToString();
            //        var modify = dataReader[10].ToString();
            //        Document doc = new Document()
            //        {
            //            Id = (int)dataReader[4],
            //            Url = dataReader[5].ToString(),
            //            Name = dataReader[6].ToString(),
            //            Comment = dataReader[7].ToString(),
            //            ReminderId = string.IsNullOrEmpty(reminder) ? 0 : int.Parse(reminder),
            //            CreatedAt = (DateTime)dataReader[9],
            //            ModifiedAt = string.IsNullOrEmpty(modify) ? DateTime.Parse("01.01.2000 00:00:00") : DateTime.Parse(modify),
            //            type = (EDocumentType)Enum.Parse(typeof(EDocumentType), dataReader[11].ToString(), true)
            //        };
            //        doclist.Add(doc);
            //    }
            //}

            return documentClasses.Select(x => x.Document).ToList();
        }
    }
}