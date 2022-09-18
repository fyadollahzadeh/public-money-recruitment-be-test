using Mapster;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;
using VacationRental.Logic.Interfaces;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalLogic _rentalLogic;
        public RentalsController(IRentalLogic rentalLogic)
        {
            _rentalLogic = rentalLogic;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        public async Task<RentalViewModel> Get(int rentalId, CancellationToken ct)
        {
            var rentalEntity = await _rentalLogic.GetRentalAsync(rentalId, ct);
            return rentalEntity.Adapt<RentalViewModel>();
        }

        [HttpPost]
        public async Task<RentalViewModel> Post(RentalBindingModel model, CancellationToken ct)
        {
            var addedItem = await _rentalLogic.AddRentalAsync(model.Adapt<Logic.DTOs.RentalCreationDto>(), ct);

            return new RentalViewModel
            {
                Id = addedItem,
                Units = model.Units
            };
        }
    }
}
