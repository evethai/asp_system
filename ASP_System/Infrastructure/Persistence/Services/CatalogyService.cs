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
    public class CatalogyService : ICatalogyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly object catalogy;

        public CatalogyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<CatalogyDTO> AddCatalogy(CatalogyDTO catalogy)
        {
            var newCatalogy = _mapper.Map<Category>(catalogy);
            _unitOfWork.Repository<Category>().AddAsync(newCatalogy);
            _unitOfWork.Save();
            return Task.FromResult(catalogy);
        }

        public async Task DeteleCatalogy(int id)
        {
            var result = _unitOfWork.Repository<CatalogyDTO>().GetByIdAsync(id);
            if (result!= null)
            {
                var status = _mapper.Map<Category>(result);
                status.Status = false;
                await _unitOfWork.Repository<Category>().UpdateAsync(status);
            }
        }

        public async Task<IEnumerable<CatalogyDTO>> GetAllCatalogy()
        {
            var result = await _unitOfWork.Repository<Category>().GetAllAsync();
            return _mapper.Map<List<CatalogyDTO>>(result); // Take to list with Mapper
        }

        public async Task<CatalogyDTO> GetCatalogyById(int id)
        {
            var result = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            return _mapper.Map<CatalogyDTO>(result);
        }

        public async Task UpdateCatalogy(int id, CatalogyDTO catalogy)
        {
            if (id == catalogy.Id)
            {
                var update = _mapper.Map<Category>(catalogy);
                await _unitOfWork.Repository<Category>().UpdateAsync(update);
                _unitOfWork.Save();
            }
        }
    }
}
