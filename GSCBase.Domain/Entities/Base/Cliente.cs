using GSCBase.Domain.Entities.Auth;

namespace GSCBase.Domain.Entities.Base
{
    public class Cliente : BaseModel
    {
        public Pessoa Pessoa { get; set; }
        public string ReferenceCode { get; set; }
        public string GatewayContact { get; set; }

        public Cliente()
        {

        }
        public Cliente(Pessoa pessoa,string contact, string reference,ApplicationUser user)
        {
            this.Pessoa = pessoa;
            this.GatewayContact = contact;
            this.ReferenceCode = reference;
            this.SetUserCreate(user);
        }
    }
}
