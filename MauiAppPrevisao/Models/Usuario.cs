using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SQLite;

namespace MauiAppPrevisao.Models
{
    public class Usuario
    {
        string _nome;
        string _dataNascimento;
        string _email;
        string _senha;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nome
        {
            get => _nome;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Por favor, preencha o nome.");
                }
                _nome = value.Trim();
            }
        }

        public string DataNascimento
        {
            get => _dataNascimento;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Por favor, preencha com a sua Data de Nascimento.");
                }
               
                _dataNascimento = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Por favor, preencha o email.");
                }
              
                if (!Regex.IsMatch(value.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    throw new Exception("O email inserido não é válido.");
                }
                _email = value.Trim();
            }
        }

        public string Senha
        {
            get => _senha;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Por favor, preencha a Senha.");
                }
                
                if (value.Length < 6)
                {
                    throw new Exception("A senha deve ter no mínimo 6 caracteres.");
                }
                _senha = value;
            }
        }
    }
}