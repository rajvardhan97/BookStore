using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class FeedbackModel
    {
        public int Rating { get; set; }
        public string Review { get; set; }
        public int BookId { get; set; }
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
    }
}
