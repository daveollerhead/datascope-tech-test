using DatascopeTest.Data.Repositories;
using DatascopeTest.Models;
using Moq;

namespace DatascopeTest.Tests.TestHelpers.Extensions
{
    public static class MockRepositoryExtensions
    {
        public static void SetupGet<T>(this Mock<IGenericRepository<T>> source, T entity) where T : Entity
        {
            source.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(entity);
        }
    }
}