﻿namespace GSCBase.Domain.Models.Auth
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Unity { get; set; }
        public string AccessKey { get; set; }
        public int? IdPessoa { get; set; }
    }
}