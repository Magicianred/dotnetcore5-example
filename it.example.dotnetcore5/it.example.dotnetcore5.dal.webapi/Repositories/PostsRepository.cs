using it.example.dotnetcore5.dal.webapi.Factories;
using it.example.dotnetcore5.dal.webapi.Interfaces;
using it.example.dotnetcore5.domain.Interfaces.Models;
using it.example.dotnetcore5.domain.Interfaces.Repositories;
using Refit;
using System.Collections.Generic;

namespace it.example.dotnetcore5.dal.webapi.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        public void AddPost(IPost item)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IPost> GetAll()
        {
            var postsApi = RestService.For<IPostsApi>("https://jsonplaceholder.typicode.com");
            var posts = postsApi.GetAllPosts().GetAwaiter();
            return posts.GetResult().ToModelsDomain();
        }

        public IPost GetById(int id)
        {
            var postApi = RestService.For<IPostsApi>("https://jsonplaceholder.typicode.com");
            var post = postApi.GetPostById(id).GetAwaiter();
            return post.GetResult().ToModelDomain();
        }
    }
}
