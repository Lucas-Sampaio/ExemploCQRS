using System.Collections.Generic;

namespace Domain.PessoaAggregate
{
    public interface IPessoaMongoRepository
    {
        void Adicionar(PessoaDocument obj);
        void Atualizar(int id, PessoaDocument obj);
        void Remover(int id);
        PessoaDocument ObterPorId(int id);
        List<PessoaDocument> ObterTodos();
    }
}
