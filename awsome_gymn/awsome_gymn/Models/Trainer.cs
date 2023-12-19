using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace awsome_gymn.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public byte[] Image { get; set; }

        public string FbLink { get; set; }
        public string InstaLink { get; set; }
        public string TwitterLink { get; set; }

        public List<TrainingSession> TrainingSessions { get; set; }
    }
}