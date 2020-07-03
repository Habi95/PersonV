using MySql.Data.MySqlClient;
using PersonData.model.ENUM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PersonData.repo
{
    public class DocumentRepository : Repository<Document>
    {
        PersonEntities entities;
        public DocumentRepository(PersonEntities entities)
        {
            this.entities = entities;
        }

        private MySqlConnection connection()
        {
            var conn = new MySqlConnection(entities.DbLocal);
            conn.Open();
            return conn;
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
        public Dictionary<int, List<Document>> GetDocuments(string classType)
        {
            Dictionary<int, List<Document>> dd = new Dictionary<int, List<Document>>();
            List<Document> doclist;
            MySqlCommand command = connection().CreateCommand();
            MySqlDataReader dataReader;
            command.CommandText =
            $"SELECT * FROM `document_class`" +
            $" Inner JOIN documents on doc_id = documents.id " +
            $"WHERE class = '{classType}'";
            using (dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    int id = (int)(int?)dataReader[3];
                    var x = dataReader[8].ToString();
                    var y = dataReader[10].ToString();
                    Document doc = new Document()
                    {
                     Id = (int)dataReader[4],
                     Url = dataReader[5].ToString(),
                     Name = dataReader[6].ToString(),
                    Comment = dataReader[7].ToString(),                    
                     ReminderId = string.IsNullOrEmpty(x) ? 0 : int.Parse(x),
                     CreatedAt = (DateTime)dataReader[9],                    
                     ModifiedAt = string.IsNullOrEmpty(y) ? DateTime.Parse("01.01.2000 00:00:00") : DateTime.Parse(y),
                     type = (EDocumentType)dataReader[11]

                    };
                    if (!dd.ContainsKey(id))
                    {
                        doclist = new List<Document>();
                        dd.Add(id, doclist);
                        dd[id].Add(doc);

                    }
                    else
                    {
                        dd[id].Add(doc);
                    }
                }
            }

            return dd;

        }

    }
}
