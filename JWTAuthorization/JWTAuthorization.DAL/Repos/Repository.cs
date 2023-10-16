namespace JWTAuthorization.DAL;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly JwtDbContext _context;

    public Repository(JwtDbContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAll => _context.Set<T>().ToList();

    public void Create(T entity) => _context.Set<T>().Add(entity);

    public void Delete(T entity) => _context.Set<T>().Remove(entity);

    public T? GetById(int id) => _context.Set<T>().Find(id);

    public int SaveChanges() => _context.SaveChanges();

    public void Update(T entity) => _context.Set<T>().Update(entity);
}
