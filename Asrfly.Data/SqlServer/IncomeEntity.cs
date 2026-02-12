using Asrfly.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asrfly.Data.SqlServer
{
    public class IncomeEntity : IDataHelper<Income>
    {
        private DBContext db;
        public IncomeEntity() { db = new DBContext(); }


        public List<Income> GetAllData()
        {
            try
            {
                var data = (from i in db.Income
                            join c in db.Categories on i.CategoryId equals c.Id
                            join cust in db.Customers on i.SupplierId equals cust.Id into customers
                            from customer in customers.DefaultIfEmpty()
                            join p in db.Projects on i.ProjectId equals p.Id into projs
                            from project in projs.DefaultIfEmpty()
                            select new Income
                            {
                                Id = i.Id,
                                CategoryId = i.CategoryId,
                                SupplierId = i.SupplierId,
                                ProjectId = i.ProjectId,
                                IncomeDate = i.IncomeDate,
                                RecNo = i.RecNo,
                                Amount = i.Amount,
                                Details = i.Details,
                                Image = i.Image,
                                CategoryName = c.Name,
                                SupplierName = customer == null ? "بدون عميل" : customer.Name,
                                ProjectName = project == null ? "" : project.Name
                            }).OrderByDescending(x => x.IncomeDate).ToList();
                return data;
            }
            catch { return new List<Income>(); }
        }

        public async Task<List<Income>> GetAllDataAsync()
        {
            try
            {
                var data = await (from i in db.Income
                                  join c in db.Categories on i.CategoryId equals c.Id
                                  join cust in db.Customers on i.SupplierId equals cust.Id into customers
                                  from customer in customers.DefaultIfEmpty()
                                  join p in db.Projects on i.ProjectId equals p.Id into projs
                                  from project in projs.DefaultIfEmpty()
                                  select new Income
                                  {
                                      Id = i.Id,
                                      CategoryId = i.CategoryId,
                                      SupplierId = i.SupplierId,
                                      ProjectId = i.ProjectId,
                                      IncomeDate = i.IncomeDate,
                                      RecNo = i.RecNo,
                                      Amount = i.Amount,
                                      Details = i.Details,
                                      Image = i.Image,
                                      CategoryName = c.Name,
                                      SupplierName = customer == null ? "بدون عميل" : customer.Name,
                                      ProjectName = project == null ? "" : project.Name
                                  }).OrderByDescending(x => x.IncomeDate).ToListAsync();
                return data;
            }
            catch { return new List<Income>(); }
        }

        public List<Income> Search(string SearchItem) { return new List<Income>(); }
        public Task<List<Income>> SearchAsync(string SearchItem) { return null; }
        public int Add(Income table) { db.Income.Add(table); db.SaveChanges(); return 1; }
        public async Task<int> AddAsync(Income table) { await db.Income.AddAsync(table); await db.SaveChangesAsync(); return 1; }
        public int Edit(Income table) { db.Income.Update(table); db.SaveChanges(); return 1; }
        public async Task<int> EditAsync(Income table) { db.Income.Update(table); await db.SaveChangesAsync(); return 1; }
        public int Delete(int Id) { var t = Find(Id); db.Income.Remove(t); db.SaveChanges(); return 1; }
        public async Task<int> DeleteAsync(int Id) { var t = await FindAsync(Id); db.Income.Remove(t); await db.SaveChangesAsync(); return 1; }
        public Income Find(int Id) { return db.Income.FirstOrDefault(x => x.Id == Id); }
        public async Task<Income> FindAsync(int Id) { return await db.Income.FirstOrDefaultAsync(x => x.Id == Id); }
    }
}
