using JwtDemo.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace JwtDemo.Domain.Abstraction
{
    public interface IJWTTokenService
    {
        string CreateToken(User user);
    }
}
