using Learn.BlazorServerApp.Models;

namespace Learn.BlazorServerApp.Services
{
    public class ToDoService
    {
        private List<TodoItem> _TodoItems;
        public ToDoService()
        {
            _TodoItems = new List<TodoItem>
            {
                new TodoItem(){ Id = 1, Title = "Breakfast", CreatedDate = DateTime.Now, IsCompleted =false },
                new TodoItem(){ Id = 2, Title = "Shoping", CreatedDate = DateTime.Now, IsCompleted =false },
                new TodoItem(){ Id = 3, Title = "Cleaning", CreatedDate = DateTime.Now, IsCompleted =false },
                new TodoItem(){ Id = 4, Title = "Car Wash", CreatedDate = DateTime.Now, IsCompleted =false },
            };
        }
        public Task<List<TodoItem>> GetList()
        {
            return Task.FromResult(_TodoItems);
        }

        public void Add(TodoItem item)
        {
            item.Id = _TodoItems.Max(x => x.Id) + 1;
            this._TodoItems.Add(item);
        }

        public void Update(TodoItem item)
        {
            var itemRecord = _TodoItems.Where(x=>x.Id == item.Id).FirstOrDefault();
            if(itemRecord != null)
            {
            }

        }

        public TodoItem Get(int itemId)
        {
            return this._TodoItems.Where(x=>x.Id == itemId).First();
        }

        public bool Delete(int itemId)
        {
            return this._TodoItems.Remove(this.Get(itemId));
        }
    }
}
