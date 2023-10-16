namespace JWTAuthorization.DAL;

public class Product
{
    public  int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; } 

    public int CategoryId { get; set; }

    public Category? category { get; set; }

}
