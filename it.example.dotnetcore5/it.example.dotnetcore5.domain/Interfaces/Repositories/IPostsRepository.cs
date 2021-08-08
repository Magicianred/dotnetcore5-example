using it.example.dotnetcore5.domain.Interfaces.Models;
using it.example.dotnetcore5.domain.ModelsHelpers;
using System.Collections.Generic;
using System.Threading;

namespace it.example.dotnetcore5.domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface for Post Repository
    /// </summary>
    public interface IPostsRepository
    {
        /// <summary>
        /// Count all post service
        /// </summary>
        /// <param name="postParamsHelper">Sorting and filters for posts</param>
        /// <param name="cancelToken">cancel token</param>
        /// <returns>List of posts</returns>
        public long GetCountAll(PostParamsHelper postParamsHelper, CancellationToken cancelToken = default);

        /// <summary>
        /// Retrieve all post service
        /// </summary>
        /// <param name="postParamsHelper">Sorting and filters for posts</param>
        /// <param name="cancelToken">cancel token</param>
        /// <returns>List of posts</returns>
        public IEnumerable<IPost> GetAll(PostParamsHelper postParamsHelper, CancellationToken cancelToken = default);

        /// <summary>
        /// Retrieve a post from identifier
        /// </summary>
        /// <param name="id">identifier of the post</param>
        /// <param name="cancelToken">cancel token</param>
        /// <returns>the post with id</returns>
        IPost GetById(int id, CancellationToken cancelToken = default);

        /// <summary>
        /// Add a new post
        /// </summary>
        /// <param name="entry">data of the new post</param>
        /// <param name="cancelToken">cancel token</param>
        void AddPost(IPost item, CancellationToken cancelToken = default);
    }
}
