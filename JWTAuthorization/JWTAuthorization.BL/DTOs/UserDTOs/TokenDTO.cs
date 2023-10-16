using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthorization.BL;

public class TokenDTO
{
    public string Token { get; set; }
    public DateTime Expire { get; set; }
}
