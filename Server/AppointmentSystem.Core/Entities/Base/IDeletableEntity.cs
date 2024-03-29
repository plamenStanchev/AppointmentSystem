﻿namespace AppointmentSystem.Core.Entities.Base
{
    using System;

    public interface IDeletableEntity : IEntity
    {
        DateTime? DeletedOn { get; set; }

        string DeletedBy { get; set; }

        bool IsDeleted { get; set; }
    }
}
