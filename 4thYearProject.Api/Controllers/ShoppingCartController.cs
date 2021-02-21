namespace _4thYearProject.Api.Controllers
{
    using _4thYearProject.Api.Models;
    using _4thYearProject.Shared.Models;
    using _4thYearProject.Shared.Models.BusinessLogic;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartRepository _cartRepository;

        private readonly IPostRepository _postRepository;

        public ShoppingCartController(IShoppingCartRepository cartRepository, IPostRepository postRepository)
        {
            _cartRepository = cartRepository;
            _postRepository = postRepository;
        }

        [HttpPost("/add/{UserId}/{post}")]
        public IActionResult AddToCart(string UserId, Post post)
        {

            return Ok(_cartRepository.AddToCart(UserId, post));
        }

        [HttpGet("/orders/{UserId}")]
        public IActionResult GetOrders(string UserId)
        {
            return Ok(_cartRepository.GetOrders(UserId));
        }

        [HttpDelete("/empty/{UserId}")]
        public IActionResult EmptyBasket(string UserId)
        {
            return Ok(_cartRepository.EmptyBasket(UserId));
        }

        [HttpPut("/remove/{UserId}/{LineItemId}")]
        public IActionResult RemoveOne(string UserId, OrderLineItem lineItem)
        {
            return Ok(_cartRepository.RemoveOne(UserId, lineItem.Id));
        }

        [HttpPut("add/incre/{UserId}/post")]
        public IActionResult AddOne(string UserId, Post post)
        {

            return Ok(_cartRepository.AddOne(UserId, post.PostId));
        }

        //[HttpDelete("/remove/")]
        //public IActionResult RemoveOne(string UserId, OrderLineItem lineItem)
        //{
        //    return Ok(_cartRepository.RemoveOne(UserId, lineItem.Id));
        //}

    }
}
