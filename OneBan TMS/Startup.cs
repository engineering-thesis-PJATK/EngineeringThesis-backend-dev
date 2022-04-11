using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OneBan_TMS.Controllers;
using OneBan_TMS.Handlers;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Repository;
using OneBan_TMS.Validators;
using Swashbuckle.AspNetCore.Filters;

namespace OneBan_TMS
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
            var connectionString = Configuration.GetConnectionString("SQLConnection");
            services.AddDbContextPool<OneManDbContext>(options =>
                options.UseSqlServer(connectionString));
            
            
            
            services.AddScoped<IValidator<CompanyDto>, CompanyValidator>();
            services.AddScoped<IValidator<Employee>, EmployeeValidator>();
            services.AddScoped<IValidatorHandler, ValidatorHandler>();
            services.AddScoped<ICompanyHandler, CompanyHandler>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITicketRepository, TicketRepository>();
            services.AddTransient<IEmployeeTeamRoleRepository, EmployeeTeamRoleRepository>();
            services.AddTransient<IProjectTaskRepository, ProjectTaskRepository>();
            services.AddSingleton<IPasswordHandler, PasswordHandler>();
            services.AddSingleton<ITokenHandler, CustomTokenHandler>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings").GetSection("Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddControllers();
            //services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CompanyValidator>());
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OneBan_TMS", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard authorization header using the Bearer scheme",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            services.AddCors();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:44351", "http://localhost:4200")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowOrigin",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:44351", "http://localhost:4200")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OneBan_TMS v1"));
            }
            app.UseCors();
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}