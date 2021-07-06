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
        public async Task<IActionResult> Create(PessoaFormViewModel pessoaFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                var enderecos = await _enderecoService.FindAllAsync();
                var viewModel = new PessoaFormViewModel { Pessoa = pessoaFormViewModel.Pessoa, Enderecos = enderecos };
                return View(viewModel);
            }
            try
            {
                var pessoa1 = new Pessoa
                {
                    Name = pessoaFormViewModel.Pessoa.Name,
                    BirthDate = pessoaFormViewModel.Pessoa.BirthDate,
                    CPF = pessoaFormViewModel.Pessoa.CPF,
                    Sexo = pessoaFormViewModel.Pessoa.Sexo
                };
                /*var endereco1 = new Endereco
                {
                    Logradouro = pessoaFormViewModel.Endereco.Logradouro,
                    Complemento = pessoaFormViewModel.Endereco.Complemento,
                    Cidade = pessoaFormViewModel.Endereco.Cidade,
                    Estado = pessoaFormViewModel.Endereco.Estado,
                    CEP = pessoaFormViewModel.Endereco.CEP,
                    TipoDeRua = pessoaFormViewModel.Endereco.TipoDeRua,
                    Pessoa = pessoaFormViewModel.Pessoa
                };*/ //salva o endereço e pessoa

                await _pessoaService.InsertAsync(pessoa1);
                /*await _enderecoService.InsertAsync(endereco1); */ //salva o endereço junto com a pessoa, so um por pessoa!
                /*return RedirectToAction(nameof(Index));*/ // volta na tela do index
                return RedirectToAction(nameof(AdicionarEndereco));

            }
            catch(FoundCPFException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        public async Task<IActionResult> AdicionarEndereco(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID da criação endereço não provido" });
            }
            var pessoa = await _pessoaService.FindByIdAsync(id.Value);
            var endereco = await _enderecoService.FindByIdAsync(id.Value);
            PessoaFormViewModel viewModel = new PessoaFormViewModel { Pessoa = pessoa, Endereco = endereco };
            return PartialView(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarEndereco(PessoaFormViewModel pessoaFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new PessoaFormViewModel { Pessoa = pessoaFormViewModel.Pessoa, Endereco = pessoaFormViewModel.Endereco };
                return PartialView(viewModel);
            }

            await _enderecoService.InsertAsync(pessoaFormViewModel.Endereco);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var pessoa = await _pessoaService.FindByIdAsync(id.Value);
            var endereco = await _enderecoService.FindByIdAsync(id.Value);
            if (pessoa == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            PessoaFormViewModel viewModel = new PessoaFormViewModel { Pessoa = pessoa, Endereco = endereco };
            return View(viewModel);
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
            var pessoa = await _pessoaService.FindByIdAsync(id.Value);
            var endereco = await _enderecoService.FindByIdAsync(id.Value);
            if (pessoa == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            PessoaFormViewModel viewModel = new PessoaFormViewModel { Pessoa = pessoa, Endereco = endereco };
            return View(viewModel);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var pessoa = await _pessoaService.FindByIdAsync(id.Value);
            var endereco = await _enderecoService.FindByIdAsync(id.Value);
            if (pessoa == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            /*List<Pessoa> pessoas = await _pessoaService.FindAllAsync();
            List<Endereco> enderecos = await _enderecoService.FindAllAsync();*/
            //provavel adicionar uma list da endereços aqui para colocar no viewModel
            PessoaFormViewModel viewModel = new PessoaFormViewModel { Pessoa = pessoa,Endereco = endereco };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PessoaFormViewModel pessoaFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new PessoaFormViewModel { Pessoa = pessoaFormViewModel.Pessoa, Endereco = pessoaFormViewModel.Endereco };
                return View(viewModel);
            }
            if (id != pessoaFormViewModel.Pessoa.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            if (id != pessoaFormViewModel.Endereco.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _pessoaService.UpdateAsync(pessoaFormViewModel);
                /*await _enderecoService.UpdateAsync(pessoaFormViewModel.Endereco);*/
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
