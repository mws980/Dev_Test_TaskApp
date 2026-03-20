using DevTest_TaskApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevTest_TaskApi.Services
{
    public interface ITasksService
    {
        Task<ActionResult<IEnumerable<Tasks>>> GetAllTasksAsync();
        Task<ActionResult<Tasks>> GetTasksAsync(int id);
        Task<bool> DeleteTasksAsync(int id);
        Task<bool> UpdateTasksAsync(int id, Tasks tasks);
        Task<Tasks> CreateTasksAsync(Tasks tasks);
            
    }
}
