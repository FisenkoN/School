using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace School.Models
{
    public class Student:EntityBase
    {
        [Required] 
        [StringLength(20, ErrorMessage = "FirstName cannot be longer than 20 characters.")]
        public string FirstName { get; set; }

        [Required] 
        [StringLength(20, ErrorMessage = "LastName cannot be longer than 20 characters.")]
        public string LastName { get; set; }

        [Range(5,18, ErrorMessage = "Age mast be more then 5 and less then 18")]
        public int Age { get; set; }
        
        [Required] 
        public Gender Gender { get; set; }
    
        [ForeignKey("Class")] 
        public int? ClassId { get; set; }

        public virtual Class Class { get; set; }

        public virtual List<Subject> Subjects { get; set; }
    
        [NotMapped] 
        public string FullName => FirstName + " " + LastName;
    }
}