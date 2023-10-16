using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthorization.BL;

public interface IProductManager
{
    IEnumerable<ProductWithCategoryReadDTO> GetProductsWithCategory();

    IEnumerable<ProductReadDTO> GetProducts();

    ProductReadDTO? GetProduct(int id);

    ProductWithCategoryReadDTO? GetProductWithCategory(int id);

    void DeleteProduct(int id);

    void CreateProduct (ProductCreateDTO productCreateDTO);

    void UpdateProduct(int id, ProductUpdateDTO productUpdateDTO);


}
