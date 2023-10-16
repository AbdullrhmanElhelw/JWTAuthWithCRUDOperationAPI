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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;

        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<CategoryReadDTO>> GetAllCategories()=> Ok( _categoryManager.GetAll);


        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<CategoryReadDTO> GetCategoryById(int id) => Ok(_categoryManager.GetCategory(id));



        [HttpPost]
        public ActionResult<CategoryCreateDTO> CreateCategory(CategoryCreateDTO categoryCreateDTO)
        {
             _categoryManager.CreateCategory(categoryCreateDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            _categoryManager.DeleteCategory(id);
            return NoContent();
        }


        [HttpPut("{id}")]
        public ActionResult<CategoryUpdateDTO> UpdateCategory(int id,CategoryUpdateDTO categoryUpdateDTO)
        {
             _categoryManager.UpdateCategory(id,categoryUpdateDTO);
             return NoContent();
        }

  
    }
}
