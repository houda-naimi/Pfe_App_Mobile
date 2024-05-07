using PfeShell.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PfeShell.Repositories
{
    public class EventTable
    {
        SQLiteAsyncConnection connection;
        public EventTable(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);
            connection.CreateTableAsync<AppointmentModel>().Wait();
        }

        public Task<int> AddEvent(AppointmentModel c)
        {
            if (c.Id !=0)
            {
                return connection.UpdateAsync(c);
            }
            else
            {
                return connection.InsertAsync(c);
            }
        }
        public async Task<List<AppointmentModel>> GetEvents()
        {
            return await connection.Table<AppointmentModel>().ToListAsync();
        }

        public Task<AppointmentModel> GetItemAsync(int id)
        {
            
          return  connection.Table<AppointmentModel>().Where(i=>i.Equals(id)).FirstOrDefaultAsync();

        }

        public Task<int> SupprimerItemAsync(AppointmentModel e)
        {
            return connection.DeleteAsync(e);
        }

        public async Task<AppointmentModel> GetSpecificAction(string subject, string starttime)
        {
            return await connection.Table<AppointmentModel>().Where(i => i.Subject.Equals(subject) && i.StartTime.Equals(starttime)).FirstOrDefaultAsync();
        }


    }
}
