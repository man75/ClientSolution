using ClientSi.APPLICATION.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSi.APPLICATION.UseCases.AddClient
{
    public interface IOutputPort
    {
        void Ok(AddEntityResponse response);
        void Invalid();
    }
}
