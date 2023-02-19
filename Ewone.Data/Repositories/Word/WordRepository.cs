using Ewone.Data.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ewone.Data.Repositories.Word;

public class WordRepository : Repository<Entities.Word>, IWordRepository
{
    public WordRepository(DbContext dbContext) : base(dbContext)
    {
    }
}