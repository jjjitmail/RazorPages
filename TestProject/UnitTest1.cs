using Dto;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Internal;
using ServiceLib;
using ServiceLib.Interfaces;

namespace TestProject
{
    public class Tests
    {        
        private readonly IValidationService _iValidationService;
        private readonly IDataService _iDataService;

        public Tests()
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            services.AddSingleton<IValidationService, ValidationService>();
            services.AddSingleton<IDataService, DataService>();
            services.AddSingleton<IMemoryCache, MemoryCache>();
            var _serviceProvider = services.BuildServiceProvider();
            _iValidationService = _serviceProvider.GetService<IValidationService>();
            _iDataService = _serviceProvider.GetService<IDataService>();
        }               

        [SetUp]
        public void Setup()
        {
            
        }
                       
        [Test]
        public void TestTaskWithSameName()
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions());

            IList<Todo> list = new List<Todo>();
            list.Add(new Todo() { Name = "task1", Priority = 1, Status = Dto.Enum.StatusType.not_started });
            list.Add(new Todo() { Name = "task2", Priority = 2, Status = Dto.Enum.StatusType.not_started });
            list.Add(new Todo() { Name = "task3", Priority = 3, Status = Dto.Enum.StatusType.not_started });

            var result = _iDataService.UpdateCacheAsync(list, memoryCache);
            Assert.That(result, Is.Not.Null);

            var newTodo = new Todo() { Name = "newtask", Priority = 3, Status = Dto.Enum.StatusType.not_started };
            
            bool canBeCreated = _iValidationService.ValidateForCreateAsync(newTodo, memoryCache).Result;
            Assert.IsTrue(canBeCreated, "it can be created");

            var invalidnewTodo = new Todo() { Name = "task1", Priority = 3, Status = Dto.Enum.StatusType.not_started };
            canBeCreated = _iValidationService.ValidateForCreateAsync(invalidnewTodo, memoryCache).Result;
            Assert.IsTrue(!canBeCreated, "It cannot be created");

            var itemToUpdate = _iDataService.GetListAsync<Todo>(memoryCache).Result.First(x=> x.Name == "task1");

            itemToUpdate.Name = "task2";

            canBeCreated = _iValidationService.ValidateForUpdateAsync(itemToUpdate, memoryCache).Result;
            Assert.IsTrue(!canBeCreated, "It cannot be updated");
        }

        [Test]
        public void TestTodoTaskWithEmptyName()
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions());

            IList<Todo> list = new List<Todo>();
            list.Add(new Todo() { Name = "task1", Priority = 1, Status = Dto.Enum.StatusType.not_started });
            list.Add(new Todo() { Name = "task2", Priority = 2, Status = Dto.Enum.StatusType.not_started });
            list.Add(new Todo() { Name = "task3", Priority = 3, Status = Dto.Enum.StatusType.not_started });

            var result = _iDataService.UpdateCacheAsync(list, memoryCache);
            Assert.That(result, Is.Not.Null);

            var newTodo = new Todo() { Name = "", Priority = 3, Status = Dto.Enum.StatusType.not_started };

            bool canBeCreated = _iValidationService.ValidateForDeleteAsync(newTodo, memoryCache).Result;
            Assert.IsTrue(!canBeCreated, "it cannot be created");

            newTodo.Name = "New Task";

            canBeCreated = _iValidationService.ValidateForCreateAsync(newTodo, memoryCache).Result;
            Assert.IsTrue(canBeCreated, "It can be created");
        }

        [Test]
        public void TestDeleteIncompletedTodoTask()
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions());

            IList<Todo> list = new List<Todo>();
            list.Add(new Todo() { Name = "task1", Priority = 1, Status = Dto.Enum.StatusType.not_started });
            list.Add(new Todo() { Name = "task2", Priority = 2, Status = Dto.Enum.StatusType.not_started });
            list.Add(new Todo() { Name = "task3", Priority = 3, Status = Dto.Enum.StatusType.not_started });

            var result = _iDataService.UpdateCacheAsync(list, memoryCache);
            Assert.That(result, Is.Not.Null);

            var newTodo = _iDataService.GetListAsync<Todo>(memoryCache).Result.First();

            bool canBeDeleted = _iValidationService.ValidateForDeleteAsync(newTodo, memoryCache).Result;
            Assert.IsTrue(!canBeDeleted, "it cannot be deleted");

            newTodo.Status = Dto.Enum.StatusType.completed;

            canBeDeleted = _iValidationService.ValidateForDeleteAsync(newTodo, memoryCache).Result;
            Assert.IsTrue(canBeDeleted, "It can be deleted");
        }
    }
}