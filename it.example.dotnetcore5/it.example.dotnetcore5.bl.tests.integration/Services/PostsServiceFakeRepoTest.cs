using it.example.dotnetcore5.bl.Services;
using it.example.dotnetcore5.dal.fake.Repositories;
using it.example.dotnetcore5.domain.Interfaces.Models;
using it.example.dotnetcore5.domain.Interfaces.Repositories;
using it.example.dotnetcore5.domain.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace it.example.dotnetcore5.bl.tests.integration.Services
{
    [TestFixture]
    public class PostsServiceFakeRepoTest
    {
        /// <summary>
        /// PostsService is our System Under Test
        /// </summary>
        private PostsService _sut;

        /// <summary>
        /// A mock of posts repository
        /// </summary>
        private IPostsRepository _postsRepository;

        #region SetUp and TearDown
        [OneTimeSetUp]
        public void SetupOneTime()
        {
            _postsRepository = new FakePostsRepository();
            _sut = new PostsService(_postsRepository);
        }

        [OneTimeTearDown]
        public void TearDownOneTime()
        {
            _sut = null;
            _postsRepository = null;
        }
        #endregion

        [Test]
        [Category("Integration Test")]
        public void should_retrieve_all_posts()
        {
            // Arrange

            // Act
            var posts = _sut.GetAll();

            // Assert
            Assert.IsNotNull(posts, "There are not posts");
            Assert.That(posts.Count == 3, "POsts count is not correct");
        }

        public void should_add_post()
        {
            // Arrange
            var posts = _postsRepository.GetAll();
            var maxId = posts.Max(x => x.Id);

            var dateNow = DateTime.Now;

            IPost postToAdd = new Post()
            {
                Id = 5,
                Title = "New post to test",
                Text = "Text new post",
                CreateDate = dateNow
            };

            // Act
            var newPost = _sut.AddPost(postToAdd);

            // Assert
            Assert.IsNotNull(newPost, "No post created or returned");
            Assert.That(newPost.Id == maxId + 1, "Error in generate Id");
            Assert.That(newPost.CreateDate == dateNow, "CreateDate not correct");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [Category("Integration Test")]
        public void should_retrieve_post_by_id(int postId)
        {
            // Arrange

            // Act
            var post = _sut.GetById(postId);

            // Assert
            Assert.IsNotNull(post, "There is not post");
            Assert.That(post.Id == postId, "Post id is not correct");
        }

        [TestCase(4)]
        [TestCase(5)]
        [Category("Integration Test")]
        public void should_retrieve_not_exists_post_by_id(int postId)
        {
            // Arrange

            // Act
            var post = _sut.GetById(postId);

            // Assert
            Assert.IsNull(post, "There is post");
        }
    }
}
