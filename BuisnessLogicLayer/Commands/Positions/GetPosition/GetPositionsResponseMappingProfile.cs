using AutoMapper;
using BuisnessLogicLayer.Requests.Positions;
using BuisnessLogicLayer.Responses.Positions;

namespace BuisnessLogicLayer.Commands.Positions.GetPosition;

public class GetPositionsResponseMappingProfile : Profile
{
    public GetPositionsResponseMappingProfile()
    {
        CreateMap<Position, GetPositionResponse>();
    }
}