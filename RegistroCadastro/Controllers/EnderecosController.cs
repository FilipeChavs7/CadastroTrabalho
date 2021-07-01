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

        public async  Task<IActionResult> Index()
        {
            var list = await _enderecoService.FindAllAsync();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Endereco endereco)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new EnderecoFormViewModel { Endereco = endereco };
                return View(viewModel);
            }
            await _enderecoService.InsertAsync(endereco);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var obj = await _enderecoService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _enderecoService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async  Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = await _enderecoService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var obj = await _enderecoService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return NotFound();
            }
            List<Endereco> enderecos = await _enderecoService.FindAllAsync();
            EnderecoFormViewModel viewModel = new EnderecoFormViewModel { Endereco = obj };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Endereco endereco)
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
                await _enderecoService.UpdateAsync(endereco);
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
