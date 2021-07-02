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
        public async Task<List<Pessoa>> FindAllAsync()
        {
            return await _context.Pessoa.ToListAsync();
        }
        public async Task<Pessoa> FindByIdAsync(int id)
        {
            return await _context.Pessoa.Include(obj => obj.Endereco).FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public async Task InsertAsync(Pessoa obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        
        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Pessoa.FindAsync(id);
            _context.Pessoa.Remove(obj);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Pessoa obj)
        {
            bool hasAny = await _context.Pessoa.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
