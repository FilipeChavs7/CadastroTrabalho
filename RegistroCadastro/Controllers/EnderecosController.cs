using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistroCadastro.Services;
using RegistroCadastro.Models;
using RegistroCadastro.Models.ViewModels;
using RegistroCadastro.Services.Exceptions;
using System.Diagnostics;

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
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _enderecoService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
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
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _enderecoService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _enderecoService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            List<Pessoa> pessoas = await _pessoaService.FindAllAsync();
            EnderecoFormViewModel viewModel = new EnderecoFormViewModel { Endereco = obj, Pessoas = pessoas };
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
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _enderecoService.UpdateAsync(endereco);
                return RedirectToAction(nameof(Index));
            }
            catch(NotFoundException e)
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
