namespace AppointmentSystem.Server.Features.Patients
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Core.Interfaces.Repository;
    using AppointmentSystem.Infrastructure.Data.Identity;
    using AppointmentSystem.Infrastructure.Services;
    using AppointmentSystem.Infrastructure.Constants;
    using AppointmentSystem.Core.Interfaces.Features;

    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using AppointmentSystem.Infrastructure.Extensions;

    //TODO : Move validation in difrent methods
    public class PatientService : IPatientService
    {
        private readonly IDeletableEntityRepository<Patient> repository;
        private readonly UserManager<ApplicationUser> userManager;

        public PatientService(
            IDeletableEntityRepository<Patient> repository,
              UserManager<ApplicationUser> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }

        public async Task<Result> CreatePatientAsync(Patient patient)
        {
            var user = await userManager.FindByIdAsync(patient.AccountId);
            if (user == null)
            {
                return "this patients account id dosent exist";
            }
            var patientExists = await this.GetPatientAsync(patient.AccountId);

            if (patientExists != null)
            {
                return "Patient exists";
            }

            await this.repository.AddAsync(patient);

            var result = await this.userManager
                .AddToRoleAsync(user, RolesNames.Patient);

            return result.Succeeded switch
            {
                true => await this.repository.SaveChangesAsync() != default,
                _ => result.GetError()
            };
        }

        public async Task<Result> DeletePatientAsync(string accountId)
        {
            var patientResult = await this.GetPatientAsync(accountId);

            if (patientResult is null)
            {
                return "Couldnt Find Patient In Db";
            }
            if (patientResult.AccountId != accountId)
            {
                return "Couldnt Find AccountId In Db";
            }

            this.repository.Delete(patientResult);

            return await this.repository.SaveChangesAsync() != default;
        }

        public async Task<Patient> GetPatientAsync(string accountId)
            => await this.repository.All()
                .FirstOrDefaultAsync(p => p.AccountId == accountId);

        public async Task<Result> UpdatePatientAsync(Patient patient)
        {
            this.repository.Update(patient);

            return await this.repository.SaveChangesAsync() != default;
        }
    }
}
