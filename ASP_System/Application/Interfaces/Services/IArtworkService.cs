using Domain.Model;

namespace Application.Interfaces.Services
{
    public interface IArtworkService
    {
        Task<IEnumerable<ArtworkDTO>> GetAllArtworks();
        Task<ArtworkDTO> GetArtworkById(int id);
        Task<ArtworkDTO> AddArtwork(ArtworkDTO artwork);
    }
}
