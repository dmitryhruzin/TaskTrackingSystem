namespace BuisnessLogicLayer.Responses.UserProjects;

public record GetUserProjectResponse(
    int Id,
    string UserName,
    string UserEmail,
    int UserId,
    string TaskName,
    string TaskId,
    string PositionName,
    string PositionId);