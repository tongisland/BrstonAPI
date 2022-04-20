using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrstonApi.Entities
{
    [Serializable]
    [Table("B_UserColumns")]
    public class UserColumns
    {
        public int ID { get; set; }

        public string UserKey { get; set; }

        public string Columns { get; set; }
    }
}
