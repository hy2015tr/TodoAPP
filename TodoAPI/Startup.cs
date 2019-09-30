using System.Text;
using TodoAPI.Models;
using TodoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace TodoAPI
{
    public class Startup
    {
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

            // DbContext
            p_Services.AddDbContext<TodoDBContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            // Mvc
            p_Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Swagger
            p_Services.AddSwaggerGen(x => x.SwaggerDoc("v1", new Info { Title = "TodoAPI", Version = "v1" }));

            // Key
            var secretKey = this.Configuration.GetSection("AppSettings:SecretKey").Value;

            // JWT
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

            // Add UserService(DI)
            p_Services.AddScoped<IUserService, UserService>();

            // Add TodoService(DI)
            p_Services.AddScoped<ITodoService, TodoService>();

            // Log Settings
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public void Configure(IApplicationBuilder p_Application, IHostingEnvironment p_Environment)
        {
            // Development
            if (p_Environment.IsDevelopment())
            {
                // ExceptionPage
                p_Application.UseDeveloperExceptionPage();

                // Swagger
                p_Application.UseSwagger();
                p_Application.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoAPIv1"));
            }
            else // Production
            {
                p_Application.UseHsts();
                p_Application.UseHttpsRedirection();
            }

            // Cors
            p_Application.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            // Authentication
            p_Application.UseAuthentication();

            // Mvc
            p_Application.UseMvc();
        }

        //---------------------------------------------------------------------------------------------------------------------//

    }
}
