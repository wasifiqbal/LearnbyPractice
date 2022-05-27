using Learn.BlazorServerApp.Models;

namespace Learn.BlazorServerApp.Services
{
    public class ToDoService
    {
        public Task<List<TodoItem>> GetList()
        {
            return Task.FromResult(new List<TodoItem>
            {
                new TodoItem(){ Id = 1, Title = "Breakfast", CreatedDate = DateTime.Now, IsCompleted =false },
                new TodoItem(){ Id = 2, Title = "Shoping", CreatedDate = DateTime.Now, IsCompleted =false },
                new TodoItem(){ Id = 3, Title = "Cleaning", CreatedDate = DateTime.Now, IsCompleted =false },
                new TodoItem(){ Id = 4, Title = "Car Wash", CreatedDate = DateTime.Now, IsCompleted =false },
            });
        }
    }
}
