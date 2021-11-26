using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module3_LayeredArchitectures_Task1.BusinessLogic;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Module3_LayeredArchitectures_Task1.Controllers
{
    /// <summary>
    /// V2
    /// </summary>
    [ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    [Route("v{version:apiVersion}/cart/")]
    public class V2CartController : Controller
    {
        private readonly ICartingService _cartingService;
        public IEnumerable<Item> Items { get; set; }
        public V2CartController(ICartingService cartingService)
        {
            _cartingService = cartingService;
        }

        /// <summary>
        /// v2/ Get cart info object
        /// </summary>
        /// <param name="id">Cart Id</param>
        /// <returns></returns>
        [Route("{id}"), HttpGet]
        public async Task<IActionResult> CartInfoAsync(string id)
        {
            var obj = await _cartingService.GetItemListAsync(new ObjectId(id));

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
        public async Task<IActionResult> AddItemAsync([FromRoute]string id, [FromBody] Item item)
        {
            if (await _cartingService.GetCartInfo(new ObjectId(id)) != null)
            {
                await _cartingService.AddItemAsync(new ObjectId(id), item);
                return Ok();
            }

            var cartCreated = await _cartingService.CreateAsync(new[] { item });
            return Created($"v2/cart/{cartCreated.Id}", cartCreated);
        }

        /// <summary>
        /// Remove item from the cart
        /// </summary>
        /// <param name="id">Cart Id</param>
        /// <param name="itemId">Item Id</param>
        /// <returns></returns>
        /// <response code="200">If the item is deleted</response>
        /// <response code="404">If cart with id was not found</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("{id}/item/{itemId}"), HttpDelete]
        public async Task<IActionResult> Delete(string id, string itemId)
        {
            if (await _cartingService.GetCartInfo(new ObjectId(id)) != null)
            {
                await _cartingService.RemoveItemAsync(new ObjectId(id), new ObjectId(itemId));

                return Ok();
            }
            return NotFound();
        }
    }
}
