using AutoMapper;
using Domain.Entities;
using Domain.Model;

namespace API.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Artwork,ArtworkDTO>().ReverseMap();
            CreateMap<Artwork,ArtworkAddDTO>().ReverseMap();
            CreateMap<Artwork,ArtworkUpdateDTO>().ReverseMap();
            CreateMap<Order,OrderDTO>().ReverseMap();
        }
    }
}
