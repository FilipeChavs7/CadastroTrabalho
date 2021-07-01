using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistroCadastro.Services;
using RegistroCadastro.Models;


namespace RegistroCadastro.Controllers
{
    public class EnderecosController : Controller
    {
        private readonly PessoaService _pessoaService;
        private readonly EnderecoService _enderecoService;

        public EnderecosController(PessoaService pessoaService, EnderecoService enderecoService)
        {
            _pessoaService = pessoaService;
            _enderecoService = enderecoService;
        }

        public IActionResult Index()
        {
            var list = _enderecoService.FindAll();
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Endereco endereco)
        {
            _enderecoService.Insert(endereco);
            return RedirectToAction(nameof(Index));
        }
        
    }
}
