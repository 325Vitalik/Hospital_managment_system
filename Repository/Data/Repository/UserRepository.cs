using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private HospitalDbContext ctx;

        public UserRepository(HospitalDbContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await ctx.SaveChangesAsync() > 0) return true;
            return false;
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await this.ctx.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}
