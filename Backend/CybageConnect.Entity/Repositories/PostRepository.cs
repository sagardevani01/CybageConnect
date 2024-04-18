using CybageConnect.Entity.Models;
using CybageConnect.Entity.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Entity.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly CybageConnectDbContext _context;
        public PostRepository(CybageConnectDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>?> GetAllPosts()
        {
            try
            {
                return await _context.Posts.Include(c=>c.Category).Include(u=>u.User).ToListAsync();
            }
            catch (Exception e)
            {
                Console.Write("Error While getting all posts"+e.Message);
                return null;
            }
        }

        public async Task<int> CreatePost(Post post)
        {
            try
            {
                await _context.Posts.AddAsync(post);
                int result = await _context.SaveChangesAsync();
                return result > 0 ? post.Id : 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while adding post"+ex.Message);
                return 0;
            }
        }

        public async Task<bool> DeletePost(int id)
        {
            try
            {
                var post = await _context.Posts.FindAsync(id);
                if (post == null)
                {
                    return false;
                }
                else
                {
                    _context.Posts.Remove(post);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }catch (Exception ex)
            {
                return false;
            }
        }

        

        public async Task<Post?> GetPostById(int id)
        {
            try
            {
                if (id > 0)
                {
                    Post? post = await _context.Posts.Include(c => c.Category).Include(u => u.User).FirstOrDefaultAsync(p => p.Id == id);
                if(post is not null)
                    {
                        return post;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("An error occurred while fetching Post by id\n" + ex.Message);
            }
            return null;
        }

        public async Task<int?> UpdatePost(int id, Post post)
        {
            try
            {
                _context.Entry(post).State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error while editing the post"+e.Message);    
                return 0;
            }
        }
    }
}
