using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DatascopeTest.Commands;
using DatascopeTest.Data.Repositories;
using DatascopeTest.Models;
using DatascopeTest.Tests.TestHelpers;
using DatascopeTest.Tests.TestHelpers.Extensions;
using FluentValidation;
using Moq;
using Xunit;
using ValidationException = DatascopeTest.Exceptions.ValidationException;

namespace DatascopeTest.Tests.Commands
{
    public class CreateGameCommandHandlerTests
    {
        private readonly Mock<IGenericRepository<Game>> _mockRepository = new Mock<IGenericRepository<Game>>();
        private readonly Mock<IValidator<CreateGameCommand>> _mockValidator = new Mock<IValidator<CreateGameCommand>>();

        private readonly CreateGameCommandHandler _sut;

        public CreateGameCommandHandlerTests()
        {
            _sut = new CreateGameCommandHandler(_mockRepository.Object, _mockValidator.Object, Mapper.Init());
        }

        [Fact]
        public async Task Handle_CommandFailsValidation_ThrowsValidationException()
        {
            _mockValidator.SetupValidateAsyncFails("property", "message");

            var ex = await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(new CreateGameCommand(), new CancellationToken()));
            
            Assert.Contains("property", ex.Errors.Select(x => x.PropertyName));
            Assert.Contains("message", ex.Errors.Select(x => x.ErrorMessage));
        }

        [Fact]
        public async Task Handle_CommandPassesValidation_GameAddedToDb()
        {
            _mockValidator.SetupValidateAsyncPasses();

            await _sut.Handle(new CreateGameCommand(), new CancellationToken());

            _mockRepository.Verify(x => x.Add(It.IsAny<Game>()), Times.Once);
            _mockRepository.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
