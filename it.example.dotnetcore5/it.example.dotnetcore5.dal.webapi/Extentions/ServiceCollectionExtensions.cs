using it.example.dotnetcore5.dal.webapi.Repositories;
using it.example.dotnetcore5.domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace it.example.dotnetcore5.dal.webapi.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDalWebApi(this IServiceCollection services)
        {
            services.AddScoped<IPostsRepository, PostsRepository>();
        }
    }
}