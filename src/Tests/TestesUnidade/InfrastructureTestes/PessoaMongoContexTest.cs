using Domain.PessoaAggregate;
using Infrastructure;
using Infrastructure.Configs;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Tests.TestesUnidade.InfrastructureTestes
{
    public class PessoaMongoContexTest
    {
        private Mock<IOptions<MongoConfig>> _mockOptions;

        public PessoaMongoContexTest()
        {
            _mockOptions = new Mock<IOptions<MongoConfig>>();
        }

        [Fact(DisplayName = "cria um mongoDbContext com Sucesso")]
        [Trait("MongoContext", "Mongo Context Tests")]
        public void MongoDBContext_Construir_DeveExecutarComSucesso()
        {
            //Arrange
            var settings = new MongoConfig()
            {
                Connection = "mongodb://tes123 ",
                DatabaseName = "TestDB"
            };

            _mockOptions.Setup(s => s.Value).Returns(settings);

            //Act 
            var context = new MongoDbContext(_mockOptions.Object);

            //Assert 
            Assert.NotNull(context);
        }

        [Fact(DisplayName = "obtem uma colletion mas falha")]
        [Trait("MongoContext", "Mongo Context Tests")]
        public void MongoDBContext_ObtemCollection_NomeVazio_Falha()
        {

            //Arrange
            var settings = new MongoConfig()
            {
                Connection = "mongodb://tes123",
                DatabaseName = "TestDB"
            };

            _mockOptions.Setup(s => s.Value).Returns(settings);

            //Act 
            var context = new MongoDbContext(_mockOptions.Object);
            var myCollection = context.GetCollection<PessoaDocument>("");

            //Assert 
            Assert.Null(myCollection);

        }

        [Fact(DisplayName = "obtem uma collection com Sucesso")]
        [Trait("MongoContext", "Mongo Context Tests")]
        public void MongoDBContext_ObtemCollection_Successo()
        {
            //Arrange
            var settings = new MongoConfig()
            {
                Connection = "mongodb://tes123 ",
                DatabaseName = "TestDB"
            };

            _mockOptions.Setup(s => s.Value).Returns(settings);

            //Act 
            var context = new MongoDbContext(_mockOptions.Object);
            var myCollection = context.GetCollection<PessoaDocument>("Pessoas");

            //Assert 
            Assert.NotNull(myCollection);
        }
    }
}
