﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistroCadastro.Models;
using Microsoft.EntityFrameworkCore;
using RegistroCadastro.Services.Exceptions;
using RegistroCadastro.Models.ViewModels;

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
            bool hasAnyCPF = await _context.Pessoa.AnyAsync(x => x.CPF == obj.CPF);
            if (!hasAnyCPF)
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new FoundCPFException("CPF cadastrado ja existe!");
                
            }
        }
        
        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Pessoa.FindAsync(id);
            _context.Pessoa.Remove(obj);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(PessoaFormViewModel obj)
        {
            bool hasAny = await _context.Pessoa.AnyAsync(x => x.Id == obj.Pessoa.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj.Pessoa);
                _context.Update(obj.Endereco);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
