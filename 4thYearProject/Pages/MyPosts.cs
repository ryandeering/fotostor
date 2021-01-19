using _4thYearProject.Server.Services;
using _4thYearProject.Shared;
using _4thYearProject.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Pages
{
    public partial class MyPosts : ComponentBase
{
    public IEnumerable<Post> Posts { get; set; }

    [Inject]
    public IPostDataService PostDataService { get; set; }

    [Inject]
    public IUserService _userService { get; set; }


    protected async override Task OnInitializedAsync()
        {
                ClaimsPrincipal identity = await _userService.GetUserAsync();
                //First get user claims    
                var claims = identity.Claims.Where(c => c.Type.Equals("sub"))
                      .Select(c => c.Value).SingleOrDefault();

            //Filter specific claim    
                String UserId = claims.ToString()[0..8];
                Posts = (await PostDataService.GetPostsByUserId(UserId)).ToList();
    }





}
}
