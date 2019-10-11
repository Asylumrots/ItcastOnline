using Itcast.IBLL;
using Itcast.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itcast.BLL
{
    public class MsgManager : BaseManager<Msg>,IMsgBLL
    {
        public bool AddUserEntities(int id,string txt, out int nums)
        {
            this.CurrentBbSession.MsgDal.AddUserEntities(id, txt, out nums);
            return this.CurrentBbSession.SaveChanges();
        }

        public override void SetCurrentDAL()
        {
            CurrentDAL = this.CurrentBbSession.MsgDal;
        }
    }
}
