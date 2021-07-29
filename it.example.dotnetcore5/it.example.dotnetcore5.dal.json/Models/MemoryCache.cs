using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it.example.dotnetcore5.dal.json.Models
{
    public static class MemoryCache<T>
    {
        public static List<T> Items = new List<T>();
    }
}
