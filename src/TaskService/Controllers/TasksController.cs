using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskService.Services;

namespace TaskService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> _logger;
        private readonly ITaskService _taskService;

        public TasksController(ILogger<TasksController> logger, ITaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        [HttpGet]
        public IEnumerable<TaskItem> Get()
        {
            return _taskService.GetAll();
        }

        [HttpGet("{id}")]
        public TaskItem GetById(int id)
        {
            return _taskService.GetById(id);
        }

        [HttpPost]
        public TaskItem AddOrUpdate([FromBody]TaskItem taskItem)
        {
            return _taskService.AddOrUpdate(taskItem);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _taskService.Delete(id);

            return Ok();
        }
    }
}
