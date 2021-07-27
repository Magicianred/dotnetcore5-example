﻿using it.example.dotnetcore5.domain.Interfaces.Models;

namespace it.example.dotnetcore5.domain.Models
{
    public partial class Post : IPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
