using Itcast.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itcast.DAL
{
    public class BaseServices<T> where T:class,new()
    {
        public DbContext Db { get { return DbContextFactory.CreateDbContext(); } }

        public IQueryable<T> LoadEntity(System.Linq.Expressions.Expression<Func<T, bool>> whereLamda)
        {
            return Db.Set<T>().Where<T>(whereLamda);
        }

        public bool ModifyEntity(T Entity)
        {
            Db.Entry<T>(Entity).State = System.Data.Entity.EntityState.Modified;
            return true;
            //return Db.SaveChanges() > 0;
        }

        public bool AddEntity(T Entity)
        {
            Db.Entry<T>(Entity).State = System.Data.Entity.EntityState.Added;
            return true;
            //return Db.SaveChanges() > 0;
        }

        public bool DeleteEntity(T Entity)
        {
            Db.Entry<T>(Entity).State = System.Data.Entity.EntityState.Deleted;
            return true;
            //return Db.SaveChanges() > 0;
        }
    }
}
