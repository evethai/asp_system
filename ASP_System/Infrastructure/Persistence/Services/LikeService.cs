using Application.Interfaces;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Services
{
    public class LikeService : ILikeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LikeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<LikeDTO> GetLike(int ArtworkId)
        {
            var Like = await _unitOfWork.Repository<LikeDTO>().GetByIdAsync(ArtworkId);
            return _mapper.Map<LikeDTO>(Like);
        }
    }
}
