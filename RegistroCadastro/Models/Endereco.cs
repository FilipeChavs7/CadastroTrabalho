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
        [Required(ErrorMessage = "Preencha com o logradouro")]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "Preencha o complemento")]
        public string Complemento { get; set; }
        [Required(ErrorMessage = "Preencha a cidade")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Preencha com a sigla do estado")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Somente a sigla do estado")]
        public string Estado { get; set; }
        [Required(ErrorMessage = "Preencha o CEP")]
        [DisplayFormat(DataFormatString = "{0:00\\.000\\-000}",ApplyFormatInEditMode = false)]
        public double CEP { get; set; }

        public TipoDeLoc TipoDeRua { get; set; }

        public Pessoa Pessoa { get; set; }
        public int PessoaId { get; set; }

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