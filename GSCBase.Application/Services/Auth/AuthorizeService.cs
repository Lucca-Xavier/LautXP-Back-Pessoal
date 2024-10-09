using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using GSCBase.Application.ISevices.Auth;
using GSCBase.Application.Services.Base;
using GSCBase.Domain.Entities.Auth;
using GSCBase.Domain.Models.Auth;
using GSCBase.Domain.Models.Configs;
using GSCBase.Infrastructure.Contexts;
using GSCBase.Infrastructure.IRepositories.Auth;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace GSCBase.Application.Services.Auth
{
    public class AuthorizeService : BaseService<ApplicationUser>, IAuthorizeService
    {
        public AuthorizeService(IApplicationUserRepository repository) : base(repository)
        {

        }
        public virtual AuthToken GenerateAccessToken(User user,
                                             SigningConfiguration signingConfigurations,
                                             TokenConfiguration tokenConfigurations,
                                             IDistributedCache cache)
        {
            try
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                   new GenericIdentity(user.UserId.ToString(), "Login"),
                   new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserId.ToString()),
                        new Claim("userId", user.UserId.ToString()),
                        new Claim("userName", user.UserName.ToString())
                   }
               );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                // Calcula o tempo máximo de validade do refresh token
                TimeSpan finalExpiration =
                    TimeSpan.FromSeconds(tokenConfigurations.FinalExpiration);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                //Gera o refreshToken
                var refreshToken = Guid.NewGuid().ToString().Replace("-", "");// UserToken.MakeCryptoGuid().ToString("N");

                //Retorna o modelo
                var authToken = new AuthToken()
                {
                    Created = dataCriacao,
                    Expiration = dataExpiracao,
                    AccessToken = token,
                    UserName = user.UserName,
                    RefreshToken = refreshToken,
                    PrimeiroAcesso = user.IdPessoa == null
                };

                // Armazena o refresh token em cache através do sqlserver 
                var refreshTokenData = new RefreshTokenData();
                refreshTokenData.RefreshToken = authToken.RefreshToken;
                refreshTokenData.UserId = user.UserId;
                refreshTokenData.UserName = user.UserName;

                DistributedCacheEntryOptions opcoesCache =
                    new DistributedCacheEntryOptions();
                opcoesCache.SetAbsoluteExpiration(finalExpiration);


                cache.SetString(authToken.RefreshToken,
                    JsonConvert.SerializeObject(refreshTokenData),
                    opcoesCache);
                return authToken;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AuthToken Login(AuthClient model, SigningConfiguration signingConfigurations, TokenConfiguration tokenConfigurations,
           UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager, IDistributedCache cache)
        {
            User user = new User();
            AuthToken token = new AuthToken();

            bool credenciaisValidas = false;
            if (model == null)
            {
                credenciaisValidas = false;
                token.errors.Add("Dados de acesso inválido");
            }

            if (model.GrantType == "password")
            {
                var result = signManager.PasswordSignInAsync(model.UserName, model.Password, false, false).Result;

                if (result.Succeeded)
                {
                    var _user = userManager.Users.SingleOrDefault(r => r.UserName == model.UserName);
                    credenciaisValidas = true;
                    if (_user != null)
                    {
                        user = new User
                        {
                            UserId = _user.Id,
                            UserName = _user.UserName,
                            IdPessoa = _user.IdPessoa
                        };
                    }
                }
            }
            else if (model.GrantType == "refresh_token")
            {
                if (!String.IsNullOrWhiteSpace(model.RefreshToken))
                {
                    RefreshTokenData refreshTokenBase = null;

                    string strTokenArmazenado =
                        cache.GetString(model.RefreshToken);
                    if (!String.IsNullOrWhiteSpace(strTokenArmazenado))
                    {
                        refreshTokenBase = JsonConvert.DeserializeObject<RefreshTokenData>(strTokenArmazenado);
                        user.UserId = refreshTokenBase.UserId;
                        user.UserName = refreshTokenBase.UserName;
                    }
                    credenciaisValidas = (refreshTokenBase != null &&
                        model.RefreshToken == refreshTokenBase.RefreshToken);

                    // Elimina o token de refresh já que um novo será gerado
                    if (credenciaisValidas)
                        cache.Remove(model.RefreshToken);
                }
            }

            else
            {
                token.errors.Add("GrantType invalido");
            }

            //Logica para realizar o login
            if (credenciaisValidas)
            {
                token = GenerateAccessToken(user, signingConfigurations, tokenConfigurations, cache);

                token.RefreshToken = token.RefreshToken;
                token.UserId = user.UserId;
                token.UserName = user.UserName;
            }
            return token;
        }

        public RegisterViewModel Register(RegisterViewModel user)
        {
            throw new NotImplementedException();
        }
    }
}
