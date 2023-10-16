namespace JWTAuthorization.DAL;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly JwtDbContext context;

    public ProductRepository(JwtDbContext context) : base(context)
    {
        this.context = context;
    }
}
