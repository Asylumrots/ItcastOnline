using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Itcast.Model;
using Itcast.IDAL;
using Itcast.DALFactory;

namespace Itcast.BLL
{
    public abstract class BaseManager<T> where T:class,new()
    {
        public IDbSession CurrentBbSession
        {
            get { return DbSessionFactory.CreateDbSession(); }
        }

        public IBaseDAL<T> CurrentDAL { get; set; }
        public abstract void SetCurrentDAL();
        public BaseManager()
        {
            SetCurrentDAL();
        }

        public IQueryable<T> LoadEntity(System.Linq.Expressions.Expression<Func<T, bool>> whereLamda)
        {
            return CurrentDAL.LoadEntity(whereLamda);
        }

        public bool ModifyEntity(T Entity)
        {
            CurrentDAL.ModifyEntity(Entity);
            return CurrentBbSession.SaveChanges();
        }

        public bool AddEntity(T Entity)
        {
            CurrentDAL.AddEntity(Entity);
            return CurrentBbSession.SaveChanges();
        }

        public bool DeleteEntity(T Entity)
        {
            CurrentDAL.DeleteEntity(Entity);
            return CurrentBbSession.SaveChanges();
        }
    }

}
