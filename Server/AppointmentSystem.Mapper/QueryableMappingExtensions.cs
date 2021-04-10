namespace AppointmentSystem.Mapper
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using AutoMapper.QueryableExtensions;

    public static class QueryableMappingExtensions
    {
        public static IQueryable<TDestination> To<TDestination>(
            this IQueryable source,
            params Expression<Func<TDestination, object>>[] membersToExpand)
            => source switch
            {
                null => throw new ArgumentNullException(nameof(source)),
                _ => source.ProjectTo(AutoMapperConfig.MapperInstance.ConfigurationProvider, null, membersToExpand)
            };

        public static IQueryable<TDestination> To<TDestination>(
            this IQueryable source,
            object parameters)
            => source switch
            {
                null => throw new ArgumentNullException(nameof(source)),
                _ => source.ProjectTo<TDestination>(AutoMapperConfig.MapperInstance.ConfigurationProvider, parameters)
            };
    }
}
