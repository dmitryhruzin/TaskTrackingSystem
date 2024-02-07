using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.DataBase.Repositories;

public class AssignmentStatusRepository : GenericRepository<AssignmentStatus>, IAssignmentStatusRepository
{
    public AssignmentStatusRepository(TaskDbContext dbContext) : base(dbContext)
    {
    }
}