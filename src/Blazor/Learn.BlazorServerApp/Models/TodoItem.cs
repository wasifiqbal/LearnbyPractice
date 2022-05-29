using System.ComponentModel.DataAnnotations;

namespace Learn.BlazorServerApp.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = String.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime ScheduledDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CompletedDate { get; set; }
        
        public bool IsCompleted { get; set; } = false;
    }
}
