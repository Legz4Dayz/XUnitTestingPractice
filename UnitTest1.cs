
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Commander;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Commander.Models;
using System;
using System.Collections.Generic;

namespace MVCPractice.Test
{       
    //https://timdeschryver.dev/blog/how-to-test-your-csharp-web-api
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Startup>>
    {
        public HttpClient _client {get; }

        public UnitTest1(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task Test1Async()
        {
            var response = await _client.GetAsync("api/commands");
            
            Assert.Equal(response.StatusCode, HttpStatusCode.OK);
        }

        [Fact]
        public async Task CommandsQueryAsync()
        {
            var response = await _client.GetAsync("api/commands");

            List<Command> commands = JsonConvert.DeserializeObject<List<Command>>(await response.Content.ReadAsStringAsync());

            Assert.Equal(commands.Count, 6 );
        }
    }
}
