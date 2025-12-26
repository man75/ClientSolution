using AutoMapper;
using ClientSi.API.Dtos;
using ClientSi.Domain.Entities;


namespace ClientSi.API.Mapping;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<AddClientDto, Client > ();
    }
}
