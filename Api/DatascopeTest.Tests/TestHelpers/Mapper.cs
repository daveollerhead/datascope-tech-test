using AutoMapper;
using DatascopeTest.Mappings;

namespace DatascopeTest.Tests.TestHelpers
{
    public static class Mapper
    {
        public static IMapper Init()
        {
            var config = new MapperConfiguration(x => x.AddProfile<MappingProfile>());
            return config.CreateMapper();
        }
    }
}