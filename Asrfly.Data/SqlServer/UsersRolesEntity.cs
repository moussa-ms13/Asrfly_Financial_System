using Asrfly.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Asrfly.Data.SqlServer
{
    public class UsersRolesRolesEntity : IDataHelper<UsersRoles>
    {
        private DBContext db;
        private UsersRoles table;

        public UsersRolesRolesEntity()
        {
            db = new DBContext();
        }

        #region Methods

        public int Add(UsersRoles table)
        {
            try
            {
                db.UsersRoles.Add(table);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> AddAsync(UsersRoles table)
        {
            try
            {
                await db.UsersRoles.AddAsync(table);
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
                    db.UsersRoles.Remove(table);
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
                    db.UsersRoles.Remove(table);
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

        public int Edit(UsersRoles table)
        {
            try
            {
                db = new DBContext();
                db.UsersRoles.Update(table);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> EditAsync(UsersRoles table)
        {
            try
            {
                db = new DBContext();
                db.UsersRoles.Update(table);
                await db.SaveChangesAsync();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public UsersRoles Find(int Id)
        {
            try
            {
                return db.UsersRoles.FirstOrDefault(x => x.Id == Id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<UsersRoles> FindAsync(int Id)
        {
            try
            {
                return await db.UsersRoles.FirstOrDefaultAsync(x => x.Id == Id);
            }
            catch
            {
                return null;
            }
        }

        public List<UsersRoles> GetAllData()
        {
            try
            {
                return db.UsersRoles.ToList();
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<UsersRoles>> GetAllDataAsync()
        {
            try
            {
                return await db.UsersRoles.ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public List<UsersRoles> Search(string SearchItem)
        {
            try
            {
                return db.UsersRoles.Where(x => x.Id.ToString() == SearchItem || x.Key.Contains(SearchItem)).ToList();
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<UsersRoles>> SearchAsync(string SearchItem)
        {
            try
            {
                return await db.UsersRoles.Where(x => x.Id.ToString() == SearchItem || x.Key.Contains(SearchItem)).ToListAsync();
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
