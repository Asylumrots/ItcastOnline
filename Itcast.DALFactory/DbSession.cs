using Itcast.IDAL;
using Itcast.DAL;
using Itcast.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Itcast.DALFactory
{
    //数据会话层
    //工厂类，实现所有数据操作类的实例，业务层可以通过会话层来直接获取操作类的实例，实现两层的连接耦合
    //在一个中实现所有数据的保存
    public class DbSession:IDbSession
    {
        public DbContext Db { get { return DbContextFactory.CreateDbContext(); } }

        private IUserInfoDAL _UserInfoDal;
        public IUserInfoDAL UserInfoDal
        {
            get
            {
                if (_UserInfoDal == null)
                {
                    //_UserInfoDal = new UserInfoDal();
                    _UserInfoDal = AbstractFactory.CreateUserInfoDAL();//通过抽象工厂封装了类的实例的创建
                }
                return _UserInfoDal;
            }
            set
            {
                _UserInfoDal = value;
            }
        }

        private ITestDAL _TestDal;
        public ITestDAL TestDal
        {
            get
            {
                if (_TestDal == null)
                {
                    _TestDal = AbstractFactory.CreateTestDAL();//通过抽象工厂封装了类的实例的创建
                }
                return _TestDal;
            }
            set
            {
                _TestDal = value;
            }
        }

        private ITopicDAL _TopicDal;
        public ITopicDAL TopicDal
        {
            get
            {
                if (_TopicDal == null)
                {
                    _TopicDal = AbstractFactory.CreateTopicDAL();//通过抽象工厂封装了类的实例的创建
                }
                return _TopicDal;
            }
            set
            {
                _TopicDal = value;
            }
        }

        private IMarkDAL _MarkDal;
        public IMarkDAL MarkDal
        {
            get
            {
                if (_MarkDal == null)
                {
                    _MarkDal = AbstractFactory.CreateMarkDAL();//通过抽象工厂封装了类的实例的创建
                }
                return _MarkDal;
            }
            set
            {
                _MarkDal = value;
            }
        }

        private IMsgDAL _MsgDal;
        public IMsgDAL MsgDal
        {
            get
            {
                if (_MsgDal == null)
                {
                    _MsgDal = AbstractFactory.CreateMsgDAL();//通过抽象工厂封装了类的实例的创建
                }
                return _MsgDal;
            }
            set
            {
                _MsgDal = value;
            }
        }

        /// <summary>
        /// 一个业务中实现对多张表的操作，在连接一次数据库的过程中完成对多张表的操作,工作单元模式
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            return Db.SaveChanges() > 0;
        }
    }
}
