namespace AppointmentSystem.Server.Features.BaseFeatures.Models
{
	public class ApiError
	{
		public ApiError()
		{
		}

		public ApiError(string item, string error)
		{
			this.Item = item;
			this.Error = error;
		}

		public string Item { get; }

		public string Error { get; }

		public override string ToString() => $"{this.Item}: {this.Error}";
	}
}