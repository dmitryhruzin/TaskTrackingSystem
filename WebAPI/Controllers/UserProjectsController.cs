using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    ///   UserProjectsController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserProjectsController : ControllerBase
    {
        readonly IUserProjectService userProjectService;

        /// <summary>Initializes a new instance of the <see cref="UserProjectsController" /> class.</summary>
        /// <param name="userProjectService">The user project service.</param>
        /// <param name="emailService">The email service.</param>
        public UserProjectsController(IUserProjectService userProjectService)
        {
            this.userProjectService = userProjectService;
        }

        /// <summary>Gets the user projects.</summary>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpGet]
        [Authorize(Roles = "User, Manager")]
        public async Task<ActionResult<IEnumerable<UserProjectModel>>> GetUserProjects()
        {
            return Ok(await userProjectService.GetAllAsync());
        }

        /// <summary>Gets the user project by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   OkObjectResult or NotFoundObjectResult
        /// </returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "User, Manager")]
        public async Task<ActionResult<UserProjectModel>> GetUserProjectById(int id)
        {
            var model = await userProjectService.GetByIdAsync(id);

            if (model is null)
                return NotFound();

            return Ok(model);
        }

        /// <summary>Gets the positions.</summary>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpGet("positions")]
        [Authorize(Roles = "User, Manager")]
        public async Task<ActionResult<IEnumerable<PositionModel>>> GetPositions()
        {
            return Ok(await userProjectService.GetAllPositionsAsync());
        }

        /// <summary>Gets the position.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   OkObjectResult or NotFoundObjectResult
        /// </returns>
        [HttpGet("positions/{id}")]
        [Authorize(Roles = "User, Manager")]
        public async Task<ActionResult<PositionModel>> GetPosition(int id)
        {
            var model = await userProjectService.GetPositionByIdAsync(id);

            if (model is null)
                return NotFound();

            return Ok(model);
        }

        /// <summary>Adds the user projects.</summary>
        /// <param name="models">The models.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> AddUserProjects([FromBody] IEnumerable<UserProjectModel> models)
        {
            await userProjectService.AddUserProjectsAsync(models);

            return Ok();
        }

        /// <summary>Adds the user project.</summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpPost("project")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> AddUserProject([FromBody] UserProjectModel model)
        {
            await userProjectService.AddAsync(model);

            return Ok();
        }

        /// <summary>Adds the position.</summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   OkObjectResult 
        /// </returns>
        [HttpPost("positions")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> AddPosition([FromBody] PositionModel model)
        {
            await userProjectService.AddPositionAsync(model);

            return Ok();
        }

        /// <summary>Updates the user project.</summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpPut("project")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> UpdateUserProject([FromBody] UserProjectModel model)
        {
            await userProjectService.UpdateAsync(model);

            return Ok();
        }

        /// <summary>Updates the position.</summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpPut("positions")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> UpdatePosition([FromBody] PositionModel model)
        {
            await userProjectService.UpdatePositionAsync(model);

            return Ok();
        }

        /// <summary>Deletes the user projects.</summary>
        /// <param name="ids">The ids.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpDelete]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> DeleteUserProjects([FromQuery] IEnumerable<int> ids)
        {
            await userProjectService.DeleteUserProjectsAsync(ids);

            return Ok();
        }

        /// <summary>Deletes the user project.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpDelete("project/{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> DeleteUserProject(int id)
        {
            await userProjectService.DeleteAsync(id);

            return Ok();
        }

        /// <summary>Deletes the position.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpDelete("positions/{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> DeletePosition(int id)
        {
            await userProjectService.DeletePositionAsync(id);

            return Ok();
        }
    }
}
