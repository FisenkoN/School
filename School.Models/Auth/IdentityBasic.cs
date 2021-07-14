using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace School.Models.Auth
{
    public abstract class IdentityBase:EntityBase
    {
        [PasswordPropertyText]
        public string Password { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }

        public Role Role { get; set; } = Role.Visitor;

        [Required] public bool IsRegistered { get; set; } = false;
    }
}