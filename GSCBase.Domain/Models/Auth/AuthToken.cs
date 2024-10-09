using System;
using System.Collections.Generic;

namespace GSCBase.Domain.Models.Auth
{
    public class AuthToken : User
    {
        public DateTime Expiration { get; set; }
        public DateTime Created { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool PrimeiroAcesso { get; set; }
        public ICollection<string> errors { get; set; } = new List<string>();
        public AuthToken()
        {
            errors = new List<string>();
        }
    }
}
