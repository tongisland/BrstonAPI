using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrstonApi.Entities.Detail.branch
{
    public class Chassis
    {
        public string DriveHubDiameter { get; set; }
        public string DriveTireHeightAspectRatio { get; set; }
        public string DriveTireWidth { get; set; }
        public string DrivingMethod { get; set; }
        public string FWDMethod { get; set; }
        public string FrontBrake { get; set; }
        public string FrontSuspension { get; set; }
        public string FrontTireSize { get; set; }
        public string PowerSteering { get; set; }
        public string RearBrake { get; set; }
        public string RearSuspension { get; set; }
        public string RearTireSize { get; set; }
        public string SpareTire { get; set; }
        public string Transmission { get; set; }
        public string FrontHubMaterial { get; set; }
        public string GearMode { get; set; }
        public string RearHubMaterial { get; set; }
        public string SpareTireHubMaterial { get; set; }
        public string SpareTireNum { get; set; }
        public string SpareTireNumSize { get; set; }
        public string SteeringSystem { get; set; }
    }
}
