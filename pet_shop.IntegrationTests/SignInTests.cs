using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
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
        public async Task SignInTest()
        {
            // Arrange

            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });

            HttpClient client = webHost.CreateClient();

            var data = new SignInFormData("notExistingLogin........", "password");

            var mock = new Mock<IUserMySQLRepository>();
            mock.Setup(a => a.GetUserByLogin(data.login)).Returns(new Models.User() );
            SignInController controller = new SignInController();
            string expected = "Пользователь не найден!";

            // Act
            

            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:5001/SignIn/Check", data) ;

            // Assert

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
