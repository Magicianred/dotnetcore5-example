using it.example.dotnetcore5.domain.Interfaces.Models;
using it.example.dotnetcore5.domain.Interfaces.Services;
using it.example.dotnetcore5.domain.Models;
using it.example.dotnetcore5.domain.ModelsHelpers;
using it.example.dotnetcore5.webapi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace it.example.dotnetcore5.webapi.Controllers
{
    /// <summary>
    /// Handle Posts of blog
    /// </summary>
    [Route("api/[controller]")]
    //[ApiController]
    public class HomeController : ControllerBase
    {
        private const int DEFAULTITEMSPERPAGE = 10;
        private const int DEFAULTPAGE = 1;
        private const string DEFAULTSORTNAME = "createdate";
        private const string DEFAULTSORTORDER = "desc";

        private readonly ILogger<HomeController> _logger;
        private readonly IPostsService _postsService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="postsService"></param>
        /// <param name="logger"></param>
        public HomeController(IPostsService postsService, ILogger<HomeController> logger)
        {
            _postsService = postsService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieve all Posts
        /// GET: api/<HomeController>
        /// </summary>
        /// <returns>list of Posts</returns>
        [HttpGet]
        public IEnumerable<IPost> Get(CancellationToken cancelToken = default)
        {
            // retrieve querystring parameters 
            var _params = Request.Query.ToDictionary(q => q.Key, q => q.Value, StringComparer.OrdinalIgnoreCase);

            var page = QuerystringHelper.GetIntValueOrDefault(_params, "page", DEFAULTPAGE);
            var size = QuerystringHelper.GetIntValueOrDefault(_params, "size", DEFAULTITEMSPERPAGE);
            var sortName = QuerystringHelper.GetStringValueOrDefault(_params, "sort", DEFAULTSORTNAME);
            var sortOrder = QuerystringHelper.GetStringValueOrDefault(_params, "order", DEFAULTSORTORDER);
            var filterTitle = QuerystringHelper.GetStringValueOrDefault(_params, "title", String.Empty);

            PostParamsHelper modelParams = new()
            {
                Page = page,
                ItemsPerPage = size,
                SortName = sortName,
                SortOrder = sortOrder,
                Title = filterTitle
            };

            cancelToken.ThrowIfCancellationRequested();

            var posts = _postsService.GetAll(modelParams, cancelToken);

            return posts;
        }

        /// <summary>
        /// Retrieve the post with the id
        /// GET api/<HomeController>/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the post with requested id</returns>
        [HttpGet("{id}")]
        public IPost Get(int id, CancellationToken cancelToken = default)
        {
            cancelToken.ThrowIfCancellationRequested();
            var post = _postsService.GetById(id, cancelToken);

            return post;
        }

        [HttpPost]
        public void Add(CancellationToken cancelToken = default)
        {
            Post newPost = new()
            {
                Title = "New test post",
                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut quis enim eu augue tincidunt tincidunt. Nam luctus pharetra tortor, sit amet sodales odio bibendum non.",
                CreateDate = System.DateTime.Now
            };
            cancelToken.ThrowIfCancellationRequested();
            _postsService.Add(newPost, cancelToken);
            _logger.LogInformation("Added fake post!");
        }
    }
}