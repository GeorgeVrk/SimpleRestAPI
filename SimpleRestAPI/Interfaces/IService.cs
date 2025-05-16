using SimpleRestAPI.Models;

namespace SimpleRestAPI.Interfaces
{
    public interface IService
    {
        List<Item> GetAllItems();
        Item? GetItemById(int id);
        bool Add(List<Item> items);
        bool Delete(int id);
        bool Update(int id, Item item);

    }
}
