using AutoMapper;
using ClientSi.API.Dtos;
using ClientSi.APPLICATION.Responses;
using ClientSi.APPLICATION.Services;
using ClientSi.APPLICATION.UseCases.AddClient;
using ClientSi.Domain.Entities;

using Microsoft.AspNetCore.Mvc;

namespace ClientSi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddClientController : ControllerBase, IOutputPort
{
    private readonly IAddClientUseCase _useCase;
    private readonly IMapper _mapper;
    private readonly Notification _notification;
    private IActionResult _viewModel;

    public AddClientController(IAddClientUseCase useCase,
                               Notification notification,
                               IMapper mapper)
    {
        _useCase = useCase;
        _notification = notification;
        _mapper = mapper;
    }

    [HttpPost("ajouter")]
    public async Task<IActionResult> Ajouter(AddClientDto clientDto)
    {
        var client = _mapper.Map<Client>(clientDto);
    
       

        _useCase.SetOutputPort(this);
        await _useCase.Execute(client);

        return _viewModel;
    }

    void IOutputPort.Ok(AddEntityResponse response)
    {
        _viewModel = Ok(response);
    }

    void IOutputPort.Invalid()
    {
        var problemDetails = new ValidationProblemDetails(_notification.Errors);
        _viewModel = BadRequest(problemDetails);
    }
}
