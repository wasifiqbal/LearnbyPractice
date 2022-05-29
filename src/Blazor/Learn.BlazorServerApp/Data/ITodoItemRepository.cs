using Learn.BlazorServerApp.Models;
using Learn.BlazorServerApp.Services.Todo.Dto;

namespace Learn.BlazorServerApp.Data
{
    public interface ITodoItemRepository
    {
        Task<TodoItem> Get(int id);
        Task<List<TodoItem>> GetAll();
        Task Insert(CreateTodoDto todoItem);
        Task Update(UpdateTodoDto todoItem);
        Task Delete(int id);
    }
}