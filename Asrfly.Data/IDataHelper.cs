using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Asrfly.Data
{
    public interface IDataHelper<Table>
    {
        List<Table> GetAllData();
        List<Table> Search(string SearchItem);
        Table Find(int Id);

        int Add(Table table);
        int Edit(Table table);
        int Delete(int Id);

        Task<List<Table>> GetAllDataAsync();
        Task<List<Table>> SearchAsync(string SearchItem);
        Task<Table> FindAsync(int Id);

        Task<int> AddAsync(Table table);
        Task<int> EditAsync(Table table);
        Task<int> DeleteAsync(int Id);
    }
}
