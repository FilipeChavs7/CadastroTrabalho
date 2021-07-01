using RegistroCadastro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroCadastro.Services
{
    public class EnderecoService
    {
        private readonly RegistroCadastroContext _context;

        public EnderecoService(RegistroCadastroContext context)
        {
            _context = context;
        }
        public List<Endereco> FindAll()
        {
            return _context.Endereco.ToList();
        }
        public void Insert(Endereco obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
