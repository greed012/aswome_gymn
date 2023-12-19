using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace awsome_gymn.Models
{
    public class DashboardViewModel
    {
        public int ClassCount { get; set; }
        public int AccountCount { get; set; }

        public int UserCount { get; set; }
        public int AdminCount { get; set; }
        public int TrainerCount { get; set; }
    }
}