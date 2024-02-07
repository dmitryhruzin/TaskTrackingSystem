using AutoMapper;
using BuisnessLogicLayer.Requests.Positions;

namespace BuisnessLogicLayer.Commands.Positions.AddPosition;

public class AddPositionRequestMappingProfile : Profile
{
    public AddPositionRequestMappingProfile()
    {
        CreateMap<AddPositionRequest, Position>();
    }
}