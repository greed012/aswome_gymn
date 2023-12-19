using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace awsome_gymn.Models
{
    public class AttendanceCreateViewModel
    {
        public List<string> SelectedUserIds { get; set; } = new List<string>(); // Initialize the list
        public int ClassId { get; set; }
        public int TrainingSessionId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Comments { get; set; }
        public bool IsPresent { get; set; }
    }
}