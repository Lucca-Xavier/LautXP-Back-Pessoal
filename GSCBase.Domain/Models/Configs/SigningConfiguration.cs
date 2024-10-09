using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GSCBase.Domain.Models.Configs
{
    public class SigningConfiguration
    {
        public SigningCredentials SigningCredentials { get; }
        public SecurityKey Key { get; }

        public SigningConfiguration(TokenConfiguration tokenConfigurations)
        {
            var byteArray = Encoding.ASCII.GetBytes(tokenConfigurations.SecretKey);
            var signingKey = new SymmetricSecurityKey(byteArray);

            SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            Key = signingKey;
        }
    }
}
