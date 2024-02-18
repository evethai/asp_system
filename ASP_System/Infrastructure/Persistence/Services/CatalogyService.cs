using Application.Interfaces;
using Application.Interfaces.Services;
using Domain.Entities;
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
        public CatalogyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddNewCatalogy(Category category)
        {
            _unitOfWork.Repository<Category>().AddAsync(category);
            _unitOfWork.Save();
        }

        public async Task<IEnumerable<Category>> GetAllCatalogy()
        {
            var result = await _unitOfWork.Repository<Category>().GetAllAsync();
            return result;
        }
    }
}
