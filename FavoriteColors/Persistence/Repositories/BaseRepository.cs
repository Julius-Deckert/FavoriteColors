using FavoriteColors.Persistence.Contexts;

namespace FavoriteColors.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly AppDbContext Context;

        protected BaseRepository(AppDbContext context)
        {
            Context = context;
        }
    }
}
