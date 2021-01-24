using System.Linq;
using DatascopeTest.Helpers;
using Xunit;

namespace DatascopeTest.Tests.Helpers
{
    public class PagedListTests
    {
        [Fact]
        public void HasPreviousPage_CollectionHasNoPreviousPage_ReturnsFalse()
        {
            var collection = new[] {1};
            var sut = new PagedList<int>(collection, collection.Length, page: 1, pageSize: 1);

            var result = sut.HasPreviousPage;

            Assert.False(result);
        }

        [Fact]
        public void HasPreviousPage_CollectionHasAPreviousPage_ReturnsTrue()
        {
            var collection = new[] { 1, 2 };
            var sut = new PagedList<int>(collection, collection.Length, page: 2, pageSize: 1);

            var result = sut.HasPreviousPage;

            Assert.True(result);
        }

        [Fact]
        public void HasNextPage_CollectionHasNoNextPage_ReturnsFalse()
        {
            var collection = new[] { 1 };
            var sut = new PagedList<int>(collection, collection.Length, page: 1, pageSize: 1);

            var result = sut.HasNextPage;

            Assert.False(result);
        }

        [Fact]
        public void HasNextPage_CollectionHasANextPage_ReturnsTrue()
        {
            var collection = new[] { 1, 2 };
            var sut = new PagedList<int>(collection, collection.Length, page: 1, pageSize: 1);

            var result = sut.HasNextPage;

            Assert.True(result);
        }

        [Theory]
        [InlineData(6, 5, 2)]
        [InlineData(10, 5, 2)]
        [InlineData(5, 5, 1)]
        [InlineData(0, 5, 0)]
        public void TotalPages(int listSize, int pageSize, int expectedPages)
        {
            var collection = Enumerable.Range(1, listSize).ToList();
            var sut = new PagedList<int>(collection, collection.Count, page: 1, pageSize);

            var result = sut.TotalPages;

            Assert.Equal(expectedPages, result);
        }
    }
}
