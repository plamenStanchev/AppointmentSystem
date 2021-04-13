namespace AppointmentSystem.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> acion)
        {
            foreach (var item in enumerable)
            {
                acion(item);
            }
        }
    }
}