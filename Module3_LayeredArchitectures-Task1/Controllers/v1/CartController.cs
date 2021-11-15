using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module3_LayeredArchitectures_Task1.BusinessLogic;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Module3_LayeredArchitectures_Task1.Controllers
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("v{version:apiVersion}/cart/")]
    public class CartController : Controller
    {
        private readonly ICartingService _cartingService;
        public IEnumerable<Item> Items { get; set; }
        public CartController(ICartingService cartingService)
        {
            _cartingService = cartingService;
        }

        /// <summary>
        /// Get cart info object
        /// </summary>
        /// <param name="id">Cart Id</param>
        /// <returns></returns>
        [Route("{id}"), HttpGet]
        public IActionResult CartInfo(string id)
        {
            var obj = JsonConvert.SerializeObject(_cartingService.GetCartInfo(new ObjectId(id)));

            return Ok(obj);
        }

        /// <summary>
        /// Add item to the cart
        /// </summary>
        /// <param name="id">Cart Id</param>
        /// <param name="item">Item object</param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created cart with item inside</response>
        /// <response code="200">If the item is added</response>
        [Route("{id}/item"), HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AddItem([FromRoute]string id, [FromBody] Item item)
        {
            if (_cartingService.GetCartInfo(new ObjectId(id)) != null)
            {
                _cartingService.AddItem(new ObjectId(id), item);
                return Ok();
            }

            var cartCreated = _cartingService.Create(new[] { item });
            return Created($"v1/cart/{cartCreated.Id}", cartCreated);
        }

        /// <summary>
        /// Remove item from the cart
        /// </summary>
        /// <param name="id">Cart Id</param>
        /// <param name="itemId">Item Id</param>
        /// <returns></returns>
        /// <response code="200">If the item is deleted</response>
        /// <response code="404">If cart with id was not found</response>
        [Route("{id}/item/{itemId}"), HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Delete([FromRoute]string id, [FromRoute]string itemId)
        {
            if (_cartingService.GetCartInfo(new ObjectId(id)) != null)
            {
                _cartingService.RemoveItem(new ObjectId(id), new ObjectId(itemId));

                return Ok();
            }
            return NotFound();
        }
    }
}
