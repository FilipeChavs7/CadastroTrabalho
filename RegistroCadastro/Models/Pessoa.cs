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
        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "{0} required")]
        public Sexo Sexo { get; set; }
        public int Idade { get; set; }
        public ICollection<Endereco> Endereco { get; set; } = new List<Endereco>();

        public Pessoa()
        {
        }

        public Pessoa(int id, string name, DateTime birthDate, Sexo sexo)
        {
            Id = Id;
            Name = name;
            BirthDate = birthDate;
            Sexo = sexo;
            Idade = FindAge();
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