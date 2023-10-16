using JWTAuthorization.DAL;

namespace JWTAuthorization.BL;

public interface ICategoryManager
{
    IEnumerable<CategoryReadDTO> GetAll { get; }
    CategoryReadDTO? GetCategory(int id);

    void CreateCategory(CategoryCreateDTO categoryDTO);
    void UpdateCategory(int id , CategoryUpdateDTO updateDTO); 

    void DeleteCategory(int id);


}
