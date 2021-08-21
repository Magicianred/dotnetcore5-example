using it.example.dotnetcore5.domain.Interfaces.Models;
using System.Collections.Generic;

namespace it.example.dotnetcore5.domain.Interfaces.Repositories
{
    public interface IPostsRepository
    {
        IEnumerable<IPost> GetAll();
        IPost GetById(int id);
        IPost AddPost(IPost item);
        void RemoveAll();
    }
}
