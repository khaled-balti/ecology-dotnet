using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecology_dotnet.Models
{
    public class Complain
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required]
        [StringLength(200)]
        [Column("title")]
        public string title { get; set; }

        [Required]
        [StringLength(1000)]
        [Column("description")]
        public string description { get; set; }

        // Foreign key
        [Required]
        [Column("userId")]
        public int userId { get; set; }

        // Navigation property
        [ForeignKey("userId")]
        public virtual User user { get; set; }
    }
}
