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
            CreateMap<Category,CatalogyDTO>().ReverseMap();
            CreateMap<Category, CatalogyAddDTO>().ReverseMap();
            CreateMap<Package, PackageDTO>().ReverseMap();
            CreateMap<Package, PackageAddDTO>().ReverseMap();
            CreateMap<Poster, PosterDTO>().ReverseMap();
            CreateMap<Poster, PosterAddDTO>().ReverseMap();
        }
    }
}
