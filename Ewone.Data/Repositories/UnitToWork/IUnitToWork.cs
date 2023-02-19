using Ewone.Data.Repositories.Card;
using Ewone.Data.Repositories.Module;
using Ewone.Data.Repositories.User;
using Ewone.Data.Repositories.Word;

namespace Ewone.Data.Repositories.UnitToWork;

public interface IUnitToWork : IDisposable
{
    Task<int> CommitAsync();

    IUserRepository Users { get; }
    IModuleRepository Modules { get; }
    ICardRepository Cards { get; }
    IWordRepository Words { get; set; }
}