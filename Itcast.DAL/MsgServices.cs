using Itcast.IDAL;
using Itcast.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itcast.DAL
{
    public class MsgServices : BaseServices<Msg>, IMsgDAL
    {
        public void AddUserEntities(int id, string txt, out int nums)
        {
            nums = 0;
            UserInfoServices userInfoServices = new UserInfoServices();
            var list = userInfoServices.LoadEntity(u => u.Grade == 0);
            foreach (var item in list)
            {
                Msg msg = new Msg();
                msg.SendId = id;
                msg.ReceiveId = item.Id;
                msg.MsgTxt = txt;
                msg.MsgSendTime = DateTime.Now.ToLocalTime();
                this.AddEntity(msg);
                nums++;
            }
        }
    }
}
