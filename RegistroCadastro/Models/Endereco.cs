using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RegistroCadastro.Models.Enums;


namespace RegistroCadastro.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Somente a sigla do estado")]
        public string Estado { get; set; }
        [DisplayFormat(DataFormatString = "{0:00\\.000\\-000}",ApplyFormatInEditMode = false)]
        public double CEP { get; set; }
        public TipoDeLoc TipoDeRua { get; set; }

        public Pessoa Pessoa { get; set; }
        

        public Endereco()
        {
        }

        public Endereco(int id, string logradouro, string complemento, string cidade, string estado, double cep,TipoDeLoc tipoDeRua, Pessoa pessoa)
        {
            Id = id;
            Logradouro = logradouro;
            Complemento = complemento;
            Cidade = cidade;
            Estado = estado;
            CEP = cep;
            TipoDeRua = tipoDeRua;
            Pessoa = pessoa;
        }

    }
}