using AutoMapper;

namespace TaskOrganizer.Repository.Mapping
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

            return new Mapper(mappingConfiguration);
        }
    }
}