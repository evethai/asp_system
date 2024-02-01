using ASP.Infracstructure.Data;
using ASP.Infrastructure.Data;
using Asp_System.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.Infracstructure.Repositories
{
    public interface IPackageRepository : IBaseRepository<Package, Guid>
    {

    }
    public class PackageRepository : BaseRepository<Package, Guid>, IPackageRepository
    {
        public PackageRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
    
}
