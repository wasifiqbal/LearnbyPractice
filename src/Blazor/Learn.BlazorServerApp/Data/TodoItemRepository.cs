using Learn.BlazorServerApp.Models;
using Learn.BlazorServerApp.Services.Todo.Dto;

namespace Learn.BlazorServerApp.Data
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly IDA _DA;
        public TodoItemRepository(IDA da)
        {
            _DA = da;
        }

        public Task<List<TodoItem>> GetAll()
        {
            string sql = "SELECT * FROM dbo.TodoItems";
            return _DA.LoadData<TodoItem, dynamic>(sql, new { });
        }

        public Task<TodoItem> Get(int id)
        {
            string sql = "SELECT * FROM dbo.TodoItems WHERE Id = @Id";
            return _DA.Find<TodoItem, dynamic>(sql, new { Id = id });
        }

        public Task Insert(CreateTodoDto todoItem)
        {
            string sql = @"INSERT INTO dbo.TodoItems (Title, ScheduledDate, CreatedDate, IsCompleted)
                            VALUES (@Title, @ScheduledDate, @CreatedDate, @IsCompleted)";

            return _DA.SaveData(sql, todoItem);
        }

        public Task Update(UpdateTodoDto todoItem)
        {

            string sql = @"UPDATE dbo.TodoItems 
                                SET Title = @Title, ScheduledDate = @ScheduledDate,
                                ModifiedDate = @ModifiedDate, CompletedDate = @CompletedDate,
                                IsCompleted = @IsCompleted
                                WHERE Id = @Id";

            return _DA.SaveData(sql, todoItem);
        }

        public Task Delete(int id)
        {
            string sql = @"DELETE FROM dbo.TodoItems 
                                WHERE Id = @Id";

            return _DA.Delete<dynamic>(sql, new { Id = id });
        }
    }
}
