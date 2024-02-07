using AutoMapper;
using BuisnessLogicLayer.Requests.Positions;

namespace BuisnessLogicLayer.Commands.Positions.UpdatePosition;

public class UpdatePositionRequestMappingProfile : Profile
{
    public UpdatePositionRequestMappingProfile()
    {
        CreateMap<UpdatePositionRequest, Position>();
    }
}