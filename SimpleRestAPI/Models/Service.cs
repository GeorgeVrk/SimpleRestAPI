using SimpleRestAPI.Interfaces;

namespace SimpleRestAPI.Models
{
    public class Service : IService
    {
        private List<Item> items = new List<Item>();
        private int nextId = 1;

        public bool Add(List<Item> newItems)
        {
            foreach (var item in newItems)
            {
                item.Id = nextId++;
                items.Add(item);
            }
            return true;
        }

        public bool Delete(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                items.Remove(item);
                return true;
            }
            return false;
        }

        public List<Item> GetAllItems()
        {
            return items;
        }

        public Item? GetItemById(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            return item;
        }

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
