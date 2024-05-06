using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RapidApiProject.Models;
using System.Net.Http;



namespace RapidApiProject.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {

            return View();

        }

        [HttpPost]

        public async Task<IActionResult> GetSearchList(BookingSearchViewModel search)
        {

            var clientGetCityId = new HttpClient();
            var requestGetCityId = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com15.p.rapidapi.com/api/v1/hotels/searchDestination?query={search.cityName}"),
                Headers =
    {
        {  "X-RapidAPI-Key",
                     "01e2c3d585msh1afb95d2ac454c2p1bb84djsnc18b9688678f" },
        { "X-RapidAPI-Host",
                     "booking-com15.p.rapidapi.com" },

    },
            };

            using (var response = await clientGetCityId.SendAsync(requestGetCityId))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetCityIdViewsModel>(body);
                var cityID = values.data[0].dest_id;
                var getSearch = new BookingSearchViewModel
                {
                    destID = cityID,
                    cityName = search.cityName,
                    arrivalDate = search.arrivalDate,
                    departureDate = search.departureDate,
                    adultCount = search.adultCount,
                    roomCount = search.roomCount
                };
                return RedirectToAction("BookingHotelList", getSearch);

            }
        }

        public async Task<IActionResult> BookingHotelList(BookingSearchViewModel search)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com15.p.rapidapi.com/api/v1/hotels/searchHotels?dest_id={search.destID}&search_type=CITY&arrival_date={search.arrivalDate.ToString("yyyy-MM-dd")}&departure_date={search.departureDate.ToString("yyyy-MM-dd")}&adults={search.adultCount}&room_qty={search.roomCount}&page_number=1&languagecode=en-us&currency_code=EUR"),
                Headers =
    {
        { "X-RapidAPI-Key",
                     "01e2c3d585msh1afb95d2ac454c2p1bb84djsnc18b9688678f" },
        { "X-RapidAPI-Host",
                     "booking-com15.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<BookingListViewModel>(body);
                TempData["Photo"] = values.data.hotels[0].property.photoUrls[0].Replace("square60", "square480");
                return View(values.data.hotels.ToList());

            }

        }
        [HttpPost]
        public async Task<IActionResult> GetHotelDetails(string hotelID,string arrivalDate,string departureDate)
        {
            
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com15.p.rapidapi.com/api/v1/hotels/getHotelDetails?hotel_id={hotelID}&arrival_date={arrivalDate}&departure_date={departureDate}&languagecode=en-us&currency_code=EUR"),
                Headers = {
                  {
                     "X-RapidAPI-Key",
                     "01e2c3d585msh1afb95d2ac454c2p1bb84djsnc18b9688678f"
                  },
                  {
                     "X-RapidAPI-Host",
                     "booking-com15.p.rapidapi.com"
                  },
               },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<BookingDetailsViewModel>(body);
                return View(value);
            }
        }
    }
}

