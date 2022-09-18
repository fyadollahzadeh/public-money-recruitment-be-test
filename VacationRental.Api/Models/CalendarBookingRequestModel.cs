using FluentValidation;

namespace VacationRental.Api.Models
{
    public class CalendarBookingRequestModel
    {
        public int RentalId { set; get; }
        public DateTime Start { set; get; }
        public int Nights { set; get; }
    }
    public class CalendarBookingRequestValidator : AbstractValidator<CalendarBookingRequestModel>
    {
        public CalendarBookingRequestValidator()
        {
            RuleFor(x => x.Nights).GreaterThan(0);
        }
    }
}
