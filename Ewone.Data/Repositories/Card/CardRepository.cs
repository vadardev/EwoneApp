using System.Linq.Expressions;
using System.Net.NetworkInformation;
using Ewone.Data.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ewone.Data.Repositories.Card;

public class CardRepository : Repository<Entities.Card>, ICardRepository
{
    private readonly DbContext _dbContext;

    public CardRepository(DbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public new async Task<IEnumerable<Entities.Card>> GetAllAsync(Expression<Func<Entities.Card, bool>> expression,
        CancellationToken cancellationToken = default)
        => await _dbContext.Set<Entities.Card>()
            .Where(expression)
            .Include(x => x.Word)
            .ToListAsync(cancellationToken);
}