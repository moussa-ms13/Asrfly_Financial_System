using Asrfly.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asrfly.Data.SqlServer
{
    public class CategoriesEntity : IDataHelper<Categories>
    {
        private DBContext db;
        private Categories table;

        public CategoriesEntity()
        {
            db = new DBContext();
        }

        #region Methods

        public int Add(Categories table)
        {
            try
            {
                db.Categories.Add(table);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> AddAsync(Categories table)
        {
            try
            {
                await db.Categories.AddAsync(table);
                await db.SaveChangesAsync();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int Delete(int Id)
        {
            try
            {
                table = Find(Id);
                if (table != null)
                {
                    db.Categories.Remove(table);
                    db.SaveChanges();
                    return 1;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> DeleteAsync(int Id)
        {
            try
            {
                table = await FindAsync(Id);
                if (table != null)
                {
                    db.Categories.Remove(table);
                    await db.SaveChangesAsync();
                    return 1;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public int Edit(Categories table)
        {
            try
            {
                db = new DBContext();
                db.Categories.Update(table);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> EditAsync(Categories table)
        {
            try
            {
                db = new DBContext();
                db.Categories.Update(table);
                await db.SaveChangesAsync();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public Categories Find(int Id)
        {
            try
            {
                return db.Categories.FirstOrDefault(x => x.Id == Id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<Categories> FindAsync(int Id)
        {
            try
            {
                return await db.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            }
            catch
            {
                return null;
            }
        }

        public List<Categories> GetAllData()
        {
            try
            {
                return db.Categories.ToList();
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Categories>> GetAllDataAsync()
        {
            try
            {
                return await db.Categories.ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public List<Categories> Search(string SearchItem)
        {
            try
            {
                return db.Categories.Where(x => x.Id.ToString() == SearchItem
                    || x.Name.Contains(SearchItem)
                    || x.Details.Contains(SearchItem)
                    || x.AddedDate.Date.ToString().Contains(SearchItem)
                    ).ToList();
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Categories>> SearchAsync(string SearchItem)
        {
            try
            {
                return await db.Categories.Where(x => x.Id.ToString() == SearchItem
                    || x.Name.Contains(SearchItem)
                    || x.Details.Contains(SearchItem)
                    || x.AddedDate.Date.ToString().Contains(SearchItem)
                    ).ToListAsync();
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
