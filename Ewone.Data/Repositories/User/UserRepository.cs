using Ewone.Data.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ewone.Data.Repositories.User;

public class UserRepository : Repository<Entities.User>, IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {
    }
}