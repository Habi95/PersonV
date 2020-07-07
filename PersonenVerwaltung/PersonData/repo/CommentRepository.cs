using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonData.repo
{
    public class CommentRepository : IRepository<Comment>
    {
        private PersonEntities entities = new PersonEntities();

        public CommentRepository(PersonEntities entities)
        {
            this.entities = entities;
        }

        public int Create(Comment comment)
        {
            entities.comment.Add(comment);
            return entities.SaveChanges();
        }

        public void DeleteOne(Comment comment)
        {
            entities.comment.Remove(comment);
            entities.SaveChanges();
        }

        public List<Comment> FindAll()
        {
            return entities.comment.ToList();
        }

        public Comment FindOne(int id)
        {
            return entities.comment.FirstOrDefault(x => x.id == id);
        }

        public void Update(Comment entity)
        {
            throw new NotImplementedException();
        }
    }
}