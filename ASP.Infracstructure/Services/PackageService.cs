using ASP.Infracstructure.Data;
using ASP.Infracstructure.Repositories;
using Asp_System.Core.Models;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ASP.Infracstructure.Services
{
    public interface IPackageService 
    {
        Task<Package> FindAsync(Guid id);
        IQueryable<Package> GetAll();
        IQueryable<Package> Get(Expression<Func<Package, bool>> where);
        IQueryable<Package> Get(Expression<Func<Package, bool>> where, params Expression<Func<Package, object>>[] includes);
        IQueryable<Package> Get(Expression<Func<Package, bool>> where, Func<IQueryable<Package>, IIncludableQueryable<Package, object>> include = null);
        Task AddAsync(Package Transfer);
        Task AddRange(IEnumerable<Package> package);
        void Update(Package package);
        Task<bool> Remove(Guid id);
        Task<bool> CheckExist(Expression<Func<Package, bool>> where);
        Task<int> SaveChangeAsync();
    }
    public class PackageService : IPackageService
    {
        private IUnitOfWork _unitOfWork;
        private IPackageRepository _packageRepository;
        public PackageService(IUnitOfWork unitOfWork, IPackageRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _packageRepository = categoryRepository;
        }
        public async Task AddAsync(Package package)
        {
            await _packageRepository.AddAsync(package);
        }

        public async Task AddRange(IEnumerable<Package> package)
        {
            await _packageRepository.AddRange(package);
        }

        public async Task<bool> CheckExist(Expression<Func<Package, bool>> where)
        {
            return await _packageRepository.CheckExist(where);
        }

        public async Task<Package> FindAsync(Guid id)
        {
            return await _packageRepository.FindAsync(id);
        }

        public IQueryable<Package> Get(Expression<Func<Package, bool>> where)
        {
            return _packageRepository.Get(where);
        }

        public IQueryable<Package> Get(Expression<Func<Package, bool>> where, params Expression<Func<Package, object>>[] includes)
        {
            return _packageRepository.Get(where, includes);
        }

        public IQueryable<Package> Get(Expression<Func<Package, bool>> where, Func<IQueryable<Package>, IIncludableQueryable<Package, object>> include = null)
        {
            return _packageRepository.Get(where, include);
        }

        public IQueryable<Package> GetAll()
        {
            return _packageRepository.GetAll();
        }

        public async Task<bool> Remove(Guid id)
        {
            return await _packageRepository.Remove(id);
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _unitOfWork.SaveChangeAsync();
        }

        public void Update(Package package)
        {
            _packageRepository.Update(package);
        }
    }
}
