using Asrfly.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asrfly.Data.SqlServer
{
    public class ProjectsEntity : IDataHelper<Projects>
    {
        private DBContext db;
        private Projects table;

        public ProjectsEntity()
        {
            db = new DBContext();
        }

        #region Methods

        public int Add(Projects table)
        {
            try
            {
                db.Projects.Add(table);
                db.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }

        public async Task<int> AddAsync(Projects table)
        {
            try
            {
                await db.Projects.AddAsync(table);
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
                    db.Projects.Remove(table);
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
                    db.Projects.Remove(table);
                    await db.SaveChangesAsync();
                    return 1;
                }
                return 0;
            }
            catch { return 0; }
        }

        public int Edit(Projects table)
        {
            try
            {
                db = new DBContext();
                db.Projects.Update(table);
                db.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }

        public async Task<int> EditAsync(Projects table)
        {
            try
            {
                db = new DBContext();
                db.Projects.Update(table);
                await db.SaveChangesAsync();
                return 1;
            }
            catch { return 0; }
        }

        public Projects Find(int Id)
        {
            try
            {
                return db.Projects.FirstOrDefault(x => x.Id == Id);
            }
            catch { return null; }
        }

        public async Task<Projects> FindAsync(int Id)
        {
            try
            {
                return await db.Projects.FirstOrDefaultAsync(x => x.Id == Id);
            }
            catch { return null; }
        }

        public List<Projects> GetAllData()
        {
            try
            {
                return db.Projects.ToList();
            }
            catch { return null; }
        }

        public async Task<List<Projects>> GetAllDataAsync()
        {
            try
            {
                return await db.Projects.ToListAsync();
            }
            catch { return null; }
        }

        public List<Projects> Search(string SearchItem)
        {
            try
            {
                return db.Projects.Where(x => x.Id.ToString() == SearchItem
                    || x.Name.Contains(SearchItem)
                    || x.Company.Contains(SearchItem)
                    || x.Customer.Contains(SearchItem)
                    || x.Address.Contains(SearchItem)
                    || x.Details.Contains(SearchItem)
                    ).ToList();
            }
            catch { return null; }
        }

        public async Task<List<Projects>> SearchAsync(string SearchItem)
        {
            try
            {
                return await db.Projects.Where(x => x.Id.ToString() == SearchItem
                    || x.Name.Contains(SearchItem)
                    || x.Company.Contains(SearchItem)
                    || x.Customer.Contains(SearchItem)
                    || x.Address.Contains(SearchItem)
                    || x.Details.Contains(SearchItem)
                    ).ToListAsync();
            }
            catch { return null; }
        }
        #endregion
    }
}
