using GSCBase.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Domain.Entities.Base
{
    public class BaseModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime DtCreate { get; set; }
        public string UserCreate { get; set; }
        public DateTime? DtAlter { get; set; }
        public string UserAlter { get; set; }
        public DateTime? DtDeleted { get; set; }
        public string UserDeleted { get; set; }

        public ApplicationUser _UserCreate { get; set; }
        public ApplicationUser _UserAlter { get; set; }
        public ApplicationUser _UserDeleted { get; set; }

        public void SetUserCreate(ApplicationUser user)
        {
            this.IsActive = true;
            this._UserCreate = user;
            this.DtCreate = DateTime.Now;
        }
        public void SetUserAlter(ApplicationUser user)
        {
            this.IsActive = true;
            this._UserAlter = user;
            this.DtAlter = DateTime.Now;
        }
        public void SetUserDeleted(ApplicationUser user)
        {
            this.IsActive = false;
            this._UserDeleted = user;
            this.DtDeleted = DateTime.Now;
        }
    }
}
