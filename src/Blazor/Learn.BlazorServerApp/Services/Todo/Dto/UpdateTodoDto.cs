using System.ComponentModel.DataAnnotations;

namespace Learn.BlazorServerApp.Services.Todo.Dto
{
    public class UpdateTodoDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Title is not valid")]
        public string Title { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public DateTime? CompletedDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
