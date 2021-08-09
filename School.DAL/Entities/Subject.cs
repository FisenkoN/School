using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace School.DAL.Entities
{
    public class Subject:EntityBase
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public ICollection<Teacher> Teachers { get; set; }

        public ICollection<Student> Students { get; set; }

        public Subject()
        {
            Teachers = new List<Teacher>();

            Students = new List<Student>();
        }
        
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