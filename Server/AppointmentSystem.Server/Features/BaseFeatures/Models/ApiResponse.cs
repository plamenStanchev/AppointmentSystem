namespace AppointmentSystem.Server.Features.BaseFeatures.Models
{
	using System.Collections.Generic;
	using System.Linq;

	public class ApiResponse<T>
	{
		public ApiResponse() { }

		public ApiResponse(T data) => this.Data = data;

		public ApiResponse(IEnumerable<ApiError> errors)
		{
			if (errors == null || !errors.Any())
			{
				this.Errors = new List<ApiError> { new ApiError(nameof(ApiResponse<T>), "Unspecified error.") };
			}
			else
			{
				this.Errors = errors;
			}
		}

		public ApiResponse(ApiError error) => this.Errors = new List<ApiError> { error };

		public IEnumerable<ApiError> Errors { get; }

		public T Data { get; }

		public bool IsSuccessful => !this.Errors?.Any() ?? true;
	}

	public static class ApiResponseExtensions
	{
		public static ApiResponse<T> ToApiResponse<T>(this T data) => new(data);
	}
}