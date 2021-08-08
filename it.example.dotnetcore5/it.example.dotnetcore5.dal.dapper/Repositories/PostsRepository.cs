using Dapper;
using it.example.dotnetcore5.domain.Interfaces.Models;
using it.example.dotnetcore5.domain.Interfaces.Repositories;
using it.example.dotnetcore5.domain.Models;
using it.example.dotnetcore5.domain.ModelsHelpers;
using System;
using System.Collections.Generic;
using System.Threading;

namespace it.example.dotnetcore5.dal.dapper.Repositories
{
    /// <summary>
    /// Repository of posts
    /// </summary>
    public class PostsRepository : IPostsRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public PostsRepository(IDatabaseConnectionFactory connectionFactory)
        {
            this._connectionFactory = connectionFactory;
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
            long postsCount = 0;
            using (var connection = _connectionFactory.GetConnection())
            {
                postsCount = connection.QueryFirst<long>("SELECT COUNT(Id) FROM Posts ORDER BY CreateDate DESC");
            }
            return postsCount;
        }

        /// <summary>
        /// Retrieve all Posts items
        /// </summary>
        /// <returns>list of post</returns>
        public IEnumerable<IPost> GetAll(PostParamsHelper postParamsHelper, CancellationToken cancelToken = default)
        {
            cancelToken.ThrowIfCancellationRequested();
            IEnumerable<IPost> posts = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                posts = connection.Query<Post>("SELECT Id, Title, Text FROM Posts ORDER BY CreateDate DESC");
            }
            return posts;
        }

        /// <summary>
        /// Retrieve post by own id
        /// </summary>
        /// <param name="id">id of the post to retrieve</param>
        /// <returns>the post, null if id not found</returns>
        public IPost GetById(int id, CancellationToken cancelToken = default)
        {
            cancelToken.ThrowIfCancellationRequested();
            IPost post = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                // TOP 1 is not a command for SQLite, remove
                post = connection.QueryFirstOrDefault<Post>("SELECT * FROM Posts WHERE Id = @PostId", new { PostId = id });
            }
            return post;
        }

        public void AddPost(IPost item, CancellationToken cancelToken = default)
        {
            cancelToken.ThrowIfCancellationRequested();
            item.CreateDate = DateTime.Now;
            using var connection = _connectionFactory.GetConnection();
            var sqlInsert = "INSERT INTO Posts (Title, Text, CreateDate) VALUES (@Title, @Text, @CreateDate)";
            connection.Execute(sqlInsert, item);
        }
    }
}
