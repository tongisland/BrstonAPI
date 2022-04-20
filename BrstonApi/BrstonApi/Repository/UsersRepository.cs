using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BrstonApi.DB;
using BrstonApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrstonApi.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly BrstonApiContext _context;
        private static readonly HttpClient client;
        static UsersRepository()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("http://car.iautos.cn")
            };
        }
        public UsersRepository(BrstonApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Users> GetUserByKey(string userkey)
        {
            return await _context.UsersItems.Where(w => w.UserKey == userkey).FirstOrDefaultAsync();
        }

        public async Task UpdateUserInfoByUser(string userkey, string username, string phonenumber,string useremail) 
        {
            var existUser = await _context.UsersItems.Where(w => w.UserKey == userkey).FirstOrDefaultAsync();
            _context.Entry(existUser).State = EntityState.Modified;

            existUser.UserName = username;
            existUser.PhoneNumber = phonenumber;
            existUser.UserEmail = useremail;
            existUser.UserUpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 更新用户调用总次数
        /// </summary>
        /// <param name="userkey"></param>
        public void UpdateUserTotalCount(string userkey)
        {
            var userinfo = _context.UsersItems.Where(w => w.UserKey == userkey).FirstOrDefault();
            if(userinfo!=null)
            {
                _context.Entry(userinfo).State = EntityState.Modified;

                var currentCount = userinfo.TotalCount==null ? 0: userinfo.TotalCount.Value;
                if (currentCount < userinfo.TopLimit)
                {
                    userinfo.TotalCount = currentCount + 1;
                }
            }
        }
        
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="userkey"></param>
        public void UpdateUserInfoByUser(string userkey)
        {
            var userinfo = _context.UsersItems.Where(w => w.UserKey == userkey).FirstOrDefault();
            if (userinfo != null)
            {
                _context.Entry(userinfo).State = EntityState.Modified;

                var currentCount = userinfo.TotalCount == null ? 0 : userinfo.TotalCount.Value;
                userinfo.TotalCount = currentCount + 1;
            }
        }

        public void AddUserLog(UserLog log)
        {
            _context.UserLogItems.Add(log);
        }

        public async Task<List<string>> GetColumnsByUser(string userkey)
        {
            List<string> listColumns = new List<string>();
            var usercolumns =await _context.UserColumnsItems.Where(w => w.UserKey == userkey).FirstOrDefaultAsync();

            //先取用户配置列，如果取不到，则使用版本配置列
            if(usercolumns != null && !string.IsNullOrEmpty(usercolumns.Columns))
            {
                listColumns = usercolumns.Columns.Split(',').ToList<string>();
            }
            else
            {
                var userinfo = _context.UsersItems.Where(w => w.UserKey == userkey).FirstOrDefault();
                if (userinfo.TotalCount< userinfo.TopLimit)
                {
                    var user = await _context.UsersItems.Where(w => w.UserKey == userkey).FirstOrDefaultAsync();

                    var apiversion = await _context.ApiVersionItems.Where(w => w.VersionCode == user.VersionCode).FirstOrDefaultAsync();

                    if (!string.IsNullOrEmpty(apiversion.Columns))
                    listColumns = apiversion.Columns.Split(',').ToList<string>();
                }
            }

            return listColumns;
        }
        
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
        /// <summary>
        /// 调用第三方接口
        /// </summary>
        /// <param name="VIN">车架号</param>
        /// <param name="City">城市</param>
        /// <param name="Date">调用时间</param>
        /// <returns></returns>
        public async Task<List<Thethirdparty>> TheThirdParty(string VIN, string City,DateTime Date)
        {
            var time = Date.ToString("yyyy-MM-dd HH:mm:ss");
            var combination = VIN + City + time + "a14fc6ad-ca0e-4080-b05e-4fe549c31873";
            #region MD5加密VIN
            byte[] bt = Encoding.UTF8.GetBytes(combination);
            //创建默认实现的实例
            var md5 = MD5.Create();
            //计算指定字节数组的哈希值。
            var md5bt = md5.ComputeHash(bt);
            //将byte数组转换为字符串
            StringBuilder builder = new StringBuilder();
            foreach (var item in md5bt)
            {
                builder.Append(item.ToString("x2"));
            }
            string md5Str = builder.ToString();
            #endregion
            var url = string.Format($"/Maverick/Professional/Vin/GetVinSingleAllCity?key=fa01cc52-f643-42ff-88de-5608e4e536ed&vin={VIN}&city={City}&date={time}&secret={md5Str}");
            var result = new List<Thethirdparty>();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<List<Thethirdparty>>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }
    }
}
