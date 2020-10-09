using GameStore.DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Repository.Implemention
{
    public class EfRepository<TEntity> : IGenericRepository<TEntity> where TEntity: class
    {
        private readonly DbContext context;
        private readonly DbSet<TEntity> set;
        //dependency injection
        public EfRepository(DbContext _context)
        {
            context = _context;
            set = context.Set<TEntity>();
        }
        public void Create(TEntity entity)
        {
            set.Add(entity);
            Save();
        }

        private void Save()
        {
            context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            if (entity != null)
            {
                set.Remove(entity);
                Save();
            }
        }

        public TEntity Find(int id)
        {
            return set.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return set.AsEnumerable();
        }

        public void Update(TEntity entity)
        {
            set.AddOrUpdate(entity);
            //context.Entry(entity).State = EntityState.Modified;
            Save();
        }
    }
}
