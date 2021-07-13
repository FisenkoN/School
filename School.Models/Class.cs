using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Models
{
    public class Class:EntityBase
    {
        [Required]
        [StringLength(10,ErrorMessage = "Name cannot be longer than 10 characters.")]
        public string Name { get; set; }

        [ForeignKey("Teacher")]
        public int? TeacherId { get; set; }

        public Teacher Teacher { get; set; }
        
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}