using Asrfly.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Asrfly.Data.SqlServer
{
    public class UsersEntity : IDataHelper<Users>
    {
        private DBContext db;
        private Users table;

        public UsersEntity()
        {
            db = new DBContext();
        }

        #region Methods

        public int Add(Users table)
        {
            try
            {
                db.Users.Add(table);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> AddAsync(Users table)
        {
            try
            {
                await db.Users.AddAsync(table);
                await db.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
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
                    db.Users.Remove(table);
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
                    db.Users.Remove(table);
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

        public int Edit(Users table)
        {
            try
            {
                db = new DBContext();
                db.Users.Update(table);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> EditAsync(Users table)
        {
            try
            {
                db = new DBContext();
                db.Users.Update(table);
                await db.SaveChangesAsync();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public Users Find(int Id)
        {
            try
            {
                return db.Users.FirstOrDefault(x => x.Id == Id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<Users> FindAsync(int Id)
        {
            try
            {
                return await db.Users.FirstOrDefaultAsync(x => x.Id == Id);
            }
            catch
            {
                return null;
            }
        }

        public List<Users> GetAllData()
        {
            try
            {
                return db.Users.ToList();
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Users>> GetAllDataAsync()
        {
            try
            {
                return await db.Users.ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public List<Users> Search(string SearchItem)
        {
            try
            {
                return db.Users.Where(x => x.Id.ToString() == SearchItem
                    || x.UserName.Contains(SearchItem)
                    || x.FullName.Contains(SearchItem)
                    || x.Password.Contains(SearchItem)
                    || x.Email.Contains(SearchItem)
                    || x.Phone.Contains(SearchItem)
                    || x.AddedDate.Date.ToString().Contains(SearchItem)
                    ).ToList();
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Users>> SearchAsync(string SearchItem)
        {
            try
            {
                return await db.Users.Where(x => x.Id.ToString() == SearchItem
                    || x.UserName.Contains(SearchItem)
                    || x.FullName.Contains(SearchItem)
                    || x.Password.Contains(SearchItem)
                    || x.Email.Contains(SearchItem)
                    || x.Phone.Contains(SearchItem)
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
