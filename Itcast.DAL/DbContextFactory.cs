using Itcast.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Itcast.DAL
{
    /// <summary>
    /// 负责EF数据操作上下文一致，保证线程内唯一
    /// </summary>
    public class DbContextFactory
    {
        public static DbContext CreateDbContext()
        {
            DbContext DbContext = (DbContext)CallContext.GetData("dbContext");
            if (DbContext == null)
            {
                DbContext = new ItcastEntities();
                CallContext.SetData("dbContext", DbContext);
            }
            return DbContext;
        }
    }
}
