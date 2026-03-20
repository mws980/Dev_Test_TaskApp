
using DevTest_TasksWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;

namespace DevTest_TaskApi.Controllers
{
    public class TasksController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TasksController> _logger;
        private readonly IConfiguration _config;

        public TasksController(
            ILogger<TasksController> logger,
            HttpClient httpClient,
            IConfiguration config)
        {
            _config = config;
            _logger = logger;
            _httpClient = httpClient;
            var ApiUri = _config["ApiUri"];
            _httpClient.BaseAddress = new Uri(ApiUri);
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<IEnumerable<Tasks>>("");
            return View(model);
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            string path = _config["ApiUri"] + "/" + id;
            _httpClient.BaseAddress= new Uri(path);
            var model = await _httpClient.GetFromJsonAsync<Tasks>("");
            return View(model);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Task_Status,DueAt")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                var model = await _httpClient.PostAsJsonAsync("", tasks);
                ViewBag.messagetype = "success";
                ViewBag.message = "New task added";
                return View();
            }
            else {
                ViewBag.messagetype = "danger";
                ViewBag.message = "Something went wrong, Please check your input.";
                return View();
            }
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string path = _config["ApiUri"] + "/" + id;
            _httpClient.BaseAddress = new Uri(path);
            var model = await _httpClient.GetFromJsonAsync<Tasks>("");
            return View(model);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Task_Status,DueAt")] Tasks tasks)
        {
            if (id != tasks.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                string path = _config["ApiUri"] + "/" + id;
                _httpClient.BaseAddress = new Uri(path);
                var resp = await _httpClient.PutAsJsonAsync("", tasks);
                ViewBag.messagetype = "success";
                ViewBag.message = "Task updated.";
            }
            else
            {
                ViewBag.messagetype = "danger";
                ViewBag.message = "Something went wrong, Please check your input.";
            }
            return View(tasks);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string path = _config["ApiUri"] + "/" + id;
            _httpClient.BaseAddress = new Uri(path);
            var model = await _httpClient.GetFromJsonAsync<Tasks>("");
            return View(model);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string path = _config["ApiUri"] + "/" + id;
            _httpClient.BaseAddress = new Uri(path);
            var model = await _httpClient.DeleteAsync("");
            return RedirectToAction(nameof(Index));
        }
    }
}
