using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RegistroCadastro.Models;

namespace RegistroCadastro.Models
{
    public class RegistroCadastroContext : DbContext
    {
        public RegistroCadastroContext (DbContextOptions<RegistroCadastroContext> options)
            : base(options)
        {
        }

        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
    }
}
