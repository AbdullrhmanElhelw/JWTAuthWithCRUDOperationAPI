using JWTAuthorization.BL;
using JWTAuthorization.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthorization.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Policy = nameof(Roles.Admin))]
    public class ProductController : ControllerBase
    {
        private IProductManager _productManager;
        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<ProductReadDTO>> GetProductsWithCategory() => Ok(_productManager.GetProductsWithCategory());



        [HttpGet("{id}")]
        [AllowAnonymous]

        public ActionResult<ProductReadDTO> GetProductWithCategory(int id) 
            => Ok(_productManager.GetProductWithCategory(id));


        [HttpDelete("{id}")]
        public ActionResult<string> DeleteProduct(int id)
        {
            _productManager.DeleteProduct(id);
            return Ok("Deleted");
        }

        [HttpGet]
        [AllowAnonymous]

        public ActionResult<IEnumerable<ProductReadDTO>> GetProducts() => Ok(_productManager.GetProducts());


        [HttpGet("{id}")]
        [AllowAnonymous]

        public ActionResult<ProductReadDTO> GetProduct(int id)=> Ok(_productManager.GetProduct(id));


        [HttpPost]
        public ActionResult<ProductCreateDTO> CreateProduct(ProductCreateDTO productCreateDTO)
        {
            _productManager.CreateProduct(productCreateDTO);
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        public ActionResult<ProductUpdateDTO> UpdateProduct(int id, ProductUpdateDTO productUpdateDTO)
        {
            _productManager.UpdateProduct(id, productUpdateDTO);
            return Ok("Updated Successfully");
        }

        
    }
}
