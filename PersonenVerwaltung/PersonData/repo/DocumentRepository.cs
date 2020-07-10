using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using PersonData.model.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonData.repo
{
    public class DocumentRepository
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
            var className = typeof(T).Name;
            var classes = entities.Set<T>();
            var documentClasses = entities.document_class.Include(x => x.Document).Where(c => c.class_id == Pid && c.classValue == className).ToList();
            documentClasses.ForEach(x =>
            {
                x.Document.DocumentOwner = classes.FirstOrDefault(x => x.Id == Pid);
            });
            return documentClasses.Select(x => x.Document).ToList();
        }
    }
}