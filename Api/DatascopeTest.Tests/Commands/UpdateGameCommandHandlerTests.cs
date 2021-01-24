using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DatascopeTest.Commands;
using DatascopeTest.Data.Repositories;
using DatascopeTest.Exceptions;
using DatascopeTest.Models;
using DatascopeTest.Tests.TestHelpers;
using DatascopeTest.Tests.TestHelpers.Extensions;
using FluentValidation;
using Moq;
using Xunit;
using ValidationException = DatascopeTest.Exceptions.ValidationException;

namespace DatascopeTest.Tests.Commands
{
    public class UpdateGameCommandHandlerTests
    {
        private readonly Mock<IGenericRepository<Game>> _mockRepository = new Mock<IGenericRepository<Game>>();
        private readonly Mock<IValidator<UpdateGameCommand>> _mockValidator = new Mock<IValidator<UpdateGameCommand>>();

        private readonly UpdateGameCommandHandler _sut;

        public UpdateGameCommandHandlerTests()
        {
            _sut = new UpdateGameCommandHandler(_mockRepository.Object, _mockValidator.Object, Mapper.Init());
        }

        [Fact]
        public async Task Handle_CommandFailsValidation_ThrowsValidationException()
        {
            _mockValidator.SetupValidateAsyncFails("property", "message");
            _mockRepository.SetupGet(GameFactory.Random());

            var ex = await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(new UpdateGameCommand(), new CancellationToken()));

            Assert.Contains("property", ex.Errors.Select(x => x.PropertyName));
            Assert.Contains("message", ex.Errors.Select(x => x.ErrorMessage));
        }

        [Fact]
        public async Task Handle_NoGameWithGivenIdExists_ThrowsNoEntityExistsException()
        {
            _mockRepository.SetupGet(default);

            var ex = await Assert.ThrowsAsync<NoEntityExistsException>(() => _sut.Handle(new UpdateGameCommand(), new CancellationToken()));

            Assert.Equal("Could not find Game with id 0", ex.Message);
        }

        [Fact]
        public async Task Handle_CommandPassesValidation_UpdatesGame()
        {
            _mockValidator.SetupValidateAsyncPasses();
            _mockRepository.SetupGet(GameFactory.Random());

            await _sut.Handle(new UpdateGameCommand(), new CancellationToken());

            _mockRepository.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
