using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using SimpleRestAPI.Controllers;
using SimpleRestAPI.Models;

namespace SimpleTest
{
    [TestClass]
    public sealed class SimpleRestAPITests
    {
        [TestMethod, Description("Ensures that GetUsers() returns a 200 StatusCode as well as the resulting List")]
        public void Get_All_Users_As_List_Of_Items()
        {
            //Arrange
            var service = new Service(new NullLogger<Service>());
            var controller = new SimpleController(new NullLogger<SimpleController>(), service);
            controller.PostUser(new List<Item>
            {
                new Item {name = "testName", surname = "testSurname", address = "testAddress", phone = "testPhone"}
            });

            //Act
            var result = controller.GetUsers();
            var okResult = result.Result as OkObjectResult;
            var users = okResult.Value as List<Item>;

            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(users);
            Assert.AreEqual(1, users.Count);
            Assert.AreEqual("testName", users[0].name);
            Assert.AreEqual("testSurname", users[0].surname);
            Assert.AreEqual("testAddress", users[0].address);
            Assert.AreEqual("testPhone", users[0].phone);
        }



        [TestMethod, Description("Ensures that PostUsers() adds items in the List")]
        public void Post_Users()
        {
            //Arrange
            var service = new Service(new NullLogger<Service>());
            var controller = new SimpleController(new NullLogger<SimpleController>(), service);
            List<Item> items = new List<Item>
            {
                new Item { name = "test1", surname = "test1", address = "test1", phone = "test1"},
                new Item { name = "test2", surname = "test2", address = "test2", phone = "test2"},
                new Item { name = "test3", surname = "test3", address = "test3", phone = "test3"},
                new Item { name = "test4", surname = "test4", address = "test4", phone = "test4"},
            };

            //Act
            controller.PostUser(items);
            var result = controller.GetUsers();
            var okResult = result.Result as OkObjectResult;
            var users = okResult.Value as List<Item>;

            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(users);
            Assert.AreEqual(4, users.Count);
            Assert.AreEqual("test1", users[0].name);
            Assert.AreEqual("test1", users[0].surname);
            Assert.AreEqual("test1", users[0].address);
            Assert.AreEqual("test1", users[0].phone);
        }



        [TestMethod, Description("Ensures that GetUserById correctly fetches the correct user by the given Id input")]
        public void Get_Specific_User_By_Id()
        {
            //Arrange
            var service = new Service(new NullLogger<Service>());
            var controller = new SimpleController(new NullLogger<SimpleController>(), service);
            controller.PostUser(new List<Item>
            {
                new Item { name = "test1", surname = "test1", address = "test1", phone = "test1"},
                new Item { name = "test2", surname = "test2", address = "test2", phone = "test2"},
                new Item { name = "test3", surname = "test3", address = "test3", phone = "test3"},
                new Item { name = "test4", surname = "test4", address = "test4", phone = "test4"},
            });

            //Act
            var result1 = controller.GetUserById(1);
            var result2 = controller.GetUserById(2);
            var result3 = controller.GetUserById(3);
            var result4 = controller.GetUserById(4);
            var okResult1 = result1.Result as OkObjectResult;
            var okResult2 = result2.Result as OkObjectResult;
            var okResult3 = result3.Result as OkObjectResult;
            var okResult4 = result4.Result as OkObjectResult;
            var user1 = okResult1.Value as Item;
            var user2 = okResult2.Value as Item;
            var user3 = okResult3.Value as Item;
            var user4 = okResult4.Value as Item;



            //Assert
            Assert.IsNotNull(okResult1);
            Assert.IsNotNull(okResult2);
            Assert.IsNotNull(okResult3);
            Assert.IsNotNull(okResult4);

            Assert.AreEqual(200, okResult1.StatusCode);
            Assert.AreEqual(200, okResult2.StatusCode);
            Assert.AreEqual(200, okResult3.StatusCode);
            Assert.AreEqual(200, okResult4.StatusCode);

            Assert.IsNotNull(user1);
            Assert.IsNotNull(user2);
            Assert.IsNotNull(user3);
            Assert.IsNotNull(user4);

            Assert.IsInstanceOfType(user1, typeof(Item));
            Assert.IsInstanceOfType(user2, typeof(Item));
            Assert.IsInstanceOfType(user3, typeof(Item));
            Assert.IsInstanceOfType(user4, typeof(Item));


            Assert.AreEqual("test1", user1.name);
            Assert.AreEqual("test2", user2.name);
            Assert.AreEqual("test3", user3.name);
            Assert.AreEqual("test4", user4.name);
        }



        [TestMethod, Description("Ensures that UpdateUser correctly updates the user by their corresponding Id")]
        public void Update_User()
        {
            //Arrange
            var service = new Service(new NullLogger<Service>());
            var controller = new SimpleController(new NullLogger<SimpleController>(), service);
            controller.PostUser(new List<Item>
            {
                new Item {name = "testUpdate", surname = "testSurname", address = "testAddress", phone = "testPhone"}
            });

            //Act
            var updatedItem = new Item
            {
                name = "updatedName", surname = "updatedSurname", address = "updatedAddress", phone = "updatedPhone"
            };
            var updateResult = controller.UpdateUser(1, updatedItem);
            var updateResultStatusCode = updateResult as NoContentResult;
            var result = controller.GetUserById(1);
            var okResult = result.Result as OkObjectResult;
            var updatedUser = okResult.Value as Item;

            //Assert
            Assert.IsNotNull(updateResultStatusCode);
            Assert.AreEqual(204, updateResultStatusCode.StatusCode);

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            Assert.IsNotNull(updatedUser);
            Assert.IsInstanceOfType(updatedUser, typeof(Item));
            Assert.AreEqual("updatedName", updatedUser.name);
            Assert.AreEqual("updatedSurname", updatedUser.surname);
            Assert.AreEqual("updatedAddress", updatedUser.address);
            Assert.AreEqual("updatedPhone", updatedUser.phone);
        }



        [TestMethod, Description("Ensures that DeleteUser correctly removes a user from the list by their corresponding Id")]
        public void Delete_User()
        {
            //Arrange
            var service = new Service(new NullLogger<Service>());
            var controller = new SimpleController(new NullLogger<SimpleController>(), service);
            controller.PostUser(new List<Item>
            {
                new Item {name = "user1Name", surname = "user1Surname", address = "user1Address", phone = "user1Phone"},
                new Item {name = "user2Name", surname = "user2Surname", address = "user2Address", phone = "user3Phone"},
            });

            //Act
            var result = controller.DeleteUser(1);
            var resultCode = result as NoContentResult;
            var usersResult = controller.GetUsers();
            var users = usersResult.Result as OkObjectResult;
            var usersList = users.Value as List<Item>;

            //Assert
            Assert.IsNotNull(resultCode);
            Assert.AreEqual(204, resultCode.StatusCode);

            Assert.IsNotNull(usersList);
            Assert.AreEqual(1, usersList.Count);
        }
    }
}
