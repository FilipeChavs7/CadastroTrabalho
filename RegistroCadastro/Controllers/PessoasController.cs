using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RegistroCadastro.Models;
using RegistroCadastro.Models.ViewModels;
using RegistroCadastro.Services;
using RegistroCadastro.Services.Exceptions;

namespace RegistroCadastro.Controllers
{
    public class PessoasController : Controller
    {
        private readonly RegistroCadastroContext _context;
        private readonly PessoaService _pessoaService;
        private readonly EnderecoService _enderecoService;

        public PessoasController(RegistroCadastroContext context, PessoaService pessoaService, EnderecoService enderecoService)
        {
            _context = context;
            _pessoaService = pessoaService;
            _enderecoService = enderecoService;
        }

        

        public async Task<IActionResult> Index()
        {
            var list = await _pessoaService.FindAllAsync();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            var enderecos = await _enderecoService.FindAllAsync();
            var viewModel = new PessoaFormViewModel { Enderecos = enderecos };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pessoa pessoa)
        {
            if (!ModelState.IsValid)
            {
                var enderecos = await _enderecoService.FindAllAsync();
                var viewModel = new PessoaFormViewModel { Pessoa = pessoa, Enderecos = enderecos };
                return View(viewModel);
            }
            await _pessoaService.InsertAsync(pessoa);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _pessoaService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _pessoaService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _pessoaService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _pessoaService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            List<Pessoa> pessoas = await _pessoaService.FindAllAsync();
            //provavel adicionar uma list da endereços aqui para colocar no viewModel
            PessoaFormViewModel viewModel = new PessoaFormViewModel { Pessoa = obj };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pessoa pessoa)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new PessoaFormViewModel { Pessoa = pessoa };
                return View(viewModel);
            }
            if (id != pessoa.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _pessoaService.UpdateAsync(pessoa);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
