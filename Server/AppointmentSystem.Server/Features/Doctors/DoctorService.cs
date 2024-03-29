﻿namespace AppointmentSystem.Server.Features.Doctors
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Core.Interfaces.Features;
    using AppointmentSystem.Core.Interfaces.Repository;
    using AppointmentSystem.Infrastructure.Constants;
    using AppointmentSystem.Infrastructure.Data.Identity;
    using AppointmentSystem.Infrastructure.Services;
    using AppointmentSystem.Infrastructure.Extensions;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Threading;

    //TODO : Move validation in difrent methods
    internal class DoctorService : IDoctorService
    {
        private readonly IDeletableEntityRepository<Doctor> repository;
        private readonly UserManager<ApplicationUser> userManager;
        public DoctorService(
            IDeletableEntityRepository<Doctor> repository,
            UserManager<ApplicationUser> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }

        public async Task<Result> CreateDoctorAsync(Doctor doctor, CancellationToken cancellationToken = default)
        {
            var user = await this.userManager.FindByIdAsync(doctor.AccountId);
            if (user is null)
            {
                return "this doctor account id dosent exist";
            }
            var doctorExists = await this.GetDoctorAsync(doctor.AccountId, cancellationToken);

            if (doctorExists is not null)
            {
                return "Doctor Exists";
            }

            await this.repository.AddAsync(doctor);

            var result = await this.userManager
                .AddToRoleAsync(user, RolesNames.Doctor);

            return result.Succeeded switch
            {
                true => await this.repository.SaveChangesAsync(cancellationToken) != default,
                _ => result.GetError()
            };
        }

        public async Task<Result> DeleteDoctorAsync(string accountId, CancellationToken cancellationToken = default)
        {
            var doctorResult = await this.repository.All()
                .FirstOrDefaultAsync(d => d.AccountId == accountId);

            if (doctorResult is null)
            {
                return "Couldnt Find Patient In Db";
            }
            if (doctorResult.AccountId != accountId)
            {
                return "Couldnt Find AccountId In Db";
            }

            this.repository.Delete(doctorResult);

            return await this.repository.SaveChangesAsync(cancellationToken) != default;
        }

        public async Task<Result> UpdateDoctorAsync(Doctor doctor, CancellationToken cancellationToken = default)
        {
            this.repository.Update(doctor);

            return await this.repository.SaveChangesAsync(cancellationToken) != default;
        }

        public async Task<Doctor> GetDoctorAsync(string accoutId, CancellationToken cancellationToken = default)
            => await this.repository.All()
                .FirstOrDefaultAsync(d => d.AccountId == accoutId, cancellationToken);

        public async Task<IEnumerable<Doctor>> GetDoctorsInCity(int cityId, CancellationToken cancellationToken = default)
        {
            var doctorListObject = await this.repository.All()
              .Where(d => d.CityId == cityId)
              .Select(d => new Doctor()
              {
                  City = d.City,
                  FirstName = d.FirstName,
                  SecondName = d.SecondName,
                  SurName = d.SurName,
                  Description = d.Description,
                  Department = d.Department
              })
              .ToListAsync(cancellationToken);

            // .Select(d => new
            // {
            //     Doctor = new Doctor()
            //     {
            //         City = d.City,
            //         FirstName = d.FirstName,
            //         SecondName = d.SecondName,
            //         SurName = d.SurName,
            //         Description = d.Description,
            //         Department = d.Department
            //     },
            //     DepartmentName = d.Department.Name,
            //     CityName = d.City.Name
            // })
            return doctorListObject;
        }
    }
}
