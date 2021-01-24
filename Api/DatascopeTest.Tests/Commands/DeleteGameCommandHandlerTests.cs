using System;
using System.Threading;
using System.Threading.Tasks;
using DatascopeTest.Commands;
using DatascopeTest.Data.Repositories;
using DatascopeTest.Exceptions;
using DatascopeTest.Models;
using DatascopeTest.Tests.TestHelpers;
using DatascopeTest.Tests.TestHelpers.Extensions;
using Moq;
using Xunit;

namespace DatascopeTest.Tests.Commands
{
    public class DeleteGameCommandHandlerTests
    {
        private readonly Mock<IGenericRepository<Game>> _mockRepository = new Mock<IGenericRepository<Game>>();

        private readonly DeleteGameCommandHandler _sut;

        public DeleteGameCommandHandlerTests()
        {
            _sut = new DeleteGameCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_NoGameWithGivenIdExists_ThrowsNoEntityExistsException()
        {
            _mockRepository.SetupGet(default);

            var ex = await Assert.ThrowsAsync<NoEntityExistsException>(() => _sut.Handle(new DeleteGameCommand(1), new CancellationToken()));

            Assert.Equal("Could not find Game with id 1", ex.Message);
        }

        [Fact]
        public async Task Handle_GameExistsInDb_RemovesGameFromDb()
        {
            _mockRepository.SetupGet(GameFactory.Random());

            await _sut.Handle(new DeleteGameCommand(1), new CancellationToken());

            _mockRepository.Verify(x => x.Remove(It.IsAny<Game>()), Times.Once);
            _mockRepository.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
