using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrstonApi.DB;
using BrstonApi.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BrstonApi.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VehicleBaseContext _context;
        public VehicleRepository(VehicleBaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //public async Task<VehicleBaseModel> GetVehicleBaseInfoByVIN(string VIN)
        //{
        //    return await _context.VehicleBaseItems.Where(w => w.车辆识别代号 == VIN ).FirstOrDefaultAsync();
        //}

        public async Task<Dictionary<string, string>> GetVehicleInfo(string VIN,List<string> columns)
        {
            
            var templist = await _context.Set<SearchVINView>() // <-- 这就是通过显式的' DbSet '属性访问它的方式
                .FromSqlRaw("EXEC SearchVIN @VIN;",
                    new SqlParameter("VIN", VIN)
                ).ToListAsync();

            //dynamic data = new System.Dynamic.ExpandoObject();
            //IDictionary<string, string> dictionary = (IDictionary<string, string>)data;
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            Type type = typeof(SearchVINView);
            
            foreach (SearchVINView entity in templist)
            {
                foreach (string col in columns)
                {
                    dictionary.Add(col, type.GetProperty(col).GetValue(entity) == null ? string.Empty : type.GetProperty(col).GetValue(entity).ToString());
                }
            }

            return dictionary;
        }
    }
}
