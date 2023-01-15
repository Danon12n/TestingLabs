using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Moq;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using NUnit.Framework;
using Org.BouncyCastle.Asn1.X500;
using pet_shop.Controllers;
using pet_shop.MySQLRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace pet_shop.IntegrationTests
{
    [TestFixture]
    public class SomeShitTest
    {
        private string database = $"pet_shop_test_{new Random().Next(0, 9999999)}";
        private MySqlConnection testConn = new MySqlConnection($"Server=localhost;User Id=root;port=3306;password=root;SSL Mode=none");

        public async Task PreTestFunc()
        {

            testConn.Open();

            string script = File.ReadAllText("H:\\GitHub\\my-useful-site\\TestingLabs\\pet_shop.IT\\pet_shop_test.sql");

            script = script.Replace("-- Database: `pet_shop`", $"-- Database: `{database}`");

            var cmd = new MySqlCommand($"CREATE DATABASE IF NOT EXISTS {database}", testConn);
            await cmd.ExecuteNonQueryAsync();

            testConn.ChangeDatabase(database);

            cmd = new MySqlCommand(script, testConn);
            await cmd.ExecuteNonQueryAsync();

            testConn.Close();

        }

        public async Task PostTestFunc()
        {
            testConn.Open();
            var cmd = new MySqlCommand($"DROP DATABASE {database}", testConn);
            await cmd.ExecuteNonQueryAsync();
            testConn.Close();
        }

        [Test]
        public async Task CreateTestDB()
        {


            await PreTestFunc();
            var rep = new UserMySQLRepository()

            //await PostTestFunc();
            // Arrange

            /*
             
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
             */

        }


    }
}
