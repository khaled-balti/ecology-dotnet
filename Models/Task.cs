using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecology_dotnet.Models
{
    public class Task
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [StringLength(500)]
        [Column("picture")]
        public string picture { get; set; }

        [Column("longitude")]
        public double? longitude { get; set; }

        [Column("latitude")]
        public double? latitude { get; set; }

        [StringLength(1000)]
        [Column("description")]
        public string description { get; set; }

        [Column("point")]
        public int? point { get; set; }

        [Column("approved")]
        public bool approved { get; set; }

        [Column("createdAt")]
        public DateTime createdAt { get; set; }

        // Foreign key
        [Required]
        [Column("userId")]
        public int userId { get; set; }

        // Navigation property
        [ForeignKey("userId")]
        public virtual User user { get; set; }

        public Task()
        {
            createdAt = DateTime.UtcNow;
            approved = false;
        }
    }
}
