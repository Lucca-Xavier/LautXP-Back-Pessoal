namespace GSCBase.Domain.Models.Base
{
    public class ClienteModel : PessoaModel
    {
        public string NmCliente { get; set; }
        public string NmModulo { get; set; }
        public string NmUnidade { get; set; }
        public int? IdModulo { get; set; }
        public int? IdUnidade { get; set; }
    }
}
