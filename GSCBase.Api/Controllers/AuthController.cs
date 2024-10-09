using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using GSCBase.Application.ISevices.Auth;
using GSCBase.Domain.Entities.Auth;
using GSCBase.Application.IServices.Base;
using GSCBase.Domain.Models.Auth;
using GSCBase.Domain.Models.Configs;
using GSCBase.Infrastructure.Contexts;

namespace GSCBase.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthorizeService authService;
        private readonly SignInManager<ApplicationUser> _signManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService emailService;

        public AuthController(IAuthorizeService _authorizeService,
                                UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signManager,
                                IEmailService _emailService) : base(userManager)
        {
            authService = _authorizeService;
            _userManager = userManager;
            _signManager = signManager;
            emailService = _emailService;
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("token")]
        public async Task<IActionResult> Token(
            [FromBody] AuthClient model,
            [FromServices] SigningConfiguration signingConfigurations,
            [FromServices] TokenConfiguration tokenConfigurations,
            [FromServices] IDistributedCache cache)
        {
            var auth = authService.Login(model, signingConfigurations, tokenConfigurations, _userManager, _signManager, cache);
            if (!string.IsNullOrEmpty(auth.AccessToken) && !auth.errors.Any())
                return Ok(auth);
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterViewModel model)
        {
            bool valido = true;
            if (valido == true)
            {
                ApplicationUser user = new ApplicationUser(model.UserName, model.PhoneNumber, model.Email, "register", model.UserName);

                var result = _userManager.CreateAsync(user, model.Password).Result;

                if (result.Succeeded)
                {
                    if (user != null)
                    {
                        var token = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                        var callbackUrl = $"http://sistema.GSCBase.com.br/confirm.html?userid={user.Id}&token={token}";
                        string body =
                            "<div style=\"font-family:Lucida Sans Unicode,Lucida Grande,Sans-Serif;font-size:15px\"><div class=\"adM\">" +
                             "</div><div style=\"width:600px;margin:10px;border:1px solid rgb(230,230,230);border-radius:5px\"><div class=\"adM\">" +
                                 "</div><div style=\"background-color:rgb(245,245,245);border-bottom:1px solid rgb(230,230,230);border-radius:5px 5px 0 0;padding:10px\"><div class=\"adM\">" +
                                     "</div>" +
                                 "<img style=\"aling-self:center\" src=\"https://GSCBase.com.br/wp-content/uploads/2019/03/cropped-logo-site.png\" width =\"200\">" +
                                 "</div>" +
                                 "<div style=\"background-color:rgb(255,255,255);border-bottom:solid 2px #14273d\">" +
                                             "<div style=\"width:95%;padding:5px 0;margin:auto\">" +
                                                 "<div id=\"content\">" +
                                                     "<p style=\"color:rgb(135,135,135);border-bottom:solid 1px rgb(230,230,230)\"></p>" +
                                                     "<p style=\"font-family:Lucida Sans Unicode,Lucida Grande,sans-serif;line-height:21.4286px;color:rgb(135,135,135)\">" +

                                                "<h3> Confirmação de conta </h3> <b>Para confirmar o email:</b> <p> " + "<a href=\"" + callbackUrl + "\">Clique aqui!</a>" + " <br/>" +
                                                 "</p>" +
                                                 "</div>" +
                                             "</div>" +
                                         "</div>" +
                                     "</div>" +
                                 "</div>";

                        emailService.SendEmail("GSCBase", user.Email, body, "Confirme seu cadastro!");
                    }
                }
                else
                {
                        return BadRequest(string.Join("*", result.Errors.Select(c=>c.Description)));
                }
            }
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("selecionarModulo")]
        public async Task<IActionResult> SelecionarModulo([FromBody] AuthClient model)
        {
            //implementar permissão na unidade no modulo
            this.AddUpdateUserClaims("IdUnidade", model.IdUnidade.ToString());
            this.AddUpdateUserClaims("IdModulo", model.IdUnidade.ToString());

            this.GetUsuarioLogado();
            return Ok();
        }

        [HttpGet]
        [Route("reenviarConfirmarEmail")]
        public async Task<IActionResult> ReenviarConfirmEmail(string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);

                if (user == null)
                {
                    return BadRequest("Cadastro não encontrado");
                }

                if (user != null)
                {
                    var token = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                    var callbackUrl = $"http://sistema.GSCBase.com.br/confirm.html?userid={user.Id}&token={token}";

                    string body =
                        "<div style=\"font-family:Lucida Sans Unicode,Lucida Grande,Sans-Serif;font-size:15px\"><div class=\"adM\">" +
                         "</div><div style=\"width:600px;margin:10px;border:1px solid rgb(230,230,230);border-radius:5px\"><div class=\"adM\">" +
                             "</div><div style=\"background-color:rgb(245,245,245);border-bottom:1px solid rgb(230,230,230);border-radius:5px 5px 0 0;padding:10px\"><div class=\"adM\">" +
                                 "</div>" +
                             "<img style=\"aling-self:center\" src=\"https://GSCBase.com.br/wp-content/uploads/2019/03/cropped-logo-site.png\" width =\"200\">" +
                             "</div>" +
                             "<div style=\"background-color:rgb(255,255,255);border-bottom:solid 2px #14273d\">" +
                                         "<div style=\"width:95%;padding:5px 0;margin:auto\">" +
                                             "<div id=\"content\">" +
                                                 "<p style=\"color:rgb(135,135,135);border-bottom:solid 1px rgb(230,230,230)\"></p>" +
                                                 "<p style=\"font-family:Lucida Sans Unicode,Lucida Grande,sans-serif;line-height:21.4286px;color:rgb(135,135,135)\">" +

                                            "<h3> Confirmação de conta </h3> <b>Para confirmar o email:</b> <p> " + "<a href=\"" + callbackUrl + "\">Clique aqui!</a>" + " <br/>" +
                                             "</p>" +
                                             "</div>" +
                                         "</div>" +
                                     "</div>" +
                                 "</div>" +
                             "</div>";

                    emailService.SendEmail("GSCBase", user.Email, body, "Confirme seu cadastro!");
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("confirmarEmail")]
        public async Task<ActionResult> ConfirmarEmail([FromBody]ConfirmEmailModel model)
        {
            if (model.userId == null || model.code == null)
            {
                return BadRequest("Error");
            }

            var user = _userManager.FindByIdAsync(model.userId).Result;
            try
            {
                var result = await _userManager.ConfirmEmailAsync(user, model.code);
                if (result.Succeeded)
                {
                    user.EmailConfirmed = true;
                    var _result = _userManager.UpdateAsync(user).Result;
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return BadRequest("Error");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("forgotPassword")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordViewModel model)
        {
            var user = _userManager.FindByNameAsync(model.userName).Result;

            if (user != null)
            {
                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                string code = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
                string body =
                    "<div style=\"font-family:Lucida Sans Unicode,Lucida Grande,Sans-Serif;font-size:15px\"><div class=\"adM\">" +
                     "</div><div style=\"width:600px;margin:10px;border:1px solid rgb(230,230,230);border-radius:5px\"><div class=\"adM\">" +
                         "</div><div style=\"background-color:rgb(245,245,245);border-bottom:1px solid rgb(230,230,230);border-radius:5px 5px 0 0;padding:10px\"><div class=\"adM\">" +
                             "</div>" +
                         "<img style=\"aling-self:center\" src=\"https://GSCBase.com.br/wp-content/uploads/2019/03/cropped-logo-site.png\" width=\"200\">" +
                         "</div>" +
                         "<div style=\"background-color:rgb(255,255,255);border-bottom:solid 2px #14273d\">" +
                                     "<div style=\"width:95%;padding:5px 0;margin:auto\">" +
                                         "<div id=\"content\">" +
                                             "<p style=\"color:rgb(135,135,135);border-bottom:solid 1px rgb(230,230,230)\"></p>" +
                                             "<p style=\"font-family:Lucida Sans Unicode,Lucida Grande,sans-serif;line-height:21.4286px;color:rgb(135,135,135)\">" +

                                         "<h3> Recuperação da conta </h3><p> <b>Código de verificação:</b> " + code + " <br/>" +
                                         "</p>" +
                                         "</div>" +
                                     "</div>" +
                                 "</div>" +
                                 "<div style=\"background-color:rgb(230,230,230);border-radius:0 0 5px 5px;padding:10px\">" +
                                     "<p style=\"color:#14273d;font-size:12px;margin:auto;text-align:center\"><a href=\"http://www.GSCBase.com.br/\" style=\"color:rgb(51,122,183)\" target=\"_blank\">Clique Aqui</a> para acessar o site.</p>" +
                                 "</div>  " +
                             "</div>" +
                         "</div>"; ;

                emailService.SendEmail("GSCBase", user.Email, body, "Esqueceu a senha?");
                user.ResetPasswordCode = code;
                var _result = _userManager.UpdateAsync(user).Result;

                return Ok();
            }
            return BadRequest("Usuário inválido.");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("resetPassword")]
        public IActionResult ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            var user = _userManager.FindByNameAsync(model.userName).Result;

            if (user != null && user.ResetPasswordCode == model.ResetPasswordCode)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                user.ResetPasswordCode = null;
                var result = _userManager.UpdateAsync(user).Result;

                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            return BadRequest("Usuário/Código inválido.");
        }


        /*[HttpPost]
        [Route("salvarUsuario")]
        public IActionResult SalvarUsuario([FromBody] UsuarioModel model)
        {
            Usuario usuarioLogado = GetUsuarioLogado();
            TipoUsuario tipoUsuario = _tipoUsuarioService.ObterTipoUsuarioPorId(model.IdTipoUsuario);
            GrupoUsuario grupoUsuario = _grupoUsuarioService.ObterGrupoUsuarioPorId(model.IdGrupoUsuario);
            Empresa empresa = _empresaService.ObterEmpresaPorId(model.IdEmpresa);
            Usuario usuario;

            if (model.IdUsuario > 0)
            {
                usuario = _usuarioService.ObterUsuarioPorId(model.IdUsuario);
                usuario.Pessoa.PessoaUsuario(model.Nome, empresa, model.Email, model.TelefoneCelular, model.Observacao, usuarioLogado);
                usuario.Alterar(model.Login, model.Nome, model.Email, tipoUsuario, grupoUsuario, usuario.Pessoa, usuarioLogado);
            }
            else
            {
                Pessoa pessoa = new Pessoa(usuarioLogado).PessoaUsuario(model.Nome, empresa, model.Email, model.TelefoneCelular, model.Observacao, usuarioLogado);

                usuario = new Usuario(model.Login, model.Nome, model.Email, pessoa, tipoUsuario, grupoUsuario, usuarioLogado);
            }

            _usuarioService.SalvarUsuario(usuario);

            return Ok();
        }*/
    }
}