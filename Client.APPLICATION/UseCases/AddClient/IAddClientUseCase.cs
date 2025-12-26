using ClientSi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSi.APPLICATION.UseCases.AddClient
{
    public interface IAddClientUseCase
    {
        void SetOutputPort(IOutputPort outputPort);
        Task Execute(Client client);
    }
}
