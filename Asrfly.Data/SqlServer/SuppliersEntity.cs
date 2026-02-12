using Asrfly.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asrfly.Data.SqlServer
{
    public class SuppliersEntity : IDataHelper<Suppliers>
    {
        private DBContext db;
        private Suppliers table;

        public SuppliersEntity()
        {
            db = new DBContext();
        }

        #region Methods

        public int Add(Suppliers table)
        {
            try
            {
                db.Suppliers.Add(table);
                db.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }

        public async Task<int> AddAsync(Suppliers table)
        {
            try
            {
                await db.Suppliers.AddAsync(table);
                await db.SaveChangesAsync();
                return 1;
            }
            catch { return 0; }
        }

        public int Delete(int Id)
        {
            try
            {
                table = Find(Id);
                if (table != null)
                {
                    db.Suppliers.Remove(table);
                    db.SaveChanges();
                    return 1;
                }
                return 0;
            }
            catch { return 0; }
        }

        public async Task<int> DeleteAsync(int Id)
        {
            try
            {
                table = await FindAsync(Id);
                if (table != null)
                {
                    db.Suppliers.Remove(table);
                    await db.SaveChangesAsync();
                    return 1;
                }
                return 0;
            }
            catch { return 0; }
        }

        public int Edit(Suppliers table)
        {
            try
            {
                db = new DBContext();
                db.Suppliers.Update(table);
                db.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }

        public async Task<int> EditAsync(Suppliers table)
        {
            try
            {
                db = new DBContext();
                db.Suppliers.Update(table);
                await db.SaveChangesAsync();
                return 1;
            }
            catch { return 0; }
        }

        public Suppliers Find(int Id)
        {
            try
            {
                return db.Suppliers.FirstOrDefault(x => x.Id == Id);
            }
            catch { return null; }
        }

        public async Task<Suppliers> FindAsync(int Id)
        {
            try
            {
                return await db.Suppliers.FirstOrDefaultAsync(x => x.Id == Id);
            }
            catch { return null; }
        }

        public List<Suppliers> GetAllData()
        {
            try
            {
                return db.Suppliers.ToList();
            }
            catch { return null; }
        }

        public async Task<List<Suppliers>> GetAllDataAsync()
        {
            try
            {
                return await db.Suppliers.ToListAsync();
            }
            catch { return null; }
        }

        public List<Suppliers> Search(string SearchItem)
        {
            try
            {
                return db.Suppliers.Where(x => x.Id.ToString() == SearchItem
                    || x.Name.Contains(SearchItem)
                    || x.PhoneNumber.Contains(SearchItem)
                    || x.Address.Contains(SearchItem)
                    || x.Details.Contains(SearchItem)
                    || x.Balance.ToString().Contains(SearchItem)
                    || x.AddedDate.Date.ToString().Contains(SearchItem)
                    ).ToList();
            }
            catch { return null; }
        }

        public async Task<List<Suppliers>> SearchAsync(string SearchItem)
        {
            try
            {
                return await db.Suppliers.Where(x => x.Id.ToString() == SearchItem
                    || x.Name.Contains(SearchItem)
                    || x.PhoneNumber.Contains(SearchItem)
                    || x.Address.Contains(SearchItem)
                    || x.Details.Contains(SearchItem)
                    || x.Balance.ToString().Contains(SearchItem)
                    || x.AddedDate.Date.ToString().Contains(SearchItem)
                    ).ToListAsync();
            }
            catch { return null; }
        }
        #endregion
    }
}
