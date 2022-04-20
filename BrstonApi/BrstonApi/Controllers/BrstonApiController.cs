using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BrstonApi.Entities;
using BrstonApi.Repository;
using BrstonApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BrstonApi.Controllers
{
    /// <summary>
    /// @Api(description = "Brston车辆查询接口") 
    /// </summary>
    [ApiController]
    [Route("brston")]
    public class BrstonApiController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IVehicleRepository _vehicleRepository;
        public BrstonApiController(IUsersRepository usersRepository, IVehicleRepository vehicleRepository)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
        }

        /// <summary>
        /// VIN码解析接口
        /// </summary>
        /// <param name="userkey">用户密钥</param>
        /// <param name="VIN">车辆VIN号</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api")]
        public async Task<ActionResult<ApiResultModel>> QueryCarByVIN(string userkey,  string VIN)
        {
            //声明返回值
            ApiResultModel resultModel = new ApiResultModel();

            try
            {
                //校验登录信息
                var userinfo = await _usersRepository.GetUserByKey(userkey);
                if (userinfo == null)
                {
                    resultModel.status = 1;
                    resultModel.msg = "用户验证未通过";

                    return resultModel;
                }
                else
                {
                    //如果校验通过，查询车辆信息(需要获取用户使用版本)
                    //获取用户所属版本的列
                    List<string> listColumn = await _usersRepository.GetColumnsByUser(userkey);
                    #region MD5加密VIN
                    byte[] bt = Encoding.UTF8.GetBytes(VIN);
                    //创建默认实现的实例
                    var md5 = MD5.Create();
                    //计算指定字节数组的哈希值。
                    var md5bt = md5.ComputeHash(bt);
                    //将byte数组转换为字符串
                    StringBuilder builder = new StringBuilder();
                    foreach (var item in md5bt)
                    {
                        builder.Append(item.ToString("X2"));
                    }
                    string md5Str = builder.ToString();
                    #endregion

                    var vehiclebaseinfo = await _vehicleRepository.GetVehicleInfo(md5Str, listColumn);

                    //记录用户调用接口信息，更新用户调用总次数
                    UserLog log = new UserLog();
                    log.userkey = userkey;
                    log.VIN = VIN;
                    log.respStr = JsonConvert.SerializeObject(vehiclebaseinfo);
                    log.createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    //记录用户调用日志
                    _usersRepository.AddUserLog(log);

                    //更新用户调用总次数
                    _usersRepository.UpdateUserTotalCount(userkey);

                    await _usersRepository.SaveAsync();


                    if (vehiclebaseinfo.Count == 0)
                    {
                        resultModel.status = 1;
                        resultModel.msg = "车辆信息查询失败，没有查询数据或查询次数上限，请联系管理员";
                        resultModel.data = vehiclebaseinfo;
                        return resultModel;
                    }
                    else
                    {
                        resultModel.status = 1;
                        resultModel.msg = "车辆信息查询成功";
                        resultModel.data = vehiclebaseinfo;
                        return resultModel;
                    }
                }
            }
            catch(Exception ex)
            {
                resultModel.status = -1;
                resultModel.msg = "查询异常";
                resultModel.errormsg = ex.Message;
                return resultModel;
            }
            
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="userkey">用户密钥</param>
        /// <param name="username">用户名称（必填）</param>
        /// <param name="phonenumber">手机号</param>
        /// <param name="useremail">邮箱（必填）</param>
        /// <returns></returns>
        [HttpGet]
        [Route("regist")]
        public async Task<ActionResult<ApiResultModel>> Regist(string userkey,string username,string phonenumber,string useremail)
        {
            //声明返回值
            ApiResultModel resultModel = new ApiResultModel();

            try
            {
                //校验登录信息
                var existUser = await _usersRepository.GetUserByKey(userkey);

                if (existUser == null)
                {
                    resultModel.status = 1;
                    resultModel.msg = "注册失败！用户密钥不存在";

                    return resultModel;
                }
                else
                {
                    if (username == null|| useremail == null)
                    {
                        resultModel.status = 1;
                    resultModel.msg = "注册失败！必填项为空";

                    return resultModel;
                    }
                    else
                    {
                        //更新用户信息

                        await _usersRepository.UpdateUserInfoByUser(userkey, username, phonenumber, useremail);

                        await _usersRepository.SaveAsync();

                        resultModel.status = 1;
                        resultModel.msg = "注册成功";

                        return resultModel;
                    }
                }
            }
            catch (Exception ex)
            {
                resultModel.status = -999;
                resultModel.msg = "查询异常";
                resultModel.errormsg = ex.Message;
                return resultModel;
            }

        }

        /// <summary>
        /// 调用第三方接口
        /// </summary>
        /// <param name="VIN"></param>
        /// <param name="City"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Third-party Interface")]
        public async Task<ActionResult<ApiResultModel>> TheThirdParty(string VIN, string City, DateTime Date)
        {
            ApiResultModel resultModel = new ApiResultModel();
            try
            {
                List<Thethirdparty> holidays = new List<Thethirdparty>();
                holidays = await _usersRepository.TheThirdParty(VIN, City, Date);
                if (holidays.Count != 0)
                {
                    resultModel.status = 1;
                    resultModel.msg = "车辆信息查询成功";
                    resultModel.data = holidays;
                    return resultModel;
                }
                else
                {
                    resultModel.status = 2;
                    resultModel.msg = "车辆信息查询成功";
                    resultModel.data = holidays;
                    return resultModel;
                }
            }
            catch (Exception ex)
            {
                resultModel.status = -1;
                resultModel.msg = "查询异常";
                resultModel.errormsg = ex.Message;
                return resultModel;
            }
        }

        /// <summary>
        /// 测试方法 
        /// 克隆List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("test")]
        public async Task<ActionResult<ApiResultModel>> Test()
        {
            //声明返回值
            ApiResultModel resultModel = new ApiResultModel();
            List<string> listResult = new List<string>();
            try
            {
                
                List<SearchVINView> list = new List<SearchVINView>();
                SearchVINView model1 = new SearchVINView();
                model1.企业集团 = "京东集团";
                model1.制造商 = "京东京造";
                list.Add(model1);
                SearchVINView model2 = new SearchVINView();
                model2.企业集团 = "阿里巴巴";
                model2.制造商 = "阿里";
                list.Add(model2);

                foreach (SearchVINView entity in list)
                {
                    Type type = typeof(SearchVINView);
                    var property = type.GetProperty("企业集团");
                    listResult.Add(property.GetValue(entity).ToString());
                }

                resultModel.status = 1;
                resultModel.msg = "执行成功";
                resultModel.data = listResult;

                return resultModel;
            }
            catch (Exception ex)
            {
                resultModel.status = -999;
                resultModel.msg = "查询异常";
                resultModel.errormsg = ex.Message;
                return resultModel;
            }

        }

    }
}
