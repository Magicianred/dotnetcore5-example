using it.example.dotnetcore5.domain.Interfaces.Models;
using it.example.dotnetcore5.domain.Interfaces.Repositories;
using it.example.dotnetcore5.domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace it.example.dotnetcore5.dal.json.Repositories
{
    /// <summary>
    /// Repository of posts
    /// </summary>
    public class PostsRepository : IPostsRepository
    {
        private List<IPost> _source;

        /// <summary>
        /// Constructor
        /// </summary>
        public PostsRepository()
        {
            _source = this.LoadData();
        }

        /// <summary>
        /// Retrieve all Posts items
        /// </summary>
        /// <returns>list of post</returns>
        public IEnumerable<IPost> GetAll()
        {
            IEnumerable<IPost> posts = _source;

            return posts;
        }

        /// <summary>
        /// Retrieve post by own id
        /// </summary>
        /// <param name="id">id of the post to retrieve</param>
        /// <returns>the post, null if id not found</returns>
        public IPost GetById(int id)
        {
            IPost post = _source.Find(item => item.Id == id);

            return post;
        }


        #region private methods

        private List<IPost> LoadData()
        {
            var fileContent = System.IO.File.ReadAllText("Resources/posts.json");
            var posts = JsonConvert.DeserializeObject<List<Post>>(fileContent);
            if(posts != null)
            {
                return posts.ToList<IPost>();
            }
            return null;
        }

        #endregion
    }
}
