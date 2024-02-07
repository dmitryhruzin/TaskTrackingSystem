using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    ///   ProjectsController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        readonly IProjectService projectService;

        /// <summary>Initializes a new instance of the <see cref="ProjectsController" /> class.</summary>
        /// <param name="projectService">The project service.</param>
        public ProjectsController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        /// <summary>Gets the projects.</summary>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpGet]
        [Authorize(Roles = "User, Manager")]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> GetProjects()
        {
            return Ok(await projectService.GetAllAsync());
        }

        /// <summary>Gets the project by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   OkObjectResult or NotFoundObjectResult
        /// </returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "User, Manager")]
        public async Task<ActionResult<ProjectModel>> GetProjectById(int id)
        {
            var model = await projectService.GetByIdAsync(id);

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
            return Ok(await projectService.GetAllStatusesAsync());
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
            var model = await projectService.GetStatusByIdAsync(id);

            if (model is null)
                return NotFound();

            return Ok(model);
        }

        /// <summary>Gets the tasks by project identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpGet("{id}/tasks")]
        [Authorize(Roles = "User, Manager")]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasksByProjectId(int id)
        {
            return Ok(await projectService.GetAllTasksByProjectIdAsync(id));
        }

        /// <summary>Gets the users by project identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpGet("{id}/users")]
        [Authorize(Roles = "User, Manager")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsersByProjectId(int id)
        {
            return Ok(await projectService.GetAllUsersByProjectIdAsync(id));
        }

        /// <summary>Adds the project.</summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> AddProject([FromBody] ProjectModel model)
        {
            await projectService.AddAsync(model);

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
            await projectService.AddStatusAsync(model);

            return Ok();
        }

        /// <summary>Updates the project.</summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpPut]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> UpdateProject([FromBody] ProjectModel model)
        {
            await projectService.UpdateAsync(model);

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
            await projectService.UpdateStatusAsync(model);

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
            await projectService.DeleteAsync(id);

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
            await projectService.DeleteStatusAsync(id);

            return Ok();
        }
    }
}
