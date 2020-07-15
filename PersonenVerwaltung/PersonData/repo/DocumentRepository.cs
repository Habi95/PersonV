using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using PersonData.model.course;
using PersonData.model.ENUM;
using System;
using System.Collections.Generic;
using System.IO;
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

        public string DeleteById(int id)
        {
            ///delete the Relations from Documents to Classes
            List<DocumentClass> relationList = entities.document_class.Where(x => x.doc_id == id).ToList();
            foreach (var item in relationList)
            {
                entities.document_class.Remove(item);
            }
            /////set the affected Absences - DocumentId to null
            //List<Absence> absenceList = entities.Absences.Where(x => x.DocumentId == id).ToList();
            //foreach (var item in absenceList)
            //{
            //    item.DocumentId = null;
            //    entities.Absences.Update(item);
            //}
            ///set the affected Communications - DocumentId to null
            List<Communication> communicationList = entities.communication.Where(x => x.DocumentId == id).ToList();
            foreach (var item in communicationList)
            {
                item.DocumentId = null;
                entities.communication.Update(item);
            }

            Document documentToDelete = entities.documents.SingleOrDefault(x => x.Id == id);
            if (documentToDelete == null)
            {
                return "The Document you want to delete could not be found.";
            }
            ///Deletes Document with its Path
            bool fileFound = DeleteRealDocument(documentToDelete);
            ///Deletes Document entry in Database
            entities.documents.Remove(documentToDelete);
            entities.SaveChanges();

            if (fileFound)
            {
                return "Record has been successfully deleted";
            }
            else
            {
                return "File not found.";
            }
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

        public bool DeleteRealDocument(Document documentToDelete)
        {
            bool fileFound = true;
            try
            {
                string filename = documentToDelete.Url;

                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                else
                {
                    fileFound = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return fileFound;
        }
    }
}