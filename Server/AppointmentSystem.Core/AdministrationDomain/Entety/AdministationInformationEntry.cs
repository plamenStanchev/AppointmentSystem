namespace AppointmentSystem.Core.AdministrationDomain.Entety
{
    using AppointmentSystem.Core.Entities.Base;

    public class AdministationInformationEntry : Entity //Is it a good Idea to put AdministationInformationEntry Validation her or in ApplicationBase
    {
        public AdministationInformationEntry(Status status)
        {
            this.Status = status;
        }

        public int Id { get; set; }

        public Status Status { get; private set; }

        public int ApplicationId { get; private set; }
    }
}
