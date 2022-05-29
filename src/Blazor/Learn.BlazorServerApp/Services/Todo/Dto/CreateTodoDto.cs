using System.ComponentModel.DataAnnotations;

namespace Learn.BlazorServerApp.Services.Todo.Dto
{
    public class CreateTodoDto
    {
        [Required]
        [StringLength(50, ErrorMessage ="Title is not valid")]
        public string Title { get; set; } = String.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime? ScheduledDate { get; set; } = DateTime.Today;
        public bool IsCompleted { get; set; } = false;
    }
}
