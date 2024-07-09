using AutoMapper;
using EventPlanningBackend.Models;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterDto,Account>().ReverseMap();
        CreateMap<Account, AccountDto>().ReverseMap();
        CreateMap<Event, EventDto>().ReverseMap();
        CreateMap<EventAdditionalField, EventAdditionalFieldDto>().ReverseMap();
        CreateMap<AccountAdditionalField, AccountAdditionalFieldDto>().ReverseMap();
        CreateMap<Registration, RegistrationDto>().ReverseMap();
    }
}