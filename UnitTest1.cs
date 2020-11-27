
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
using MVCPractice.Test.Commander;

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

        //GET api/commands/{id}
        [Theory]
        [MemberData(nameof(Data))]
        public async Task MultipleCommands(int id, Command expected)
        {   
            var response = await _client.GetAsync($"api/commands/{id}");
            Command responseCommand = JsonConvert.DeserializeObject<Command>(await response.Content.ReadAsStringAsync());

            Assert.Equal(expected.Id, responseCommand.Id);
            Assert.Equal(expected.HowTo, responseCommand.HowTo);
            Assert.Equal(expected.Line, responseCommand.Line);
            Assert.Equal(expected.Platform, responseCommand.Platform);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] {4, new Command(){
                    Id = 4,
                    HowTo = "Run a .NET Core app",
                    Line = "dotnet run",
                    Platform = null
                }},
                new object[] {2, new Command(){
                    Id = 2,
                    HowTo = "How to run migrations",
                    Line = "dotnet database update",
                    Platform = null
                }},
                new object[] {3, new Command(){
                    Id = 3,
                    HowTo = "Run a .NET Core app",
                    Line = "dotnet run",
                    Platform = null
                }}
            };
    }
}
