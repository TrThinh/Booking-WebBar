using System.ComponentModel.DataAnnotations;

namespace BarBob.Models
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NameBranch { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
