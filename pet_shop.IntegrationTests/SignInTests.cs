using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using MySqlX.XDevAPI;
using NUnit.Framework;
using pet_shop.Controllers;
using pet_shop.MySQLRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace pet_shop.IntegrationTests
{
    [TestFixture]
    public class SignInTests
    {
        [Test]
        public async Task SignIn_WithIncorrectData_ReturnsMessageThatUserWasntFound()
        {
            // Arrange

            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });

            HttpClient client = webHost.CreateClient();

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/SignIn/Check");
            var data = new Dictionary<string, string>
            {
                {"login", "lalalala" },
                {"password","lalalala" }
            };
            postRequest.Content = new FormUrlEncodedContent(data);

            // Act
            var response = await client.SendAsync(postRequest);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(responseString.Contains("User Not Found!"));
        }

        [Test]
        public async Task SignIn_WithIncorrectPassword_ReturnsMessageOfIncorrectPassword()
        {
            // Arrange

            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });

            HttpClient client = webHost.CreateClient();

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/SignIn/Check");
            var data = new Dictionary<string, string>
            {
                {"login", "admin" },
                {"password","lalalala" }
            };
            postRequest.Content = new FormUrlEncodedContent(data);

            // Act
            var response = await client.SendAsync(postRequest);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(responseString.Contains("Incorrect password!"));
        }

        [Test]
        public async Task SignIn_WithCorrectData_ReturnsToHomePageWithSuccessMessage()
        {
            // Arrange

            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });

            HttpClient client = webHost.CreateClient();

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/SignIn/Check");
            var data = new Dictionary<string, string>
            {
                {"login", "admin" },
                {"password","1234" }
            };
            postRequest.Content = new FormUrlEncodedContent(data);

            // Act
            var response = await client.SendAsync(postRequest);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(responseString.Contains("System message: Everything all right!"));
        }

        [Test]
        public async Task AccessToAdminMenu_WithAuthorizingAsCustomer_ReturnsMessageOfNotEnoughRights()
        {
            // Arrange

            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });

            HttpClient client = webHost.CreateClient();

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/SignIn/Check");
            var data = new Dictionary<string, string>
            {
                {"login", "login" },
                {"password","password" }
            };
            postRequest.Content = new FormUrlEncodedContent(data);

            // Act
            var response = await client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();

            var accessResponse = await client.GetAsync("https://localhost:5001/Admin");
            accessResponse.EnsureSuccessStatusCode();

            // Assert
            
            var responseString = await accessResponse.Content.ReadAsStringAsync();
            Assert.IsTrue(responseString.Contains("System message: Not enough rights to access!"));
        }
    }
}
