using SimpleRestAPI.Interfaces;

namespace SimpleRestAPI.Models
{
    public class Service : IService
    {
        #region Private Vars
        ILogger<Service> _logger;
        private List<Item> items = new List<Item>();    // List to hold all items
        private int nextId = 1; // Unique Id for each item
        #endregion 

        public Service(ILogger<Service> logger) 
        { 
            _logger = logger;
        }



        /// <summary>
        /// Adds new items to the list with auto-incremented IDs.
        /// </summary>
        /// <param name="newItems"></param>
        /// <returns>True if successfull; otherwise False</returns>
        public bool Add(List<Item> newItems)
        {
            try
            {
                foreach (var item in newItems)
                {
                    item.Id = nextId++;
                    items.Add(item);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when trying to add items to the list: {ex}. {ex.Message}");
                return false;
            }
        }



        /// <summary>
        /// Deletes a specified object from the list by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if successfull; otherwise False</returns>
        public bool Delete(int id)
        {
            try
            {
                var item = items.FirstOrDefault(i => i.Id == id);
                if (item != null)
                {
                    items.Remove(item);
                    return true;
                }

                _logger.LogError("item is null.");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when trying to delete from the list: {ex}. {ex.Message}");
                return false;
            }
        }



        /// <summary>
        /// Returns a list of all the items.
        /// </summary>
        /// <returns>List of all items otherwise null</returns>
        public List<Item> GetAllItems()
        {
            return items;
        }



        /// <summary>
        /// Fetches an item from the list by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The item otherwise null</returns>
        public Item? GetItemById(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            return item;
        }



        /// <summary>
        /// Updates an object from the list by Id with the new data.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns>True if successfull otherwise false</returns>
        public bool Update(int id, Item item)
        {
            var itemToUpdate = items.FirstOrDefault(i => i.Id == id);
            if (itemToUpdate != null)
            {
                itemToUpdate.name = item.name;
                itemToUpdate.surname = item.surname;
                itemToUpdate.address = item.address;
                itemToUpdate.phone = item.phone;
                return true;
            }
            return false;
        }
    }
}
