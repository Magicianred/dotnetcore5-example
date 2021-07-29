using it.example.dotnetcore5.domain.Models;
using System.Collections.Generic;

namespace it.example.dotnetcore5.dal.fake.Models
{
    public static class MemoryCache<T>
    {
        public static List<T> Items = new List<T>();
    }
}
