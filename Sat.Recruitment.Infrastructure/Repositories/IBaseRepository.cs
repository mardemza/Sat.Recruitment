using System.Collections.Generic;

namespace Sat.Recruitment.Infrastructure.Repositories
{
    public interface IBaseRepository<T>
        where T : class
    {
        IList<T> GetAll();
        T Get(int id);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(int id);
    }
}