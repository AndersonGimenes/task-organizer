using AutoMapper;
using TaskOrganizer.Api.Mapping;

namespace TaskOrganizer.IntegrationTest.TaskIntegrationTest.Common
{
    public static class CreateMapper
    {
        public static IMapper CreateMapperProfile()
        {
            //Mapper configuration
            var mappingConfiguration = new MapperConfiguration(x => 
            { 
                x.AddProfile(new MappingProfile());
            });

            var mapper = mappingConfiguration.CreateMapper();
            
            return new Mapper(mappingConfiguration);
        }
    }
}