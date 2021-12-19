using System.Collections.Generic;

namespace Domain.PessoaAggregate
{
    public interface IPessoaMongoRepository
    {
        void AdicionarOuAtualizarPessoa(PessoaDocument pessoa);
        void Remover(int id);
        PessoaDocument ObterPorId(int id);
        PessoaDocument ObterPorCPF(string cpf);
        List<PessoaDocument> ObterTodos();
    }
}
