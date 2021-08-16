using System.Linq;
using System.Threading.Tasks;
using _4thYearProject.Api.Models;
using _4thYearProject.Shared;
using _4thYearProject.Shared.Models.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace _4thYearProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartRepository _cartRepository;

        private readonly IUserService _userService;


        public ShoppingCartController(IShoppingCartRepository cartRepository,
            IUserService userService)
        {
            _cartRepository = cartRepository;
            _userService = userService;
        }

        [HttpGet]
        [Route("analysis/{ArtistId}")]
        public async Task<IActionResult> GetOrderLinesForArtist(string ArtistId)
        {
            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != ArtistId)
                return Unauthorized();

            return Ok(_cartRepository.GetOrderLinesForArtist(ArtistId));
        }


        [HttpPost]
        [Route("add/{UserId}")]
        public async Task<IActionResult> AddToCart(string UserId, [FromBody] OrderLineItem ol)
        {
            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != UserId)
                return Unauthorized();

            return Ok(_cartRepository.AddToCart(UserId, ol));
        }

        [HttpGet]
        [Route("orders/{UserId}")]
        public async Task<IActionResult> GetOrders(string UserId)
        {
            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != UserId)
                return Unauthorized();

            return Ok(_cartRepository.GetOrders(UserId));
        }

        [HttpGet]
        [Route("orders/spec/{OrderId}")]
        public async Task<IActionResult> GetOrderById(int OrderId)
        {
            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();


            var Order = _cartRepository.GetOrderById(OrderId);

            if (LoggedInID != Order.UserId)
                return Unauthorized();

            return Ok(Order);
        }

        [HttpDelete]
        [Route("empty/{UserId}")]
        public async Task<IActionResult> EmptyBasket(string UserId)
        {
            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != UserId)
                return Unauthorized();

            return Ok(_cartRepository.EmptyBasket(UserId));
        }

        [HttpPut]
        [Route("remove/{UserId}")]
        public async Task<IActionResult> RemoveOne(string UserId, [FromBody] OrderLineItem lineItem)
        {
            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != UserId)
                return Unauthorized();

            return Ok(_cartRepository.RemoveOne(UserId, lineItem.Id));
        }

        [HttpPut]
        [Route("add/incre/{UserId}")]
        public async Task<IActionResult> AddOne(string UserId, [FromBody] OrderLineItem lineItem)
        {
            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != UserId)
                return Unauthorized();

            return Ok(_cartRepository.AddOne(UserId, lineItem.Id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCart([FromBody] ShoppingCart cart)
        {
            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != cart.UserId)
                return Unauthorized();

            if (_cartRepository.GetCart(cart.UserId) != null)
                ModelState.AddModelError("UserId", "Cart already exists.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            return Ok(_cartRepository.AddCart(cart));
        }

        [HttpGet]
        [Route("{UserId}")]
        public async Task<IActionResult> GetCart(string UserId)
        {
            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != UserId)
                return Unauthorized();

            return Ok(_cartRepository.GetCart(UserId));
        }
    }
}