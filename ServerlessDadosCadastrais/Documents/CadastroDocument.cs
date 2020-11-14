using MongoDB.Bson;

namespace ServerlessDadosCadastrais.Documents
{
    public class CadastroDocument
    {
        public ObjectId _id { get; set; }
        public string Nome { get; set; }
        public string NomePai { get; set; }
        public string NomeMae { get; set; }
        public string Tecnologia { get; set; }
        public int Idade { get; set; }
        public string Localidade { get; set; }
        public string ReceberNovidades { get; set; }
    }
}