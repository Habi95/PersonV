using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonData.repo
{
    class CommentRepository : Repository<Comment>
    {
        PersonEntities entities = new PersonEntities();

        public int create(Comment comment)
        {
            entities.comment.Add(comment);
            return entities.SaveChanges();
        }

        public void deleteOne(Comment comment)
        {
            entities.comment.Remove(comment);
            entities.SaveChanges();
        }

        public List<Comment> findAll()
        {
            return entities.comment.ToList();
        }

        public Comment findOne(int id)
        {
            return entities.comment.FirstOrDefault(x => x.id == id);
        }
    }
}
