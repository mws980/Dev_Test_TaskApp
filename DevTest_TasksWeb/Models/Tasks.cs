using System.ComponentModel.DataAnnotations;

namespace DevTest_TasksWeb.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public Task_Status Task_Status { get; set; }
        [Required]
        public DateTime DueAt { get; set; }
    }
    public enum Task_Status
    {
        NotStarted = 0,
        InProgress = 1,
        Completed = 2
    }
}
