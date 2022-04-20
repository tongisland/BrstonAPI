using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrstonApi.Entities
{
    [Serializable]
    [Table("S_UserLog")]
    public class UserLog
    {
        [Key]
        public int ID { get; set; }

        public string userkey { get; set; }

        public string VIN { get; set; }


        public string createtime{get;set;}

        public string respStr { get; set; }
    }
}
