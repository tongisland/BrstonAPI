using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrstonApi.Entities;

namespace BrstonApi.Repository
{
    public interface IUsersRepository
    {
        /// <summary>
        /// 根据用户密钥查询用户信息
        /// </summary>
        /// <param name="userkey"></param>
        /// <returns></returns>
        Task<Users> GetUserByKey(string userkey);

        /// <summary>
        /// 添加一条用户调用日志
        /// </summary>
        /// <param name="log"></param>
        void AddUserLog(UserLog log);

        /// <summary>
        /// 更新用户调用总次数
        /// </summary>
        /// <param name="userkey"></param>
        void UpdateUserTotalCount(string userkey);

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="userkey"></param>
        /// <param name="username"></param>
        /// <param name="phonenumber"></param>
        Task UpdateUserInfoByUser(string userkey,string username,string phonenumber,string useremail);

        /// <summary>
        /// 根据用户密钥获取显示的列
        /// </summary>
        /// <param name="userkey"></param>
        /// <returns></returns>
        Task<List<string>> GetColumnsByUser(string userkey);

        /// <summary>
        /// 异步保存到数据库方法
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveAsync();
        Task<List<Thethirdparty>> TheThirdParty(string VIN, string City, DateTime Date);
    }
}
