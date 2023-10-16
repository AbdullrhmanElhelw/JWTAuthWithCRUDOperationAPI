namespace JWTAuthorization.BL;

public class CategoryReadDTO
{ 
    public required  int Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required int InStock { get; set; }
}
