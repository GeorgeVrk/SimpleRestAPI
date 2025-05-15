using Microsoft.AspNetCore.Mvc;
using SimpleRestAPI.Models;

namespace SimpleRestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimpleController : ControllerBase
    {
        #region private 
        private static List<Item> items = new List<Item>();
        private static int nextId = 1;
        private ILogger<SimpleController> _logger;
        #endregion

        public SimpleController(ILogger<SimpleController> logger)
        {
            _logger = logger;
        }
        
        /// <summary>
        /// Clears the List of items and resets nextId to 1
        /// </summary>
        public static void ResetList()
        {
            items.Clear();
            nextId = 1;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>All users</returns>
        [HttpGet("users")]
        public ActionResult<List<Item>> GetUsers()
        {
            _logger.LogInformation("User has invoked GetUsers");

            return items;
        }



        /// <summary>
        /// Posts new users 
        /// </summary>
        /// <param name="newItems"></param>
        /// <returns>A 200 OK response</returns>
        [HttpPost("users")]
        public ActionResult PostUser(List<Item> newItems) 
        {
            _logger.LogInformation("User has invoked PostUser");

            try
            {
                foreach (var item in newItems)
                {
                    item.Id = nextId++;
                    items.Add(item);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thrown while adding users : {ex}. Ex : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

            _logger.LogInformation("PostUser has completed successfully");

            return Ok(items);
        }



        /// <summary>
        /// Gets a user based on their Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the user based on their Id or 404 if they dont exist</returns>
        [HttpGet("users/{id}")]
        public ActionResult<Item> GetUserById(int id)
        {
            _logger.LogInformation("User has invoked GetUserById");

            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null)
                return NotFound();

            return item;
        }



        /// <summary>
        /// Updates a users info based on their Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedItem"></param>
        /// <returns>204 if the update was successful or 404 if the user was not found</returns>
        [HttpPut("users/{id}")]
        public ActionResult UpdateUser(int id, Item updatedItem)
        {
            _logger.LogInformation("User has invoked UpdateUser");

            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null)
                return NotFound();

            item.name = updatedItem.name;
            item.surname = updatedItem.surname;
            item.phone = updatedItem.phone;
            item.address = updatedItem.address;
            return NoContent();
        }



        /// <summary>
        /// Deletes a user based on their Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>204 if the user was deleted successfully or 404 if the user was not found</returns>
        [HttpDelete("users/{id}")]
        public ActionResult DeleteUser(int id)
        {
            _logger.LogInformation("User has invoked DeleteUser");

            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null)
                return NotFound();

            items.Remove(item);
            return NoContent();
        }
    }
}
