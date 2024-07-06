using AutoMapper;

namespace WorkOrder.Tests.Configs
{
    public static class MapConfig
    {
        public static IMapper Get()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WorkOrder.API.Profiles.MappingProfile());
            });

            return mockMapper.CreateMapper();
        }
    }
}
