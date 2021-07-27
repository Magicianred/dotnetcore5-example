using it.example.dotnetcore5.domain.Interfaces.Services;
using it.example.dotnetcore5.domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace it.example.dotnetcore5.webapi.Controllers
{
    /// <summary>
    /// Handle Posts of blog
    /// </summary>
    [Route("api/[controller]")]
    //[ApiController]
    public class HomeController : ControllerBase
    {
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
        public IEnumerable<Post> Get()
        {
            var posts = _postsService.GetAll();

            // TO DO : mapping interface -> model
            //return posts;

            return null;
        }

        /// <summary>
        /// Retrieve the post with the id
        /// GET api/<HomeController>/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the post with requested id</returns>
        [HttpGet("{id}")]
        public Post Get(int id)
        {
            var post = _postsService.GetById(id);

            // TO DO : mapping interface -> model
            //return post;
            return null;
        }
    }
}