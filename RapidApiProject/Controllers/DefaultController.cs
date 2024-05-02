using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RapidApiProject.Models;
using System.Security.Cryptography.X509Certificates;

namespace RapidApiProject.Controllers
{
    public class DefaultController : Controller
    {
        public async Task<IActionResult>   Index()
        {

            return View();
            
        }

        public async Task<IActionResult> BookingHotelList(string arrivalDate)
        {
            if (!string.IsNullOrEmpty(arrivalDate))
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
               
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://booking-com15.p.rapidapi.com/api/v1/hotels/searchHotels?dest_id=-1456928&search_type=CITY&arrival_date={arrivalDate}&departure_date=2024-05-07&adults=1&children_age=0%2C17&room_qty=1&page_number=1&languagecode=en-us&currency_code=EUR"),
                    Headers =
                {
        { "X-RapidAPI-Key", "51c955dfa9msh53f306b50b0ded1p173382jsn13316903dea8" },
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
            else
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://booking-com15.p.rapidapi.com/api/v1/hotels/searchHotels?dest_id=-1456928&search_type=CITY&arrival_date=2024-05-02&departure_date=2024-05-07&adults=1&children_age=0%2C17&room_qty=1&page_number=1&languagecode=en-us&currency_code=EUR"),
                    Headers =
                {
        { "X-RapidAPI-Key", "51c955dfa9msh53f306b50b0ded1p173382jsn13316903dea8" },
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
}
