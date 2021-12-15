    using API.Context;
using API.Repository;
using API.Repository.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API
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
            object p = services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
           
            services.AddScoped<EmployeeRepository>();
            services.AddScoped<AccountRepository>();
            services.AddScoped<ProfillingRepository>();
            services.AddScoped<EducationRepository>();
            services.AddScoped<UniversityRepository>();
            services.AddScoped<RoleRepository>();
            services.AddScoped<AccountRoleRepository>();

            services.AddDbContext<MyContext>(
                options => {
                    options.UseLazyLoadingProxies(true);
                    options.UseSqlServer(Configuration.GetConnectionString("APIContext"));
                    }
                );

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        // Call this to skip the default logic and avoid using the default response
                        context.HandleResponse();

                        // Write to the response in any way you wish
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            status = HttpStatusCode.Unauthorized,
                            messageResult = "Kamu Belum terotorisasi oleh sistem"
                        }));
                    }
                };
            });

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options =>
                    options.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                ) ;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowOrigin");

            app.UseHttpsRedirection();

            app.UseRouting();
            //app.Use(async (context, next) =>
            //{
            //    await next();

            //    if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            //    {
            //        await context.Response.WriteAsync("Kamu belum terOtorisasi oleh sistem");
            //    }else if(context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            //    {
            //        await context.Response.WriteAsync("Role kamu Employee tidak dapat menggunakan fitur ini");
            //    }
            //});
            
            app.UseAuthentication();
            app.UseAuthorization();

           
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });





        }
    }
}
