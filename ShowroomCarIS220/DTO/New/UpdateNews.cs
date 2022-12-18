﻿using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.DTO.New
{
    public class UpdateNews
    {
        [Required]
        public string author { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string image { get; set; }
        [Required]
        public string description { get; set; }
        public string dateSource { get; set; }
        [Required]
        public string detail { get; set; }
    }
}
