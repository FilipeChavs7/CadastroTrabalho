using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroCadastro.Models.ViewModels
{
    public class EnderecoFormViewModel
    {
        public Endereco Endereco { get; set; }
        public ICollection<Pessoa> Pessoas { get; set; }
    }
}
