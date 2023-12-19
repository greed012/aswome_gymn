using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace awsome_gymn.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int TrainingSessionId { get; set; }

        [Required]
        public int ClassId { get; set; }

        [Required]
        public DateTime AttendanceDate { get; set; }

        public string Comments { get; set; }

        [Required]
        public bool IsPresent { get; set; } // Add a field to record whether the attendance is present or absent

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("TrainingSessionId")]
        public virtual TrainingSession TrainingSession { get; set; }

        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }
    }
}