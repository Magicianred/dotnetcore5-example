using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace it.example.dotnetcore5.webapi.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
