using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.DataBase.Repositories
{
    /// <summary>
    ///   Implements a positionRepository
    /// </summary>
    public class PositionRepository : GenericRepository<Position>, IPositionRepository
    {
        /// <summary>Initializes a new instance of the <see cref="PositionRepository" /> class.</summary>
        /// <param name="dbContext">The database context.</param>
        public PositionRepository(TaskDbContext dbContext) : base(dbContext)
        {

        }
    }
}
