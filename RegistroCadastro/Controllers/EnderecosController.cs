using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistroCadastro.Services;
using RegistroCadastro.Models;
using RegistroCadastro.Models.ViewModels;
using RegistroCadastro.Services.Exceptions;

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
            if (!ModelState.IsValid)
            {
                var viewModel = new EnderecoFormViewModel { Endereco = endereco };
                return View(viewModel);
            }
            _enderecoService.Insert(endereco);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var obj = _enderecoService.FindById(id.Value);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _enderecoService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _enderecoService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var obj = _enderecoService.FindById(id.Value);
            if(obj == null)
            {
                return NotFound();
            }
            List<Endereco> enderecos = _enderecoService.FindAll();
            EnderecoFormViewModel viewModel = new EnderecoFormViewModel { Endereco = obj };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Endereco endereco)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new EnderecoFormViewModel { Endereco = endereco };
                return View(viewModel);
            }
            if (id != endereco.Id)
            {
                return BadRequest();
            }
            try
            {
                _enderecoService.Update(endereco);
                return RedirectToAction(nameof(Index));
            }
            catch(NotFoundException)
            {
                return BadRequest();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }

    }
}
