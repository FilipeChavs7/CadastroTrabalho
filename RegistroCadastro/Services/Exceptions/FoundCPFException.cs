using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroCadastro.Services.Exceptions
{
    public class FoundCPFException : ApplicationException
    {
        public FoundCPFException(string message) : base(message)
        {
        }
    }
}
