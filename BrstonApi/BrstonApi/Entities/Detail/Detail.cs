using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrstonApi.Entities.Detail
{
    public class Detail
    {
        //Basic
        public object Basic { get; set; }
        //Body
        public object Body { get; set; }
        //Chassis
        public object Chassis { get; set; }
        //Driving
        public object Driving { get; set; }
        //ElectricMotor
        public object ElectricMotor { get; set; }
        //Engine
        public object Engine { get; set; }
        //Truck
        public object Truck { get; set; }
    }
}
