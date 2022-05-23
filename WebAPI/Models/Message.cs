﻿using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Message
    {
        public int id { get; set; }

        [Required]
        public string content { get; set; }
        [Required]
        public DateTime created { get; set; }
        [Required]
        public bool sent { get; set; }
    }
}
