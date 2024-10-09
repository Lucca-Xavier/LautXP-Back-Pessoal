using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using GSCBase.Application.IServices.Base;
using GSCBase.Domain.Entities.Auth;
using GSCBase.Domain.Models.Auth;
using GSCBase.Domain.Models.Configs;

namespace GSCBase.Application.ISevices.Auth
{
    public interface IAuthorizeService : IBaseService<ApplicationUser>
    {
        AuthToken GenerateAccessToken(User user, SigningConfiguration signingConfigurations, TokenConfiguration tokenConfigurations, IDistributedCache cache);
        AuthToken Login(AuthClient model, SigningConfiguration signingConfigurations, TokenConfiguration tokenConfigurations,
                                                UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager, IDistributedCache cache);
        RegisterViewModel Register(RegisterViewModel user);
    }
}
