using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackingTest
{
    internal class RoleEqualityComparer : IEqualityComparer<IdentityRole<int>>
    {
        public bool Equals(IdentityRole<int>? x, IdentityRole<int>? y)
        {
            if (x is null && y is null)
                return true;

            if(x is null || y is null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name
                && x.NormalizedName == y.NormalizedName;
        }

        public int GetHashCode([DisallowNull] IdentityRole<int> obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class UserEqualityComparer : IEqualityComparer<User>
    {
        public bool Equals(User? x, User? y)
        {
            if (x is null && y is null)
                return true;

            if (x is null || y is null)
                return false;

            return x.Id == y.Id
                && x.FirstName == y.FirstName
                && x.LastName == y.LastName
                && x.Email == y.Email
                && x.UserName == y.UserName;
        }

        public int GetHashCode([DisallowNull] User obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class UserRoleEqualityComparer : IEqualityComparer<IdentityUserRole<int>>
    {
        public bool Equals(IdentityUserRole<int>? x, IdentityUserRole<int>? y)
        {
            if (x is null && y is null)
                return true;

            if (x is null || y is null)
                return false;

            return x.UserId == y.UserId
                && x.RoleId == y.RoleId;
        }

        public int GetHashCode([DisallowNull] IdentityUserRole<int> obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class AssignmentStatusEqualityComparer : IEqualityComparer<AssignmentStatus>
    {
        public bool Equals(AssignmentStatus? x, AssignmentStatus? y)
        {
            if (x is null && y is null)
                return true;

            if (x is null || y is null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] AssignmentStatus obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class ProjectStatusEqualityComparer : IEqualityComparer<ProjectStatus>
    {
        public bool Equals(ProjectStatus? x, ProjectStatus? y)
        {
            if (x is null && y is null)
                return true;

            if (x is null || y is null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] ProjectStatus obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class ProjectEqualityComparer : IEqualityComparer<Project>
    {
        public bool Equals(Project? x, Project? y)
        {
            if (x is null && y is null)
                return true;

            if (x is null || y is null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name
                && x.Description == y.Description
                && x.StartDate.Date == y.StartDate.Date
                && x.ExpiryDate.Date == y.ExpiryDate.Date
                && x.StatusId == y.StatusId;
        }

        public int GetHashCode([DisallowNull] Project obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class AssignmentEqualityComparer : IEqualityComparer<Assignment>
    {
        public bool Equals(Assignment? x, Assignment? y)
        {
            if (x is null && y is null)
                return true;

            if (x is null || y is null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name
                && x.Description == y.Description
                && x.StartDate.Date == y.StartDate.Date
                && x.ExpiryDate.Date == y.ExpiryDate.Date
                && x.ManagerId == y.ManagerId
                && x.StatusId == y.StatusId
                && x.ProjectId == y.ProjectId;
        }

        public int GetHashCode([DisallowNull] Assignment obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class PositionEqualityComparer : IEqualityComparer<Position>
    {
        public bool Equals(Position? x, Position? y)
        {
            if (x is null && y is null)
                return true;

            if (x is null || y is null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name
                && x.Description == y.Description;
        }

        public int GetHashCode([DisallowNull] Position obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class UserProjectEqualityComparer : IEqualityComparer<UserProject>
    {
        public bool Equals(UserProject? x, UserProject? y)
        {
            if (x is null && y is null)
                return true;

            if (x is null || y is null)
                return false;

            return x.Id == y.Id
                && x.UserId == y.UserId
                && x.TaskId == y.TaskId
                && x.PositionId == y.PositionId;
        }

        public int GetHashCode([DisallowNull] UserProject obj)
        {
            return obj.GetHashCode();
        }
    }
}
