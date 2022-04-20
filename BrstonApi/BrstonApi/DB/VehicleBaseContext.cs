using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BrstonApi.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BrstonApi.DB
{
    public class VehicleBaseContext : DbContext
    {
        public VehicleBaseContext(DbContextOptions<VehicleBaseContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchVINView>(builder =>
            {
                builder.HasBaseType((Type)null); // 不认为SearchVINView作为数据库继承的一部分
                builder.ToView(null); // 说明它没有关联表的最简单方法
                builder.HasKey(e => e.ID); //或使用HasNoKey()将其视为无主键
            });
        }
    }
}
