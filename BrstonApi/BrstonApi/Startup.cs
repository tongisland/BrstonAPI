using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BrstonApi.DB;
using BrstonApi.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BrstonApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options => {
                // 忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling =
                ReferenceLoopHandling.Ignore;
                // 不使用驼峰
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                // 设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                // 如字段为 null 值，该字段不会返回到前端
                // options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            //注册Swagger生成器，定义一个和多个Swagger 文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Brston 车辆信息查询API",
                    Version = "v1",
                    Description = "@2022 北京博睿黑石市场研究有限公司版权所有"
                });
                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "BrstonApi.xml");
                c.IncludeXmlComments(xmlPath);
            });
            

            #region BrstonApiContext
            //每一次HTTP请求，会建立一个新的服务实例
            services.AddScoped<IUsersRepository, UsersRepository>();
            //DbContext注册到DI容器中
            //使用SqlServer数据库
            //读取配置文件的数据库连接字符串
            services.AddDbContext<BrstonApiContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("BrstonApi")));
            #endregion

            #region VehicleBaseContext
            //每一次HTTP请求，会建立一个新的服务实例
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            //DbContext注册到DI容器中
            //使用SqlServer数据库
            //读取配置文件的数据库连接字符串
            services.AddDbContext<VehicleBaseContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SearchVIN")));
            #endregion

            #region 注入第三方接口
            services.AddControllersWithViews();
            //注入第三方接口
            services.AddHttpClient();
            services.AddControllers().AddNewtonsoftJson();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BrstonAPI V1");
            });


        }
    }
}
