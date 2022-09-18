using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using VacationRental.Api.Models;
using Xunit;

namespace VacationRental.Api.Tests
{
    [Collection("Integration")]
    public class PutRentalTests
    {
        private readonly HttpClient _client;

        public PutRentalTests(IntegrationFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task GivenCompleteRequest_WhenPutRental_ThenAGetReturnsTheUpdateRental()
        {
            var request = new RentalBindingModel
            {
                Units = 14
            };

            ResourceIdViewModel postRentalResult;
            using (var postResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", request))
            {
                Assert.True(postResponse.IsSuccessStatusCode);
                postRentalResult = await postResponse.Content.ReadAsAsync<ResourceIdViewModel>();
            }


            var postBookingRequest = new BookingBindingModel
            {
                RentalId = postRentalResult.Id,
                Nights = 3,
                Start = new DateTime(2001, 01, 01)
            };
            ResourceIdViewModel postBookingResult;
            using (var postBookingResponse = await _client.PostAsJsonAsync($"/api/v1/bookings", postBookingRequest))
            {
                Assert.True(postBookingResponse.IsSuccessStatusCode);
                postBookingResult = await postBookingResponse.Content.ReadAsAsync<ResourceIdViewModel>();
            }


            var postBookingRequest1 = new BookingBindingModel
            {
                RentalId = postRentalResult.Id,
                Nights = 3,
                Start = new DateTime(2001, 01, 01)
            };
            ResourceIdViewModel postBookingResult1;
            using (var postBookingResponse = await _client.PostAsJsonAsync($"/api/v1/bookings", postBookingRequest1))
            {
                Assert.True(postBookingResponse.IsSuccessStatusCode);
                postBookingResult1 = await postBookingResponse.Content.ReadAsAsync<ResourceIdViewModel>();
            }


            var putRequest = new RentalBindingModel
            {
                Units = 25
            };
            ResourceIdViewModel putResult;
            using (var putResponse = await _client.PutAsJsonAsync($"/api/v1/rentals/{postRentalResult.Id}", putRequest))
            {
                Assert.True(putResponse.IsSuccessStatusCode);
                putResult = await putResponse.Content.ReadAsAsync<ResourceIdViewModel>();
            }

            using (var getResponse = await _client.GetAsync($"/api/v1/rentals/{postRentalResult.Id}"))
            {
                Assert.True(getResponse.IsSuccessStatusCode);

                var getResult = await getResponse.Content.ReadAsAsync<RentalViewModel>();
                Assert.Equal(putRequest.Units, getResult.Units);
            }
        }
    }
}
