using AutoMapper;
using TaskOrganizer.Repository.Mapping;

namespace TaskOrganizer.IntegrationTest.TaskIntegrationTest.Common
{
    public static class CreateMapper
    {
        public static IMapper CreateMapperProfile()
        {
            //Mapper configuration
            var mappingConfiguration = new MapperConfiguration(x => 
            { 
                x.AddProfile(new MappingProfileRepository());
            });

            return new Mapper(mappingConfiguration);
        }
    }
}