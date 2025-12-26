using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSi.APPLICATION.Responses
{
    public class AddEntityResponse
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public int Id { get; set; }
    }
}
