using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace School.Models
{
    public class Subject:EntityBase
    {
        [Required]
        [MaxLength(30,ErrorMessage = "Name cannot be longer than 30 characters.")]
        public string Name { get; set; }

        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}