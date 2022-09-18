using Mapster;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;
using VacationRental.Infrastructure.Entities;
using VacationRental.Infrastructure.Exceptions;
using VacationRental.Logic.DTOs;
using VacationRental.Logic.Interfaces;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingLogic _bookingLogic;

        public BookingsController(IBookingLogic bookingLogic)
        {
            _bookingLogic = bookingLogic;
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public async Task<BookingViewModel> Get(int bookingId, CancellationToken ct)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DateOnly, DateTime>().MapWith(src => new DateTime(src.Year, src.Month, src.Day));
            
            var bookingItem = await _bookingLogic.GetBookingAsync(bookingId, ct);
            return bookingItem.Adapt<BookingViewModel>(config);
        }

        [HttpPost]
        public async Task<ResourceIdViewModel> Post(BookingBindingModel model, CancellationToken ct)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DateTime,DateOnly>() .MapWith(src => new DateOnly(src.Year, src.Month, src.Day));

            try
            {

                var addedBookingId = await _bookingLogic.AddBookingAsync(model.Adapt<BookingCreationDto>(config), ct);

                var result = new ResourceIdViewModel { Id = addedBookingId };
                return result;
            }
            catch (NotAvailableForBookingException ex)
            {

                throw new ApplicationException("Not Available For Booking");
            }
        }
    }
}
