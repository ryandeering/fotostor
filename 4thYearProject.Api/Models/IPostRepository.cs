using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Api.Models
{
public interface IPostRepository
{
        IEnumerable<Post> GetAllPosts();
        Post GetPostById(int postId);
        Post AddPost(Post post);
        Post UpdatePost(Post post);
        void DeletePost(int postId);
    }
}
