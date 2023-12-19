using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace awsome_gymn.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }
        public string Time { get; set; }

        public int Group { get; set; }
        public int Duration { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }
    }
}