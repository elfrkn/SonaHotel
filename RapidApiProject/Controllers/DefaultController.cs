using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RapidApiProject.Models;
using System.Security.Cryptography.X509Certificates;
using static RapidApiProject.Models.BookingListViewModel;

namespace RapidApiProject.Controllers
{
    public class DefaultController : Controller
    {
        public  IActionResult Index()
        {
            
            return View();
           
        }
 

        public async Task<IActionResult> BookingHotelList(BookingSearchViewModel p)
        {
           
            var arrivalDate = p.arrivalDate.ToString("yyyy-MM-dd");
            var departureDate = p.departureDate.ToString("yyyy-MM-dd"); ; 
            var adultCount = p.adultCount.ToString();
            var roomCount = p.roomCount.ToString();


        
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com15.p.rapidapi.com/api/v1/hotels/searchHotels?dest_id=-553173&search_type=CITY&arrival_date={arrivalDate}&departure_date={departureDate}&adults={adultCount}&room_qty={roomCount}&page_number=1&languagecode=en-us&currency_code=EUR"),
                Headers =
    {
        { "X-RapidAPI-Key", "26e74b2436mshecef036cc415a73p148c7cjsncf928ce9fba3" },
        { "X-RapidAPI-Host", "booking-com15.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<BookingListViewModel>(body);
                return View(values.data.hotels.ToList());

            }

        }
    }

}


//var clientGetCityId = new HttpClient();
//var requestGetCityId = new HttpRequestMessage
//{
//    Method = HttpMethod.Get,
//    RequestUri = new Uri($"https://booking-com15.p.rapidapi.com/api/v1/hotels/searchDestination?query={cityName}"),
//    Headers =
//    {
//        { "X-RapidAPI-Key", "26e74b2436mshecef036cc415a73p148c7cjsncf928ce9fba3" },
//        { "X-RapidAPI-Host", "booking-com15.p.rapidapi.com" },
//    },
//};
//var cityID = "";
//using (var response = await clientGetCityId.SendAsync(requestGetCityId))
//{
//    response.EnsureSuccessStatusCode();
//    var body = await response.Content.ReadAsStringAsync();
//    var values = JsonConvert.DeserializeObject<GetCityIdViewsModel>(body);
//    cityID = values.data[0].dest_id;

//}
