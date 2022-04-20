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
                // ����ѭ������
                options.SerializerSettings.ReferenceLoopHandling =
                ReferenceLoopHandling.Ignore;
                // ��ʹ���շ�
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                // ����ʱ���ʽ
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                // ���ֶ�Ϊ null ֵ�����ֶβ��᷵�ص�ǰ��
                // options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            //ע��Swagger������������һ���Ͷ��Swagger �ĵ�
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Brston ������Ϣ��ѯAPI",
                    Version = "v1",
                    Description = "@2022 �������ʯ�г��о����޹�˾��Ȩ����"
                });
                // Ϊ Swagger JSON and UI����xml�ĵ�ע��·��
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//��ȡӦ�ó�������Ŀ¼�����ԣ����ܹ���Ŀ¼Ӱ�죬������ô˷�����ȡ·����
                var xmlPath = Path.Combine(basePath, "BrstonApi.xml");
                c.IncludeXmlComments(xmlPath);
            });
            

            #region BrstonApiContext
            //ÿһ��HTTP���󣬻Ὠ��һ���µķ���ʵ��
            services.AddScoped<IUsersRepository, UsersRepository>();
            //DbContextע�ᵽDI������
            //ʹ��SqlServer���ݿ�
            //��ȡ�����ļ������ݿ������ַ���
            services.AddDbContext<BrstonApiContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("BrstonApi")));
            #endregion

            #region VehicleBaseContext
            //ÿһ��HTTP���󣬻Ὠ��һ���µķ���ʵ��
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            //DbContextע�ᵽDI������
            //ʹ��SqlServer���ݿ�
            //��ȡ�����ļ������ݿ������ַ���
            services.AddDbContext<VehicleBaseContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SearchVIN")));
            #endregion

            #region ע��������ӿ�
            services.AddControllersWithViews();
            //ע��������ӿ�
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

            //�����м����������Swagger��ΪJSON�ս��
            app.UseSwagger();
            //�����м�������swagger-ui��ָ��Swagger JSON�ս��
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BrstonAPI V1");
            });


        }
    }
}
