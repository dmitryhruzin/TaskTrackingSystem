namespace BuisnessLogicLayer.Responses.Projects;

public record GetProjectsResponse(IReadOnlyCollection<GetProjectResponse> Projects);