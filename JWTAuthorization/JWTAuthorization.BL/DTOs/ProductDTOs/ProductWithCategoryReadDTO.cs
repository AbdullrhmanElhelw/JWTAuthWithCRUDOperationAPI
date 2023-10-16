using JWTAuthorization.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthorization.BL;

public class ProductWithCategoryReadDTO
{
    public required int Id { get; set; } 
    public required string Name { get; set; } = string.Empty;
    public required string? Description { get; set; }

    public CategoryReadDTO ? Category { get; set; }

}
