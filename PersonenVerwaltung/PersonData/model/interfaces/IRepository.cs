using System.Collections.Generic;

namespace PersonData.repo
{
    internal interface IRepository<T>
    {
        List<T> FindAll();

        T FindOne(int id);

        int Create(T entity); // returns last insert id

        void DeleteOne(T entity);

        void Update(T entity);
    }
}