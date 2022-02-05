using Domain.PessoaAggregate;
using Infrastructure.Configs;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PessoaCosmoRepository : IPessoaCosmoRepository
    {
        private CosmosClient _cosmoClient;

        private Database _database;

        private Container _container;

        // The name of the database and container we will create
        private const string databaseId = "PessoaDatabase";
        private const string containerId = "PessoaContainer";
        public PessoaCosmoRepository(IOptions<AzureCosmoConfig> cosmoConfig)
        {
            var cosmoOption = new CosmosClientOptions
            {
                SerializerOptions = new CosmosSerializationOptions
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            };
            _cosmoClient = new CosmosClient(cosmoConfig.Value.EndpointUri, cosmoConfig.Value.PrimaryKey, cosmoOption);
            InitializeService().Wait();
        }
        public async Task AdicionarOuAtualizarAsync(PessoaCosmo entidade)
        {
            ItemResponse<PessoaCosmo> pessoaResponse;
            var partitionKeyName = "PessoaTeste";
            entidade.Partition = partitionKeyName;
            try
            {
                //verifica se pessoa existe
                pessoaResponse = await _container.ReadItemAsync<PessoaCosmo>(entidade.Id.ToString(), new PartitionKey(partitionKeyName));
                //atualiza item
                await _container.ReplaceItemAsync(entidade, entidade.Id.ToString(), new PartitionKey(partitionKeyName));
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                //cria item
                try
                {
                    pessoaResponse = await _container.CreateItemAsync(entidade);
                }
                catch (Exception ex2)
                {
                    _ = ex2;
                    throw;
                }

            }
        }

        public async Task<PessoaCosmo> ObterPorIdAsync(int id)
        {
            var partitionKeyName = "PessoaTeste";
            var pessoaResponse = await _container.ReadItemAsync<PessoaCosmo>(id.ToString(), new PartitionKey(partitionKeyName));
            return pessoaResponse.Resource;
        }

        public async Task<List<PessoaCosmo>> ObterTodosAsync()
        {
            var partitionKeyName = "PessoaTeste";
            using var resultSet = _container.GetItemQueryIterator<PessoaCosmo>(
                queryDefinition: null,
                requestOptions: new QueryRequestOptions()
                {
                    PartitionKey = new PartitionKey(partitionKeyName)
                });
            var lista = new List<PessoaCosmo>();
            while (resultSet.HasMoreResults)
            {
                var response = await resultSet.ReadNextAsync();
                foreach (var pessoa in response)
                {
                    lista.Add(pessoa);
                }
            }
            return lista;
        }

        /// <summary>
        /// Executa uma query (usando Azure Cosmos DB SQL syntax) no the container
        /// </summary>
        public async Task<PessoaCosmo> ObterPorCPFAsync(string cpf)
        {
            var sqlQueryText = "SELECT * FROM c WHERE c.Cpf = @cpf";
            var queryDefinition = new QueryDefinition(sqlQueryText).WithParameter("@cpf", cpf);
            using var queryResultSetIterator = _container.GetItemQueryIterator<PessoaCosmo>(queryDefinition);

            while (queryResultSetIterator.HasMoreResults)
            {
                var currentResultSet = await queryResultSetIterator.ReadNextAsync();
                return currentResultSet.FirstOrDefault();
            }
            return null;
        }
        public async Task RemoverAsync(int id)
        {
            var partitionKeyName = "PessoaTeste";
            _ = await _container.DeleteItemAsync<PessoaDocument>(id.ToString(), new PartitionKey(partitionKeyName));
        }

        /// <summary>
        /// Cria uma database se não existir
        /// </summary>
        private async Task CreateDatabaseAsync()
        {
            // Create a new database
            _database = await _cosmoClient.CreateDatabaseIfNotExistsAsync(databaseId);
            Console.WriteLine("Created Database: {0}\n", _database);
        }
        /// <summary>
        ///Cria um container se não existir
        /// </summary>
        /// <returns></returns>
        private async Task CreateContainerAsync()
        {
            _container = await _database.CreateContainerIfNotExistsAsync(containerId, "/PessoaTeste");
            Console.WriteLine("Created Container: {0}\n", _container.Id);
        }
        private async Task InitializeService()
        {
            await CreateDatabaseAsync();
            await CreateContainerAsync();
        }
    }
}
