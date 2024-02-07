using BuisnessLogicLayer.Models;

namespace BuisnessLogicLayer.Interfaces
{
    /// <summary>
    ///   Describes a userService
    /// </summary>
    public interface IUserService
    {
        /// <summary>Gets all users asynchronous.</summary>
        /// <returns>
        ///   UserModels
        /// </returns>
        Task<IEnumerable<UserModel>> GetAllAsync();

        /// <summary>Gets the user by identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   UserModel
        /// </returns>
        Task<UserModel> GetByIdAsync(int id);

        /// <summary>Gets the user by email asynchronous.</summary>
        /// <param name="email">The email.</param>
        /// <returns>
        ///   UserModel
        /// </returns>
        Task<UserModel> GetByEmailAsync(string email);

        /// <summary>Adds user the asynchronous.</summary>
        /// <param name="model">The model.</param>
        Task AddAsync(RegisterUserModel model);

        /// <summary>Updates the user asynchronous.</summary>
        /// <param name="model">The model.</param>
        Task UpdateAsync(UpdateUserModel model);

        /// <summary>Deletes the user asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        Task DeleteAsync(int id);

        /// <summary>Adds user to role asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="role">The role.</param>
        Task AddToRoleAsync(int id, RoleModel role);

        /// <summary>Adds user to roles asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="roles">The roles.</param>
        Task AddToRolesAsync(int id, IEnumerable<RoleModel> roles);

        /// <summary>Deletes user to role asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="roleName">Name of the role.</param>
        Task DeleteToRoleAsync(int id, string roleName);

        /// <summary>Deletes user to roles asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="roleNames">The role names.</param>
        Task DeleteToRolesAsync(int id, IEnumerable<string> roleNames);

        /// <summary>Gets all projects by user identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   ProjectModels
        /// </returns>
        Task<IEnumerable<ProjectModel>> GetAllProjectsByUserIdAsync(int id);

        /// <summary>Gets all tasks by user identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   TaskModels
        /// </returns>
        Task<IEnumerable<TaskModel>> GetAllTasksByUserIdAsync(int id);

        /// <summary>Gets all tasks by manager identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   TaskModels
        /// </returns>
        Task<IEnumerable<TaskModel>> GetAllTasksByManagerIdAsync(int id);

        /// <summary>Gets all positions by user identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   PositionModels
        /// </returns>
        Task<IEnumerable<PositionModel>> GetAllPositionsByUserIdAsync(int id);

        /// <summary>Gets all roles by user identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   RoleModels
        /// </returns>
        Task<IEnumerable<RoleModel>> GetAllRolesByUserIdAsync(int id);

        /// <summary>Gets all roles asynchronous.</summary>
        /// <returns>
        ///   RoleModels
        /// </returns>
        Task<IEnumerable<RoleModel>> GetAllRolesAsync();

        /// <summary>Gets the role by identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   RoleModel
        /// </returns>
        Task<RoleModel> GetRoleByIdAsync(int id);

        /// <summary>Adds the role asynchronous.</summary>
        /// <param name="model">The model.</param>
        Task AddRoleAsync(RoleModel model);

        /// <summary>Updates the role asynchronous.</summary>
        /// <param name="model">The model.</param>
        Task UpdateRoleAsync(RoleModel model);

        /// <summary>Deletes the role asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        Task DeleteRoleAsync(int id);
    }
}
