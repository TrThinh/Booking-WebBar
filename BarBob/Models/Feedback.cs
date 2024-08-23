﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarBob.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(1000)]
        public string Status { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public DateTime FeedbackDate { get; set; }

        public string? Image { get; set; }

        // ---------------Foreign Key-----------------
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public User User { get; set; }
    }
}
