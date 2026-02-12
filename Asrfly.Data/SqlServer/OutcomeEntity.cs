using Asrfly.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asrfly.Data.SqlServer
{
    public class OutcomeEntity : IDataHelper<Outcome>
    {
        private DBContext db;
        public OutcomeEntity() { db = new DBContext(); }


        public List<Outcome> GetAllData()
        {
            try
            {
                var data = (from o in db.Outcome
                            join c in db.Categories on o.CategoryId equals c.Id
                            join s in db.Suppliers on o.SupplierId equals s.Id into supps
                            from supplier in supps.DefaultIfEmpty()
                            join p in db.Projects on o.ProjectId equals p.Id into projs
                            from project in projs.DefaultIfEmpty()
                            select new Outcome
                            {
                                Id = o.Id,
                                CategoryId = o.CategoryId,
                                SupplierId = o.SupplierId,
                                ProjectId = o.ProjectId,
                                OutcomeDate = o.OutcomeDate,
                                RecNo = o.RecNo,
                                Amount = o.Amount,
                                Details = o.Details,
                                Image = o.Image,
                                CategoryName = c.Name,
                                SupplierName = supplier == null ? "بدون مورد" : supplier.Name,
                                ProjectName = project == null ? "" : project.Name
                            }).OrderByDescending(x => x.OutcomeDate).ToList();
                return data;
            }
            catch { return new List<Outcome>(); }
        }

        public async Task<List<Outcome>> GetAllDataAsync()
        {
            try
            {
                var data = await (from o in db.Outcome
                                  join c in db.Categories on o.CategoryId equals c.Id
                                  join s in db.Suppliers on o.SupplierId equals s.Id into supps
                                  from supplier in supps.DefaultIfEmpty()
                                  join p in db.Projects on o.ProjectId equals p.Id into projs
                                  from project in projs.DefaultIfEmpty()
                                  select new Outcome
                                  {
                                      Id = o.Id,
                                      CategoryId = o.CategoryId,
                                      SupplierId = o.SupplierId,
                                      ProjectId = o.ProjectId,
                                      OutcomeDate = o.OutcomeDate,
                                      RecNo = o.RecNo,
                                      Amount = o.Amount,
                                      Details = o.Details,
                                      Image = o.Image,
                                      CategoryName = c.Name,
                                      SupplierName = supplier == null ? "بدون مورد" : supplier.Name,
                                      ProjectName = project == null ? "" : project.Name
                                  }).OrderByDescending(x => x.OutcomeDate).ToListAsync();
                return data;
            }
            catch { return new List<Outcome>(); }
        }

        public List<Outcome> Search(string SearchItem) { return new List<Outcome>(); /* اختصاراً للرد */ }
        public Task<List<Outcome>> SearchAsync(string SearchItem) { return null; }
        public int Add(Outcome table) { db.Outcome.Add(table); db.SaveChanges(); return 1; }
        public async Task<int> AddAsync(Outcome table) { await db.Outcome.AddAsync(table); await db.SaveChangesAsync(); return 1; }
        public int Edit(Outcome table) { db.Outcome.Update(table); db.SaveChanges(); return 1; }
        public async Task<int> EditAsync(Outcome table) { db.Outcome.Update(table); await db.SaveChangesAsync(); return 1; }
        public int Delete(int Id) { var t = Find(Id); db.Outcome.Remove(t); db.SaveChanges(); return 1; }
        public async Task<int> DeleteAsync(int Id) { var t = await FindAsync(Id); db.Outcome.Remove(t); await db.SaveChangesAsync(); return 1; }
        public Outcome Find(int Id) { return db.Outcome.FirstOrDefault(x => x.Id == Id); }
        public async Task<Outcome> FindAsync(int Id) { return await db.Outcome.FirstOrDefaultAsync(x => x.Id == Id); }
    }
}
