using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flexion.Test.API.Controllers;
using Flexion.Test.API.Models.GraphQLModels.InputTypes;
using Flexion.Test.API.Models.GraphQLModels.ModelTypes;
using Flexion.Test.API.Operations;
using Flexion.Test.Application;
using Flexion.Test.Application.ApplicationInterface;
using Flexion.Test.Domain;
using Flexion.Test.Domain.DomainInterface;
using Flexion.Test.Domain.DomainService;
using Flexion.Test.Infrastructure.InfrastructureInterface;
using Flexion.Test.Infrastructure.Persistence;
using Flexion.Test.Infrastructure.Persistence.Repositories;
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

namespace Flexion.Test.API
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
            services.AddDbContext<examDBContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:TestDB"]));
            services.AddTransient<ITestRepository, TestRepository>();
            #endregion
            #region DomainServices
            
            services.AddTransient<IConversionService, ConversionService>();
            services.AddTransient<ITestService, TestService>();
            #endregion
            #region ApplicationDriver
            services.AddTransient<ITestApplicationDriver, TestApplicationDriver>();
            #endregion
            #region Schema
            services.AddScoped<ResourceFilter>();
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddScoped<TestQuery>();
            services.AddScoped<TestMutation>();
            services.AddScoped<ReportType>();

            services.AddTransient<ExamType>();
            services.AddTransient<ExamInputType>();
            services.AddTransient<ExamQuestionInputType>();
            services.AddTransient<ExamAnswerInputType>();
            services.AddTransient<ConversionType>();
            services.AddTransient<ExamQuestionType>();

            var sp = services.BuildServiceProvider();
            services.AddScoped<ISchema>(_ => new TestSchema(new FuncDependencyResolver(type => (GraphType)sp.GetService(type))));

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
