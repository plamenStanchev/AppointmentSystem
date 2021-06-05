namespace AppointmentSystem.Core.Entities.Models
{
    using AppointmentSystem.Core.Entities.Base;

    public class ApplicationRequest : DeletableEntity
    {
        public int Id { get; set; }

        public string AccountId { get; set; }

        public string Data { get; set; }

        public StatusEnum Status { get; set; }

        public TypeRequestEnum RequestType { get; set; }
    }

    public enum TypeRequestEnum
    {
        DoctorCreation
    }

    public enum StatusEnum
    {
        Approved, Closed,
    }
}