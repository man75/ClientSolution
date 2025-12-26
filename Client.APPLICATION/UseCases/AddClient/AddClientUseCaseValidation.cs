using ClientSi.APPLICATION.Services;
using ClientSi.Domain.Entities;

namespace ClientSi.APPLICATION.UseCases.AddClient;

public class AddClientUseCaseValidation : IAddClientUseCase
{
    private readonly Notification _notification;
    private readonly IAddClientUseCase _inner;
    private IOutputPort _outputPort;

    public AddClientUseCaseValidation(Notification notification, IAddClientUseCase inner)
    {
        _notification = notification;
        _inner = inner;
    }

    public void SetOutputPort(IOutputPort outputPort)
    {
        _outputPort = outputPort;
        _inner.SetOutputPort(outputPort);
    }

    public async Task Execute(Client client)
    {
        if (!Validate(client))
        {
            _outputPort.Invalid();
            return;
        }

        await _inner.Execute(client);
    }

    private bool Validate(Client client)
    {
        if (client is null)
        {
            _notification.AddError("Client", "Client obligatoire");
            return false;
        }

        if (string.IsNullOrWhiteSpace(client.Nom))
            _notification.AddError("Nom", "Nom obligatoire");

        if (string.IsNullOrWhiteSpace(client.Prenom))
            _notification.AddError("Prenom", "Prénom obligatoire");

        return !_notification.HasErrors;
    }
}
