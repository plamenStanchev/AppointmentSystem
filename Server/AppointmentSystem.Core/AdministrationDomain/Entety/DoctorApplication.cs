namespace AppointmentSystem.Core.AdministrationDomain.Entety
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Mapper;
    using System;
    public class DoctorApplication : ApplicationBase,IMapTo<Doctor>
    {
        
        public DoctorApplication()
        {
            // EF
        }
        public DoctorApplication(
            string accountId,
            string firstName, 
            string secondName,
            string surName, 
            string PIN, 
            int cityId,
            string description,
            int deparmentId,
            AdministationInformationEntry administationInformationEntry) : base(accountId)
        {
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.SurName = surName;
            this.PIN = PIN;
            this.CityId = cityId;
            this.Description = description;
            this.DepartmentId = deparmentId;
            base.AddAdministationInformationEntry(administationInformationEntry);
        }
        public string FirstName { get; private set; }
        public string SecondName { get; private  set; }
        public string SurName { get; private  set; }
        public string PIN { get; private set; }
        public int CityId { get; private set; }
        public string Description { get; private set; }
        public int DepartmentId { get; private  set; }

        //Validation Logig

        public override void Approve(AdministationInformationEntry administationInformationEntry)
        {
            base.AddAdministationInformationEntry(administationInformationEntry);// can have some buisness rules for example to reject approve if surten conditions arent met and Entety Needs Updates/Corections
        }

        public override void Rejected(AdministationInformationEntry administationInformationEntry)
        {
            base.AddAdministationInformationEntry(administationInformationEntry);//read Approve
        }
    }
}
