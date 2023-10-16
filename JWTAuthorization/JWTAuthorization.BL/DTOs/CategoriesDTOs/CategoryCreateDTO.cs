namespace JWTAuthorization.BL;

public class CategoryCreateDTO
{
    public required string Name { get; set; } = string.Empty;
    public required int InStock { get; set; }
}
