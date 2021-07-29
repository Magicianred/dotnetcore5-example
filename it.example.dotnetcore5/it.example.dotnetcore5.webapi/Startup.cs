using it.example.dotnetcore5.bl.Services;
using it.example.dotnetcore5.domain.Interfaces.Models;
using it.example.dotnetcore5.domain.Interfaces.Repositories;
using it.example.dotnetcore5.domain.Interfaces.Services;
using it.example.dotnetcore5.domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

// for dapper
using it.example.dotnetcore5.dal.dapper.Repositories;

// for json file
// using it.example.dotnetcore5.dal.json.Repositories;

// for fake repository
//using it.example.dotnetcore5.dal.fake.Repositories;

namespace it.example.dotnetcore5.webapi
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "it.example.dotnetcore5.webapi", Version = "v1" });
            });

            services.AddScoped<IPost, Post>();

            // TO DO : only for dapper - move in dapper project
            services.AddScoped<IDatabaseConnectionFactory, DatabaseConnectionFactory>();

            services.AddScoped<IPostsRepository, PostsRepository>();
            
            // for fake repository
            //services.AddScoped<IPostsRepository, FakePostsRepository>();
            
            services.AddScoped<IPostsService, PostsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "it.example.dotnetcore5.webapi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
