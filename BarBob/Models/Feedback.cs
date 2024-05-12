using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarBob.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime FeedbackDate { get; set; }
        //------ForeignKey here-----
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public User User { get; set; }

        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        [ValidateNever]
        public Branch Branch { get; set; }
    }
}
