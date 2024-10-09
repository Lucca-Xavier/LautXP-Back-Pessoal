using Microsoft.EntityFrameworkCore.Storage;
using GSCBase.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Domain.Entities.Base
{
    public class Module : BaseModel
    {
        public string NmModule { get; set; }
        public string SgModule { get; set; }

        public Module()
        {

        }
        public Module(string nmModule, string sgModule, ApplicationUser user)
        {
            this.NmModule = nmModule;
            this.SgModule = sgModule;
            this.SetUserCreate(user);
        }

        public void Alterar(string nmModule, string sgModule, ApplicationUser user)
        {
            this.NmModule = nmModule;
            this.SgModule = sgModule;
            this.SetUserAlter(user);
        }
    }
}
