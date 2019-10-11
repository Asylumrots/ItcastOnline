using Itcast.DAL;
using Itcast.Model;
using Itcast.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Itcast.IDAL;

namespace Itcast.BLL
{
    public class UserInfoManager : BaseManager<UserInfo>, IUserInfoBLL
    {
        IUserInfoDAL userInfoDAL = new UserInfoServices();
        //根据name获取用户数据
        public UserInfo GetUserInfo(string name)
        {
            return userInfoDAL.GetUserInfo(name);
        }

        public override void SetCurrentDAL()
        {
            CurrentDAL = this.CurrentBbSession.UserInfoDal;
        }
    }
}
