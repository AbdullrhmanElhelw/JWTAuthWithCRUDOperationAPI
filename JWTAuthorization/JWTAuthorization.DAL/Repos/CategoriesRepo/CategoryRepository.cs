namespace JWTAuthorization.DAL;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly JwtDbContext context;

    public CategoryRepository(JwtDbContext context) : base(context)
    {
        this.context = context;
    }
}
