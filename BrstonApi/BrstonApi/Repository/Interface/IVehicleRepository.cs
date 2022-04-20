using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrstonApi.Entities;

namespace BrstonApi.Repository
{
    public interface IVehicleRepository
    {
        //Task<VehicleBaseModel> GetVehicleBaseInfoByVIN(string VIN);
        Task<Dictionary<string, string>> GetVehicleInfo(string VIN, List<string> columns);
    }
}
