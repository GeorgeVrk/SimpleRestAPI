using Microsoft.Extensions.Logging.Abstractions;
using SimpleRestAPI.Controllers;
using SimpleRestAPI.Models;

namespace SimpleTest
{
    [TestClass]
    public sealed class SimpleRestAPITests
    {
        SimpleController controller;

        [TestInitialize]
        public void Initialize_Controller()
        {
            SimpleController.ResetList();
            controller = new SimpleController(new NullLogger<SimpleController>);
        }

        [TestMethod]
        public void GetAllUsers_As_List_Of_Items()
        {
            //Arrange

            controller.PostUser(new List<Item>
            {
                new Item {name = "test", surname = "test", address = "address", phone = "test"}
            });

            //Act
            var result = controller.GetUsers();
            var users = result.Value;

            //Assert
            Assert.IsNotNull(users);
            Assert.AreEqual(1, users.Count);
            Assert.AreEqual("test", users[0].name);
        }

        [TestMethod]
        public void PostUsers()
        {
            //Arrange

            List<Item> items = new List<Item>
            {
                new Item { name = "test1", surname = "test1", address = "test1", phone = "test1"},
                new Item { name = "test2", surname = "test2", address = "test2", phone = "test2"},
                new Item { name = "test3", surname = "test3", address = "test3", phone = "test3"},
                new Item { name = "test4", surname = "test4", address = "test4", phone = "test4"},
            };

            //Act
            controller.PostUser(items);
            var users = controller.GetUsers();
            var result = users.Value;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count);
            Assert.IsInstanceOfType(result, typeof(List<Item>));
        }

        [TestMethod]
        public void Get_Specific_User()
        {
            //Arrange

            controller.PostUser(new List<Item>
            {
                new Item { name = "test1", surname = "test1", address = "test1", phone = "test1"},
                new Item { name = "test2", surname = "test2", address = "test2", phone = "test2"},
                new Item { name = "test3", surname = "test3", address = "test3", phone = "test3"},
                new Item { name = "test4", surname = "test4", address = "test4", phone = "test4"},
            });

            //Act
            var user1 = controller.GetUserById(1);
            var user2 = controller.GetUserById(2);
            var user3 = controller.GetUserById(3);
            var user4 = controller.GetUserById(4);
            var result1 = user1.Value;
            var result2 = user2.Value;
            var result3 = user3.Value;
            var result4 = user4.Value;


            //Assert
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result3);
            Assert.IsNotNull(result4);

            Assert.AreEqual("test1", result1.name);
            Assert.AreEqual("test2", result2.name);
            Assert.AreEqual("test3", result3.name);
            Assert.AreEqual("test4", result4.name);

            Assert.IsInstanceOfType(result1, typeof(Item));
        }
    }
}
