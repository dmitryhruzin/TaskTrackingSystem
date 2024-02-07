using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    ///   TasksController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        readonly ITaskService taskService;

        /// <summary>Initializes a new instance of the <see cref="TasksController" /> class.</summary>
        /// <param name="taskService">The task service.</param>
        public TasksController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        /// <summary>Gets the tasks.</summary>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpGet]
        [Authorize(Roles = "User, Manager")]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasks()
        {
            return Ok(await taskService.GetAllAsync());
        }

        /// <summary>Gets the task by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "User, Manager")]
        public async Task<ActionResult<TaskModel>> GetTaskById(int id)
        {
            var model = await taskService.GetByIdAsync(id);

            if (model is null)
                return NotFound();

            return Ok(model);
        }

        /// <summary>Gets the statuses.</summary>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpGet("statuses")]
        [Authorize(Roles = "User, Manager")]
        public async Task<ActionResult<IEnumerable<StatusModel>>> GetStatuses()
        {
            return Ok(await taskService.GetAllStatusesAsync());
        }

        /// <summary>Gets the status.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   OkObjectResult or NotFoundObjectResult
        /// </returns>
        [HttpGet("statuses/{id}")]
        [Authorize(Roles = "User, Manager")]
        public async Task<ActionResult<StatusModel>> GetStatus(int id)
        {
            var model = await taskService.GetStatusByIdAsync(id);

            if (model is null)
                return NotFound();

            return Ok(model);
        }

        /// <summary>Gets the users by task identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpGet("{id}/users")]
        [Authorize(Roles = "User, Manager")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsersByTaskId(int id)
        {
            return Ok(await taskService.GetAllUsersByTaskIdAsync(id));
        }

        /// <summary>Adds the task.</summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> AddTask([FromBody] TaskModel model)
        {
            await taskService.AddAsync(model);

            return Ok();
        }

        /// <summary>Adds the status.</summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpPost("statuses")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> AddStatus([FromBody] StatusModel model)
        {
            await taskService.AddStatusAsync(model);

            return Ok();
        }

        /// <summary>Updates the task.</summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpPut]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> UpdateTask([FromBody] TaskModel model)
        {
            await taskService.UpdateAsync(model);

            return Ok();
        }

        /// <summary>Updates the status.</summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpPut("statuses")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> UpdateStatus([FromBody] StatusModel model)
        {
            await taskService.UpdateStatusAsync(model);

            return Ok();
        }

        /// <summary>Updates the task status.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpPut("{id}/status")]
        [Authorize(Roles = "User, Manager")]
        public async Task<ActionResult> UpdateTaskStatus(int id, [FromBody] StatusModel model)
        {
            await taskService.UpdateTaskStatusAsync(id, model);

            return Ok();
        }

        /// <summary>Deletes the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> Delete(int id)
        {
            await taskService.DeleteAsync(id);

            return Ok();
        }

        /// <summary>Deletes the status.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpDelete("statuses/{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> DeleteStatus(int id)
        {
            await taskService.DeleteStatusAsync(id);

            return Ok();
        }
    }
}
