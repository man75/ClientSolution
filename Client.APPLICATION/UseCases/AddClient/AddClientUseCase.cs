using ClientSi.APPLICATION.Responses;
using ClientSi.Domain.Abstractions;
using ClientSi.Domain.Entities;


namespace ClientSi.APPLICATION.UseCases.AddClient;

public class AddClientUseCase : IAddClientUseCase
{
    private readonly IClientRepository _repository;
    private IOutputPort _outputPort;

    public AddClientUseCase(IClientRepository repository)
    {
        _repository = repository;
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;

    public async Task Execute(Client client)
    {
        await _repository.AddAsync(client);

        var response = new AddEntityResponse
        {
           IsSuccess = true,
        };

        _outputPort.Ok(response);
    }
}
