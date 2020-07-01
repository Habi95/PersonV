using System;
using System.Collections.Generic;
using System.Text;

namespace PersonData.repo
{
    interface Repository<T>
    {
        List<T> findAll();
        T findOne(int id);
        int create(T entity); // returns last insert id
        void deleteOne(T entity);
    }
}
