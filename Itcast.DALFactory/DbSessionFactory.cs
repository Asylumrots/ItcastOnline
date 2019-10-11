using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Itcast.IDAL;
using Itcast.DALFactory;

namespace Itcast.DALFactory
{
    public class DbSessionFactory
    {
        public static IDbSession CreateDbSession()
        {
            IDbSession DbSession = (IDbSession)CallContext.GetData("dbSession");
            if (DbSession == null)
            {
                DbSession = new DbSession();
                CallContext.SetData("dbSession", DbSession);
            }
            return DbSession;
        }
    }
}
