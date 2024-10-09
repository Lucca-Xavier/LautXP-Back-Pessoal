using GSCBase.Domain.Entities.Auth;

namespace GSCBase.Domain.Models.Base
{
    public class UnidadeModel : BaseViewModel
    {
        public string NmUnidade { get; set; }
        public string Sistema { get; set; }
        public PessoaModel Pessoa { get; set; }

        public UnidadeModel()
        {

        }

        public UnidadeModel(string nmUnidade, PessoaModel pessoa, ApplicationUser user)
        {
            this.NmUnidade = nmUnidade;
            this.Pessoa = pessoa;
        }

        public void Alterar(string nmUnidade,ApplicationUser user)
        {
            this.NmUnidade = nmUnidade;
        }

    }
}
