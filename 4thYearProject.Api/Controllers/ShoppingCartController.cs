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

        [HttpPost]
        [Route("add/{UserId}")]
        public IActionResult AddToCart(string UserId, [FromBody] OrderLineItem ol)
        {

            return Ok(_cartRepository.AddToCart(UserId, ol));
        }

        [HttpGet]
        [Route("orders/{UserId}")]
        public IActionResult GetOrders(string UserId)
        {
            return Ok(_cartRepository.GetOrders(UserId));
        }

        [HttpDelete]
        [Route("empty/{UserId}")]
        public IActionResult EmptyBasket(string UserId)
        {
            return Ok(_cartRepository.EmptyBasket(UserId));
        }

        [HttpPut]
        [Route("remove/{UserId}")]
        public IActionResult RemoveOne(string UserId, [FromBody] OrderLineItem lineItem)
        {
            return Ok(_cartRepository.RemoveOne(UserId, lineItem.Id));
        }

        [HttpPut]
        [Route("add/incre/{UserId}")]
        public IActionResult AddOne(string UserId, [FromBody] Post post)
        {

            return Ok(_cartRepository.AddOne(UserId, post.PostId));
        }

        [HttpPost]
        public IActionResult AddCart([FromBody] ShoppingCart cart)
        {
            if (_cartRepository.GetCart(cart.UserId) != null)
            {
                ModelState.AddModelError("UserId", "Cart already exists.");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            return Ok(_cartRepository.AddCart(cart));
        }

        [HttpGet]
        [Route("{UserId}")]
        public IActionResult GetCart(string UserId)
        {

            return Ok(_cartRepository.GetCart(UserId));
        }







        //[HttpDelete("/remove/")]
        //public IActionResult RemoveOne(string UserId, OrderLineItem lineItem)
        //{
        //    return Ok(_cartRepository.RemoveOne(UserId, lineItem.Id));
        //}

    }
}
