using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                if (value == null)
                {
                    throw new Exception(
                        "Por favor, preencha o nome");
                }
                _nome = value;

            }
        }

        public string DataNascimento
        {
            get => _dataNascimento;
            set
            {
                if (value == null)
                {
                    throw new Exception(
                        "Por favor, preencha com a sua Data de Nascimento");
                }
                _dataNascimento = value;

            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (value == null)
                {
                    throw new Exception(
                        "Por favor, preencha o email ");
                }
                _email = value;

            }
        }

        public string Senha
        {
            get => _senha;
            set
            {
                if (value == null)
                {
                    throw new Exception(
                        "Por favor, preencha a Senha");
                }
                _senha = value;

            }
        }
    }

    }
