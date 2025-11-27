using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppPrevisao.Models
{
    public class Consulta
    {
        
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed] 
        public int UsuarioId { get; set; }

        [NotNull]
        public string Cidade { get; set; }

        public string PrevisaoDetalhes { get; set; }

        [NotNull]
        public DateTime DataConsulta { get; set; } = DateTime.Now;
    }
}

