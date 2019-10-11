using Itcast.IBLL;
using Itcast.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itcast.BLL
{
    public class MarkManager : BaseManager<Mark>,IMarkBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.CurrentBbSession.MarkDal;
        }

        public Dictionary<string, string> GetNameAndScore(int id)
        {
            return CurrentBbSession.MarkDal.GetNameAndScore(id);
        }
    }
}
