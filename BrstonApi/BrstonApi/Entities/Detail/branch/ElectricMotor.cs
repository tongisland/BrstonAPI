using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrstonApi.Entities.Detail.branch
{
    public class ElectricMotor
    {
        public string BatteryCapacity { get; set; }
        public string ElectromotorModel { get; set; }
        public string HyBridType { get; set; }
        public string MaximumMileage { get; set; }
        public string MaximumPower { get; set; }
        public string PeakTorque { get; set; }
    }
}
