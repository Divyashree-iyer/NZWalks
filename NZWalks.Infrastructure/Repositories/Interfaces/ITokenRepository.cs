using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.Infrastructure.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTtoken(IdentityUser user, List<string> roles);
    }
}
