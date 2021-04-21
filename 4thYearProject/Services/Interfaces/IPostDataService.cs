﻿using System.Collections.Generic;
using System.Threading.Tasks;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Server.Services
{
    public interface IPostDataService
    {
        Task<Post> GetPostDetails(int postId);
        Task<Post> AddPost(Post post);
        Task<IEnumerable<Post>> GetAllPostsbyFollowing(string id);
        Task UpdatePost(Post post);
        Task DeletePost(int postId);
        Task<IEnumerable<Post>> GetPostsByUserId(string id);
    }
}