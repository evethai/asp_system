using Domain.Model;

namespace Application.Interfaces.Services
{
    public interface IArtworkService
    {
        Task<IEnumerable<ArtworkDTO>> GetAllArtworks();
        Task<ArtworkDTO> GetArtworkById(int id);
        Task<ResponseDTO> AddArtwork(ArtworkAddDTO artwork);
        Task<ResponseDTO> UpdateArtwork(ArtworkDTO artwork);
        Task<IEnumerable<ArtworkDTO>> GetArtworkByFilter(ArtworkFilterParameterDTO filter);
    }
}
