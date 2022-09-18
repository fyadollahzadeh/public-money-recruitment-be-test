using VacationRental.Infrastructure.Entities;
using VacationRental.Infrastructure.Exceptions;
using VacationRental.Logic.DTOs;

namespace VacationRental.Logic.Interfaces
{
    public interface ICalendarLogic
    {
        Task<CalendarBookingResultDto> GetCalendarBookings(CalendarBookingRequestDto callendarBookingDto, CancellationToken ct);
    }

    public class CalendarLogic : ICalendarLogic
    {
        private readonly IBookingLogic _bookingsLogic;
        private readonly IRentalLogic _rentalsLogic;
        public CalendarLogic(IBookingLogic bookingsLogic, IRentalLogic rentalsLogic)
        {
            _bookingsLogic = bookingsLogic;
            _rentalsLogic = rentalsLogic;
        }

        public async Task<CalendarBookingResultDto> GetCalendarBookings(CalendarBookingRequestDto calendarBookingDto,CancellationToken ct)
        {
            var rental = await _rentalsLogic.GetRentalAsync(calendarBookingDto.RentalId, ct);
            if (rental == null) throw new RentalNotFoundException("Rental not found");


            var result = new CalendarBookingResultDto
            {
                RentalId = calendarBookingDto.RentalId,
                Dates = new ()
            };
            for (var i = 0; i < calendarBookingDto.Nights; i++)
            {
                var startDate = calendarBookingDto.Start.Date.AddDays(i);
                var calendarViewModel = new CalendarDateDto
                {
                    Date = startDate,
                    Bookings = new (),
                    PreparationTimes = new List<PreparationTime>()
                };
                result.Dates.Add(calendarViewModel);
            }

            return result;
        }
    }
}
