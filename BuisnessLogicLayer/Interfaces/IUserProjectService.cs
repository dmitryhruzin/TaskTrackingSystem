using BuisnessLogicLayer.Models;

namespace BuisnessLogicLayer.Interfaces
{
    /// <summary>
    ///   Describes a userProject service
    /// </summary>
    public interface IUserProjectService : ICrud<UserProjectModel>
    {
        /// <summary>Adds the user projects asynchronous.</summary>
        /// <param name="models">The models.</param>
        Task AddUserProjectsAsync(IEnumerable<UserProjectModel> models);

        /// <summary>Deletes the user projects asynchronous.</summary>
        /// <param name="ids">The ids.</param>
        Task DeleteUserProjectsAsync(IEnumerable<int> ids);

        /// <summary>Gets all positions asynchronous.</summary>
        /// <returns>
        ///   PositionModels
        /// </returns>
        Task<IEnumerable<PositionModel>> GetAllPositionsAsync();

        /// <summary>Gets the position by identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   PositionModel
        /// </returns>
        Task<PositionModel> GetPositionByIdAsync(int id);

        /// <summary>Adds the position asynchronous.</summary>
        /// <param name="model">The model.</param>
        Task AddPositionAsync(PositionModel model);

        /// <summary>Updates the position asynchronous.</summary>
        /// <param name="model">The model.</param>
        Task UpdatePositionAsync(PositionModel model);

        /// <summary>Deletes the position asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        Task DeletePositionAsync(int id);
    }
}
