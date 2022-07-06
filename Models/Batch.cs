using System;
using System.Collections.Generic;

namespace StudentRecords.Models
{
    public partial class Batch
    {
        public Batch()
        {
            Registrations = new HashSet<Registration>();
        }

        public int Id { get; set; }
        public string? Batch1 { get; set; }
        public string? Year { get; set; }

        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
