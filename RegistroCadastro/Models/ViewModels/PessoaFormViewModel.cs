using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroCadastro.Models.ViewModels
{
    public class PessoaFormViewModel
    {
        public Pessoa Pessoa { get; set; }
        public Endereco Endereco { get; set; }
        public ICollection<Endereco> Enderecos { get; set; }

    }
}
