using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DAL.Data;
using Blog.Interfaces.IRepository;

namespace Blog.DAL.Repositories
{
   
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity: class
    {

        private readonly BlogDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(BlogDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }


        public virtual IQueryable<TEntity> GetPaged(int top = 20, int skip = 0, object orderBy = null,
            object filter = null)

        {
            return null;
        }

        public virtual IQueryable<TEntity> GetAll(object filter)
        {
            return null;
        }

        public virtual TEntity GetFullObject(object id)
        {
            return null;
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State=EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            else
            {
                _dbSet.Remove(entity);
            }
        }


        public virtual void Delete(int id)
        {
            TEntity entity = _dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Commit()
        {
            _context.SaveChanges();
        }

        public virtual void Dispose()
        {
            _context.Dispose();
        }

    }



    
}
