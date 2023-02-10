using Ewone.Data.Core;
using Ewone.Data.Repositories.User;

namespace Ewone.Data.Repositories.UnitToWork;

public class UnitToWork : IUnitToWork
{
    private readonly EwoneDbContext _ewoneDbContext;

    public UnitToWork(EwoneDbContext ewoneDbContext)
    {
        _ewoneDbContext = ewoneDbContext;
        Users = new UserRepository(ewoneDbContext);
    }

    public Task<int> CommitAsync()
    {
        return _ewoneDbContext.SaveChangesAsync();
    }

    public IUserRepository Users { get; }

    public void Dispose()
    {
        _ewoneDbContext.Dispose();
    }
}