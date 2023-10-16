using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace JWTAuthorization.DAL;

public class JwtDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public JwtDbContext(DbContextOptions<JwtDbContext> options) : base(options) { }

}
