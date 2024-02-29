using Domain.Entities;
using Domain.Model;

namespace Application.Interfaces.Services
{
    public interface IArtworkService
    {
        Task<IEnumerable<ArtworkDTO>> GetAllArtworks();
        Task<ArtworkDTO> GetArtworkById(int id);
        Task<ResponseDTO> AddArtwork(ArtworkAddDTO artwork, string UserId);
        Task<ResponseDTO> UpdateArtwork(ArtworkUpdateDTO artwork);
        Task<IEnumerable<ArtworkDTO>> GetArtworkByFilter(ArtworkFilterParameterDTO filter);
        Task<string> GetUserIdByArtworkId(int id);
    }
}
