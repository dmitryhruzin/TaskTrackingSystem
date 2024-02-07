using BuisnessLogicLayer.Models;

namespace BuisnessLogicLayer.Interfaces
{
    /// <summary>
    ///   Describes a project service
    /// </summary>
    public interface IProjectService : ICrud<ProjectModel>
    {
        /// <summary>Gets all tasks by project identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   TaskModels
        /// </returns>
        Task<IEnumerable<TaskModel>> GetAllTasksByProjectIdAsync(int id);

        /// <summary>Gets all users by project identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   UserModels
        /// </returns>
        Task<IEnumerable<UserModel>> GetAllUsersByProjectIdAsync(int id);

        /// <summary>Gets all statuses asynchronous.</summary>
        /// <returns>
        ///   StatusModels
        /// </returns>
        Task<IEnumerable<StatusModel>> GetAllStatusesAsync();

        /// <summary>Gets the status by identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   Status
        /// </returns>
        Task<StatusModel> GetStatusByIdAsync(int id);

        /// <summary>Adds the status asynchronous.</summary>
        /// <param name="model">The statusModel.</param>
        Task AddStatusAsync(StatusModel model);

        /// <summary>Updates the status asynchronous.</summary>
        /// <param name="model">The statusModel.</param>
        Task UpdateStatusAsync(StatusModel model);

        /// <summary>Deletes the status asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        Task DeleteStatusAsync(int id);
    }
}
