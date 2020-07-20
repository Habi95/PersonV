using System.Collections.Generic;

namespace PersonData.repo
{
    public interface IRepository<T>
    {
        List<T> FindAll();

        T FindOne(int id);

        int Create(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}