using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ILikeService
    {
        Task<LikeDTO> GetLike(int ArtworkId);
        Task<ResponseDTO> CreateLike(LikeCreateDTO like, string userId);
        Task<ResponseDTO> DeleteLike(LikeDeleteDTO like);
        Task<IEnumerable<LikeDTO>> GetAllLike();
    }
}
