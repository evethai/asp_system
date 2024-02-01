using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.Infracstructure.Data
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangeAsync();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _appDbContext;
        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> SaveChangeAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }
   
        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
