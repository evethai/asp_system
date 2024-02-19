using Domain.Entities;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ICatalogyService
    {
        Task<IEnumerable<CatalogyDTO>> GetAllCatalogy();
        Task<CatalogyDTO> AddCatalogy(CatalogyDTO catalogy);
        Task<CatalogyDTO> GetCatalogyById(int id);
        Task DeteleCatalogy(int id);
        Task UpdateCatalogy(int id,CatalogyDTO catalogy);
    }
}
