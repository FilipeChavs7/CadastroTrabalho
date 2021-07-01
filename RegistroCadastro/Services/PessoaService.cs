using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistroCadastro.Models;
using Microsoft.EntityFrameworkCore;
using RegistroCadastro.Services.Exceptions;

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
        
        public void Remove(int id)
        {
            var obj = _context.Pessoa.Find(id);
            _context.Pessoa.Remove(obj);
            _context.SaveChanges();
        }
        public void Update(Endereco obj)
        {
            if (!_context.Pessoa.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
