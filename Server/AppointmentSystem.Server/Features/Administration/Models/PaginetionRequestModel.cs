namespace AppointmentSystem.Server.Features.Administration.Models
{
    public class PaginetionRequestModel
    {
        public PaginetionRequestModel(int pageSize,int pageNumber)
        {
            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
        }
        public int PageSize { get; set; }

        public int PageNumber { get; set; } = 1;
    }
}
