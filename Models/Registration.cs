using System;
using System.Collections.Generic;

namespace StudentRecords.Models
{
    public partial class Registration
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Nic { get; set; }
        public int? CourseId { get; set; }
        public int? BatchId { get; set; }
        public int? TelphoneNo { get; set; }

        public virtual Batch? Batch { get; set; }
        public virtual Course? Course { get; set; }
    }
}
