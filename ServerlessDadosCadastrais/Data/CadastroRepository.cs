using System;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using ServerlessDadosCadastrais.Documents;

namespace ServerlessDadosCadastrais.Data
{
    public class CadastroRepository
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<CadastroDocument> _collection;

        public CadastroRepository()
        {
            _client = new MongoClient(
                Environment.GetEnvironmentVariable("MongoConnection"));
            _db = _client.GetDatabase(
                Environment.GetEnvironmentVariable("MongoDatabase"));
            _collection = _db.GetCollection<CadastroDocument>(
                Environment.GetEnvironmentVariable("MongoCollection"));
        }

        public void Save(CadastroDocument document)
        {
            _collection.InsertOne(document);
        }       

        public List<CadastroDocument> ListAll()
        {
            return _collection.Find(all => true).ToList();
        }
    }
}