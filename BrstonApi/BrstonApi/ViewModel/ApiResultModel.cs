using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrstonApi.ViewModel
{
    public class ApiResultModel
    {
        /// <summary>
        /// 状态码  1：执行成功   -1:程序执行异常  
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 接口返回信息 对应状态码
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 接口返回数据信息
        /// </summary>
        public object data { get; set; }

        /// <summary>
        /// 接口异常信息
        /// </summary>
        public string errormsg { get; set; }
    }
}
