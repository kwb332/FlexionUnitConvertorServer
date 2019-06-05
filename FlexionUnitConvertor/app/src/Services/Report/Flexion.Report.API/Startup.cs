using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flexion.Report.API.Controllers;
using Flexion.Report.API.Models.GraphQLModels.InputTypes;
using Flexion.Report.API.Models.GraphQLModels.ModelTypes;
using Flexion.Report.API.Operations;
using Flexion.Report.Application;
using Flexion.Report.Application.ApplicationInterface;
using Flexion.Report.Domain.DomainInterface;
using Flexion.Report.Domain.DomainService;
using Flexion.Report.Infrastructure.InfrastructureInterface;
using Flexion.Report.Infrastructure.Persistence.Repositories;
using GraphiQl;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Flexion.Report.API
{
    public class Startup
    {
        public const string GraphQlPath = "/graphql";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #region Repositories
            services.AddDbContext<ReportDBContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:ReportDB"]));
            services.AddTransient<IReportRepository, ReportRepository>();
            #endregion
            #region DomainServices
            services.AddTransient<IReportService, ReportService>();
            #endregion
            #region ApplicationDriver
            services.AddTransient<IReportApplicationDriver, ReportApplicationDriver>();
            #endregion
            #region Schema
            services.AddScoped<ResourceFilter>();
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddScoped<ReportQuery>();
            services.AddScoped<ReportMutation>();
            
            services.AddTransient<ReportType>();
            services.AddTransient<ExamQuestionType>();
            services.AddTransient<ReportInputType>();
           

            var sp = services.BuildServiceProvider();
            services.AddScoped<ISchema>(_ => new ReportSchema(new FuncDependencyResolver(type => (GraphType)sp.GetService(type))));

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });

            app.UseGraphiQl(GraphQlPath);
            app.UseMvc();
        }
    }
}
