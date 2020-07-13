using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonData.repo
{
    public class CommentRepository : BaseRepository<Comment>
    {
        public CommentRepository(PersonEntities entities) : base(entities)
        {
        }

        //public int Create(Comment comment)
        //{
        //    entities.comment.Add(comment);
        //    return entities.SaveChanges();
        //}

        //public void DeleteOne(Comment comment)
        //{
        //    entities.comment.Remove(comment);
        //    entities.SaveChanges();
        //}

        //public List<Comment> FindAll()
        //{
        //    return entities.comment.ToList();
        //}

        //public Comment FindOne(int id)
        //{
        //    return entities.comment.FirstOrDefault(x => x.Id == id);
        //}

        //public void Update(Comment entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}