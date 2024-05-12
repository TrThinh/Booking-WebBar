using System.ComponentModel.DataAnnotations;

namespace BarBob.Models
{
    public class Deposit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Cost { get; set; }
    }
}
