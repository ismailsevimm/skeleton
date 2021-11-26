using Microsoft.AspNetCore.Identity;
using System;

namespace Skeleton.Models.Entities
{
    public class User : IdentityUser
    {
        public string NationalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{this.FirstName} {this.LastName}";
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
