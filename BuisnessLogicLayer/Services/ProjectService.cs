using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Validation;

namespace BuisnessLogicLayer.Services
{
    /// <summary>
    ///   Implements a projectService
    /// </summary>
    public class ProjectService : IProjectService
    {
        readonly IUnitOfWork unitOfWork;

        readonly IMapper mapper;

        readonly IEmailService emailService;

        /// <summary>Initializes a new instance of the <see cref="ProjectService" /> class.</summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="mapper">The mapper.</param>
        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.emailService = emailService;
        }

        /// <summary>Adds the project model asynchronous.</summary>
        /// <param name="model">The model.</param>
        public async Task AddAsync(ProjectModel model)
        {
            ValidateProject(model);

            var project = mapper.Map<Project>(model);

            await unitOfWork.ProjectRepository.AddAsync(project);

            await unitOfWork.SaveAsync();
        }

        /// <summary>Adds the status asynchronous.</summary>
        /// <param name="model">The statusModel.</param>
        public async Task AddStatusAsync(StatusModel model)
        {
            ValidateStatus(model);

            var status = mapper.Map<ProjectStatus>(model);

            await unitOfWork.ProjectStatusRepository.AddAsync(status);

            await unitOfWork.SaveAsync();
        }

        /// <summary>Deletes the project model by id asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteAsync(int id)
        {
            var users = await GetAllUsersByProjectIdAsync(id);

            var project = await GetByIdAsync(id);

            await unitOfWork.ProjectRepository.DeleteByIdAsync(id);

            await unitOfWork.SaveAsync();

            if (users.Count() > 0)
            {
                var message = new MessageModel(users.Select(t => t.Email),
                $"Project {project.Name} has been deleted.",
                $"Hello! We have noticed that the project {project.Name} has been deleted!\n\n" +
                $" We hope it was comfortable to work with us!\n" +
                $"Thanks, The Task tracking system team.");

                await emailService.SendEmailAsync(message);
            }
        }

        /// <summary>Deletes the status asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteStatusAsync(int id)
        {
            await unitOfWork.ProjectStatusRepository.DeleteByIdAsync(id);

            await unitOfWork.SaveAsync();
        }

        /// <summary>Gets all models asynchronous.</summary>
        /// <returns>ProjectModels</returns>
        public async Task<IEnumerable<ProjectModel>> GetAllAsync()
        {
            var list = await unitOfWork.ProjectRepository.GetAllWithDetailsAsync();

            return mapper.Map<IEnumerable<ProjectModel>>(list);
        }

        /// <summary>Gets all statuses asynchronous.</summary>
        /// <returns>StatusModels</returns>
        public async Task<IEnumerable<StatusModel>> GetAllStatusesAsync()
        {
            var list = await unitOfWork.ProjectStatusRepository.GetAllAsync();

            return mapper.Map<IEnumerable<StatusModel>>(list);
        }

        /// <summary>Gets the model by identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ProjectModel</returns>
        public async Task<ProjectModel> GetByIdAsync(int id)
        {
            var model = await unitOfWork.ProjectRepository.GetByIdWithDetailsAsync(id);

            return mapper.Map<ProjectModel>(model);
        }

        /// <summary>Gets the status by identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Status</returns>
        public async Task<StatusModel> GetStatusByIdAsync(int id)
        {
            var model = await unitOfWork.ProjectStatusRepository.GetByIdAsync(id);

            return mapper.Map<StatusModel>(model);
        }

        /// <summary>Updates the model asynchronous.</summary>
        /// <param name="model">The model.</param>
        public async Task UpdateAsync(ProjectModel model)
        {
            ValidateProject(model);

            var project = mapper.Map<Project>(model);

            unitOfWork.ProjectRepository.Update(project);

            await unitOfWork.SaveAsync();

            var users = await GetAllUsersByProjectIdAsync(model.Id);

            if (users.Count() > 0)
            {
                var message = new MessageModel(users.Select(t => t.Email),
                    $"Project {model.Name} has been modified!.",
                    $"Hello! We are happy to announce that the project {model.Name} has been modified!\n\n" +
                    $"Please, check what's new! This will help you to become better!\n" +
                    $"Small win today--big achievement tomorrow!\n" +
                    $"Thanks,The Task tracking system team.");

                await emailService.SendEmailAsync(message);
            }
        }

        /// <summary>Updates the status asynchronous.</summary>
        /// <param name="model">The statusModel.</param>
        public async Task UpdateStatusAsync(StatusModel model)
        {
            ValidateStatus(model);

            var status = mapper.Map<ProjectStatus>(model);

            unitOfWork.ProjectStatusRepository.Update(status);

            await unitOfWork.SaveAsync();
        }

        /// <summary>Gets all tasks by project identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>TaskModels</returns>
        public async Task<IEnumerable<TaskModel>> GetAllTasksByProjectIdAsync(int id)
        {
            var list = (await unitOfWork.AssignmentRepository.GetAllWithDetailsAsync())
                .Where(t => t.ProjectId == id);

            return mapper.Map<IEnumerable<TaskModel>>(list);
        }

        /// <summary>Gets all users by project identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>UserModels</returns>
        public async Task<IEnumerable<UserModel>> GetAllUsersByProjectIdAsync(int id)
        {
            var list = (await unitOfWork.ProjectRepository.GetByIdWithDetailsAsync(id))
                .Tasks
                .SelectMany(t => t.UserProjects.Select(t => t.User))
                .GroupBy(t => t.Id)
                .Select(t => t.First());

            return mapper.Map<IEnumerable<UserModel>>(list);
        }

        static void ValidateProject(ProjectModel model)
        {
            if (model is null)
                throw new TaskTrackingException("Model is null");

            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Description))
                throw new TaskTrackingException("Name or description is null or empty");

            if (model.StartDate == DateTime.MinValue)
                throw new TaskTrackingException("Date is incorrect");

            if (model.StartDate.Subtract(model.ExpiryDate).Days >= 0)
                throw new TaskTrackingException("Date is incorrect");
        }

        static void ValidateStatus(StatusModel model)
        {
            if (model is null)
                throw new TaskTrackingException("Model is null");

            if (string.IsNullOrEmpty(model.Name))
                throw new TaskTrackingException("Name is null or empty");
        }
    }
}
