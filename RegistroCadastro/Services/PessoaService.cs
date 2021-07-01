using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistroCadastro.Models;
using Microsoft.EntityFrameworkCore;


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
        public Pessoa FindById(int id)
        {
            return _context.Pessoa.Include(obj => obj.Endereco).FirstOrDefault(obj => obj.Id == id);
        }
        public void Insert(Endereco obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
