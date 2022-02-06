using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DatascopeTest;
using DatascopeTest.Commands;
using DatascopeTest.Data;
using DatascopeTest.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using Xunit;

namespace DatascopeTests.IntegrationTests
{
    public static class Configuration
    {
        public static IConfiguration Get()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
        }
    }

    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables();
            });

            builder.ConfigureServices(services =>
            {
                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var scopedServices = scope.ServiceProvider;

                var config = scopedServices.GetRequiredService<IConfiguration>();
                var connectionString = config.GetConnectionString("DatascopeTestIntegrationTests");

                Console.WriteLine($"Connection String={connectionString}");

                // DI
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<AppDbContext>));

                services.Remove(descriptor);

                services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));

                // DB
                // var db = scopedServices.GetRequiredService<AppDbContext>();
                // db.Database.Migrate();
            });
        }
    }

    [TestFixture]
    public class IntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>, IDisposable
    {
        private readonly AppDbContext _context = DbContextBuilder.Build();
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _httpClient;

        private Game _game;

        public IntegrationTest()
        {
            _factory = new CustomWebApplicationFactory<Startup>();
            _httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions());
        }

        [Test]
        public async Task Get_ValidGameIdPassed_ReturnsOk()
        {
            _game = new Game("Wow", "World of Wow", new DateTime(2004, 01, 01), 5);
            _context.Games.Add(_game);
            _context.SaveChanges();

            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"games/{_game.Id}"));

            Assert.NotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task Get_InvalidValidGameIdPassed_ReturnsNotFound()
        {
            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, "games/0"));

            Assert.NotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task Create_ValidBodyPassed_ReturnsCreated()
        {
            var gameDto = new CreateGameCommand
            {
                Name = "Wow",
                Description = "World of Wow",
                Rating = 5,
                ReleasedAt = DateTime.UtcNow
            };

            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "games")
            {
                Content = new StringContent(
                    JsonConvert.SerializeObject(gameDto),
                    Encoding.UTF8,
                    "application/json")
            });


            Assert.NotNull(response);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            _game = _context.Games.FirstOrDefault();
            Assert.NotNull(_game);
            var location = response.Headers.GetValues("Location").First();
            Assert.AreEqual(_game.Id.ToString(), location);
        }

        [Test]
        public async Task Create_BodyContainsValidationErrors_ReturnsBadRequest()
        {
            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "games")
            {
                Content = new StringContent(
                    JsonConvert.SerializeObject(new CreateGameCommand()),
                    Encoding.UTF8,
                    "application/json")
            });

            Assert.NotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task Update_NoGameExistsWithGivenId_ReturnsNotFound()
        {
            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, "games/0")
            {
                Content = new StringContent(
                    JsonConvert.SerializeObject(new CreateGameCommand()),
                    Encoding.UTF8,
                    "application/json")
            });

            Assert.NotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task Update_BodyHasValidationErrors_ReturnsBadRequest()
        {
            _game = new Game("Wow", "World of Wow", new DateTime(2004, 01, 01), 5);
            _context.Games.Add(_game);
            _context.SaveChanges();

            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, $"games/{_game.Id}")
            {
                Content = new StringContent(
                    JsonConvert.SerializeObject(new CreateGameCommand()),
                    Encoding.UTF8,
                    "application/json")
            });

            Assert.NotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task Update_ValidBodyPassedIn_ReturnsNoContent()
        {
            _game = new Game("Wow", "World of Wow", new DateTime(2004, 01, 01), 5);
            _context.Games.Add(_game);
            _context.SaveChanges();

            const string updatedName = "Wow - updated";
            var gameDto = new CreateGameCommand
            {
                Name = updatedName,
                Description = "World of Wow",
                Rating = 5,
                ReleasedAt = DateTime.UtcNow
            };

            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, $"games/{_game.Id}")
            {
                Content = new StringContent(
                    JsonConvert.SerializeObject(gameDto),
                    Encoding.UTF8,
                    "application/json")
            });

            Assert.NotNull(response);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

            var game = _context.Games.AsNoTracking().FirstOrDefault();
            Assert.NotNull(game);
            Assert.AreEqual(updatedName, game.Name);
        }

        [Test]
        public async Task Remove_ValidGameIdPassed_ReturnsNoContent()
        {
            _game = new Game("Wow", "World of Wow", new DateTime(2004, 01, 01), 5);
            _context.Games.Add(_game);
            _context.SaveChanges();

            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, $"games/{_game.Id}"));

            Assert.NotNull(response);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

            var deletedGame = _context.Games.AsNoTracking().SingleOrDefault(x => x.Id == _game.Id);
            Assert.IsNull(deletedGame);
            _game = null;
        }

        [Test]
        public async Task Remove_InvalidValidGameIdPassed_ReturnsNotFound()
        {
            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, "games/0"));

            Assert.NotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TearDown]
        public void TearDown()
        {
            if (_game != null)
            {
                _context.Games.Remove(_game);
                _context.SaveChanges();
            }

            _game = null;
        }

        public void Dispose()
        {
            _context?.Dispose();
            _factory?.Dispose();
            _httpClient?.Dispose();

        }
    }
}
