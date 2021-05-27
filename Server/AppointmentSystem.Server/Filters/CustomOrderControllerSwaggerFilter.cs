namespace AppointmentSystem.Server.Filters
{
	using System.Linq;
	using Microsoft.OpenApi.Models;
	using Swashbuckle.AspNetCore.SwaggerGen;

	internal class CustomOrderControllerSwaggerFilter : IDocumentFilter
	{
		public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
		{
			var newPaths = new OpenApiPaths();

			swaggerDoc
				.Paths
				.OrderByDescending(x => x.Key.Contains("Identity"))
				.ToList()
				.ForEach(x => newPaths.Add(x.Key, x.Value));

			swaggerDoc.Paths = newPaths;
		}
	}
}