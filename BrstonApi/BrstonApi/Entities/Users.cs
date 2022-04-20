using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrstonApi.Entities
{
    [Serializable]
    public class Users
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }
        public string CreateTime { get; set; }
        public string UpdateTime { get; set; }
        public string Remarks { get; set; }
        public decimal? BalanceMoney { get; set; }
        public int? BalanceCount { get; set; }

        public  int? TotalCount { get; set; }

        /// <summary>
        /// 接口调用类型 1：标准版 2：豪华版 3：豪华增价版
        /// </summary>
        public int? VersionCode { get; set; }

        public string VersionName { get; set; }

        public string UserKey { get; set; }
        public string PhoneNumber { get; set; }

        public string UserUpdateTime { get; set; }
        [Required(ErrorMessage ="邮箱不能为空")]
        public string UserEmail { get; set; }
        public int TopLimit { get; set; } = 10;

    }
}
