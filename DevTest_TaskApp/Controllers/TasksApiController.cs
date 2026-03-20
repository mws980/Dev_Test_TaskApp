using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevTest_TaskApi.Data;
using DevTest_TaskApi.Models;
using DevTest_TaskApi.Services;

namespace DevTest_TaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksApiController : ControllerBase
    {
        private readonly ILogger<TasksApiController> _logger;
        private readonly ITasksService _tasksService;
       

        public TasksApiController(
            ILogger<TasksApiController> logger,
            ITasksService tasksService)
        {
            _logger = logger;
            _tasksService = tasksService;
        }

        // GET: api/TasksApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetTasks()
        {
            return await _tasksService.GetAllTasksAsync(); 
        }

        // GET: api/TasksApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tasks>> GetTasks(int id)
        {
            var tasks = await _tasksService.GetTasksAsync(id);

            if (tasks == null)
            {
                return NotFound();
            }

            return tasks;
        }

        // PUT: api/TasksApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTasks(int id, Tasks tasks)
        {
            var resp = await _tasksService.UpdateTasksAsync(id, tasks);
            if ((resp))
            {
                return Ok();
            }
            else
            {
                return NoContent();
            }
        }

        // POST: api/TasksApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tasks>> PostTasks(Tasks tasks)
        {
            return await _tasksService.CreateTasksAsync(tasks);

        }

        // DELETE: api/TasksApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasks(int id)
        {
            var resp = await _tasksService.DeleteTasksAsync(id);
            if ((resp))
            {
                return Ok();
            }
            else
            {
                return NoContent();
            }
        }

    }
}
