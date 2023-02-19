using Ewone.Data.Core;
using Ewone.Data.Repositories.Card;
using Ewone.Data.Repositories.Module;
using Ewone.Data.Repositories.User;
using Ewone.Data.Repositories.Word;

namespace Ewone.Data.Repositories.UnitToWork;

public class UnitToWork : IUnitToWork
{
    private readonly EwoneDbContext _ewoneDbContext;

    public UnitToWork(EwoneDbContext ewoneDbContext)
    {
        _ewoneDbContext = ewoneDbContext;
        Users = new UserRepository(ewoneDbContext);
        Modules = new ModuleRepository(ewoneDbContext);
        Cards = new CardRepository(ewoneDbContext);
        Words = new WordRepository(ewoneDbContext);
    }

    public Task<int> CommitAsync()
    {
        return _ewoneDbContext.SaveChangesAsync();
    }

    public IUserRepository Users { get; }
    public IModuleRepository Modules { get; }
    public ICardRepository Cards { get; }
    public IWordRepository Words { get; set; }

    public void Dispose()
    {
        _ewoneDbContext.Dispose();
    }
}