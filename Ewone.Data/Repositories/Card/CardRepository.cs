using Ewone.Data.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ewone.Data.Repositories.Card;

public class CardRepository : Repository<Entities.Card>, ICardRepository
{
    public CardRepository(DbContext dbContext) : base(dbContext)
    {
    }
}