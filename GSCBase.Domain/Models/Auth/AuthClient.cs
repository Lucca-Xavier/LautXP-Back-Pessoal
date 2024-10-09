using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Security.Cryptography;

namespace GSCBase.Domain.Models.Auth
{
    public class AuthClient
    {
        public int IdCliente { get; set; }
        public string CPF { get; set; }
        public DateTime DtNascimento { get; set; }
        public string GrantType { get; set; }
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int IdUnidade { get; set; }
        public int IdModulo { get; set; }

    }
    public class RefreshTokenData
    {
        public string RefreshToken { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }

    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
        public int FinalExpiration { get; set; }
    }
    public class SigningConfigurations
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }

    public class ApplicationUserModel
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Tipo { get; set; }
        public bool IsActive { get; set; }
        public string TelefoneCelular { get; set; }
        public int? IdTipoUsuario { get; set; }
        public string Situacao
        {
            get
            {
                return this.IsActive ? "Ativo" : "Inativo";
            }
        }
    }

    public class ConfirmEmailModel
    {
        public string userId { get; set; }
        public string code { get; set; }
    }

    public class RegisterViewModel
    {
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O/A {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
        public string ConfirmPassword { get; set; }

        public string PhoneNumber { get; set; }
        public string Nome { get; set; }
        public int IdTipoUsuario { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        public string userName { get; set; }
    }
    public class ResetPasswordViewModel
    {
        public string userName { get; set; }
        public string Password { get; set; }
        public string ResetPasswordCode { get; set; }
    }
}
