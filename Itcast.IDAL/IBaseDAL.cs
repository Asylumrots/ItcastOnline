using Itcast.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itcast.IDAL
{
    public interface IBaseDAL<T> where T : class, new()
    {
        IQueryable<T> LoadEntity(System.Linq.Expressions.Expression<Func<T, bool>> whereLamda);
        bool AddEntity(T Entity);
        bool DeleteEntity(T Entity);
        bool ModifyEntity(T Entity);
    }
}
