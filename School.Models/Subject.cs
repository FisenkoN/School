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

        public override string ToString()
        {
            var res = $"Name: {Name}\nTeacher:\n";

            foreach (var teacher in Teachers)
            {
                res += $"{teacher.FullName}\n";
            }

            res += "\nStudents:\n";
            
            foreach (var student in Students)
            {
                res += $"{student.FullName}\n";
            }

            return res;
        }
    }
}