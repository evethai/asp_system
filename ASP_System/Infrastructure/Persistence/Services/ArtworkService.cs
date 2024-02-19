using Application.Interfaces;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Services
{
    public class ArtworkService : IArtworkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ArtworkService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<ArtworkDTO> AddArtwork(ArtworkDTO artwork)
        {
            var newArtwork = _mapper.Map<Artwork>(artwork);
            _unitOfWork.Repository<Artwork>().AddAsync(newArtwork);
            _unitOfWork.Save();
            return Task.FromResult(artwork);
        }

        public async Task<IEnumerable<ArtworkDTO>> GetAllArtworks()
        {
            var ArtworkList = await _unitOfWork.Repository<Artwork>().GetAllAsync();
            return _mapper.Map<List<ArtworkDTO>>(ArtworkList);
        }

        public async Task<ArtworkDTO> GetArtworkById(int id)
        {
            var Artwork = await _unitOfWork.Repository<Artwork>().GetByIdAsync(id);
            return _mapper.Map<ArtworkDTO>(Artwork);
        }


    }
}
