using it.example.dotnetcore5.dal.webapi.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace it.example.dotnetcore5.dal.webapi.Interfaces
{
    public interface IPostsApi
    {
        [Get("/posts?_start=5&_limit=5")]
        Task<List<WebPost>> GetAllPosts();

        [Get("/posts/{id}")]
        Task<WebPost> GetPostById(long id);
    }
}
