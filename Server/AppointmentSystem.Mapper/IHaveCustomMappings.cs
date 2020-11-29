using AutoMapper;

namespace AppointmentSystem.Mapper
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);
    }
}
