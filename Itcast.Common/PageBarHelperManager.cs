using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itcast.Common
{
   public class PageBarHelperManager
    {
       public static string GetPagaBar(int pageIndex, int pageCount)
       {
           if (pageCount == 1)
           {
               return string.Empty;
           }
           int start = pageIndex - 2;//计算起始位置.要求页面上显示5个数字页码.
           if (start < 1)
           {
               start = 1;
           }
           int end = start + 4;//计算终止位置.
           if (end > pageCount)
           {
               end = pageCount;
               //重新计算一下Start值.
               start = end - 4 < 1 ? 1 : end - 4;
           }
           StringBuilder sb = new StringBuilder();
           if (pageIndex > 1)
           {
               sb.AppendFormat("<a href='/Home/TopicManager?pageIndex={0}'>上一页</a>",pageIndex-1);
           }
           for (int i = start; i <= end; i++)
           {
               if (i == pageIndex)
               {
                    sb.AppendFormat("<a href='/Home/TopicManager?pageIndex={0}' style='height:24px; margin:0 3px; border:none; background:#C00; color:#fff; line-height:24px; text-decoration:none;'>{0}</a>", i);
                }
               else
               {
                   sb.AppendFormat("<a href='/Home/TopicManager?pageIndex={0}'>{0}</a>", i);
               }
           }
           if (pageIndex < pageCount)
           {
               sb.AppendFormat("<a href='/Home/TopicManager?pageIndex={0}'>下一页</a>", pageIndex + 1);
           }

           return sb.ToString();
       }
    }
}
