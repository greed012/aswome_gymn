using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace awsome_gymn.Models
{
    public class membership_enrollment
    {
        public int Id { get; set; }

        [Required]
        public int MembershipId { get; set; } // Foreign key for the membership

        [Required]
        public string UserId { get; set; } // Foreign key for the user

        [DataType(DataType.DateTime)]
        public DateTime EnrollmentDate { get; set; } // Date when the user enrolled in the membership

        // Navigation properties
        public virtual ApplicationUser User { get; set; } // Navigation property for the user
        public virtual membership Membership { get; set; } // Navigation property for the membership
    }
}