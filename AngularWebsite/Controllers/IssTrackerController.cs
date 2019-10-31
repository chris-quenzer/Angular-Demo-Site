using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AngularWebsite.Controllers
{
    [Route("api/[controller]")]
    public class IssTrackerController : Controller
    {
        [HttpGet]
        [Route("[action]")]
        public dynamic GetIssLocation()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage result = null;

            IEnumerable<dynamic> issLocation = null;

            try
            {
                result = httpClient.GetAsync(@"http://api.open-notify.org/iss-now.json").Result;
                var contents = result.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(contents);

                issLocation = JsonConvert.DeserializeObject<dynamic>(json.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e.Message}");
            }

            return issLocation;
        }

        [HttpGet]
        [Route("[action]")]
        public dynamic GetIssPassTimes(string myLat, string myLng)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage result = null;

            IEnumerable<dynamic> issPassTimes = null;

            try
            {
                result = httpClient.GetAsync($@"http://api.open-notify.org/iss-pass.json?lat={myLat}&lon={myLng}&n=5").Result;
                var contents = result.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(contents);

                issPassTimes = JsonConvert.DeserializeObject<dynamic>(json.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e.Message}");
            }

            return issPassTimes;
        }

        [HttpGet]
        [Route("[action]")]
        public dynamic GetPeopleInSpace()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage result = null;

            IEnumerable<dynamic> peopleInSpace = null;

            try
            {
                result = httpClient.GetAsync(@"http://api.open-notify.org/astros.json").Result;
                var contents = result.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(contents);

                peopleInSpace = JsonConvert.DeserializeObject<dynamic>(json.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e.Message}");
            }

            return peopleInSpace;
        }
    }
}
