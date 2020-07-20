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
    }
}