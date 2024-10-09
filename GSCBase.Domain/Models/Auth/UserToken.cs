using System;
using System.Security.Cryptography;

namespace GSCBase.Domain.Models.Auth
{
    public class UserToken
    {
        public int Id { get; private set; }
        public string RefreshToken { get; private set; }
        public string UserName { get; private set; }
        public DateTime Expiration { get; private set; }
        public bool Revoke { get; private set; }

        protected UserToken() { }
        public UserToken(string refreshToken, string userName)
            : this(refreshToken, userName, DateTime.Now.AddDays(7)) { }

        public UserToken(string refreshToken, string userName, DateTime refreshTokenLifetime)
        {
            if (string.IsNullOrEmpty(refreshToken))
                throw new ArgumentNullException(nameof(refreshToken));
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));
            if (refreshTokenLifetime < DateTime.Now)
                throw new Exception("O ciclo de vida do RefreshToken deve ser maior que a hora corrente.");

            this.RefreshToken = refreshToken;
            this.UserName = userName;
            this.Expiration = refreshTokenLifetime;
        }

        public void RevokeAccess() => this.Revoke = true;

        public bool IsValid() => this.Revoke == false && this.Expiration >= DateTime.Now;
        public void ExtendLifetime() => this.Expiration = DateTime.Now.AddDays(7);
        public void ExtendLifetime(DateTime lifetime) => this.Expiration = lifetime;

        public static Guid MakeCryptoGuid()
        {
            // Get 16 cryptographically random bytes
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] data = new byte[16];
            rng.GetBytes(data);

            // Mark it as a version 4 GUID
            data[7] = (byte)((data[7] | (byte)0x40) & (byte)0x4f);
            data[8] = (byte)((data[8] | (byte)0x80) & (byte)0xbf);

            return new Guid(data);
        }
    }
}
