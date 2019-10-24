using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itcast.IDAL
{
    public interface IDbSession
    {
        DbContext Db { get; }
        IUserInfoDAL UserInfoDal { get; set; }
        ITestDAL TestDal { get; set; }
        ITopicDAL TopicDal { get; set; }
        IMarkDAL MarkDal { get; set; }
        IMsgDAL MsgDal { get; set; }
        bool SaveChanges();
    }
}
