using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BuisnessLogicLayer.Services
{
    /// <summary>
    ///   Implements a userService
    /// </summary>
    public class UserService : IUserService
    {
        readonly RoleManager<IdentityRole<int>> roleManager;

        readonly UserManager<User> userManager;

        readonly IUnitOfWork unitOfWork;

        readonly IMapper mapper;

        readonly IEmailService emailService;

        /// <summary>Initializes a new instance of the <see cref="UserService" /> class.</summary>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="mapper">The mapper.</param>
        public UserService(RoleManager<IdentityRole<int>> roleManager,
            UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IEmailService emailService)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.emailService = emailService;
        }

        /// <summary>Adds user asynchronous.</summary>
        /// <param name="model">The model.</param>
        public async Task AddAsync(RegisterUserModel model)
        {
            ValidateRegisterUser(model);

            var user = mapper.Map<User>(model);

            await userManager.CreateAsync(user, model.Password);

            var regUser = await userManager.FindByEmailAsync(model.Email);

            if (string.IsNullOrEmpty(model.Role))
                await userManager.AddToRoleAsync(regUser, "User");
            else
                await userManager.AddToRoleAsync(regUser, model.Role);

            var message = new MessageModel(model.Email,
                "Congratulations! You have been successfully registered.",
                $"Hello {model.UserName}! We are excited that you have started using our task tracking system.\n\n" +
                $"Hope, you will enjoy and achieve new goals, expanding your horizons.\n" +
                $"Thanks, The Task tracking system team.");

            await emailService.SendEmailAsync(message);
        }

        /// <summary>Adds the role asynchronous.</summary>
        /// <param name="model">The model.</param>
        public async Task AddRoleAsync(RoleModel model)
        {
            ValidateRole(model);

            var role = mapper.Map<IdentityRole<int>>(model);

            await roleManager.CreateAsync(role);
        }

        /// <summary>Adds user to role asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="role">The role.</param>
        public async Task AddToRoleAsync(int id, RoleModel role)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            await userManager.AddToRoleAsync(user, role.Name);

            var message = new MessageModel(user.Email,
                "Your opportunities and responsibilities have expanded.",
                $"Hello {user.UserName}! You have been added a new role!\n\n" +
                $"Our congratulations. We strongly believe that you will be the best {role.Name}.\n" +
                $"Thanks, The Task tracking system team.");

            await emailService.SendEmailAsync(message);
        }

        /// <summary>Adds user to roles asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="roles">The roles.</param>
        public async Task AddToRolesAsync(int id, IEnumerable<RoleModel> roles)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            await userManager.AddToRolesAsync(user, roles.Select(t => t.Name));

            var roleList = roles.ToList();

            var strRoles = roleList[0].Name;

            for (int i = 1; i < roleList.Count(); i++)
                strRoles += ", " + roleList[i].Name;

            var message = new MessageModel(user.Email,
                "Your opportunities and responsibilities have expanded.",
                $"Hello {user.UserName}! You have been added to new roles: {strRoles}!\n\n" +
                $"Great one! But don't stop! New heights are waiting!\n" +
                $"Thanks, The Task tracking system team.");

            await emailService.SendEmailAsync(message);
        }

        /// <summary>Deletes the user asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteAsync(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            await userManager.DeleteAsync(user);

            var message = new MessageModel(user.Email,
                "Your account has been deleted.",
                $"Hello {user.UserName}! We have found out that your account has been deleted!\n\n" +
                $"That's so sad. Share your opinion with us and we will improve our service!\n" +
                $"Thanks, The Task tracking system team.");

            await emailService.SendEmailAsync(message);
        }

        /// <summary>Deletes the role asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRoleAsync(int id)
        {
            await roleManager.DeleteAsync(await roleManager.FindByIdAsync(id.ToString()));
        }

        /// <summary>Deletes user to role asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="roleName">Name of the role.</param>
        public async Task DeleteToRoleAsync(int id, string roleName)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            await userManager.RemoveFromRoleAsync(user, roleName);

            var message = new MessageModel(user.Email,
                "You have been excluded from the role.",
                $"Hello {user.UserName}! You have been excluded from the role {roleName}!\n\n" +
                $"We hope this will better for you! Keep going to achieve new goals.\n" +
                $"Thanks, The Task tracking system team.");

            await emailService.SendEmailAsync(message);
        }

        /// <summary>Deletes user to roles asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="roleNames">The role names.</param>
        public async Task DeleteToRolesAsync(int id, IEnumerable<string> roleNames)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            await userManager.RemoveFromRolesAsync(user, roleNames);

            var roleList = roleNames.ToList();

            var strRoles = roleList[0];

            for (int i = 1; i < roleList.Count(); i++)
                strRoles += ", " + roleList[i];

            var message = new MessageModel(user.Email,
                "You have been excluded from the roles.",
                $"Hello {user.UserName}! You have been excluded from the roles: {strRoles}!\n\n" +
                $"We hope this will better for you! Keep going to achieve new goals.\n" +
                $"Thanks, The Task tracking system team.");

            await emailService.SendEmailAsync(message);
        }

        /// <summary>Gets all users asynchronous.</summary>
        /// <returns>UserModels</returns>
        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            var list = await userManager.Users
                .Include(t => t.Tasks)
                    .ThenInclude(t => t.Project)
                .Include(t => t.UserProjects)
                    .ThenInclude(t => t.Task)
                .Include(t => t.UserProjects)
                    .ThenInclude(t => t.Position)
                .ToListAsync();

            return mapper.Map<IEnumerable<UserModel>>(list);
        }

        /// <summary>Gets all positions by user identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>PositionModels</returns>
        public async Task<IEnumerable<PositionModel>> GetAllPositionsByUserIdAsync(int id)
        {
            var list = (await unitOfWork.UserProjectRepository
                .GetAllWithDetailsAsync())
                .Where(t => t.UserId == id)
                .Select(t => t.Position)
                .GroupBy(t => t.Id)
                .Select(t => t.First());

            return mapper.Map<IEnumerable<PositionModel>>(list);
        }

        /// <summary>Gets all projects by user identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ProjectModels</returns>
        public async Task<IEnumerable<ProjectModel>> GetAllProjectsByUserIdAsync(int id)
        {
            var list = (await unitOfWork.UserProjectRepository
                .GetAllWithDetailsAsync())
                .Where(t => t.UserId == id)
                .Select(t => t.Task.Project)
                .GroupBy(t => t.Id)
                .Select(t => t.First());

            return mapper.Map<IEnumerable<ProjectModel>>(list);
        }

        /// <summary>Gets all roles asynchronous.</summary>
        /// <returns>RoleModels</returns>
        public async Task<IEnumerable<RoleModel>> GetAllRolesAsync()
        {
            var list = await roleManager.Roles.ToListAsync();

            return mapper.Map<IEnumerable<RoleModel>>(list);
        }

        /// <summary>Gets all roles by user identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>RoleModels</returns>
        public async Task<IEnumerable<RoleModel>> GetAllRolesByUserIdAsync(int id)
        {
            var list = (await userManager.GetRolesAsync(await userManager.FindByIdAsync(id.ToString())))
                .Select(t => roleManager.FindByNameAsync(t).Result);

            return mapper.Map<IEnumerable<RoleModel>>(list);
        }

        /// <summary>Gets all tasks by user identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>TaskModels</returns>
        public async Task<IEnumerable<TaskModel>> GetAllTasksByUserIdAsync(int id)
        {
            var list = (await unitOfWork.AssignmentRepository
                .GetAllWithDetailsAsync())
                .Where(t => t.UserProjects.Any(m => m.UserId == id));

            return mapper.Map<IEnumerable<TaskModel>>(list);
        }

        /// <summary>Gets all tasks by manager identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>TaskModels</returns>
        public async Task<IEnumerable<TaskModel>> GetAllTasksByManagerIdAsync(int id)
        {
            var list = (await unitOfWork.AssignmentRepository
                .GetAllWithDetailsAsync())
                .Where(t => t.ManagerId == id);

            return mapper.Map<IEnumerable<TaskModel>>(list);
        }

        /// <summary>Gets the user by identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>UserModel</returns>
        public async Task<UserModel> GetByIdAsync(int id)
        {
            var model = await userManager.FindByIdAsync(id.ToString());

            return mapper.Map<UserModel>(model);
        }

        /// <summary>Gets the user by email asynchronous.</summary>
        /// <param name="email">The email.</param>
        /// <returns>UserModel</returns>
        public async Task<UserModel> GetByEmailAsync(string email)
        {
            var model = await userManager.FindByEmailAsync(email);

            return mapper.Map<UserModel>(model);
        }

        /// <summary>Gets the role by identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>RoleModel</returns>
        public async Task<RoleModel> GetRoleByIdAsync(int id)
        {
            var model = await roleManager.FindByIdAsync(id.ToString());

            return mapper.Map<RoleModel>(model);
        }

        /// <summary>Updates the user asynchronous.</summary>
        /// <param name="model">The model.</param>
        public async Task UpdateAsync(UpdateUserModel model)
        {
            ValidateUpdateUser(model);

            var user = await userManager.FindByIdAsync(model.Id.ToString());

            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.UserName = model.UserName;
            user.LastName = model.LastName;

            await userManager.UpdateAsync(user);

            var message = new MessageModel(user.Email,
                "You have changed your personal information.",
                $"Hello {user.UserName}! We have noticed that you changed personal info!\n\n" +
                $"This is great! Hope it will be easier next time.\n" +
                $"Thanks, The Task tracking system team.");

            await emailService.SendEmailAsync(message);
        }

        /// <summary>Updates the role asynchronous.</summary>
        /// <param name="model">The model.</param>
        public async Task UpdateRoleAsync(RoleModel model)
        {
            ValidateRole(model);

            var role = await roleManager.FindByIdAsync(model.Id.ToString());

            role.Name = model.Name;

            await roleManager.UpdateAsync(role);
        }

        static void ValidateRegisterUser(RegisterUserModel model)
        {
            if (model is null)
                throw new TaskTrackingException("Model is null");

            if (string.IsNullOrEmpty(model.FirstName)
                || string.IsNullOrEmpty(model.LastName)
                || string.IsNullOrEmpty(model.UserName)
                || string.IsNullOrEmpty(model.Email)
                || string.IsNullOrEmpty(model.Password))
                throw new TaskTrackingException("Person information is null or empty");

            if (model.Password != model.ConfirmPassword)
                throw new TaskTrackingException("Password is incorrect");
        }

        static void ValidateUpdateUser(UpdateUserModel model)
        {
            if (model is null)
                throw new TaskTrackingException("Model is null");

            if (string.IsNullOrEmpty(model.FirstName)
                || string.IsNullOrEmpty(model.LastName)
                || string.IsNullOrEmpty(model.UserName)
                || string.IsNullOrEmpty(model.Email))
                throw new TaskTrackingException("Person information is null or empty");
        }

        static void ValidateRole(RoleModel model)
        {
            if (model is null)
                throw new TaskTrackingException("Model is null");

            if (string.IsNullOrEmpty(model.Name))
                throw new TaskTrackingException("Name or description is null or empty");
        }
    }
}
