using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatascopeTest.Data;
using DatascopeTest.Data.Repositories;
using DatascopeTest.Tests.TestHelpers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DatascopeTest.Tests.Data.Repositories
{
    public class GamesRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly GamesRepository _sut;

        public GamesRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(nameof(GamesRepositoryTests))
                .Options;

            _context = new AppDbContext(options);
            SeedGames();
            _sut = new GamesRepository(_context);
        }

        [Theory]
        [InlineData(2, 1, 4)]
        [InlineData(2, 2, 3)]
        [InlineData(5, 1, 1)]
        public async Task GetPaged(int page, int pageSize, int expectedIdOfFirstGameInPage)
        {
            var result = (await _sut.GetPaged(page, pageSize)).ToList();

            var first = result.FirstOrDefault();
            Assert.NotNull(first);
            Assert.Equal(expectedIdOfFirstGameInPage, first.Id);
            Assert.Equal(pageSize, result.Count);
        }

        [Fact]
        public async Task GetPaged_PageRequestIsBeyondTotalPages_ReturnsEmptyList()
        {
            var result = await _sut.GetPaged(2, 5);
            Assert.Empty(result);
        }

        private void SeedGames()
        {
            if (_context.Games.Any())
                return;

            _context.Games.AddRange(GameFactory.Random(5));

            _context.SaveChanges();
        }
    }
}
