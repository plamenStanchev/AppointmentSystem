namespace AppointmentSystem.Server.Features.ApplicationRequest.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Mapper;
    using AutoMapper;

    public class ApplicationRequestModel : IMapTo<ApplicationRequest>, IHaveCustomMappings
    {
        [Required]
        public string AccountId { get; set; }

        [Required]
        public string Data { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string RequestType { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationRequestModel, ApplicationRequest>()
                .ForMember(d => d.Status,
                    opt => opt.MapFrom(x => Enum.Parse<StatusEnum>(x.Status)))
                .ForMember(d => d.RequestType,
                    opt => opt.MapFrom(x => Enum.Parse<TypeRequestEnum>(x.RequestType)));
        }
    }
}