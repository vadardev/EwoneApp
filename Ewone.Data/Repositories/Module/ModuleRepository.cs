using Ewone.Data.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ewone.Data.Repositories.Module;

public class ModuleRepository : Repository<Entities.Module>, IModuleRepository
{
    public ModuleRepository(DbContext dbContext) : base(dbContext)
    {
    }
}