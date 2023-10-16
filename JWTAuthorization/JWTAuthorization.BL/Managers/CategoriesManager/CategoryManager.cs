using JWTAuthorization.DAL;

namespace JWTAuthorization.BL;

public class CategoryManager : ICategoryManager
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryManager(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public IEnumerable<CategoryReadDTO> GetAll =>
        _categoryRepository.GetAll.Select(
            c => new CategoryReadDTO
            {
                Id = c.Id,
                Name = c.Name,
                InStock = c.InStock
            }
            );

    public void CreateCategory(CategoryCreateDTO categoryDTO)
    {
        if(categoryDTO is null)
            throw new NullReferenceException();

        _categoryRepository.Create(new Category()
        {
            Name = categoryDTO.Name,
            InStock = categoryDTO.InStock
        });
        _categoryRepository.SaveChanges();
    }

    public void DeleteCategory(int id)
    {
        var cat = _categoryRepository.GetById(id);
        if(cat is null)
            throw new NullReferenceException();
        _categoryRepository.Delete( cat );
        _categoryRepository.SaveChanges();
    }

    public CategoryReadDTO? GetCategory(int id) =>
        _categoryRepository.GetById(id) is Category category ?
        new CategoryReadDTO
        {
            Id = category.Id,
            Name = category.Name,
            InStock = category.InStock
        } : null;

    public void UpdateCategory(int id, CategoryUpdateDTO updateDTO)
    {
        var catToUpdate = _categoryRepository.GetById( id );
        if(catToUpdate is null)
            throw new NullReferenceException();

        catToUpdate.Name = updateDTO.Name;
        catToUpdate.InStock = updateDTO.InStock;
        _categoryRepository.Update( catToUpdate );
        _categoryRepository.SaveChanges();
    }
}
