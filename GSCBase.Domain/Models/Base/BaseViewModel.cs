using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Domain.Models.Base
{
    public class BaseViewModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime DtCreate { get; set; }
        public string UserCreate { get; set; }
        public DateTime? DtAlter { get; set; }
        public string UserAlter { get; set; }
        public DateTime? DtDeleted { get; }
        public string UserDeleted { get; }

        public virtual string Status
        {
            get
            {
                return IsActive ? "Ativo" : "Inativo";
            }
        }
    }
}
