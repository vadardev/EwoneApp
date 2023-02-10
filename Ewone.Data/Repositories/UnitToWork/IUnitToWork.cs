using Ewone.Data.Repositories.User;

namespace Ewone.Data.Repositories.UnitToWork;

public interface IUnitToWork : IDisposable
{
    Task<int> CommitAsync();

    IUserRepository Users { get; }
}