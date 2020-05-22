using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Data.Repository
{
    public interface IUserRepository : IRepository
    {
        public Task<User> GetUserByUserNameAsync(string userName);
    }
}
