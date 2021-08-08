using it.example.dotnetcore5.domain.Interfaces.Models;
using it.example.dotnetcore5.domain.Interfaces.Repositories;
using it.example.dotnetcore5.domain.Interfaces.Services;
using it.example.dotnetcore5.domain.ModelsHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace it.example.dotnetcore5.bl.Services
{
    /// <summary>
    /// Service of posts
    /// </summary>
    public class PostsService : IPostsService
    {
        private readonly IPostsRepository _postsRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        public PostsService(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        /// <summary>
        /// Count all post service
        /// </summary>
        /// <param name="postParamsHelper">Sorting and filters for posts</param>
        /// <param name="cancelToken">cancel token</param>
        /// <returns>List of posts</returns>
        public long GetCountAll(PostParamsHelper postParamsHelper, CancellationToken cancelToken = default)
        {
            cancelToken.ThrowIfCancellationRequested();
            return _postsRepository.GetCountAll(postParamsHelper, cancelToken);
        }

        /// <summary>
        /// Retrieve all posts
        /// </summary>
        /// <returns>list of posts</returns>
        public List<IPost> GetAll(PostParamsHelper postParamsHelper, CancellationToken cancelToken = default)
        {
            cancelToken.ThrowIfCancellationRequested();
            return _postsRepository.GetAll(postParamsHelper, cancelToken).ToList();
        }

        /// <summary>
        /// Add a new post
        /// </summary>
        /// <param name="entry">the post data</param>
        /// <param name="cancelToken">cancel token</param>
        public void Add(IPost entry, CancellationToken cancelToken = default)
        {
            cancelToken.ThrowIfCancellationRequested();
            _postsRepository.AddPost(entry, cancelToken);
        }

        /// <summary>
        /// Retrieve the post by own id
        /// </summary>
        /// <param name="id">id of post to retrieve</param>
        /// <returns>the post, null if id not found</returns>
        public IPost GetById(int id, CancellationToken cancelToken = default)
        {
            cancelToken.ThrowIfCancellationRequested();
            return _postsRepository.GetById(id, cancelToken);
        }
    }
}