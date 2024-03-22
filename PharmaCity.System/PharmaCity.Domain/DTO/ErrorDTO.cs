namespace PharmaCity.Domain.DTO
{
    public class ErrorDTO
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string Content { get; set; }
        public int Code { get; set; }
    }
}