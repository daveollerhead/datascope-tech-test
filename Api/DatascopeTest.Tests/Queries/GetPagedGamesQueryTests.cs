using DatascopeTest.Queries;
using Xunit;

namespace DatascopeTest.Tests.Queries
{
    public class GetPagedGamesQueryTests
    {
        [Fact]
        public void Page_SetPageToLessThanOne_ReturnsOne()
        {
            var sut = new GetPagedGamesQuery {Page = -1};

            var result = sut.Page;

            Assert.Equal(1, result);
        }

        [Fact]
        public void Page_SetPageToMoreThanOne_ReturnsSetValue()
        {
            var sut = new GetPagedGamesQuery { Page = 2 };

            var result = sut.Page;

            Assert.Equal(2, result);
        }

        [Fact]
        public void PageSize_NotSet_ReturnsDefaultValueOfTen()
        {
            var sut = new GetPagedGamesQuery();

            var result = sut.PageSize;

            Assert.Equal(10, result);
        }

        [Fact]
        public void PageSize_SetHigherThanMaximumValue_ReturnsMaximumValue()
        {
            var sut = new GetPagedGamesQuery{PageSize = 101};

            var result = sut.PageSize;

            Assert.Equal(100, result);
        }

        [Fact]
        public void PageSize_SetBelowMaximumRange_ReturnsSetValue()
        {
            var sut = new GetPagedGamesQuery { PageSize = 5 };

            var result = sut.PageSize;

            Assert.Equal(5, result);
        }

        [Fact]
        public void PageSize_SetBelowOne_ReturnsDefaultValue()
        {
            var sut = new GetPagedGamesQuery { PageSize = -1 };

            var result = sut.PageSize;

            Assert.Equal(10, result);
        }
    }
}
