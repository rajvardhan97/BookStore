using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class AdminModel
    {
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
        public long MobileNumber { get; set; }
        public string AdminAddress { get; set; }
    }
}
