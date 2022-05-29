using Learn.BlazorServerApp.Data;
using Learn.BlazorServerApp.Models;
using Learn.BlazorServerApp.Services.Todo.Dto;

namespace Learn.BlazorServerApp.Services.Todo
{
    public class ToDoService
    {
        private readonly ITodoItemRepository _repository;

        public ToDoService(ITodoItemRepository repository)
        {
            _repository = repository;
        }
        public Task<List<TodoItem>> GetList()
        {
            return _repository.GetAll();
        }

        public void Add(CreateTodoDto item)
        {
            _repository.Insert(item);
        }

        public void Update(UpdateTodoDto item)
        {
            _repository.Update(item);
        }

        public Task<TodoItem> Get(int itemId)
        {
            return _repository.Get(itemId);
        }

        public void Delete(int itemId)
        {
            _repository.Delete(itemId);
        }
    }
}
