using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flexion.User.API.Controllers;
using Flexion.User.API.Models.GraphQLModels.InputTypes;
using Flexion.User.API.Models.GraphQLModels.ModelTypes;
using Flexion.User.API.Operations;
using Flexion.User.Application.ApplicationInterface;
using Flexion.User.Domain;
using Flexion.User.Domain.DomainInterface;
//using Flexion.User.Domain;
//using Flexion.User.Domain.DomainInterface;
using Flexion.User.Infrastructure.InfrastructureInterface;
using Flexion.User.Infrastructure.Persistence;
using Flexion.User.Infrastructure.Persistence.Repositories;
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

namespace Flexion.User.API
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
            services.AddDbContext<UserDBContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:UserDB"]));
            services.AddTransient<IUserRepository, UserRepository>();
            #endregion
            #region DomainServices
            services.AddTransient<IUserService, UserService>();
            #endregion
            #region ApplicationDriver
            services.AddTransient<IUserApplicationDriver, UserApplicationDriver>();
            #endregion
            services.AddScoped<ResourceFilter>();
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddScoped<UserQuery>();
            services.AddScoped<UserMutation>();

             services.AddTransient<StudentType>();
              services.AddTransient<UserRoleType>();
             services.AddTransient<TeacherType>();
            services.AddTransient<StudentInputType>();
            services.AddTransient<TeacherInputType>();



            var sp = services.BuildServiceProvider();
            services.AddScoped<ISchema>(_ => new UserSchema(new FuncDependencyResolver(type => (GraphType)sp.GetService(type))));

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
