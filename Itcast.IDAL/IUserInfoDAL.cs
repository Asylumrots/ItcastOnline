using Itcast.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itcast.IDAL
{
    public interface IUserInfoDAL : IBaseDAL<UserInfo>
    {
        //定义特有方法
        UserInfo GetUserInfo(string name);
    }
}
