using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApplication;
using WebApplication.Models;
using Xunit;

namespace XUnitTestProject
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        protected IntegrationTest()
        {
            var appfactory = new WebApplicationFactory<Startup>();
            TestClient = appfactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetJwtAsync());
;       }

        private async Task<string> GetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync("api/login", new 
            {
                UserName = "user",
                Password = "1234"
            });

            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsStringAsync().Result;
            JObject jresult = (JObject)JsonConvert.DeserializeObject(result);
            return jresult.Value<string>("token");
        }

        protected async Task<HttpResponseMessage> GetBadJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync("api/login", new
            {
                UserName = "patate",
                Password = "1234"
            });

            return response;
        }
    }
}
