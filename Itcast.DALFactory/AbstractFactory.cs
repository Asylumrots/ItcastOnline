using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Itcast.IDAL;
using System.Reflection;

namespace Itcast.DALFactory
{
    public class AbstractFactory
    {
        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
        private static readonly string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
        public static IUserInfoDAL CreateUserInfoDAL()
        {
            string fullClassName = NameSpace + ".UserInfoServices";
            return CreateInstance(fullClassName) as IUserInfoDAL;
        }

        public static ITestDAL CreateTestDAL()
        {
            string fullClassName = NameSpace + ".TestServices";
            return CreateInstance(fullClassName) as ITestDAL;
        }

        public static ITopicDAL CreateTopicDAL()
        {
            string fullClassName = NameSpace + ".TopicServices";
            return CreateInstance(fullClassName) as ITopicDAL;
        }

        public static IMarkDAL CreateMarkDAL()
        {
            string fullClassName = NameSpace + ".MarkServices";
            return CreateInstance(fullClassName) as IMarkDAL;
        }

        public static IMsgDAL CreateMsgDAL()
        {
            string fullClassName = NameSpace + ".MsgServices";
            return CreateInstance(fullClassName) as IMsgDAL;
        }

        private static Object CreateInstance(string ClassName)
        {
            var assembly = Assembly.Load(AssemblyPath);
            return assembly.CreateInstance(ClassName);
        }
    }
}
