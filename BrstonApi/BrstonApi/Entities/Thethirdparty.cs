using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrstonApi.Entities
{
    public class Thethirdparty
    {
        //Detail
        public object Detail { get; set; }
        //Model
        public object Model { get; set; }
        //NewPrice
        public object NewPrice { get; set; }
        //UsedPrice
        public object UsedPrice { get; set; }
        //DiffConfigList
        public object DiffConfigList { get; set; }
        public string EngineNo { get; set; }
        public string GB { get; set; }
        public string ProductionDate { get; set; }
        public string Mfrs { get; set; }
        public string Series { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
    }
}
