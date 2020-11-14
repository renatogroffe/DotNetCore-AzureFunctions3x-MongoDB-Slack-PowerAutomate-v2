namespace ServerlessDadosCadastrais.Models
{
    public class Cadastro
    {
        public string nome { get; set; }
        public string nome_pai { get; set; }
        public string nome_mae { get; set; }
        public string tecnologia { get; set; }
        public int? idade { get; set; }
        public string cidade { get; set; }
        public bool? aceito_novidades { get; set; }
    }
}