namespace AppointmentSystem.Infrastructure.Data.Identity
{
    using AppointmentSystem.Core.Entities.Base;
    using Microsoft.AspNetCore.Identity;
    using System;
    public class ApplicationRole : IdentityRole<string>, IEntity
    {
        public ApplicationRole()
            : this(null)
        {

        }

        public ApplicationRole(string name)
            : base(name)
        {
            Id = Guid.NewGuid().ToString();
        }

        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
