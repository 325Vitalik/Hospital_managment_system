using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Data.Repository
{
    public interface IPostRepository : IRepository
    {
        public void AddPost(Post post);
        public IEnumerable<Post> GetAllPosts();
        public Task<Post> GetPostByIdAsync(long id);
    }
}
