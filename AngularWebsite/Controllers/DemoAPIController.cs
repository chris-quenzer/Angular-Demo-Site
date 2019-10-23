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
    public class DemoAPIController : Controller
    {
        [HttpGet]
        [Route("[action]")]
        public dynamic GetUsers(int? filter_id, string source = "")
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage result = null;

            IEnumerable<dynamic> userData = null;

            try
            {
                result = httpClient.GetAsync(@"http://jsonplaceholder.typicode.com/"+source).Result;
                var contents = result.Content.ReadAsStringAsync().Result;
                JArray json_array = JArray.Parse(contents);

                userData = JsonConvert.DeserializeObject<dynamic>(json_array.ToString());

                // Filter only on filter_id value if it is specified
                if (filter_id != null)
                {
                    if (source == "posts" || source == "albums" || source == "todos")
                    {
                        userData = userData.Where(x => x.userId == filter_id);
                    }
                    else if (source == "comments")
                    {
                        userData = userData.Where(x => x.postId == filter_id);
                    }
                    else if (source == "photos")
                    {
                        userData = userData.Where(x => x.albumId == filter_id);
                    }
                    else if (source == "users")
                    {
                        userData = userData.Where(x => x.id == filter_id);
                    }
                }

                // Grab the 10 latest users, assuming a higher id value means "new"
                userData = userData.OrderByDescending(x => x.id).Take(10);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e.Message}");
            }

            return userData;
        }
    }
}
