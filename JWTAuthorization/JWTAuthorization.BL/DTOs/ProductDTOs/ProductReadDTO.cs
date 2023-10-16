using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthorization.BL;

public class ProductReadDTO
{
    public required  int Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required string? Description { get; set; } 
}
