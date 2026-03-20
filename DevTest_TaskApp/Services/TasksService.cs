using DevTest_TaskApi.Data;
using DevTest_TaskApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevTest_TaskApi.Services
{
    public class TasksService : ITasksService
    {
        private readonly DevTest_TaskApiContext _context;

        public TasksService(DevTest_TaskApiContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteTasksAsync(int id)
        {
            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return false;
            }

            _context.Tasks.Remove(tasks);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ActionResult<IEnumerable<Tasks>>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }
        public async Task<ActionResult<Tasks>> GetTasksAsync(int id)
        {

            var tasks = await _context.Tasks.FindAsync(id);

            if (tasks == null)
            {
                return tasks;
            }

            return tasks;
        }

        public async Task<bool> UpdateTasksAsync(int id,Tasks tasks)        
        {
            if (id != tasks.Id)
            {
                return false;
            }

            _context.Entry(tasks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasksExists(id))
                {
                    return false; // NotFound();
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<Tasks> CreateTasksAsync(Tasks tasks) { 
            _context.Tasks.Add(tasks);
            await _context.SaveChangesAsync();
            return tasks;
        }
        private bool TasksExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
