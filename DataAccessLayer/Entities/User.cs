﻿using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string UserName { get; set; }
        
        public string HashPassword { get; set; }
        
        public string? RefreshToken { get; set; }
        
        public DateTime? RefreshTokenExpiryTime { get; set; }
        
        public virtual ICollection<UserProject> UserProjects { get; set; }
        
        public virtual ICollection<Assignment> Tasks { get; set; }
        
        public ICollection<Role> Roles { get; set; }
    }
}
