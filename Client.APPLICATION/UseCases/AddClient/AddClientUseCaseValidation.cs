using ClientSi.APPLICATION.Services;
using ClientSi.Domain.Abstractions;
using ClientSi.Domain.Entities;

namespace ClientSi.APPLICATION.UseCases.AddClient
{
    public class AddClientUseCaseValidation : IAddClientUseCase
    {
        private readonly Notification _notification;
        private readonly IAddClientUseCase _inner;
        private readonly IClientRepository _clientRepository;
        private IOutputPort _outputPort;

        public AddClientUseCaseValidation(
            Notification notification,
            IAddClientUseCase inner,
            IClientRepository clientRepository)
        {
            _notification = notification;
            _inner = inner;
            _clientRepository = clientRepository;
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
            _inner.SetOutputPort(outputPort); // on propage au use case réel
        }

        public async Task Execute(Client client)
        {
            if (!await Validate(client))
            {
                _outputPort.Invalid();
                return;
            }

            await _inner.Execute(client);
        }

        private async Task<bool> Validate(Client client)
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

            if (string.IsNullOrWhiteSpace(client.Email))
                _notification.AddError("Email", "Email obligatoire");
            else if (await _clientRepository.ExistsByEmailAsync(client.Email))
                _notification.AddError("Email", "Client existant");

            return !_notification.HasErrors;
        }
    }
}
