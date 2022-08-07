using Core.DataAccess.Abstract;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Concrete.EntityFramework
{
    public class EfEntityBaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
        where TEntity : class, IEntity, new ()
        where TContext : DbContext, new ()
    {

        protected TContext Context { get; }
        public EfEntityBaseRepository(TContext context)
        {
            Context = context;
        }

        public void Add(TEntity entity)
    {
        using (TContext Context = new TContext())//using (bu normal using değil)içerisindeki nesneler işi bitince garbage collector atar  -bu performasn sağlarr
        {
            var addedEntity = Context.Entry(entity);
            addedEntity.State = EntityState.Added;
                Context.SaveChanges();

        }
    }

        public bool Any(Expression<Func<TEntity, bool>> exp)
        {
            using (TContext Context = new TContext())
            {
                return Context.Set<TEntity>().Any(exp);
            }
        }

        public void Delete(TEntity entity)
    {
        using (TContext Context = new TContext())
        {
            var deletedEntity = Context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
                Context.SaveChanges();

        }
    }

    public TEntity Get(Expression<Func<TEntity, bool>> filter)
    {
        using (TContext Context = new TContext())
        {
            return Context.Set<TEntity>().FirstOrDefault(filter);


        }
    }

    public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
    {
        using (TContext Context = new TContext())
        {//filtre-varsa veya yoksa
         // set diyorki db setlerden product a bağlanıyorum
         //benim product uma yerleş ve oradaki tüm datayı liste çevir
         //eğer değilse
            return filter == null
                ? Context.Set<TEntity>().ToList()
                : Context.Set<TEntity>().Where(filter).ToList();


        }
    }

    public void Update(TEntity entity)
    {
        using (TContext Context = new TContext())
        {
            var updatedEntity = Context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
                Context.SaveChanges();

        }
    }
    
    }
}
