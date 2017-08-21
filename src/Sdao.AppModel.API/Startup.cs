using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Sdao.AppModel.Data;
using Sdao.AppModel.Data.Abstract;
using Sdao.AppModel.Data.Repositories;
using Sdao.AppModel.API.Models.Mappings;
using Sdao.AppModel.API.Convention;

namespace AppModel
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
       public IConfiguration Configuration { get; }
     

        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Configuration = con
            //获取数据库连接字符串
            //var sqlConnectionString = Configuration.GetSection("ConnectionStrings")["DefaultConnection"];
            //sqlConnectionString=Configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            //sqlConnectionString = Configuration.GetConnectionString("DefaultConnection");
            var sqlConnectionString= Configuration["ConnectionStrings:DefaultConnection"];
            //var sqlConnectionString = Environment.GetEnvironmentVariables()["ConnectionStrings:DefaultConnection"].ToString();

            // Add framework services.
            services.AddOptions();

            var pathToDoc = Configuration["OpenApi:XmlDocName"];

            services.AddDbContext<AppModelContext>(options =>
            {
                //dotnet ef migrations add "initial"
                //dotnet ef database update
                options.UseNpgsql(sqlConnectionString,
              b => b.MigrationsAssembly("Sdao.AppModel.API"));
            });
            services.AddSingleton<IConfiguration>(Configuration);
            //Repositories存储库类型
            services.AddScoped<IContainerRepository, ContainerRepository>();
           
            // Automapper Configuration
            AutoMapperConfiguration.Configure();

            // Enable Cors
            services.AddCors();

            // Add framework services.
            services.AddMvc(options =>
            {
                options.MaxModelValidationErrors = 50;
                options.Conventions.Add(new ApiExplorerGroupPerVersionConvention());//swagger约定:根据Controller的命名空间去判断

            }).AddJsonOptions(opts =>
            {
                // Force Camel Case to JSON 驼峰命令
                opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;//Self referencing loop detected when serializing Data models in EF core
                opts.SerializerSettings.Formatting = Formatting.Indented;//显示格式,可读性更好
                //参考http://stackoverflow.com/questions/25274817/self-referencing-loop-detected-when-serializing-data-models-in-mvc5-ef6
                //https://code.msdn.microsoft.com/Loop-Reference-handling-in-caaffaf7#content
                //https://github.com/aspnet/Mvc/issues/3093
            });

            services.AddLogging();

            //Swagger
            //services.AddSwaggerGen();
            //services.ConfigureSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("v1",
            //        new Info
            //        {
            //            Title = "Search API",
            //            Version = Assembly.GetEntryAssembly().GetName().Version.ToString(),
            //            Description = "My Api",
            //            TermsOfService = "None"
            //        }
            //    );

            //    var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, pathToDoc);
            //    options.IncludeXmlComments(filePath);
            //    options.DescribeAllEnumsAsStrings();
            //});


            services.AddSwaggerGen(c =>
            {
                //配置多个swagger.json
                c.SwaggerDoc("v0", new Info { Title = "My API - V0", Version = Assembly.GetEntryAssembly().GetName().Version.ToString(), Description = Assembly.GetEntryAssembly().GetName().Version.ToString() + " - My Api 0 - 未登录", TermsOfService = "None" });//未登录
                c.SwaggerDoc("v1", new Info { Title = "My API - V1", Version = Assembly.GetEntryAssembly().GetName().Version.ToString(), Description = Assembly.GetEntryAssembly().GetName().Version.ToString() + " - My Api 1 - 普通登录用户", TermsOfService = "None" });//登录用户
                c.SwaggerDoc("v2", new Info { Title = "My API - V2", Version = Assembly.GetEntryAssembly().GetName().Version.ToString(), Description = Assembly.GetEntryAssembly().GetName().Version.ToString() + " - My Api 2 - 运维人员", TermsOfService = "None" });//管理人员
                c.SwaggerDoc("v3", new Info { Title = "My API - V3", Version = Assembly.GetEntryAssembly().GetName().Version.ToString(), Description = Assembly.GetEntryAssembly().GetName().Version.ToString() + " - My Api 3 - 超级管理员", TermsOfService = "None" });//超级管理员
                //c.TagActionsBy(api => api.HttpMethod);
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, pathToDoc);
                c.IncludeXmlComments(filePath);
                c.DescribeAllEnumsAsStrings();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Add MVC to the request pipeline.
            app.UseCors(builder =>
               builder
               .AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod());

            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            //添加在app.UseMvc() 前面
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value);
            });

            app.UseSwaggerUi(c =>
            {
                c.SwaggerEndpoint("/swagger/v0/swagger.json", Assembly.GetEntryAssembly().GetName().Version.ToString() + "-My Api V0 Docs-未登录");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", Assembly.GetEntryAssembly().GetName().Version.ToString() + "-My Api V1 Docs-登录用户");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", Assembly.GetEntryAssembly().GetName().Version.ToString() + "-My Api V2 Docs-运维用户");
                c.SwaggerEndpoint("/swagger/v3/swagger.json", Assembly.GetEntryAssembly().GetName().Version.ToString() + "-My Api V3 Docs-超级管理员");
            });

            app.UseMvc();

            
        }
    }
}
