namespace VacationRental.Api.Models
{
    public class BaseResponse
    {
        public BaseResponse(object data=null,EStatusCode statusCode = EStatusCode.OK,string message = "")
        {
            Data = data;
            StatusCode = statusCode;
            Message = message;
        }
        public object Data { set; get; }
        public EStatusCode StatusCode { set; get; }
        public string Message { set; get; }
    }

    public enum EStatusCode
    {
        OK=200,
        ServerError = 500,
        RentalNotFound = 900,
        EntityNotFound = 901,
    }
}
