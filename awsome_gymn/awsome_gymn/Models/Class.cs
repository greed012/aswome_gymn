using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace awsome_gymn.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public byte[] Image { get; set; }

        public List<TrainingSession> TrainingSessions { get; set; }
    }
}