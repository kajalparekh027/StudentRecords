using System;
using System.Collections.Generic;

namespace StudentRecords.Models
{
    public partial class Course
    {
        public Course()
        {
            Registrations = new HashSet<Registration>();
        }

        public int Id { get; set; }
        public string? Course1 { get; set; }
        public int? Duration { get; set; }

        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
