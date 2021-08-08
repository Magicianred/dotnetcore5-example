using it.example.dotnetcore5.dal.ef.mysql.EfModels;
using it.example.dotnetcore5.dal.ef.mysql.Factories;
using it.example.dotnetcore5.domain.Interfaces.Models;
using it.example.dotnetcore5.domain.Interfaces.Repositories;
using it.example.dotnetcore5.domain.ModelsHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ModelPost = it.example.dotnetcore5.domain.Models.Post;

namespace it.example.dotnetcore5.dal.ef.mysql.Repositories
{
    /// <summary>
    /// Repository of posts
    /// </summary>
    public class PostsRepository : IPostsRepository
    {
        protected MyblogContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public PostsRepository(MyblogContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Count all Posts items
        /// </summary>
        /// <returns>list of post</returns>
        public long GetCountAll(PostParamsHelper postParamsHelper, CancellationToken cancelToken = default)
        {
            cancelToken.ThrowIfCancellationRequested();
            var query = _context.Posts.AsQueryable();

            query = PostsRepository.GetAndValidateSearchFilters(query, postParamsHelper);

            cancelToken.ThrowIfCancellationRequested();
            var total = query.Count();

            return total;
        }

        /// <summary>
        /// Retrieve all Posts items
        /// </summary>
        /// <returns>list of post</returns>
        public IEnumerable<IPost> GetAll(PostParamsHelper postParamsHelper, CancellationToken cancelToken = default)
        {
            cancelToken.ThrowIfCancellationRequested();
            if (postParamsHelper.Page <= 0)
            {
                postParamsHelper.Page = 1;
            }

            var sortName = postParamsHelper.SortName;
            var sortOrder = postParamsHelper.SortOrder;
            Func<Post, Object> orderByFunc = PostsRepository.GetAndValidateOrdersSort(ref sortName, ref sortOrder);
            postParamsHelper.SortName = sortName;
            postParamsHelper.SortOrder = sortOrder;


            var query = _context.Posts.AsQueryable();

            query = PostsRepository.GetAndValidateSearchFilters(query, postParamsHelper);
            cancelToken.ThrowIfCancellationRequested();

            int sizePerPage = postParamsHelper.ItemsPerPage;
            int start = postParamsHelper.Page - 1 * postParamsHelper.ItemsPerPage;

            var posts = new List<Post>();
            if (sortOrder == "desc")
                posts = query.OrderByDescending(orderByFunc).Skip(start).Take(sizePerPage).ToList();
            else
                posts = query.OrderBy(orderByFunc).Skip(start).Take(sizePerPage).ToList();

            return posts.ToModelsDomain();
        }

        /// <summary>
        /// Retrieve post by own id
        /// </summary>
        /// <param name="id">id of the post to retrieve</param>
        /// <returns>the post, null if id not found</returns>
        public IPost GetById(int id, CancellationToken cancelToken = default)
        {
            cancelToken.ThrowIfCancellationRequested();
            var post = _context.Posts.FirstOrDefault(x => x.Id == id);

            cancelToken.ThrowIfCancellationRequested();
            return post.ToModelDomain();
        }

        /// <summary>
        /// Save the post in database
        /// </summary>
        /// <param name="item">post to save</param>
        public void AddPost(IPost item, CancellationToken cancelToken = default)
        {
            cancelToken.ThrowIfCancellationRequested();
            var entry = ((ModelPost)item).ToEfEntity();
            _context.Posts.Add(entry);

            cancelToken.ThrowIfCancellationRequested();
            _context.SaveChanges();
        }

        #region common functions 
        private static Func<Post, Object> GetAndValidateOrdersSort(ref string sortName, ref string sortOrder)
        {
            Func<Post, Object> orderByFunc = null;

            if (String.IsNullOrEmpty(sortName))
                sortName = "createdate";
            if (String.IsNullOrEmpty(sortOrder))
                sortOrder = "desc";

            // verifico i possibili campi previsti per l'ordinamento
            switch (sortName.ToLower())
            {
                case "id":
                    orderByFunc = item => item.Id;
                    break;
                case "title":
                    orderByFunc = item => item.Title;
                    break;
                case "text":
                    orderByFunc = item => item.Text;
                    break;
                case "createdate":
                    orderByFunc = item => item.CreateDate;
                    break;
            }

            // verifico i possibili tipi previsti per l'ordinamento
            switch (sortOrder.ToLower())
            {
                case "asc":
                case "desc":
                    break;
                default:
                    sortOrder = "asc";
                    break;
            }

            return orderByFunc;
        }

        private static IQueryable<Post> GetAndValidateSearchFilters(IQueryable<Post> query, PostParamsHelper modelParams)
        {
            long? postId = modelParams.PostId;
            string postIdComparator = modelParams.PostIdComparator;
            string title = modelParams.Title;
            string titleComparator = modelParams.TitleComparator;
            string text = modelParams.Text;
            string textComparator = modelParams.TextComparator;
            DateTime? createDate = modelParams.CreateDate;
            string createDateComparator = modelParams.CreateDateComparator;

            if (postId.HasValue)
            {
                if (String.IsNullOrEmpty(postIdComparator))
                    postIdComparator = "=";

                query = postIdComparator.ToLower() switch
                {
                    "=" => query.Where(x => x.Id == postId.Value),
                    ">" => query.Where(x => x.Id > postId.Value),
                    "<=" => query.Where(x => x.Id <= postId.Value),
                    _ => query.Where(x => x.Id == postId.Value),
                };
            }

            if (!string.IsNullOrEmpty(title))
            {
                if (String.IsNullOrEmpty(title))
                    titleComparator = "=";

                query = titleComparator.ToLower() switch
                {
                    "=" => query.Where(x => x.Title.ToLower().Equals(title.ToLower())),
                    "^" => query.Where(x => x.Title.ToLower().StartsWith(title.ToLower())),
                    "$" => query.Where(x => x.Title.ToLower().EndsWith(title.ToLower())),
                    _ => query.Where(x => x.Title.ToLower().Contains(title.ToLower()))
                };

                query = query.Where(x => x.Title.ToLower().Contains(title.ToLower()));
            }

            if (!string.IsNullOrEmpty(text))
            {
                if (String.IsNullOrEmpty(text))
                    textComparator = "=";

                query = textComparator.ToLower() switch
                {
                    "=" => query.Where(x => x.Text.ToLower().Equals(text.ToLower())),
                    "^" => query.Where(x => x.Text.ToLower().StartsWith(text.ToLower())),
                    "$" => query.Where(x => x.Text.ToLower().EndsWith(text.ToLower())),
                    _ => query.Where(x => x.Text.ToLower().Contains(text.ToLower()))
                };
            }

            if (createDate.HasValue)
            {
                if (String.IsNullOrEmpty(createDateComparator))
                    createDateComparator = "=";

                query = createDateComparator.ToLower() switch
                {
                    "=" => query.Where(x => x.CreateDate == createDate.Value),
                    ">" => query.Where(x => x.CreateDate > createDate.Value),
                    ">=" => query.Where(x => x.CreateDate >= createDate.Value),
                    "<" => query.Where(x => x.CreateDate < createDate.Value),
                    "<=" => query.Where(x => x.CreateDate <= createDate.Value),
                    "!=" => query.Where(x => x.CreateDate != createDate.Value),
                    _ => query.Where(x => x.CreateDate == createDate.Value),
                };
            }

            return query;
        }

        #endregion
    }
}
