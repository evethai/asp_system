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
            CreateMap<Notification, NotificationDTO>().ReverseMap();
            CreateMap<Notification, CreateNotificationDTO>().ReverseMap();
            CreateMap<UserNofitication, UserNotificationDTO>().ReverseMap();
            CreateMap<UserNofitication, CreateUserNotificationDTO>().ReverseMap();
            CreateMap<Category,CatalogyDTO>().ReverseMap();
            CreateMap<Category, CatalogyAddDTO>().ReverseMap();
            CreateMap<Package, PackageDTO>().ReverseMap();
            CreateMap<Package, PackageAddDTO>().ReverseMap();
            CreateMap<Poster, PosterDTO>().ReverseMap();
            CreateMap<Poster, PosterAddDTO>().ReverseMap();
            CreateMap<Artwork, ArtworkDTO>().ReverseMap();
            CreateMap<Artwork, ArtworkAddDTO>().ReverseMap();
            CreateMap<Artwork, ArtworkUpdateDTO>().ReverseMap();
            CreateMap<ArtworkImage, ArtworkImageDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, OrderCreateDTO>().ReverseMap();
            CreateMap<Order, OrderUpdateDTO>().ReverseMap();
            CreateMap<Order, OrderDeleteDTO>().ReverseMap();
            CreateMap<Like, LikeDTO>().ReverseMap();
            CreateMap<Like, LikeCreateDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();
		    CreateMap<Comment, CommentAddDTO>().ReverseMap();
			CreateMap<Comment, CommentUpdateDTO>().ReverseMap();

            CreateMap<ApplicationUser, UserRoles>().ReverseMap();
            CreateMap<UserRoles, UserRolesVM>().ReverseMap();
        }
    }
}
