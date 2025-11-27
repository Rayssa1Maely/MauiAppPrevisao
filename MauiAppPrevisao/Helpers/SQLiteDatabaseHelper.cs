using SQLite;
using MauiAppPrevisao.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace MauiAppPrevisao.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);

            _conn.CreateTableAsync<Usuario>().Wait();
            _conn.CreateTableAsync<Consulta>().Wait();
        }

        
        public Task<int> Insert(Usuario usuario)
        {
            return _conn.InsertAsync(usuario);
        }

        
        public Task<List<Usuario>> Search(string email, string senha)
        {
            return _conn.Table<Usuario>()
                        .Where(u => u.Email.Equals(email) && u.Senha.Equals(senha))
                        .ToListAsync();
        }

       

        public Task<int> InsertUsuarioAsync(Usuario usuario)
        {
            return _conn.InsertAsync(usuario);
        }

        public Task<Usuario> GetUsuarioByEmailAsync(string email)
        {
            return _conn.Table<Usuario>()
                        .Where(u => u.Email.Equals(email))
                        .FirstOrDefaultAsync();
        }

        public Task<int> UpdateUsuarioAsync(Usuario usuario)
        {
            return _conn.UpdateAsync(usuario);
        }

        public Task<int> SaveHistoricoAsync(Consulta historico)
        {
            return _conn.InsertAsync(historico);
        }

        public Task<List<Consulta>> GetHistoricoByDateRangeAsync(int userId, DateTime startDate, DateTime endDate)
        {
            DateTime adjustedEndDate = endDate.Date.AddDays(1).AddSeconds(-1);

            return _conn.Table<Consulta>()

                           .Where(h => h.UsuarioId == userId &&
                                       h.DataConsulta >= startDate.Date &&
                                       h.DataConsulta <= adjustedEndDate)
                           .OrderByDescending(h => h.DataConsulta)
                           .ToListAsync();
        }
    }
}