using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrstonApi.Entities
{
    [Serializable]
    [Table("B_APIVersion")]
    public class ApiVersion
    {
        public int ID { get; set; }
        public string VersionName { get; set; }
        public decimal? UnitPrice { get; set; }

        public int VersionCode { get; set; }

        public string CreateTime { get; set; }
        public string UpdateTime { get; set; }

        public string Columns { get; set; }
    }
}
