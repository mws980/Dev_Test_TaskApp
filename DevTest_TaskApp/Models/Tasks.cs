namespace DevTest_TaskApi.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int Task_Status { get; set; }
        public DateTime DueAt { get; set; }
    }
}
