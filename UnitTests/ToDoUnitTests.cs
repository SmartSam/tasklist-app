using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Api.Controllers;
using ToDoList.Api.Data;
using ToDoList.Shared.Models;

namespace UnitTests
{
    public class Tests
    {
        private ToDoItemController _controller;
        private Mock<IToDoRepository> _repositoryMock;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IToDoRepository>();
            _controller = new ToDoItemController(_repositoryMock.Object);
        }


        [Test]
        public void GetItems_Success()
        {
            //arrange
            var taskItem = Mock.Of<ToDoItem>(x => x.Id == 1 &&
            x.Status == "ToDo" && x.Title == "Exercise");
            var taskItem2 = Mock.Of<ToDoItem>(x => x.Id == 2 &&
           x.Status == "Completed" && x.Title == "Sleep");
            var taskItem3 = Mock.Of<ToDoItem>(x => x.Id == 3 &&
           x.Status == "Started" && x.Title == "Setup meeting");
            var taskItems = new List<ToDoItem>();
            taskItems.Add(taskItem);
            taskItems.Add(taskItem2);
            taskItems.Add(taskItem2);
            taskItems.Add(taskItem3);

            _repositoryMock.Setup(x => x.GetAll()).Returns(taskItems.AsQueryable);

            //act
            var result = _controller.GetToDoItems();
            var okResult = result as ObjectResult;

            
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual(taskItems, okResult.Value);
        }

        [Test]
        public async Task GetItem_Success()
        {
            //arrange
            var id = 1;
            var taskItem = Mock.Of<ToDoItem>(x => x.Id == id &&
            x.Status == "ToDo" && x.Title == "Exercise");

            _repositoryMock.Setup(x => x.GetItem(It.IsAny<long>())).ReturnsAsync(taskItem);

            //act
            var result = await _controller.GetItem(id);

            //assert
           var okResult = result as ObjectResult;
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual(taskItem, okResult.Value);
        }

        [Test]
        public async Task CreateItem_Success()
        {
            //arrange
            var taskItem = Mock.Of<ToDoItem>(x => x.Id == 1 && x.Status == "To Do" && x.Title == "Exercise");

            _repositoryMock.Setup(x => x.AddAsync(It.IsAny<ToDoItem>())).ReturnsAsync(taskItem);

            //act
            var result = await _controller.CreateItem(taskItem);
            //assert
            var okResult = result as ObjectResult;


            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual(taskItem, okResult.Value);

        }

        [Test]
        public async Task UpdateItem_Success()
        {
            //arrange
            var taskItem = Mock.Of<ToDoItem>(x => x.Id == 1 && x.Status == "Complete" && x.Title == "Exercise");

            _repositoryMock.Setup(x => x.AddAsync(It.IsAny<ToDoItem>())).ReturnsAsync(taskItem);

            //act
            var result = await _controller.CreateItem(taskItem);
            //assert
            var okResult = result as ObjectResult;


            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual(taskItem, okResult.Value);
        }
    }
}