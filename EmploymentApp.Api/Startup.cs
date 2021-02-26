using AutoMapper;
using EmploymentApp.Core.CustomEntities;
using EmploymentApp.Core.Interfaces;
using EmploymentApp.Core.Services;
using EmploymentApp.Infrastructure.Data;
using EmploymentApp.Infrastructure.Interfaces;
using EmploymentApp.Infrastructure.Repositories;
using EmploymentApp.Infrastructure.Serices;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace EmploymentApp.Api
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
            services.AddDbContext<EmploymentDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("EmploymentDb"));
            });

            services.Configure<PaginationOptions>(Configuration.GetSection("Pagination"));
            services.Configure<AuthenticationOptions>(Configuration.GetSection("Authentication"));
            services.AddHttpContextAccessor();
            services.AddSingleton<IUriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUri);
            }); 

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IStatusService, StatusService>();
            services.AddTransient<IRoleServices, RoleService>();
            services.AddTransient<ITypeScheduleService, TypeScheduleService>();
            services.AddTransient<IJobService, JobService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserLoginService, UserLoginService>();
            services.AddTransient<ITokenService, TokenService>();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true, 
                    ValidIssuer = Configuration["Authentication:Issuer"], 
                    ValidAudience = Configuration["Authentication:Audience"], 
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]))
                };
            });   


            services.AddMvc().AddFluentValidation(options => {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
                });

            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo{Title="EmploymentApp Api", Version ="v1" }); ;
            });
          
            services.AddControllers().AddNewtonsoftJson(options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "EmploymentApp Api");
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
