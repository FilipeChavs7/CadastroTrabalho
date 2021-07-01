using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistroCadastro.Models;
namespace RegistroCadastro.Services
{
    public class PessoaService
    {
        private readonly RegistroCadastroContext _context;

        public PessoaService(RegistroCadastroContext context)
        {
            _context = context;
        }
        public List<Pessoa> FindAll()
        {
            return _context.Pessoa.ToList();
        }
    }
}
