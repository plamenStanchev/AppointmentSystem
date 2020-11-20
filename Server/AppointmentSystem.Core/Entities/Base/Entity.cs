namespace AppointmentSystem.Core.Entities.Base
{
    using System;
    public abstract class Entity : IEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
    }
}
