using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Models
{
    public class Teacher:EntityBase
    {
        [Required] 
        [StringLength(20, ErrorMessage = "FirstName cannot be longer than 20 characters.")]
        public string FirstName { get; set; }

        [Required] 
        [StringLength(20, ErrorMessage = "LastName cannot be longer than 20 characters.")]
        public string LastName { get; set; }

        [Range(19,75, ErrorMessage = "Age mast be more then 19 and less then 75")]
        public int Age { get; set; }
        
        [Required] 
        public Gender Gender { get; set; }

        [ForeignKey("Class")]
        public int? ClassId { get; set; }
        
        public Class Class { get; set; }

        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
        
        [NotMapped]
        public string FullName => FirstName + " " + LastName; 

        public override string ToString()
        {
            var result = $"FullName: {FirstName}, Age: {Age}, Gender: {Gender}, Subjects:\n";

            foreach (var subject in Subjects)
            {
                result += $"{subject}\t";
            }

            return result;
        }
    }
}