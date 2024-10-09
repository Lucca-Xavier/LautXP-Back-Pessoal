using GSCBase.Domain.Entities.Auth;

namespace GSCBase.Domain.Entities.Base
{
    public class Unidade : BaseModel
    {
        public string NmUnidade { get; set; }
        public string Sistema { get; set; }
        public Pessoa Pessoa { get; set; }

        public Unidade()
        {

        }

        public Unidade(string nmUnidade, Pessoa pessoa, ApplicationUser user)
        {
            this.NmUnidade = nmUnidade;
            this.Pessoa = pessoa;
            this.SetUserCreate(user);
        }

        public void Alterar(string nmUnidade,ApplicationUser user)
        {
            this.NmUnidade = nmUnidade;
            this.SetUserAlter(user);
        }

    }
}
