using it.example.dotnetcore5.bl.Services;
using it.example.dotnetcore5.dal.ef.sqlite.Factories;
using it.example.dotnetcore5.dal.ef.sqlite.Repositories;
using it.example.dotnetcore5.domain.Interfaces.Models;
using it.example.dotnetcore5.domain.Interfaces.Repositories;
using it.example.dotnetcore5.domain.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace it.example.dotnetcore5.bl.tests.integration.Services
{
    [TestFixture]
    public class PostsServiceSqliteRepoTest
    {
        /// <summary>
        /// PostsService is our System Under Test
        /// </summary>
        private PostsService _sut;

        /// <summary>
        /// A mock of posts repository
        /// </summary>
        private IPostsRepository _postsRepository;
        private dal.ef.sqlite.EfModels.BlogContext _context;

        #region SetUp and TearDown
        [OneTimeSetUp]
        public void SetupOneTime()
        {
            var connectionString = @"Data Source=Resources\Test.db;";

            var options = new DbContextOptionsBuilder<dal.ef.sqlite.EfModels.BlogContext>()
                             .UseSqlite(connectionString)
                             .Options;

            _context = new dal.ef.sqlite.EfModels.BlogContext(options);
            _postsRepository = new PostsRepository(_context);
            _sut = new PostsService(_postsRepository);

            this.LoadData();
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
            Assert.That(posts.Count == 3, "Posts count is not correct");
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


        #region private methods

        private void LoadData()
        {
            _postsRepository.RemoveAll();

            var checkPosts = _context.Posts.ToList();

            Assert.That(checkPosts.Count <= 0, "RemoveAll does not works");

            List<Post> posts = new List<Post>();

            // charge fake data
            if (posts == null || posts.Count == 0)
            {
                posts.Add(
                    new Post()
                    {
                        Id = 1,
                        Title = "Forth post of the blog",
                        Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut quis enim eu augue tincidunt tincidunt. Nam luctus pharetra tortor, sit amet sodales odio bibendum non. Nulla imperdiet tempor metus, sit amet posuere justo laoreet nec. Nullam vehicula commodo posuere. Ut elementum, purus id posuere porttitor, massa purus tristique sapien, nec sollicitudin massa lectus ac erat. Nunc id tortor quis leo placerat accumsan nec scelerisque ligula. Vivamus in efficitur felis. Cras tincidunt eleifend leo ut volutpat. Sed ut ligula eu risus pretium volutpat sit amet vel lorem. Aliquam gravida blandit risus non laoreet.\n\nPraesent felis velit, interdum ac laoreet luctus, finibus vel lorem. In vitae dolor ipsum. Quisque pretium eu ex in egestas. Nam imperdiet in diam eu maximus. Nulla tristique magna velit, vitae scelerisque augue facilisis id. Nulla in ultricies ex, nec lobortis felis. Nam nec vestibulum libero, ut laoreet tellus. Pellentesque ut metus sed nulla fermentum consequat at nec ligula. Donec pretium nisi rhoncus elit tincidunt, eu euismod ligula semper.\n\nIn at enim sit amet magna luctus sagittis et quis lacus. In blandit enim risus, eu pharetra nibh pharetra id. Nullam diam augue, fermentum eget aliquam sed, ornare sit amet dolor. Fusce fringilla vestibulum aliquam. Curabitur id laoreet lectus. Proin pretium nunc vel sem bibendum fringilla. Aliquam rhoncus neque enim, pellentesque consequat turpis gravida in. Ut at massa non augue fringilla pellentesque. Mauris consectetur pellentesque mauris molestie ullamcorper. Vivamus at nisi sed turpis cursus porttitor a sed enim. Quisque nec lorem ultrices, vestibulum sapien et, sollicitudin arcu. Donec augue risus, eleifend a tempus eu, hendrerit a quam.\n\nSed ex arcu, fringilla at molestie sit amet, accumsan id odio. Sed ut est orci. Suspendisse convallis mauris in fringilla facilisis. Nulla sit amet orci sed elit sollicitudin placerat. Pellentesque blandit, eros ut blandit volutpat, elit diam pulvinar tellus, vel vulputate urna augue at nisi. Suspendisse id odio quis risus dignissim elementum. Suspendisse vitae interdum dui, id euismod lacus. Mauris sit amet nisi nec diam fringilla lacinia. Nulla mauris nulla, vestibulum a convallis a, imperdiet nec neque. Phasellus aliquet sollicitudin mauris, id congue est varius sit amet. Etiam imperdiet mauris id dui iaculis commodo. Vivamus at nisl ligula. Cras iaculis varius orci, non congue nunc commodo ut.\n\nDonec a justo porttitor, placerat ante sed, ullamcorper quam. Aliquam dapibus velit leo, at fermentum libero iaculis eget. Nullam eu mattis lorem, ac vulputate libero. Duis quis dui eget leo condimentum eleifend a et nisi. Suspendisse lorem tortor, pharetra vitae ornare vel, ullamcorper eu odio. Sed suscipit iaculis massa eu varius. Proin augue quam, ullamcorper quis velit sed, ultrices condimentum orci. Vivamus nisi leo, convallis fermentum dolor quis, suscipit iaculis nisi.",
                        CreateDate = DateTime.Now.AddDays(-3)
                    }
                );
                posts.Add(
                    new Post()
                    {
                        Id = 2,
                        Title = "Fifth post of the blog",
                        Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut quis enim eu augue tincidunt tincidunt. Nam luctus pharetra tortor, sit amet sodales odio bibendum non. Nulla imperdiet tempor metus, sit amet posuere justo laoreet nec. Nullam vehicula commodo posuere. Ut elementum, purus id posuere porttitor, massa purus tristique sapien, nec sollicitudin massa lectus ac erat. Nunc id tortor quis leo placerat accumsan nec scelerisque ligula. Vivamus in efficitur felis. Cras tincidunt eleifend leo ut volutpat. Sed ut ligula eu risus pretium volutpat sit amet vel lorem. Aliquam gravida blandit risus non laoreet.\n\nPraesent felis velit, interdum ac laoreet luctus, finibus vel lorem. In vitae dolor ipsum. Quisque pretium eu ex in egestas. Nam imperdiet in diam eu maximus. Nulla tristique magna velit, vitae scelerisque augue facilisis id. Nulla in ultricies ex, nec lobortis felis. Nam nec vestibulum libero, ut laoreet tellus. Pellentesque ut metus sed nulla fermentum consequat at nec ligula. Donec pretium nisi rhoncus elit tincidunt, eu euismod ligula semper.\n\nIn at enim sit amet magna luctus sagittis et quis lacus. In blandit enim risus, eu pharetra nibh pharetra id. Nullam diam augue, fermentum eget aliquam sed, ornare sit amet dolor. Fusce fringilla vestibulum aliquam. Curabitur id laoreet lectus. Proin pretium nunc vel sem bibendum fringilla. Aliquam rhoncus neque enim, pellentesque consequat turpis gravida in. Ut at massa non augue fringilla pellentesque. Mauris consectetur pellentesque mauris molestie ullamcorper. Vivamus at nisi sed turpis cursus porttitor a sed enim. Quisque nec lorem ultrices, vestibulum sapien et, sollicitudin arcu. Donec augue risus, eleifend a tempus eu, hendrerit a quam.\n\nSed ex arcu, fringilla at molestie sit amet, accumsan id odio. Sed ut est orci. Suspendisse convallis mauris in fringilla facilisis. Nulla sit amet orci sed elit sollicitudin placerat. Pellentesque blandit, eros ut blandit volutpat, elit diam pulvinar tellus, vel vulputate urna augue at nisi. Suspendisse id odio quis risus dignissim elementum. Suspendisse vitae interdum dui, id euismod lacus. Mauris sit amet nisi nec diam fringilla lacinia. Nulla mauris nulla, vestibulum a convallis a, imperdiet nec neque. Phasellus aliquet sollicitudin mauris, id congue est varius sit amet. Etiam imperdiet mauris id dui iaculis commodo. Vivamus at nisl ligula. Cras iaculis varius orci, non congue nunc commodo ut.\n\nDonec a justo porttitor, placerat ante sed, ullamcorper quam. Aliquam dapibus velit leo, at fermentum libero iaculis eget. Nullam eu mattis lorem, ac vulputate libero. Duis quis dui eget leo condimentum eleifend a et nisi. Suspendisse lorem tortor, pharetra vitae ornare vel, ullamcorper eu odio. Sed suscipit iaculis massa eu varius. Proin augue quam, ullamcorper quis velit sed, ultrices condimentum orci. Vivamus nisi leo, convallis fermentum dolor quis, suscipit iaculis nisi.",
                        CreateDate = DateTime.Now.AddDays(-2)
                    }
                );
                posts.Add(
                    new Post()
                    {
                        Id = 3,
                        Title = "Sixth post of the blog",
                        Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut quis enim eu augue tincidunt tincidunt. Nam luctus pharetra tortor, sit amet sodales odio bibendum non. Nulla imperdiet tempor metus, sit amet posuere justo laoreet nec. Nullam vehicula commodo posuere. Ut elementum, purus id posuere porttitor, massa purus tristique sapien, nec sollicitudin massa lectus ac erat. Nunc id tortor quis leo placerat accumsan nec scelerisque ligula. Vivamus in efficitur felis. Cras tincidunt eleifend leo ut volutpat. Sed ut ligula eu risus pretium volutpat sit amet vel lorem. Aliquam gravida blandit risus non laoreet.\n\nPraesent felis velit, interdum ac laoreet luctus, finibus vel lorem. In vitae dolor ipsum. Quisque pretium eu ex in egestas. Nam imperdiet in diam eu maximus. Nulla tristique magna velit, vitae scelerisque augue facilisis id. Nulla in ultricies ex, nec lobortis felis. Nam nec vestibulum libero, ut laoreet tellus. Pellentesque ut metus sed nulla fermentum consequat at nec ligula. Donec pretium nisi rhoncus elit tincidunt, eu euismod ligula semper.\n\nIn at enim sit amet magna luctus sagittis et quis lacus. In blandit enim risus, eu pharetra nibh pharetra id. Nullam diam augue, fermentum eget aliquam sed, ornare sit amet dolor. Fusce fringilla vestibulum aliquam. Curabitur id laoreet lectus. Proin pretium nunc vel sem bibendum fringilla. Aliquam rhoncus neque enim, pellentesque consequat turpis gravida in. Ut at massa non augue fringilla pellentesque. Mauris consectetur pellentesque mauris molestie ullamcorper. Vivamus at nisi sed turpis cursus porttitor a sed enim. Quisque nec lorem ultrices, vestibulum sapien et, sollicitudin arcu. Donec augue risus, eleifend a tempus eu, hendrerit a quam.\n\nSed ex arcu, fringilla at molestie sit amet, accumsan id odio. Sed ut est orci. Suspendisse convallis mauris in fringilla facilisis. Nulla sit amet orci sed elit sollicitudin placerat. Pellentesque blandit, eros ut blandit volutpat, elit diam pulvinar tellus, vel vulputate urna augue at nisi. Suspendisse id odio quis risus dignissim elementum. Suspendisse vitae interdum dui, id euismod lacus. Mauris sit amet nisi nec diam fringilla lacinia. Nulla mauris nulla, vestibulum a convallis a, imperdiet nec neque. Phasellus aliquet sollicitudin mauris, id congue est varius sit amet. Etiam imperdiet mauris id dui iaculis commodo. Vivamus at nisl ligula. Cras iaculis varius orci, non congue nunc commodo ut.\n\nDonec a justo porttitor, placerat ante sed, ullamcorper quam. Aliquam dapibus velit leo, at fermentum libero iaculis eget. Nullam eu mattis lorem, ac vulputate libero. Duis quis dui eget leo condimentum eleifend a et nisi. Suspendisse lorem tortor, pharetra vitae ornare vel, ullamcorper eu odio. Sed suscipit iaculis massa eu varius. Proin augue quam, ullamcorper quis velit sed, ultrices condimentum orci. Vivamus nisi leo, convallis fermentum dolor quis, suscipit iaculis nisi.",
                        CreateDate = DateTime.Now.AddDays(-1)
                    }
                );
            }

            _context.AddRange(posts.ToEfEntities());
            _context.SaveChanges();
        }

        #endregion
    }
}
