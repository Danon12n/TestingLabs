using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Moq;
using MySql.Data.MySqlClient;
using NUnit.Framework;
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
using pet_shop.Models;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using System.IO;

namespace pet_shop.IntegrationTests
{
    public class SomeShitTest
    {
        private string database = $"pet_shop_test_{new Random().Next(0, 9999999)}";
        private string connString;
        private MySqlConnection testConn = new MySqlConnection($"Server=localhost;User Id=root;port=3306;password=root;SSL Mode=none");

        public async Task PreTestFunc()
        {

            testConn.Open();
            var path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var fullpath = Path.GetFullPath(Path.Combine(path, @"..\..\..\"));
            string script = File.ReadAllText(Path.Combine(fullpath,"pet_shop_test.sql"));
            Console.WriteLine(Path.Combine(fullpath, "pet_shop_test.sql"));

        
            script = script.Replace("-- Database: `pet_shop`", $"-- Database: `{database}`");
            connString = $"Server=localhost;Database={database};User Id=root;port=3306;password=root;SSL Mode=none";

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

        public async Task CreateTestDB()
        {

            // Arrange

            /*
             
             await PreTestFunc();

            VendorController controller = new VendorController();
            var formCol = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
            {
                { "shop_id", "6275_Misty_Pines" },
                { "price", "9999" },
                { "can_swim", "Yes" },
                { "reproduce_ability", "Yes" },
                { "pet_type", "Cat" },
                { "name", "Vlasov" },
                { "age", "10" },
                { "color", "black" },
                { "gender", "Male" },
                { "pet_breed", "Derlansky" }
            });

            var rep = new PetMySQLRepository(connString);

            // Act

            controller.AddNewPet(formCol,connString);

            var PI = rep.GetPetInfoById(5);
            var pet = rep.GetPetById(5);

            // Assert
            Assert.NotNull(PI);
            Assert.NotNull(pet);


            await PostTestFunc();
             
             */



        }


    }
}
