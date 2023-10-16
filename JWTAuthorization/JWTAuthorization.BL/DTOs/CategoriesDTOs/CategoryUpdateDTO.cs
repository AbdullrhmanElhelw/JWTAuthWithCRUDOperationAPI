namespace JWTAuthorization.BL;

public class CategoryUpdateDTO
{
    public required string Name { get; set; } = string.Empty;
    public required int InStock { get; set; }
}
