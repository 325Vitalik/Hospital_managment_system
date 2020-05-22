using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Data.Repository
{
    public class PostRepository : IPostRepository
    {
        private HospitalDbContext ctx;

        public PostRepository(HospitalDbContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await ctx.SaveChangesAsync() > 0) return true;
            return false;
        }

        public async Task<Post> GetPostByIdAsync(long id)
        {
            return await ctx.Posts.FirstOrDefaultAsync(p => p.Id == id);
        }


        public IEnumerable<Post> GetAllPosts()
        {
            return ctx.Posts.ToList();
        }

        public async void AddPost(Post post)
        {
            await this.ctx.Posts.AddAsync(post);
        }
    }
}
