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
            CreateMap<Category, CatalogyDTO>().ReverseMap();

        }
    }
}
