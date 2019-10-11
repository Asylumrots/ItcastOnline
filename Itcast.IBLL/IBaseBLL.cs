using Itcast.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itcast.IBLL
{
    public interface IBaseBLL<T> where T:class,new()
    {
        IDbSession CurrentBbSession { get; }
        IQueryable<T> LoadEntity(System.Linq.Expressions.Expression<Func<T, bool>> whereLamda);
        bool AddEntity(T Entity);
        bool ModifyEntity(T Entity);
        bool DeleteEntity(T Entity);
    }
}
