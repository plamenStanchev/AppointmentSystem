namespace AppointmentSystem.Server.Features.Doctors.Models
{
    public class DoctorRequsetModel
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string SurName { get; set; }

        public string PIN { get; set; }

        public int CityId { get; set; }

        public int DepartmentId { get; set; }

        public string AccountId { get; set; }
    }
}
