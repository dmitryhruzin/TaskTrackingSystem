using BuisnessLogicLayer.Models;

namespace BuisnessLogicLayer.Interfaces
{
    /// <summary>
    ///   Describes a taskService
    /// </summary>
    public interface ITaskService : ICrud<TaskModel>
    {
        /// <summary>Gets all users by task identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   UserModels
        /// </returns>
        Task<IEnumerable<UserModel>> GetAllUsersByTaskIdAsync(int id);

        /// <summary>Gets all statuses asynchronous.</summary>
        /// <returns>
        ///   StatusModels
        /// </returns>
        Task<IEnumerable<StatusModel>> GetAllStatusesAsync();

        /// <summary>Updates the task status asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="status">The status.</param>
        Task UpdateTaskStatusAsync(int id, StatusModel status);

        /// <summary>Gets the status by identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   StatusModel
        /// </returns>
        Task<StatusModel> GetStatusByIdAsync(int id);

        /// <summary>Adds the status asynchronous.</summary>
        /// <param name="model">The status model.</param>
        Task AddStatusAsync(StatusModel model);

        /// <summary>Updates the status asynchronous.</summary>
        /// <param name="model">The status model.</param>
        Task UpdateStatusAsync(StatusModel model);

        /// <summary>Deletes the status asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        Task DeleteStatusAsync(int id);
    }
}
