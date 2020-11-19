﻿namespace AssignmentSystem.Core.Entities.Models
{
    using AssignmentSystem.Core.Entities.Base;
    using System.Collections.Generic;
    public class City : DeletableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Doctor> Doctors { get; set; } 
            = new HashSet<Doctor>();
    }
}
