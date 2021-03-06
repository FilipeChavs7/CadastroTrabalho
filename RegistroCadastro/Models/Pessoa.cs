using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RegistroCadastro.Models.Enums;

namespace RegistroCadastro.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Preencha o nome")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Preencha o CPF")]
        [DisplayFormat(DataFormatString = "{0:000\\.000\\.000\\-00}", ApplyFormatInEditMode = false)]
        public string CPF { get; set; }


        [Required(ErrorMessage = "Preencha a data de nascimento")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }


        [Required(ErrorMessage = "Escolha o sexo")]
        public Sexo Sexo { get; set; }

        public ICollection<Endereco> Endereco { get; set; } = new List<Endereco>();

        public Pessoa()
        {
        }

        public Pessoa(int id, string name,string cpf, DateTime birthDate, Sexo sexo)
        {
            Id = Id;
            Name = name;
            CPF = cpf;
            BirthDate = birthDate;
            Sexo = sexo;
        }

        public void AddEndereco(Endereco loc)
        {
            Endereco.Add(loc);
        }
        public void RemoveEndereco(Endereco loc)
        {
            Endereco.Remove(loc);
        }
        public int FindAge()
        {
            int idade = DateTime.Now.Year - BirthDate.Year;
            if (DateTime.Now.Day < BirthDate.Day)
            {
                idade--;
            }
            return idade;
        }

        

    }
}