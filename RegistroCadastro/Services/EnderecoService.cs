using RegistroCadastro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistroCadastro.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace RegistroCadastro.Services
{
    public class EnderecoService
    {
        private readonly RegistroCadastroContext _context;

        public EnderecoService(RegistroCadastroContext context)
        {
            _context = context;
        }
        public async Task<List<Endereco>> FindAllAsync()
        {
            return await _context.Endereco.ToListAsync();
        }
        public async Task InsertAsync(Endereco obj)
        {



            _context.Add(obj);
            await _context.SaveChangesAsync();


        }
        public async Task<Endereco> FindByIdAsync(int id)
        {
            return await _context.Endereco.FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Endereco.FindAsync(id);
            _context.Endereco.Remove(obj);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Endereco obj)
        {
            bool hasAny = await _context.Endereco.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                /*await _context.SaveChangesAsync();*/
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
