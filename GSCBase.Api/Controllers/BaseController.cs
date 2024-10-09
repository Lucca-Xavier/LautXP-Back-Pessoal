using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using GSCBase.Domain.Entities.Auth;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GSCBase.Api.Controllers
{
    [Route("api/[controller]")]
    public class BaseController : ControllerBase, IAsyncActionFilter
    {
        private readonly UserManager<ApplicationUser> userManager;

        public BaseController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Para leitura. Pode ser via atributo na action ou verbo http
            // this.context.ChangeTracker.AutoDetectChangesEnabled = false;
            // this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var result = await next();
        }

        protected ApplicationUser GetUsuarioLogado()
        {
            try
            {
                string userId = this.User.Claims.FirstOrDefault(c => c.Type == "userId").Value;
                var user = userManager.FindByIdAsync(userId).Result;
                if (user == null)
                {
                    throw new UnauthorizedAccessException("Acesso nao autorizado! Usuário não encontrado");
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException("Acesso não autorizado!");
            }
        }

        protected ApplicationUser GetUsuarioLogadoAdministrador()
        {
            try
            {
                var user = userManager.FindByNameAsync("mauricio@GSCBase").Result;
                if (user == null)
                {
                    throw new UnauthorizedAccessException("Acesso nao autorizado! Usuário não encontrado");
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException("Acesso não autorizado!");
            }
        }

        protected int GetIdUnidade()
        {
            return 2;// Convert.ToInt32(this.GetClaimValue("IdUnidade"));
        }

        protected int GetIdModule()
        {
            return 1;// Convert.ToInt32(this.GetClaimValue("IdModulo"));
        }

        protected void AddUpdateUserClaims(string key, string value)
        {
            try
            {
                var identity = new ClaimsIdentity(this.User.Identity);
                // check for existing claim and remove it
                var existingClaim = identity.FindFirst(key);
                if (existingClaim != null)
                    identity.RemoveClaim(existingClaim);
                // add new claim
                identity.AddClaim(new Claim(key, value));

                ClaimsPrincipal principal = this.User;
                if (principal != null)
                {
                    identity = principal.Identities.ElementAt(0);
                    var old = identity.FindFirst(key);
                    if (old != null)
                        identity.RemoveClaim(old);

                    identity.AddClaim(new Claim(key, value));
                }
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException("Acesso não autorizado!");
            }
        }
        public string GetClaimValue(string key)
        {
            var identity = this.User.Identity as ClaimsIdentity;
            if (identity == null)
                return null;

            var claim = identity.Claims.First(c => c.Type == key);
            return claim.Value;
        }
    }
}