using AlunosApi.Context;
using AlunosApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace AlunosApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAlunoService, AlunosService>(); 
            services.AddSwaggerGen (c =>
            {
                c. SwaggerDoc("v1", new OpenApiInfo { Title = "Alunos.API", Version = "v1" });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c=> c.SwaggerEndpoint("/swagger/v1/swagger.json" , "AlunosApi v1"));
            }

            app.UseCors(options=>
            {
                options.WithOrigins("http://localhost:3001", "http://localhost:3000");
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>{
                endpoints.MapControllers();
            });
        }
    }
}