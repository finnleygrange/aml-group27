﻿namespace MediaService.Models
{
    public class Book : MediaItem
    {
        public string ISBN { get; set; }
        public int PageCount { get; set; }
    }
}
