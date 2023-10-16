using JWTAuthorization.DAL;

namespace JWTAuthorization.BL;

public class ProductManager : IProductManager
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

   public ProductManager(IProductRepository productRepository,ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public void DeleteProduct(int id)
    {
        var productToDelete = _productRepository.GetById(id);
        if (productToDelete is null)
            throw new NullReferenceException();

        _productRepository.Delete(productToDelete);
        _productRepository.SaveChanges();
    }

    public ProductWithCategoryReadDTO? GetProductWithCategory(int id) =>
        _productRepository.GetById(id) is Product product ?
        new ProductWithCategoryReadDTO
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Category = _categoryRepository.GetById(product.CategoryId)
                is Category cate ? new CategoryReadDTO
                {
                    Id = cate.Id,
                    InStock = cate.InStock,
                    Name = cate.Name
                } : null
        } : null;

    public IEnumerable<ProductWithCategoryReadDTO> GetProductsWithCategory() =>
        _productRepository.GetAll.Select(
                       p => new ProductWithCategoryReadDTO
                       {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Category = _categoryRepository.GetById(p.CategoryId)
                is Category cate ? new CategoryReadDTO
                {
                    Id = cate.Id,
                    InStock = cate.InStock,
                    Name = cate.Name
                } : null
            }
                                  );

    public IEnumerable<ProductReadDTO> GetProducts() =>
        _productRepository.GetAll.Select(
            p => new ProductReadDTO
            {
                Id = p.Id,
                Description = p.Description,
                Name = p.Name
            }
            );

    public ProductReadDTO? GetProduct(int id) =>
        _productRepository.GetById(id) is Product product ?
        new ProductReadDTO
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description
        } : null;

    public void CreateProduct(ProductCreateDTO productCreateDTO)
    {
        if (productCreateDTO is null)
            throw new NullReferenceException();

        var productToCreate = new Product
        {
            Name = productCreateDTO.Name,
            Description = productCreateDTO.Description,
            CategoryId = productCreateDTO.CategoryId
        };

        _productRepository.Create(productToCreate);
        _productRepository.SaveChanges();
    }

    public void UpdateProduct(int id, ProductUpdateDTO productUpdateDTO)
    {
        if (productUpdateDTO is null)
            throw new NullReferenceException();

        var productToUpdate = _productRepository.GetById(id);
        if (productToUpdate is null)
            throw new NullReferenceException();

        productToUpdate.Name = productUpdateDTO.Name;
        productToUpdate.Description = productUpdateDTO.Description;
        productToUpdate.CategoryId = productUpdateDTO.CategoryId;

        _productRepository.Update(productToUpdate);
        _productRepository.SaveChanges();
    }



}
