using System;
using System.Collections.Generic;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;
using VacationRental.Infrastructure.Exceptions;
using VacationRental.Logic.DTOs;
using VacationRental.Logic.Interfaces;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarLogic _calendarLogic;

        public CalendarController(ICalendarLogic calendarLogic)
        {
            _calendarLogic = calendarLogic;
        }

        [HttpGet]
        public async Task<CalendarViewModel> Get([FromQuery] CalendarBookingRequestModel requestDto,CancellationToken ct)
        {
            try
            {
                var config = new TypeAdapterConfig();
                config.NewConfig<DateTime, DateOnly>().MapWith(src => new DateOnly(src.Year, src.Month, src.Day));
                config.NewConfig<DateOnly, DateTime>().MapWith(src => new DateTime(src.Year, src.Month, src.Day));
                var callendarBookingDto = requestDto.Adapt<CalendarBookingRequestDto>(config);
                var calendarDto = await _calendarLogic.GetCalendarBookings(callendarBookingDto, ct);
                return calendarDto.Adapt<CalendarViewModel>(config);
            }
            catch (RentalNotFoundException ex)
            {
                throw new ApplicationException("Rental not found");
            }
        }
    }

}
