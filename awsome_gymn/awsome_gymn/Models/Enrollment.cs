using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace awsome_gymn.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } // Foreign key for the user

        [Required]
        public int ClassId { get; set; } // Foreign key for the class

        [Required]
        public int TrainingSessionId { get; set; } // Foreign key for the training session

        [DataType(DataType.DateTime)]
        public DateTime EnrollmentDate { get; set; } // Date when the user enrolled in the class

        public virtual ApplicationUser User { get; set; } // Navigation property for the user
        public virtual Class Class { get; set; } // Navigation property for the class

        // Remove cascade delete on this relationship
        [ForeignKey("TrainingSessionId")]
        public virtual TrainingSession TrainingSession { get; set; } // Navigation property for the training session
    }

}