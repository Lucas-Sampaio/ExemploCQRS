﻿using System;
using System.Collections.Generic;
using Utils;

namespace API.Application.DTOs
{
    public class PessoaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }

        public List<EnderecoDto> Enderecos { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Idade => DataNascimento.CalcularIdade();
    }
}
