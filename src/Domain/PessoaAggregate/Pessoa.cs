﻿using Domain.Exceptions;
using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Domain.PessoaAggregate
{
    public class Pessoa : Entity, IAggregateRoot
    {
        //EF
        protected Pessoa() { }
       
        public Pessoa(string nome, string cpf, DateTime dataNascimento)
        {
            Nome = nome;
            Cpf = new CPF(cpf);
            DataNascimento = dataNascimento;
        }
        public string Nome { get; private set; }
        public CPF Cpf { get; private set; }

        private readonly List<Endereco> _enderecos = new();
        public IReadOnlyCollection<Endereco> Enderecos => _enderecos;
        public DateTime DataNascimento { get; private set; }
        public int Idade => DataNascimento.CalcularIdade();

        public void AdicionarEndereco(Endereco endereco)
        {
            // ver como validar endereco
            var enderecoExistente = Enderecos.FirstOrDefault(x => x == endereco);
            if (enderecoExistente != null) AtualizarEndereco(endereco);
            _enderecos.Add(endereco);
        }
        public void AtualizarEndereco(Endereco endereco)
        {
            // ver como validar endereco
            var enderecoExistente = Enderecos.FirstOrDefault(x => x == endereco);

            if (enderecoExistente == null) throw new DomainException("O endereço não pertence ao usuario");

            _enderecos.Remove(enderecoExistente);
            _enderecos.Add(endereco);
        }  
    }
}
