namespace MiniGigWebApi.Data
{
    using AutoMapper;
    using MiniGigWebApi.Domain;

    public class GigMappingProfile : Profile
    {
        public GigMappingProfile()
        {
            CreateMap<Gig, GigModel>()
              .ForMember(c => c.Category, opt => opt.MapFrom(m => m.MusicGenre.Category))
              .ReverseMap();
        }
    }
}