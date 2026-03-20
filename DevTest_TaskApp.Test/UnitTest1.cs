using DevTest_TaskApi.Controllers;
using DevTest_TaskApi.Data;
using DevTest_TaskApi.Models;
using DevTest_TaskApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTest_TaskApp.Test
{
    public class DevTest_TaskAppTests
    {
        private DevTest_TaskApiContext _context = null!;
        private TasksService _service = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DevTest_TaskApiContext>()
                .UseInMemoryDatabase(databaseName: "Test_Tasks_Db")
                .Options;

            _context = new DevTest_TaskApiContext(options);

            // Clear and seed
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.Tasks.AddRange(new List<Tasks>
            {
                new Tasks { Id = 1, Title = "Task 1", Description = "Desc 1", Task_Status = 0 },
                new Tasks { Id = 2, Title = "Task 2", Description = "Desc 2", Task_Status = 1 },
                new Tasks { Id = 3, Title = "Task 3", Description = "Desc 3", Task_Status = 2 }
            });
            _context.SaveChanges();

            _service = new TasksService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task GetAllTasks_ReturnsAllItems()
        {
            var result = await _service.GetAllTasksAsync();
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(3, result.Value!.Count());
        }

        [Test]
        public async Task GetTask_ById_Found()
        {
            var result = await _service.GetTasksAsync(2);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(2, result.Value!.Id);
        }

        [Test]
        public async Task GetTask_ById_NotFound()
        {
            var result = await _service.GetTasksAsync(999);
            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
        }

        [Test]
        public async Task CreateTask_AddsItem()
        {
            var newTask = new Tasks { Id = 4, Title = "New Task", Description = "New", Task_Status = 0 };
            var created = await _service.CreateTasksAsync(newTask);
            Assert.IsNotNull(created);
            Assert.AreEqual(4, created.Id);

            var fromDb = await _context.Tasks.FindAsync(4);
            Assert.IsNotNull(fromDb);
            Assert.AreEqual("New Task", fromDb!.Title);
        }

        [Test]
        public async Task UpdateTask_Succeeds_WhenIdMatches()
        {
            var task = await _context.Tasks.FindAsync(1);
            task!.Title = "Updated";

            var resp = await _service.UpdateTasksAsync(1, task);
            Assert.IsTrue(resp);

            var fromDb = await _context.Tasks.FindAsync(1);
            Assert.AreEqual("Updated", fromDb!.Title);
        }

        [Test]
        public async Task UpdateTask_Fails_WhenIdMismatch()
        {
            var task = await _context.Tasks.FindAsync(1);
            var resp = await _service.UpdateTasksAsync(999, task!);
            Assert.IsFalse(resp);
        }

        [Test]
        public async Task DeleteTask_RemovesItem_WhenExists()
        {
            var resp = await _service.DeleteTasksAsync(3);
            Assert.IsTrue(resp);

            var fromDb = await _context.Tasks.FindAsync(3);
            Assert.IsNull(fromDb);
        }

        [Test]
        public async Task DeleteTask_ReturnsFalse_WhenNotExists()
        {
            var resp = await _service.DeleteTasksAsync(999);
            Assert.IsFalse(resp);
        }

        // Controller tests using a simple fake service implementation
        private class FakeTasksService : ITasksService
        {
            private readonly List<Tasks> _items;
            public FakeTasksService(List<Tasks> items)
            {
                _items = items;
            }

            public Task<Tasks> CreateTasksAsync(Tasks tasks)
            {
                _items.Add(tasks);
                return Task.FromResult(tasks);
            }

            public Task<ActionResult<IEnumerable<Tasks>>> GetAllTasksAsync()
            {
                return Task.FromResult<ActionResult<IEnumerable<Tasks>>>(_items);
            }

            public Task<ActionResult<Tasks>> GetTasksAsync(int id)
            {
                var t = _items.FirstOrDefault(x => x.Id == id);
                return Task.FromResult<ActionResult<Tasks>>(t);
            }

            public Task<bool> UpdateTasksAsync(int id, Tasks tasks)
            {
                var existing = _items.FirstOrDefault(x => x.Id == id);
                if (existing == null) return Task.FromResult(false);
                if (id != tasks.Id) return Task.FromResult(false);
                existing.Title = tasks.Title;
                return Task.FromResult(true);
            }

            public Task<bool> DeleteTasksAsync(int id)
            {
                var existing = _items.FirstOrDefault(x => x.Id == id);
                if (existing == null) return Task.FromResult(false);
                _items.Remove(existing);
                return Task.FromResult(true);
            }
        }

        [Test]
        public async Task TasksApiController_GetTasks_ReturnsList()
        {
            var items = new List<Tasks> { new Tasks { Id = 1, Title = "A" } };
            var fake = new FakeTasksService(items);
            var controller = new TasksApiController(null!, fake);

            var result = await controller.GetTasks();
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(1, result.Value!.Count());
        }

        [Test]
        public async Task TasksApiController_GetTask_NotFound_ReturnsNotFound()
        {
            var items = new List<Tasks>();
            var fake = new FakeTasksService(items);
            var controller = new TasksApiController(null!, fake);

            var result = await controller.GetTasks(5);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task TasksApiController_GetTask_Found_ReturnsTask()
        {
            var items = new List<Tasks> { new Tasks { Id = 2, Title = "B" } };
            var fake = new FakeTasksService(items);
            var controller = new TasksApiController(null!, fake);

            var result = await controller.GetTasks(2);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(2, result.Value!.Id);
        }

        [Test]
        public async Task TasksApiController_PutTasks_ReturnsOk_OnTrue()
        {
            var items = new List<Tasks> { new Tasks { Id = 10, Title = "C" } };
            var fake = new FakeTasksService(items);
            var controller = new TasksApiController(null!, fake);

            var resp = await controller.PutTasks(10, new Tasks { Id = 10, Title = "C2" });
            Assert.IsInstanceOf<OkResult>(resp);
        }

        [Test]
        public async Task TasksApiController_PutTasks_ReturnsNoContent_OnFalse()
        {
            var items = new List<Tasks> { new Tasks { Id = 11, Title = "C" } };
            var fake = new FakeTasksService(items);
            var controller = new TasksApiController(null!, fake);

            var resp = await controller.PutTasks(999, new Tasks { Id = 11, Title = "C2" });
            Assert.IsInstanceOf<NoContentResult>(resp);
        }

        [Test]
        public async Task TasksApiController_PostTasks_ReturnsCreatedTask()
        {
            var items = new List<Tasks>();
            var fake = new FakeTasksService(items);
            var controller = new TasksApiController(null!, fake);

            var result = await controller.PostTasks(new Tasks { Id = 20, Title = "New" });
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(20, result.Value!.Id);
        }

        [Test]
        public async Task TasksApiController_DeleteTasks_ReturnsOk_WhenTrue()
        {
            var items = new List<Tasks> { new Tasks { Id = 30, Title = "Del" } };
            var fake = new FakeTasksService(items);
            var controller = new TasksApiController(null!, fake);

            var resp = await controller.DeleteTasks(30);
            Assert.IsInstanceOf<OkResult>(resp);
        }

        [Test]
        public async Task TasksApiController_DeleteTasks_ReturnsNoContent_WhenFalse()
        {
            var items = new List<Tasks>();
            var fake = new FakeTasksService(items);
            var controller = new TasksApiController(null!, fake);

            var resp = await controller.DeleteTasks(999);
            Assert.IsInstanceOf<NoContentResult>(resp);
        }
    }
}