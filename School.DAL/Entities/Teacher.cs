using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.DAL.Entities
{
    public class Teacher:EntityBase
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Range(18,80)]
        public int Age { get; set; }
        
        [Required]
        public Gender Gender { get; set; }
        
        public int? ClassId { get; set; }
        
        [ForeignKey("ClassId")]
        public Class Class { get; set; }
        
        public ICollection<Subject> Subjects { get; set; }
        
        [NotMapped]
        public string FullName => FirstName + " " + LastName;

        public Teacher()
        {
            Subjects = new List<Subject>();
            
        }
        
        public override string ToString()
        {
            var res = $"FullName: {FullName}, Age: {Age}, Gender: {Gender}";

            if (Class != null) res += $"Class: {Class?.Name}";
            
            res+=", Subjects:\n";

            foreach (var subject in Subjects)
            {
                res += $"{subject.Name}\t";
            }

            return res;
        }
    }
}