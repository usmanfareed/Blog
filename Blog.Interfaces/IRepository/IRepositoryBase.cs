using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Interfaces.IRepository
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
            TEntity GetById(int id);
            IQueryable<TEntity> GetAll();

            IQueryable<TEntity> GetPaged(int top = 20, int skip = 0, object orderBy = null,
                object filter = null);

            IQueryable<TEntity> GetAll(object filter);
            TEntity GetFullObject(object id);
            void Insert(TEntity entity);
            void AddUpdate(TEntity entity);
            void Update(TEntity entity);
            void Delete(TEntity entity);
            void Delete(int id);
            void Commit();
            void Dispose();
        }

    
}
