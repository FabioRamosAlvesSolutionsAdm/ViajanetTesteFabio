using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyTestViajanet.Infra.Data.Contexto;
using MyTestViajaNet.DomainService.Interfaces.Repositories;

namespace MyTestViajanet.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected MyTestViajanetContexto Db = new MyTestViajanetContexto();

        public void Add(TEntity obj)
        {
            Db.Set<TEntity>().Add(obj);
            Db.SaveChanges();
        }

        public void Dispose()
        {
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Db.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public void Remove(TEntity obj)
        {
            //Db.Set<TEntity>().Remove(obj);
            //Db.SaveChanges();

            Db.Entry(obj).State = EntityState.Deleted;
            Db.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            Db.Entry(obj).State = EntityState.Modified;

            Db.SaveChanges();

        }
    }
}
