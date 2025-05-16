using Microsoft.AspNetCore.Mvc;
using SimpleRestAPI.Models;

namespace SimpleRestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimpleController : ControllerBase
    {
        #region private 
        private ILogger<SimpleController> _logger;
        private Service _service;
        #endregion


        /// <summary>
        /// SimpleController Constructor
        /// </summary>
        /// <param name="logger"></param>
        public SimpleController(ILogger<SimpleController> logger, Service service)
        {
            _logger = logger;
            _service = service;
        }



        /// <summary>
        /// Clears the list and resets id to 1
        /// </summary>




        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>All users</returns>
        [HttpGet("users")]
        public ActionResult<List<Item>> GetUsers()
        {
            _logger.LogInformation("User has invoked GetUsers");

            return Ok(_service.GetAllItems());
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

            return Ok(_service.Add(newItems));
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

            return Ok(_service.GetItemById(id));
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

            if (_service.Update(id, updatedItem))
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
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

            if (_service.Delete(id))
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
