using System.Text;
using TodoAPI.Models;
using TodoAPI.Services;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;

namespace TodoAPI
{
    public class Startup
    {

        //---------------------------------------------------------------------------------------------------------------------//

        private bool InDocker { get { return Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true"; } }

        //---------------------------------------------------------------------------------------------------------------------//

        public IConfiguration Configuration { get; }

        //---------------------------------------------------------------------------------------------------------------------//

        public Startup(IConfiguration p_Configuration)
        {
            this.Configuration = p_Configuration;
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public void ConfigureServices(IServiceCollection p_Services)
        {
            // Cors
            p_Services.AddCors();

            // Controllers
            p_Services.AddControllers();

            // Get ConnectionString
            string connStr = this.InDocker ? this.Configuration["ConnectionStrings:mssql2017_Docker"] : this.Configuration["ConnectionStrings:mssql2017_Local"];

            // DbContext
            p_Services.AddDbContext<TodoDBContext>(options => options.UseSqlServer(connStr));

            // Swagger
            p_Services.AddSwaggerGen(x => x.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoAPI", Version = "v1" }));

            // Key
            var secretKey = this.Configuration.GetSection("AppSettings:SecretKey").Value;

            // Authentication (JWT)
            p_Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                };
            });

            // Authorization
            p_Services.AddAuthorization();

            // Add UserService(DI)
            p_Services.AddScoped<IUserService, UserService>();

            // Add TodoService(DI)
            p_Services.AddScoped<ITodoService, TodoService>();

            // Log Settings
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public void Configure(IApplicationBuilder p_Application, IWebHostEnvironment p_Environment)
        {
            // Routing
            p_Application.UseRouting();

            // Authentication
            p_Application.UseAuthentication();

            // Authorization
            p_Application.UseAuthorization();

            // Development
            if (p_Environment.IsDevelopment())
            {
                // ExceptionPage
                p_Application.UseDeveloperExceptionPage();

                // Swagger
                p_Application.UseSwagger();
                p_Application.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoAPIv1"));
            }
            else if (p_Environment.IsProduction())
            {
                p_Application.UseHsts();
                p_Application.UseHttpsRedirection();
            }

            // Cors
            p_Application.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            // Endpoints
            p_Application.UseEndpoints(x => x.MapControllers());

        }

        //---------------------------------------------------------------------------------------------------------------------//

    }
}