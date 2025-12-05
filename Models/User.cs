using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecology_dotnet.Models
{
    public class User
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("firstName")]
        public string firstName { get; set; }

        [Required]
        [StringLength(100)]
        [Column("lastName")]
        public string lastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        [Column("email")]
        public string email { get; set; }

        [Required]
        [StringLength(255)]
        [Column("password")]
        public string password { get; set; }

        [Required]
        [StringLength(50)]
        [Column("role")]
        public string role { get; set; }

        // Navigation properties
        public virtual ICollection<Complain> complains { get; set; }
        public virtual ICollection<Task> tasks { get; set; }

        public User()
        {
            complains = new HashSet<Complain>();
            tasks = new HashSet<Task>();
        }
    }
}
